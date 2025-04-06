using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class SimpleClientJsonRequestPackage : ClientJsonRequestPackage
	{
        private string mHeaderString;

		public SimpleClientJsonRequestPackage(string headerString)
        {
            mHeaderString = headerString;
        }

        public override string getHeaderString()
        {
            return mHeaderString;
        }
    }
}
