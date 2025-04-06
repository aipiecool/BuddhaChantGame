using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class Delayer 
	{
        private static Delayer sInstace;
        private static object sSingletonLock = new object();

        private Delayer()
        {

        }

        public static Delayer get()
        {
            if(sInstace == null)
            {
                lock (sSingletonLock)
                {
                    if (sInstace == null)
                    {
                        sInstace = new Delayer();
                    }
                }
            }
            return sInstace;
        }

        public void delayer(Runable runable, int second)
        {
            ThreadLooper.get().StartCoroutine(delayerCoroutine(runable, second));
        }

        private IEnumerator delayerCoroutine(Runable runable, int second)
        {
            yield return new WaitForSeconds(second);
            runable();
        }
    }
}
