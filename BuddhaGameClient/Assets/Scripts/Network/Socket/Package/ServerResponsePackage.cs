using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public abstract class ServerResponsePackage : ServerPackage
	{
        protected ServerUnpackage mUnpackage;
        protected int mCode;
        protected string mMessage;

        public ServerResponsePackage(ServerUnpackage unpackage)
        {
            mUnpackage = unpackage;
            byte[] body = mUnpackage.getBody();
            mCode = BytesUtils.bytes2Int(BytesUtils.subbytes(body, 0, 4));
            mMessage = BytesUtils.bytes2String(BytesUtils.subbytes(body, 4, -1));
        }

        public ServerResponsePackage()
        {

        }

        public int getCode()
        {
            return mCode;
        }

        public string getMessage()
        {
            return mMessage;
        }
    }
}
