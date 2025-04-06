using BuddhaGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utils.NotifyData
{
    public class NotifyData<T>
    {
        private T mValue;

        private List<NotifyDataObserver<T>> mObservers;
        private float mNotifyIntervalSecond = -1;
        private float mNotifySecond = 0;
        private float mRealtimeSecond = 0;

        public NotifyData()
        {
            mObservers = new List<NotifyDataObserver<T>>();
        }

        public NotifyData(T defaultValue)
        {
            mValue = defaultValue;
        }

        public void setNotifyIntervalTime(float second)
        {
            mNotifyIntervalSecond = second;
            mNotifySecond = second * 2;
        }

        public void setSynchronizeTime(float second)
        {
            ThreadLooper.get().startCoroutine(synchronizeCoroutine(second));
        }

        private IEnumerator synchronizeCoroutine(float second)
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(second);
            while (true)
            {
                yield return waitForSeconds;
                if (mValue != null)
                {                
                    notify(mValue);
                }
            }            
        }

        public void addObserver(NotifyDataObserver<T> observer)
        {
            mObservers.Add(observer);
        }

        public void removeObserver(NotifyDataObserver<T> observer)
        {
            mObservers.Remove(observer);
        }

        public void notify(T value)
        {            
            if (mNotifySecond > mNotifyIntervalSecond)
            {
                foreach (NotifyDataObserver<T> o in mObservers)
                {
                    o(value);
                }
                mNotifySecond = 0;
            }
            else
            {
                mNotifySecond += Time.time - mRealtimeSecond;
            }
            mRealtimeSecond = Time.time;
        }

        public void setValue(T v)
        {
            if(v == null)
            {
                mValue = v;
                return;
            }            
            if (!v.Equals(mValue))
            {
                mValue = v;
                notify(mValue);
            }
        }
    }
}
