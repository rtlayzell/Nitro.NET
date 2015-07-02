using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Nitro.IO
{
	[StructLayout(LayoutKind.Sequential)]
	public struct RomChipInfo
	{
		public UInt32 RomOffset;
		public UInt32 EntryAddress;
		public UInt32 RamAddress;
		public UInt32 Size;

		public UInt32 OverlayOffset;
		public UInt32 OverlaySize;
		public UInt32 AutoLoadListRamAddress;
	}
}
