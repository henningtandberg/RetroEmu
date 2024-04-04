using System;
using System.Text;

namespace RetroEmu.Devices.Tests.Setup
{
    public class TestRomBuilder
    {
        public const int MinRomSize = 355;
        
        private readonly byte[] _rom;
        private int _currentInstructionPointer;

        public TestRomBuilder(int size)
        {
            if (size < MinRomSize)
            {
                size = MinRomSize;
            }
            
            _rom = new byte[size];
        }


        public TestRomBuilder SetScrollingNintendoGraphic(byte[] scrollingNintendoGraphic)
        {
            var startAddress = 0x0104;
            var endAddress = 0x0133;
            var maxScrollingNintendoGraphicLength = endAddress - startAddress + 1;
            
            if (scrollingNintendoGraphic.Length > maxScrollingNintendoGraphicLength)
            {
                throw new ArgumentException("The scrolling Nintendo graphic is too large for the allocated memory!");
            }

            int nextAddress = startAddress;
            
            foreach (byte value in scrollingNintendoGraphic)
            {
                _rom[nextAddress++] = value;
            }

            return this;
        }
        public TestRomBuilder SetGameTitle(string title)
        {
            int endIndexOfTitle = title.Length > 16 ? 16 : title.Length;
            int nextAddress = 0x0134;
            string croppedTitle = title[..endIndexOfTitle].ToUpper();

            foreach (byte character in Encoding.ASCII.GetBytes(croppedTitle))
            {
                _rom[nextAddress++] = character;
            }

            return this;
        }

        public TestRomBuilder SetGameBoyColorFlag(byte flag)
        {
            _rom[0x0143] = flag;
            return this;
        }

        public TestRomBuilder SetGameBoySuperFlag(byte flag)
        {
            _rom[0x0146] = flag;
            return this;
        }

        public TestRomBuilder SetGameBoyCartridgeType(byte cartridgeType)
        {
            _rom[0x0147] = cartridgeType;
            return this;
        }

        public TestRomBuilder SetRomSize(byte romSize)
        {
            _rom[0x0148] = romSize;
            return this;
        }

        public TestRomBuilder SetRamSize(byte ramSize)
        {
            _rom[0x0149] = ramSize;
            return this;
        }

        public TestRomBuilder SetDestinationCode(byte destinationCode)
        {
            _rom[0x014A] = destinationCode;
            return this;
        }

        public TestRomBuilder SetLicenseCodeOld(byte licenseCode)
        {
            _rom[0x014B] = licenseCode;
            return this;
        }

        public byte[] Build()
        {
            /* Todo:
             * - Set Two's Complement 0x014D
             * - Set Checksum 0x014E and 0x014F
             */
            return _rom;
        }

        public TestRomBuilder SetInstruction(byte opcode, byte[] values)
        {
            _rom[_currentInstructionPointer++] = opcode;
            return this;
        }
    }
}