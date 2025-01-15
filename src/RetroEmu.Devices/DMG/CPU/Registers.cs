namespace RetroEmu.Devices.DMG.CPU
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
		public ref byte B 
		{
			get { return ref _bc.BH; }
		}
		public ref byte C
        {
            get { return ref _bc.BL; }
        }
        public byte* D { get; }
		public byte* E { get; }
		public byte* H { get; }
		public byte* L { get; }

		public ushort* AF { get; set; }
		public ref ushort BC
        {
            get { return ref _bc.W; }
        }
        public ushort* DE { get; set; }
		public ushort* HL { get; set; }
		public ushort* SP { get; set; }
		public ushort* PC { get; set; }

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
				AF = (ushort*)p;
			}

			_bc = new Register16Bit();

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
				DE = (ushort*)p;
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
				HL = (ushort*)p;
			}

			_sp = new Register16Bit();
			fixed (void* p = &_sp.W)
			{
				SP = (ushort*)p;
			}

			_pc = new Register16Bit();
			fixed (void* p = &_pc.W)
			{
				PC = (ushort*)p;
			}
		}
	}
}

    