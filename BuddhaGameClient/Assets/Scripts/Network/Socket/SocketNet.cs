using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace BuddhaGame
{
    public abstract class SocketNet
    {
        protected String mServerIp;
        protected int mServerPort;
        protected int mLocalPort = 60000;
        protected bool mIsConnected = false;
        protected List<SocketListener> mSocketListeners;       

        protected SocketNet()
        {
            mSocketListeners = new List<SocketListener>();    
        }

        public abstract void connect(String ip, int port);

        public abstract void reconnect();

        public abstract void release();  

        public void addReceiveListener(SocketListener listener)
        {
            mSocketListeners.Add(listener);
        }

        public void removeReceiveListener(SocketListener listener)
        {
            mSocketListeners.Remove(listener);
        }

        public abstract void sendBytes(byte[] bytes);   

        protected void notifyReceive(byte[] data)
        {
            foreach (SocketListener l in mSocketListeners)
            {
                l.onReceive(data);
            }
        }

        protected void notifyError(Exception e)
        {
            foreach (SocketListener l in mSocketListeners)
            {
                l.onError(e);
            }
        }
        
    }
}
