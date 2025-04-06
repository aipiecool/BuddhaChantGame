using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class ServerRequestUnpackage : ServerUnpackage
	{
		protected int mPackageId;
		public ServerRequestUnpackage(byte[] srcBytes) : base(srcBytes)
        {			
			mPackageId = BytesUtils.bytes2Int(BytesUtils.subbytes(srcBytes, 4, 8));
			mBody = BytesUtils.subbytes(srcBytes, 8, -1);
		}
		
		public int getPackageId()
		{
			return mPackageId;
		}
	}
}
