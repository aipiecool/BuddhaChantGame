using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
    public class SystemLogOutput : LogOutput
    {
        public override void write(int level, string message)
        {
            if (level >= Log.sOutputLevel)
            {
                if (level == 2)
                {
                    Debug.LogError(message);
                }
                else
                {
                    Debug.Log(message);
                }
            };
        }
    }
}
