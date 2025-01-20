using RetroEmu.Devices.DMG.CPU.Instructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroEmu.Devices.DMG.CPU
{
    // TODO: Move to its own file?
    public enum InterruptType
    {
        VBlank = 0x01,
        LCDC = 0x02,
        Timer = 0x04,
        Serial = 0x08,
        Button = 0x10,
    }

    public class InterruptState : IInterruptState
    {
        // IME
        public bool InterruptMasterEnable { get; set; }

        // For all interrupt registers:
        // Bit 0 -> V-Blank
        // Bit 1 -> LCDC
        // Bit 2 -> Timer overflow
        // Bit 3 -> Serial I/O transfer complete
        // Bit 4 -> Buttons

        // IE Register 0xFFFF
        public byte InterruptEnable { get; set; }
        // IF Register 0xFF0F
        public byte InterruptFlag { get; set; }
        public byte DisableInterruptCounter { get; set; }
        public byte EnableInterruptCounter { get; set; }

        public ushort GetInterruptStartingAddress(InterruptType type) =>
            type switch
            {
                InterruptType.VBlank => 0x40,
                InterruptType.LCDC => 0x48,
                InterruptType.Timer => 0x50,
                InterruptType.Serial => 0x58,
                InterruptType.Button => 0x60,
                _ => throw new NotImplementedException()
            };
    }

    public interface IInterruptState
    {
        public bool InterruptMasterEnable { get; set; }
        public byte InterruptEnable { get; set; }
        public byte InterruptFlag { get; set; }
        public byte DisableInterruptCounter { get; set; }
        public byte EnableInterruptCounter { get; set; }
        
        public ushort GetInterruptStartingAddress(InterruptType type);
    }
}
