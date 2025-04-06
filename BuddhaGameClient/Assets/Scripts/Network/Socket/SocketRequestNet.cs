using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace BuddhaGame
{
	public class SocketRequestNet 
	{

		private LinkedList<SocketRequest> mRequestArray;
		private float mResponseTimeout = 5;
		private SocketPackageNet mSocketPackageNet;
		private Thread mTimeoutThread;
		private object mThreadLocker = new object();
		private int mRequestPackageIdIter = 0;
		private bool mIsRelease = false;

		public SocketRequestNet(SocketPackageNet socketNet)
        {
			mSocketPackageNet = socketNet;			
			mRequestArray = new LinkedList<SocketRequest>();
			mTimeoutThread = new Thread(new ThreadStart(timeoutRunable));
			mTimeoutThread.Start();
		}

		public void sendRequest(SocketRequest request)
		{
			lock (mThreadLocker)
            {
				request.setPackageId(DateUtils.getTimestampInt() % 10000 + mRequestPackageIdIter++);
				mRequestArray.AddLast(request);
				ClientPackage package = request.getPackage();
				mSocketPackageNet.sendPackage(package);
			}				
		}

		public void release()
        {
			mIsRelease = true;
		}

        private void timeoutRunable()
        {
			while (!mIsRelease)
			{
				lock (mThreadLocker)
				{
					LinkedListNode<SocketRequest> node = mRequestArray.First;
					while (node != null)
					{
						SocketRequest request = node.Value;
						int lifeTime = request.getLifeTime();
						if (lifeTime >= mResponseTimeout)
						{
							if (request.getReconnectTime() > 0)
							{
								request.decReconnectTime();
								request.resetLifeTime();
								Log.input().debug("请求超时，尝试重连:" + request.ToString());
								mSocketPackageNet.sendPackage(request.getPackage());
							}
							else
							{
								Log.input().warn("请求超时:" + request.ToString());
								request.getCallback()(new SocketResponse("请求超时"));
								mRequestArray.Remove(node);
							}
						}
						else
						{
							request.addLifeTime(1);
						}
						node = node.Next;
					}
				}
				Thread.Sleep(1000);
			}
		}

		public void onServerResponse(ServerRequestUnpackage requestUnpackage)
		{
			lock (mThreadLocker)
			{
				LinkedListNode<SocketRequest> node = mRequestArray.First;
				while (node != null)
				{
					SocketRequest request = node.Value;
					if(request.getPackageId() == requestUnpackage.getPackageId())
                    {
						ServerResponsePackage packageInstance = (ServerResponsePackage)ServerPackageManager.get().createInstanceByUnpackage(requestUnpackage);
						request.getCallback()(new SocketResponse(packageInstance));
						mRequestArray.Remove(request);
						Log.input().debug("收到请求回应包:" + packageInstance.ToString());
					}
					node = node.Next;
				}
			}
		}
	}
}
