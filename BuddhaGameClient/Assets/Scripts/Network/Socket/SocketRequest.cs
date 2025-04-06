using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BuddhaGame
{
    public class SocketRequest
    {
        private ClientRequestPackage mPackage;
        private SocketResponseCallback mCallback;
        private int mLifeTime = 0;
        private int mReconnectTime;
        private int mPackageId;

        public SocketRequest(ClientRequestPackage package, SocketResponseCallback callback, int reconnectTime = 5)
        {
            mPackage = package;
            mCallback = callback;
            mReconnectTime = reconnectTime;
        }

        public void setPackageId(int id)
        {
            mPackageId = id;
            mPackage.setPackageId(id);
        }

        public int getPackageId()
        {
            return mPackageId;
        }

        public ClientRequestPackage getPackage()
        {
            return mPackage;
        }

        public SocketResponseCallback getCallback()
        {
            return mCallback;
        }

        public void addLifeTime(int addtime)
        {
            mLifeTime += addtime;
        }

        internal void resetLifeTime()
        {
            mLifeTime = 0;
        }

        public int getLifeTime()
        {
            return mLifeTime;
        }

        public void decReconnectTime()
        {
            mReconnectTime--;
        }

        public int getReconnectTime()
        {
            return mReconnectTime;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(getPackage().getHeaderString());
            stringBuilder.Append("(id:");
            stringBuilder.Append(getPackageId());
            stringBuilder.Append(",reconnected:");
            stringBuilder.Append(getReconnectTime());
            stringBuilder.Append(")");
            return stringBuilder.ToString();
        }   
    }
}
