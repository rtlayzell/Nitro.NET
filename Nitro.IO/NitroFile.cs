using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nitro.IO
{
	public static class NitroFile
	{
		internal struct FntMainTable
		{
			public UInt32 SubtableOffset;
			public UInt16 FirstFileId;
			public UInt16 DirectoryCount;
			public UInt16 ParentId;
		}

		internal struct FntSubTable
		{
			byte TypeOrLength;
			string Name;
		}

		public static bool Exists(Rom rom, string path)
		{
			throw new NotImplementedException( );
		}

		public static FileStream Open(Rom rom, string path)
		{
			throw new NotImplementedException( );
		}
	}
}
