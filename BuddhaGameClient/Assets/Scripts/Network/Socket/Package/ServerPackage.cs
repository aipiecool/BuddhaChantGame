using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public abstract class ServerPackage
	{
        private int mHeader = 0;
        public abstract string getHeaderString();  

		public abstract ServerPackage create(ServerUnpackage unpackage);

        public abstract void process(object outer);

        public int getHeader()
        {
            if (mHeader == 0)
            {
                mHeader = EncodeUtils.string2HashCode(getHeaderString());
            }
            return mHeader;
        }

        public override int GetHashCode()
        {
            return getHeader();
        }

        public override string ToString()
        {
            return getHeaderString();
        }

    }
}
