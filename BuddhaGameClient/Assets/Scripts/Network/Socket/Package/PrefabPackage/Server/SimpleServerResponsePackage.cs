using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class SimpleServerResponsePackage : ServerResponsePackage
	{
        private string mHeaderString;
        
        public SimpleServerResponsePackage(string headerString) 
        {
            mHeaderString = headerString;
        }

        public SimpleServerResponsePackage(ServerUnpackage unpackage) : base(unpackage)
        {
            
        }

        public override ServerPackage create(ServerUnpackage unpackage)
        {
            return new SimpleServerResponsePackage(unpackage);
        }

        public override string getHeaderString()
        {
            if(mHeaderString == null)
            {
                mHeaderString = ServerPackageManager.get().getHeaderStringByHeader(mUnpackage.getHeader());
            }
            return mHeaderString;
        }

        public override void process(object outer)
        {
            
        }
    }
}
