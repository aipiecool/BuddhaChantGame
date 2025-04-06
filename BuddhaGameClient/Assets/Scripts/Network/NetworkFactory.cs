using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
    public class NetworkFactory 
    {
        public const String BACKEND_SERVER_HOST = "https://127.0.0.1:7519";
        public const String UDP_SERVER_HOST = "127.0.0.1";
        public const int SERVER_UDP_PORT = 10100;

        private static object sSingletonLock = new object();
        private static bool sIsConnected = false;

        private static SocketPackageNet sSocketNet;
        private static HttpNet sHttpNet;

        public static SocketPackageNet getSocketNet()
        {
            if(sSocketNet == null)
            {
                lock (sSingletonLock)
                {
                    if (sSocketNet == null)
                    {
                        sSocketNet = new SocketPackageNet(new UDPSocketNet());
                    }
                }                
            }
            if (!sIsConnected)
            {
                sSocketNet.connect(UDP_SERVER_HOST, SERVER_UDP_PORT);
                sIsConnected = true;
            }
            return sSocketNet;
        }

        public static HttpNet getHttpNet()
        {
            if (sHttpNet == null)
            {
                lock (sSingletonLock)
                {
                    if (sHttpNet == null)
                    {                       
                        sHttpNet = new HttpNet(BACKEND_SERVER_HOST);
                    }
                }
            }
            return sHttpNet;
        }
    }
}
