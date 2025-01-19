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

    public class InterruptState
    {
        // IME
        public bool InterruptMasterEnable = false;

        // For all interrupt registers:
        // Bit 0 -> V-Blank
        // Bit 1 -> LCDC
        // Bit 2 -> Timer overflow
        // Bit 3 -> Serial I/O transfer complete
        // Bit 4 -> Buttons

        // IE Register 0xFFFF
        public byte InterruptEnable = 0;

        // IF Register 0xFF0F
        public byte InterruptFlag = 0;

        public byte DisableInterruptCounter = 0;
        public byte EnableInterruptCounter = 0;

        public static ushort GetInterruptStartingAddress(InterruptType type) =>
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
}
