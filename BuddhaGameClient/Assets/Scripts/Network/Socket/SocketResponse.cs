using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class SocketResponse 
	{
        private ServerResponsePackage mPackage;
        private string mError;

        public SocketResponse(ServerResponsePackage package)
        {
			mPackage = package;
		}

        public SocketResponse(string error)
        {
            mError = error;
        }

        public bool isSuccess()
        {
            return mError == null;
        }

        public int getCode()
        {
            return mPackage.getCode();
        }

        public string getMessage()
        {
            return mPackage == null ? mError : mPackage.getMessage();
        }

        public override string ToString()
        {
            return "isSuccess:" + isSuccess() + ", message:" + getMessage();
        }
    }
}
