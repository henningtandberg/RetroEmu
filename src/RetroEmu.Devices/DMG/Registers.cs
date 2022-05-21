using System.Reflection.Metadata.Ecma335;

namespace RetroEmu.Devices.DMG
{
	public unsafe class Registers
	{
		private Register16Bit _af;
		private Register16Bit _bc;
		private Register16Bit _de;
		private Register16Bit _hl;
		private Register16Bit _sp;
		private Register16Bit _pc;

		public byte* A { get; }
		public byte* F { get; }
		public byte* B { get; }
		public byte* C { get; }
		public byte* D { get; }
		public byte* E { get; }
		public byte* H { get; }
		public byte* L { get; }

		public byte* AF { get; set; }
		public byte* BC { get; set; }
		public byte* DE { get; set; }
		public byte* HL { get; set; }
		public byte* SP { get; set; }
		public byte* PC { get; set; }

		public Registers()
		{
			_af = new Register16Bit();
			fixed (void* p = &_af.BH)
			{
				A = (byte*)p;
			}

			fixed (void* p = &_af.BL)
			{
				F = (byte*)p;
			}

			fixed (void* p = &_af.W)
			{
				AF = (byte*)p;
			}

			_bc = new Register16Bit();
			fixed (void* p = &_bc.BH)
			{
				B = (byte*)p;
			}

			fixed (void* p = &_bc.BL)
			{
				C = (byte*)p;
			}

			fixed (void* p = &_bc.W)
			{
				BC = (byte*)p;
			}

			_de = new Register16Bit();
			fixed (void* p = &_de.BH)
			{
				D = (byte*)p;
			}

			fixed (void* p = &_de.BL)
			{
				E = (byte*)p;
			}

			fixed (void* p = &_de.W)
			{
				DE = (byte*)p;
			}

			_hl = new Register16Bit();
			fixed (void* p = &_hl.BH)
			{
				H = (byte*)p;
			}

			fixed (void* p = &_hl.BL)
			{
				L = (byte*)p;
			}

			fixed (void* p = &_hl.W)
			{
				HL = (byte*)p;
			}

			_sp = new Register16Bit();
			fixed (void* p = &_sp.W)
			{
				SP = (byte*)p;
			}

			_pc = new Register16Bit();
			fixed (void* p = &_pc.W)
			{
				PC = (byte*)p;
			}
		}
	}
}

    