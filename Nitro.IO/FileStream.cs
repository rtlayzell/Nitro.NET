﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nitro.IO
{
	public class FileStream : System.IO.Stream
	{
		public override bool CanRead
		{
			get { throw new NotImplementedException( ); }
		}

		public override bool CanSeek
		{
			get { throw new NotImplementedException( ); }
		}

		public override bool CanWrite
		{
			get { throw new NotImplementedException( ); }
		}

		public override void Flush( )
		{
			throw new NotImplementedException( );
		}

		public override long Length
		{
			get { throw new NotImplementedException( ); }
		}

		public override long Position
		{
			get
			{
				throw new NotImplementedException( );
			}
			set
			{
				throw new NotImplementedException( );
			}
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException( );
		}

		public override long Seek(long offset, System.IO.SeekOrigin origin)
		{
			throw new NotImplementedException( );
		}

		public override void SetLength(long value)
		{
			throw new NotImplementedException( );
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException( );
		}
	}
}
