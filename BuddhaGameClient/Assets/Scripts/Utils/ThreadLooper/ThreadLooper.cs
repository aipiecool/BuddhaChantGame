using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace BuddhaGame
{
	public class ThreadLooper : MonoBehaviour
	{
        private static ThreadLooper sInstance;

        private Queue<Runable> mRunableQueue = new Queue<Runable>();

        private int mMainThreadId;

		private void Awake()
        {
            sInstance = this;
            mMainThreadId =  Thread.CurrentThread.ManagedThreadId;
        }

        public void autoRunMainThread(Runable runable)
        {
            if (isMainThread())
            {
                runable();
            }
            else
            {
                runMainThread(runable);
            }
        }

        public static ThreadLooper get()
        {
			return sInstance;
		}

        public bool isMainThread()
        {
            return Thread.CurrentThread.ManagedThreadId == mMainThreadId;
        }

        public void runMainThread(Runable runable)
        {
            mRunableQueue.Enqueue(runable);
        }

      

        public void startCoroutine(IEnumerator routine)
        {
            StartCoroutine(routine);
        }

        private void Update()
        {            
            while(mRunableQueue.Count > 0)
            {
                Runable runable = mRunableQueue.Dequeue();
                runable();
            }            
        }

    }
}
