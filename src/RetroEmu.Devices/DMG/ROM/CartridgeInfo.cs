namespace RetroEmu.Devices.DMG.ROM
{
    public class CartridgeInfo
    {
		public string GameTitle { get; private init; }
		public bool HasColor { get; private init; }
		public bool HasGameBoySuperFunctions { get; private init; }
		public CartridgeType CartridgeType { get; private init; }
		public RomSizeInfo RomSizeInfo { get; private init; }
		public RamSizeInfo RamSizeInfo { get; private init; }
		public DestinationCode DestinationCode { get; private init; }
		public LicenseCode LicenseCode { get; private init; }

		public static CartridgeInfo Create(byte[] rom)
		{
			return new CartridgeInfo
			{
				GameTitle = rom.GetTitle(),
				HasColor = rom.HasColor(),
				HasGameBoySuperFunctions = rom.HasSuperGameBoyFunctions(),
				CartridgeType = rom.GetCartridgeType(),
				RomSizeInfo = rom.GetRomSizeInfo(),
				RamSizeInfo = rom.GetRamSizeInfo(),
				DestinationCode = rom.GetDestinationCode(),
				LicenseCode = rom.GetLicenseCode()
			};
		}
    }
}