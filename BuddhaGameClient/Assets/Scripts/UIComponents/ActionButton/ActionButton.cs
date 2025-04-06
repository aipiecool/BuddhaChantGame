using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BuddhaGame
{
	public class ActionButton : UIHideable
	{
        private Object mCaller;
        private Button mButton;
        private Text mText;
        private Runable mRunable;
        private int mPriority = -1;

        private void Start()
        {
            mButton = GetComponent<Button>();
            mText = GetComponentInChildren<Text>();

            mButton.onClick.AddListener(onButtonClick);
        }

        public void onButtonClick()
        {
            mRunable?.Invoke();
        }

        public bool show(Object caller, string text, Runable runable, int priority, bool force)
        {           
            if(mCaller == caller)
            {
                return true;
            }
            if(mCaller == null || force)
            {
                if (mPriority <= priority )
                {
                    ThreadLooper.get().autoRunMainThread(() =>
                    {
                        mCaller = caller;
                        mRunable = runable;
                        mText.text = text;
                        show();
                    });
                    return true;
                }
            }            
            return false;
        }

        public bool hide(Object caller)
        {
            if(caller == mCaller)
            {
                mCaller = null;
                mPriority = -1;
                mRunable = null;
                hide();
                return true;
            }
            return false;
        }

        public Object getCaller()
        {
            return mCaller;
        }
    }    
}
