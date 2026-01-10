using RetroEmu.Devices.DMG.CPU.Interrupts;

namespace RetroEmu.Devices.DMG.CPU.Link;

/// <summary>
/// - Master emulator shifts out bit on internal clock tick
/// - Master sends bit (or byte + bit position) to mailbox over TCP/UDP
/// - Mailbox forwards the bit and clock “tick event” to the slave emulator
/// - Slave emulator reacts to tick:
/// - Shifts master bit into SB
///     - Shifts out its own bit
///     - Slave sends its bit back through mailbox to master
/// - Master receives the slave bit → completes full-duplex shift
///     - Repeat for 8 ticks per byte
/// </summary>
public class Serial(IWire wire, IInterruptState interruptState) : ISerial
{
    // SerialByte (SB) = 0xFF01
    public byte SerialByte { get; set; } = 0xFF;    // No data sent == 0xFF
    
    // SerialControl (SC) = 0xFF02
    public byte SerialControl { get; set; } = 0x7E; // 0x7E is the expected startup value

    private struct TransferState()
    {
        public bool IsActive { get; private set; }
        public bool WaitingForSerialByte { get; set; }
        private int CyclesSinceLastTick { get; set; }
        private int CyclesPerTick { get; init; }
        
        public byte Ticks { get; private set; } = 0;

        public static TransferState StartNewTransfer(int processorClockSpeedHz, int transferClockSpeedHz) => new()
        {
            IsActive = true,
            WaitingForSerialByte = false,
            CyclesSinceLastTick = 0,
            CyclesPerTick = processorClockSpeedHz / transferClockSpeedHz
        };

        public void EndTransfer()
        {
            IsActive = false;
            WaitingForSerialByte = false;
        }
        
        public void Tick(int cycles)
        {
            CyclesSinceLastTick += cycles;

            if (CyclesSinceLastTick < CyclesPerTick)
                return;
            
            CyclesSinceLastTick =- CyclesPerTick;
            Ticks++;
        }
    }

    private TransferState _transferState = new();

    public void Reset()
    {
        wire.Flush();
    }

    public void Update(int cycles)
    {
        if (!IsSerialTransferEnabled())
        {
            // If serial was disabled during transfer, clean up
            return;
        }
        
        // transfer is active and the programmer for some odd reason has changed from master to slave
        // clean up ..
        
        if (TransferOnInternalClock())
        {
            ShiftSerialByteOnInternalClock();
        }
        else
        {
            ShiftSerialByteOnExternalClock();
        }
        
        if (_transferState.IsActive)
        {
            _transferState.Tick(cycles);
        }

        if (_transferState.Ticks == 8)
        {
            // Here we must take into account that when on internal clock,
            // a byte may not yet have been received due to latency on the wire
            SerialControl &= 0x7F;
            _transferState.EndTransfer();

            if (interruptState.IsInterruptMasterEnabled())
            {
                interruptState.GenerateInterrupt(InterruptType.Serial);
            }
        }

        // If 8 clock cycles have been created or received, do:
        // 1: SC &= 0x7F;
        // 2: If interrupt is enabled, generate serial interrupt
        // 3: Reset transfer state to
        //      - Bit Count = 0;
    }

    // If Slave mode has been entered
    // ------------------------------
    // - Wait for external clock tick
    // - Shift SB out/in 8 ticks (transfer state)
    // - On finish, generate serial interrupt if enabled
    private void ShiftSerialByteOnExternalClock()
    {
        if (_transferState.IsActive || !wire.HasData())
            return;
        
        var (incomingSerialByte, transferClockSpeedHz) = wire.Read();
        const int processorClockSpeedHz = 4194304; // Get this from another place ..
        
        _transferState = TransferState.StartNewTransfer(processorClockSpeedHz, transferClockSpeedHz);
        wire.Write(new Data(SerialByte, transferClockSpeedHz));
        
        SerialByte = incomingSerialByte;
    }

    /// <summary>
    /// If Master mode has been entered
    /// -------------------------------
    /// - Start internal clock
    /// - Shift SB out/in 8 ticks (transfer state)
    ///      - Shifted SB + bit pos out  (transfer.Write(SBOut, BitPos))
    ///      - Shifted SB + bit pos in   (transfer.Read(SBIn, BitPos))
    /// - On finish, generate serial interrupt if enabled
    /// </summary>
    /// <param name="cycles"></param>
    private void ShiftSerialByteOnInternalClock()
    {
        if (!_transferState.IsActive)
        {
            const int processorClockSpeedHz = 4194304; // Get this from another place ..
            var transferClockSpeedHz = GetTransferClockSpeedHz();
            _transferState = TransferState.StartNewTransfer(processorClockSpeedHz, transferClockSpeedHz);
            
            wire.Write(new Data(SerialByte, transferClockSpeedHz));
            _transferState.WaitingForSerialByte = true;
        }

        if (!_transferState.WaitingForSerialByte || !wire.HasData())
            return;
        
        var data = wire.Read();
        
        SerialByte = data.SerialByte;
        _transferState.WaitingForSerialByte = false;
    }

    private bool TransferOnInternalClock() => (SerialControl & 0x01) == 0x01;

    private bool IsSerialTransferEnabled() => (SerialControl & 0x80) == 0x80;
    
    // TODO: implement other transfer speeds
    private static int GetTransferClockSpeedHz() => 8192;

    //// Master tick (internal clock)
    //void serial_master_tick(Serial *master, Serial *slave) {
    //    uint8_t out_bit = (master->SB & 0x80) >> 7;
    //    master->SB = (master->SB << 1) | (slave->SB & 1); // receive slave bit
    //    slave->SB = (slave->SB << 1) | out_bit;           // send to slave
    //    master->bit_pos++;
    //    slave->bit_pos++;
    //    if(master->bit_pos == 8) {
    //        master->SC |= 0x80; // transfer complete
    //        slave->SC |= 0x80;
    //    }
    //}

    //  // Slave tick (external clock)
    //void serial_slave_clock_tick(Serial *slave, uint8_t master_bit) {
    //    uint8_t out_bit = (slave->SB & 0x80) >> 7;
    //    slave->SB = (slave->SB << 1) | (master_bit & 1);
    //    slave->bit_pos++;
    //    if(slave->bit_pos == 8) {
    //        slave->SC |= 0x80; // transfer complete
    //    }
    //    return out_bit;
    //}

    //void serial_slave_clock_tick(Serial *s, uint8_t master_bit) {

    //    // This IS how you detect the first tick:
    //    if (!s->transfer_active && (s->SC & 0x80) && !(s->SC & 0x01)) {
    //        // Slave has been armed, first clock tick just arrived
    //        s->transfer_active = true;
    //        s->bit_pos = 0;
    //        // Do NOT return — process this first bit!
    //    }

    //    if (!s->transfer_active)
    //        return;

    //    // === Shift logic follows ===
    //    uint8_t out_bit = (s->SB & 0x80) >> 7;
    //    s->SB = (s->SB << 1) | (master_bit & 1);
    //    s->bit_pos++;

    //    if (s->bit_pos == 8) {
    //        s->transfer_active = false;
    //        s->SC |= 0x80; // transfer complete
    //    }
    //}
}