using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class SocketPackageNet
	{
		protected SocketRequestNet mSocketRequestNet;
		private SocketNet mSocketNet;
		protected List<PackageListener> mPackageListeners;
		public SocketPackageNet(SocketNet socketNet)
        {
			mSocketNet = socketNet;
			mSocketNet.addReceiveListener(new SocketReceiveCallback(this));
			mSocketRequestNet = new SocketRequestNet(this);
			mPackageListeners = new List<PackageListener>();
		}

		public void connect(String ip, int port)
        {
			mSocketNet.connect(ip, port);

		}

		public void reconnect()
        {
			mSocketNet.reconnect();

		}

		public void release()
        {
			mSocketNet.release();
			mSocketRequestNet.release();
		}

		public void sendRequest(SocketRequest request)
		{
			Log.input().debug("发送请求包:" + request.ToString());
			mSocketRequestNet.sendRequest(request);
		}

		public void sendPackage(ClientPackage pkg)
        {
			Log.input().debug("发送动作包:" + pkg.ToString());
			mSocketNet.sendBytes(pkg.getBytes());
		}

		public void addPackageListeners(PackageListener listener)
        {
			mPackageListeners.Add(listener);

		}

		public void removePackageListeners(PackageListener listener)
		{
			mPackageListeners.Remove(listener);
		}

		class SocketReceiveCallback : SocketListener
        {
			SocketPackageNet mSocketPackageNet;

			public SocketReceiveCallback(SocketPackageNet socketPackageNet)
            {
				mSocketPackageNet = socketPackageNet;
			}

			void SocketListener.onReceive(byte[] data)
			{
				ServerUnpackage unpackage = new ServerUnpackage(data);				
				bool isResponsePackage = ServerPackageManager.get().isResponsePackage(unpackage);
				if (isResponsePackage)
				{					
					ServerRequestUnpackage requestUnpackage = new ServerRequestUnpackage(data);
					mSocketPackageNet.mSocketRequestNet.onServerResponse(requestUnpackage);
				}
                else
                {
					ServerPackage packageInstance = ServerPackageManager.get().createInstanceByUnpackage(unpackage);
					if (packageInstance != null)
					{
						Log.input().debug("收到动作包:" + packageInstance.ToString());
						foreach (PackageListener l in mSocketPackageNet.mPackageListeners)
						{
							l.onReceive(packageInstance);
						}
                    }
                    else
                    {
						Log.input().warn("收到包但无法解析:" + unpackage.getHeader());
					}
				}
			}
			void SocketListener.onError(Exception e)
            {
				Dialog.get().show(e.ToString());
				Log.input().warn(e.ToString() + "\n" + e.StackTrace);
            }          
        }
	}
}
