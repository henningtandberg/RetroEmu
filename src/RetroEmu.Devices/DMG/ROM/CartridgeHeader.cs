namespace RetroEmu.Devices.DMG.ROM;

public record CartridgeHeader(
	string GameTitle,
	bool HasColor,
	bool HasGameBoySuperFunctions,
	CartridgeType CartridgeType,
	RomSizeInfo RomSizeInfo,
	RamSizeInfo RamSizeInfo,
	DestinationCode DestinationCode,
	LicenseCode LicenseCode);