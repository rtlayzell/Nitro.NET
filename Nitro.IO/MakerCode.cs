using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nitro.IO
{
	public enum MakerCode : ushort
	{
		Unknown = 0xFFFF,
		Homebrew = 0x00,
		Nintendo = 0x01,
	}

	internal static class MakerCodes
	{
		private static readonly Dictionary<byte[], MakerCode> _makeCodes = new Dictionary<byte[], MakerCode>( ) {
			{ Encoding.ASCII.GetBytes("00"), MakerCode.Homebrew },
			{ Encoding.ASCII.GetBytes("01"), MakerCode.Nintendo },
		};

		public static MakerCode FromBytes(byte[] makerCode, int startIndex = 0)
		{
			byte[] tmp = new byte[] { makerCode[startIndex], makerCode[startIndex + 1] };
			var result = _makeCodes.SingleOrDefault(x => Enumerable.SequenceEqual(tmp, x.Key));

			if (result.Equals(default(KeyValuePair<byte[], MakerCode>)))
				return MakerCode.Unknown;

			return result.Value;
		}
	}
}
