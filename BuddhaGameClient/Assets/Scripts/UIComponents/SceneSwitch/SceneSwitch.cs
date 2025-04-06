using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BuddhaGame
{
	public class SceneSwitch : MonoBehaviour
	{
		private static SceneSwitch sInstance;

		public SceneSwitchDoor mLeftDoor;
		public SceneSwitchDoor mRightDoor;
		public Text mSceneNameText;

		private float mTextColor = 0;
		private float mColorSpeed = 0.01f;
		private Runable mClosedCallback;

		private void Awake()
        {
			sInstance = this;			
		}

        protected void Start()
        {
			closeImmediatelyAndOpen();
			mSceneNameText.text = WorldScene.getCurrentSceneLocalName();
			mTextColor = 1;
			mLeftDoor.setClosedCallback(()=>
			{
				mClosedCallback?.Invoke();
			});
		}

        private void FixedUpdate()
        {
            if(mTextColor > 0)
            {
				mSceneNameText.color = new Color(1, 1, 0, mTextColor);
				mTextColor-= mColorSpeed;
			}
        }

        internal void switchScene(Runable runable)
        {
			mClosedCallback = runable;
			mLeftDoor.close();
			mRightDoor.close();			
		}

        public static SceneSwitch get()
        {
			return sInstance;
		}

		protected void closeImmediatelyAndOpen()
        {
			mLeftDoor.closeImmediately();
			mLeftDoor.open();
			mRightDoor.closeImmediately();
			mRightDoor.open();
		}
	}
}
