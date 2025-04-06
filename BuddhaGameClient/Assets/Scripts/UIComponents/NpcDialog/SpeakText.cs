using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BuddhaGame
{
	public class SpeakText : MonoBehaviour
	{
        private Text mText;
        private string mString;
        private int index = 0;
        private RuntimeController mRuntimeController;
        private Runable mSpeakFinishCallback;

        private void Start()
        {
            mText = GetComponent<Text>();
            mRuntimeController = new RuntimeController(0.05f);
        }

        public void setText(string text)
        {
            mString = text;
            index = 0;
        }

        public void setSpeakFinishCallback(Runable runable)
        {
            mSpeakFinishCallback = runable;
        }

        public void showAll()
        {
            mText.text = mString;
            mString = null;
            mSpeakFinishCallback?.Invoke();
        }

        public void clear()
        {
            mString = null;
            mText.text = "";
        }

        private void FixedUpdate()
        {

            mRuntimeController.run(() =>
            {
                if (mString != null)
                {
                    if (index <= mString.Length)
                    {
                        string subString = mString.Substring(0, index++);
                        mText.text = subString;
                    }
                    else
                    {
                        mString = null;
                        mSpeakFinishCallback?.Invoke();
                    }                        
                }
            });

        }
    }
}
