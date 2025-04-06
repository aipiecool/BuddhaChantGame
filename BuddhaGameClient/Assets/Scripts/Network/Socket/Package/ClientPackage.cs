using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
    public abstract class ClientPackage
    {
        private byte[] mHeader;
        public abstract string getHeaderString();

        protected abstract byte[] getBody();

        public virtual byte[] getHeader()
        {
            if(mHeader == null)
            {
                mHeader = BytesUtils.int2Bytes(EncodeUtils.string2HashCode(getHeaderString()));
            } 
            return mHeader;
        }

        public virtual byte[] getBytes()
        {
            byte[] header = getHeader();
            byte[] body = getBody();
            return BytesUtils.bytesConcat(header, body);
        }

        public override string ToString()
        {
            return getHeaderString();
        }
    }
}