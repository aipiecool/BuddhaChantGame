using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

namespace BuddhaGame
{
	public class UDPSocketNet : SocketNet
	{
        private static object sSingletonLock = new object();
        private UdpClient mUdpClient;
        private IPEndPoint mServerIPEndPoint;
        private IPEndPoint mLocalEndPoint;
        private byte[] mReceiveBuffer;
        private Thread mReceiveThread;
        private volatile bool mReceiveThreadStop;
        private String mReceiveString;
        private object mThreadLocker = new object();
        private volatile bool mIsSuspend = false;

        public UDPSocketNet()
        {
            mReceiveThreadStop = false;
            mThreadLocker = new object();
        }

        public override void connect(string ip, int port)
        {
            lock (sSingletonLock)
            {
                if (mIsConnected)
                {
                    return;
                }

                mServerIp = ip;
                mServerPort = port;

                mUdpClient = new UdpClient(mLocalPort);
                mServerIPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                mLocalEndPoint = new IPEndPoint(IPAddress.Any, mLocalPort);

                mReceiveThread = new Thread(new ThreadStart(reciveThreadMethod));
                mReceiveThread.IsBackground = true;
                mReceiveThread.Start();

                mIsConnected = true;
            }
        }

        public override void reconnect()
        {
            mIsSuspend = false;
        }

        public override void release()
        {
            mUdpClient.Close();
            mReceiveThreadStop = true;
        }

        public override void sendBytes(byte[] bytes)
        {
            if (!mIsSuspend)
            {
                try
                {
                    mUdpClient.Send(bytes, bytes.Length, mServerIPEndPoint);
                }
                catch (Exception e)
                {
                    mIsSuspend = true;
                    notifyError(e);
                }
            }
        }

        private void reciveThreadMethod()
        {
            while (!mReceiveThreadStop)
            {
                if (!mIsSuspend)
                {
                    lock (mThreadLocker)
                    {
                        try
                        {
                            mReceiveBuffer = mUdpClient.Receive(ref mLocalEndPoint);
                            notifyReceive(mReceiveBuffer);
                        }
                        catch (Exception e)
                        {
                            mIsSuspend = true;
                            notifyError(e);
                        }
                    }
                }
            }
        }
    }
}
