using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nitro.IO
{
	/// <summary>
	/// Specifies the type of Device/Platform a particular
	/// game has been developed for.
	/// </summary>
	[Flags]
	public enum DeviceType
	{
		/// <summary>
		/// The Nintendo DS Platform.
		/// </summary>
		NintendoDS = 0x01,


		/// <summary>
		/// The Nintendo DSi Platform.
		/// </summary>
		NintendoDSi = 0x02,


		/// <summary>
		/// Both the Nintendo DS and DSi Platforms.
		/// </summary>
		NintendoDSAndDSi = NintendoDS | NintendoDSi,
	}
}
