using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
    public interface SocketListener
    {
        void onReceive(byte[] data);

        void onError(Exception e);
    }
}