using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class ServerPackageManager 
	{
		private static ServerPackageManager sInstance;
		private static object sSingletonLock = new object();
        private Hashtable mServerPackages = new Hashtable();
        private HashSet<int> mResponsePackages = new HashSet<int>();

        public ServerPackageManager()
        {

        }

        public void registerServerPackage(ServerPackage serverPackage)
        {
            mServerPackages.Add(serverPackage.getHeader(), serverPackage);
            if(serverPackage is ServerResponsePackage)
            {
                mResponsePackages.Add(serverPackage.getHeader());
                Debug.Log("register header, "+ serverPackage.getHeaderString() +":" + serverPackage.getHeader());
            }
        }

        public ServerPackage createInstanceByUnpackage(ServerUnpackage unpackage)
        {
            ServerPackage package = searchServerPkg(unpackage);
            if(package != null)
            {
                ServerPackage packageInstance = package.create(unpackage);
                return packageInstance;
            }
            return null;
        }

        public string getHeaderStringByHeader(int header)
        {
            return (mServerPackages[header] as ServerPackage).getHeaderString();
        }

        private ServerPackage searchServerPkg(ServerUnpackage unpackage)
        {            
            return mServerPackages[unpackage.getHeader()] as ServerPackage; 
        }

        public bool isResponsePackage(ServerUnpackage unpackage)
        {
            return mResponsePackages.Contains(unpackage.getHeader());
        }

        public static ServerPackageManager get()
        {
            if (sInstance == null)
            {
                lock (sSingletonLock)
                {
                    if (sInstance == null)
                    {
                        sInstance = new ServerPackageManager();
                    }
                }
            }
            return sInstance;
        }
    }
}
