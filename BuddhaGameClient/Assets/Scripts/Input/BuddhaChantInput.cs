using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame 
{
	public delegate void ChantCallback(int wordId);

	public class BuddhaChantInput : MonoBehaviour
	{
        private static BuddhaChantInput sInstance;
        private static bool mOpenRecord = false;

        public ChantCallback mOnChantCallback;
        
        public RecognissimoInput recognissimoInput;

        private void Awake()
        {
            sInstance = this;
        }

        public static BuddhaChantInput get()
        {
            return sInstance;
        }

        public void addCallback(ChantCallback callback)
        {
            if(mOnChantCallback == null)
            {
                mOnChantCallback = callback;
            }
            else
            {
                mOnChantCallback += callback;
            }     
		}

		private void Update()
        {
            if (mOpenRecord && Debug.isDebugBuild)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Space))
                {
                    notify(0);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    notify(1);
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    notify(2);
                }
            }          
        }

        public void notify(int wordId)
        {
            mOnChantCallback?.Invoke(wordId);
        }

        public void OnAndroidInput(string wordId)
        {
            Log.input().debug("Receive OnAndroidInput:" + wordId);
            if(sInstance != null && mOpenRecord)
            {
                sInstance.notify(int.Parse(wordId));
            }            
        }

        public void setOpenRecord(bool open)
        {
            //AndroidUtils.setOpenRecord(open);
            recognissimoInput.setOpenRecord(open);
            mOpenRecord = open;
        }

        public bool getOpenRecord()
        {
            return mOpenRecord;
        }
    }
}
