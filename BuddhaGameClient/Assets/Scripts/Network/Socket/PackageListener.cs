using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public abstract class PackageListener 
	{
		private HashSet<ServerPackage> mSubscribePackages = new HashSet<ServerPackage>();

		public void subscribePackages(ServerPackage package)
        {
			mSubscribePackages.Add(package);
		}

		public void unsubscribePackages(ServerPackage package)
		{
			mSubscribePackages.Remove(package);
		}

		public bool isSubscribe(ServerPackage package)
        {
			return mSubscribePackages.Contains(package);			
        }

		public abstract void onReceive(ServerPackage package);
	}
}
