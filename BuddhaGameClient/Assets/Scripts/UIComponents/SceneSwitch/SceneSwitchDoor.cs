using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public abstract class SceneSwitchDoor : MonoBehaviour
	{
		protected RectTransform mRectTransform;
		private float mPos = 0;
		private bool mOpening = false;
		private bool mClose = false;
		private float mSpeed = 0.05f;
		private Runable mClosedCallback;

		private void FixedUpdate()
		{
			if (mOpening)
			{
				if (mPos > 1)
				{
					mPos = 1;
				}
				mPos += mSpeed;
			}
			else if (mClose)
			{
				if (mPos < 0)
				{
					mPos = 0;
					mClosedCallback?.Invoke();
				}
				mPos -= mSpeed;
			}
			setPosition(mPos);
		}

		public void open()
		{
			mOpening = true;
			mClose = false;

		}

		public void close()
		{
			mOpening = false;
			mClose = true;
		}

		public void setClosedCallback(Runable runable)
        {
			mClosedCallback = runable;

		}

        public void closeImmediately()
        {
			setPosition(0);
		}

		protected abstract void setPosition(float mPos);
    }
}
