using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nitro.IO
{
	public class Rom
	{
		private Stream _baseStream;
		private UInt32 _iconTitleOffset;

		private string _shortTitle;

		private RomChipInfo _arm7;
		private RomChipInfo _arm9;

		public RomChipInfo Arm7 { get { return _arm7; }	}
		public RomChipInfo Arm9 { get { return _arm9; }	}

		public bool HasIcon { get { return _iconTitleOffset != 0; } }

		/// <summary>
		/// Gets or sets the short title of the ROM limited to 12 characters.
		/// </summary>
		public string ShortTitle
		{
			get { return _shortTitle; }
			set 
			{
				if (value.Length > 12)
					throw new ArgumentOutOfRangeException("ShortTitle limited to 12 ASCII characters");
				_shortTitle = value;
			}
		}

		/// <summary>
		/// Gets or sets the game code (usually printed on the package and sticker).
		/// </summary>
		public string GameCode { get; set; }


		/// <summary>
		/// Gets or sets the maker code of the ROM, eg. Homebrew, Nintendo, Unknown.
		/// </summary>
		public MakerCode MakerCode { get; set; }

		/// <summary>
		/// Gets or set the supported device type of the ROM, eg. DS, DSi, or Both.
		/// </summary>
		public DeviceType SupportedDevices { get; set; }

		/// <summary>
		/// Gets or sets the Encryption Seed Select (00..07h, usually 00h).
		/// </summary>
		public byte EncryptionSeed { get; set; }

		public byte DeviceCapacity { get; set; }

		/// <summary>
		/// Gets or sets the ROM version (usually 00h)
		/// </summary>
		public byte Version { get; set; }

		/// <summary>
		/// Skips the "Press Button" after the Health and Safety notice.
		/// Also skips the bootmenu, even in Manual mode & even Start pressed.
		/// </summary>
		public bool AutoStart { get; set; }


		/// TODO: File Name/Allocation Table properties here..


		public UInt16 SecureAreaChecksum { get; set; }
		public UInt16 SecureAreaDelay { get; set; }

		private Rom(Stream stream)
		{
			var buffer = new byte[stream.Length];
			stream.Read(buffer, 0, (int)stream.Length);
			_baseStream = new MemoryStream(buffer);

			_LoadHeaderData(this, stream);
		}

		public static Rom Load(Stream stream)
		{
			return new Rom(stream);
		}

		public static Rom Load(string fileName)
		{
			return Load(File.OpenRead(fileName));
		}

		private static void _LoadHeaderData(Rom rom, Stream stream)
		{
			var reader = new BinaryReader(stream);
			reader.BaseStream.Seek(0L, SeekOrigin.Begin);

			rom.ShortTitle = Encoding.ASCII.GetString(reader.ReadBytes(12));
			rom.GameCode = Encoding.ASCII.GetString(reader.ReadBytes(4));
			rom.MakerCode = MakerCodes.FromBytes(reader.ReadBytes(2));
			rom.SupportedDevices = _GetSupportedDevices(reader.ReadByte( ));
			rom.EncryptionSeed = reader.ReadByte( );
			rom.DeviceCapacity = reader.ReadByte( );
			reader.ReadBytes(9); // 9 bytes reserved at 015h
			rom.Version = reader.ReadByte( );
			rom.AutoStart = reader.ReadBoolean( );

			/// Read the basic info of the Arm9 Chip.
			rom._arm9 = new RomChipInfo
			{
				RomOffset = reader.ReadUInt32( ),
				EntryAddress = reader.ReadUInt32( ),
				RamAddress = reader.ReadUInt32( ),
				Size = reader.ReadUInt32( ),
			};

			/// Read the basic info of the Arm7 Chip.
			rom._arm7 = new RomChipInfo
			{
				RomOffset = reader.ReadUInt32( ),
				EntryAddress = reader.ReadUInt32( ),
				RamAddress = reader.ReadUInt32( ),
				Size = reader.ReadUInt32( ),
			};

			/// TODO: File Name/Allocation Table Reads...
			reader.ReadUInt32( );
			reader.ReadUInt32( );
			reader.ReadUInt32( );
			reader.ReadUInt32( );

			reader.ReadUInt32( ); // Port 40001A4h setting for normal commands (usually 00586000h)
			reader.ReadUInt32( ); // Port 40001A4h setting for KEY1 commands   (usually 001808F8h)
			rom._iconTitleOffset = reader.ReadUInt32( );


			rom.SecureAreaChecksum = reader.ReadUInt16( );
			rom.SecureAreaDelay = reader.ReadUInt16( );

			rom._arm9.AutoLoadListRamAddress = reader.ReadUInt32( );
			rom._arm7.AutoLoadListRamAddress = reader.ReadUInt32( );

			// 8     Secure Area Disable (by encrypted "NmMdOnly") (usually zero)
			// 4     Total Used ROM size (remaining/unused bytes usually FFh-padded)
			// 4     ROM Header Size (4000h)
			// 38h   Reserved (zero filled)
			// 9Ch   Nintendo Logo (compressed bitmap, same as in GBA Headers)
			// 2     Nintendo Logo Checksum, CRC-16 of [0C0h-15Bh], fixed CF56h
			// 2     Header Checksum, CRC-16 of [000h-15Dh]
			// 4     Debug rom_offset   (0=none) (8000h and up)       ;only if debug
			// 4     Debug size         (0=none) (max 3BFE00h)        ;version with
			// 4     Debug ram_address  (0=none) (2400000h..27BFE00h) ;SIO and 8MB
			// 4     Reserved (zero filled) (transferred, and stored, but not used)
			// 90h   Reserved (zero filled) (transferred, but not stored in RAM)
		}

		private static DeviceType _GetSupportedDevices(byte unitCode)
		{
			var devices = DeviceType.NintendoDS;

			/// Both Nintendo DS and the DSi are supported.
			if (unitCode == 0x02)
				devices |= DeviceType.NintendoDSi;

			/// Only Nintendo DSi is supported.
			if (unitCode == 0x03)
				devices = DeviceType.NintendoDSi;

			return devices;
		}


	}
}
