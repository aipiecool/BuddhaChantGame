using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class ServerUnpackage 
	{
		protected int mHeader;
		protected byte[] mBody;
		public ServerUnpackage(byte[] srcBytes)
        {
			mHeader = BytesUtils.bytes2Int(BytesUtils.subbytes(srcBytes, 0, 4));
			mBody = BytesUtils.subbytes(srcBytes, 4, -1);
		}

		public int getHeader()
        {
			return mHeader;
        }

		public byte[] getBody()
        {
			return mBody;
        }

        public override string ToString()
        {
            return "ServerUnpackage(header:" + mHeader + ", mBody" + mBody.ToString() + ")";
        }
    }
}
