using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class SimpleClientRequestPackage : ClientRequestPackage
	{
        private string mHeaderString;
        private byte[] mBody;

        public SimpleClientRequestPackage(string headerString) : this(headerString, 0)
        {
            
        }

        public SimpleClientRequestPackage(string headerString, int code)
        {
            mHeaderString = headerString;
            mBody = BytesUtils.int2Bytes(code);
        }

        public SimpleClientRequestPackage(string headerString, string message)
        {
            mHeaderString = headerString;
            mBody = BytesUtils.string2Bytes(message);
        }

        public override string getHeaderString()
        {
            return mHeaderString;
        }

        protected override byte[] getBody()
        {
            return mBody;
        }
    }
}
