using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace BuddhaGame
{
	public class HeartbeatNetwork : MonoBehaviour
	{
        private SocketPackageNet mSocket;
        private Thread mNetworkThread;
        private bool mIsStop = false;
        private UserRecorder mUserRecorder;
        
        private void Start()
        {
            mSocket = NetworkFactory.getSocketNet();
            mUserRecorder = UserRecorder.get();
            mNetworkThread = new Thread(networkRunable);
            mNetworkThread.Start();
        }

        private void OnDestroy()
        {
            mIsStop = true;
        }

        private void networkRunable()
        {
            while (!mIsStop)
            {
                if (mUserRecorder.isLogin())
                {
                    mSocket.sendPackage(new HeartbeatClientPackage());
                }
                Thread.Sleep(2000);
            }
        }

        private class HeartbeatClientPackage : ClientPackage
        {
            public override string getHeaderString()
            {
                return "Heartbeat";
            }

            protected override byte[] getBody()
            {
                return new byte[0];
            }
        }
    }
}
