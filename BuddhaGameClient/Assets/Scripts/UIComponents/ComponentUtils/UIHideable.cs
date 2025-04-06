using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace BuddhaGame
{
	public class UIHideable : MonoBehaviour
	{
		private RectTransform mRectTransform;
		private int mMainThreadId;

		private void Awake()
        {
			mMainThreadId = Thread.CurrentThread.ManagedThreadId;
			mRectTransform = GetComponent<RectTransform>();
			hide();
		}

		public virtual void show()
		{
			if (isMainThread())
			{
				transform.localScale = Vector3.one;
			}
			else
			{
				ThreadLooper.get().runMainThread(()=>
				{
					transform.localScale = Vector3.one;
				});
			}	
		}

        public virtual void hide()
		{
			if (isMainThread())
			{
				transform.localScale = Vector3.zero;
			}
			else
			{
				ThreadLooper.get().runMainThread(() =>
				{
					transform.localScale = Vector3.zero;
				});
			}			
		}

		private bool isMainThread()
		{
			return Thread.CurrentThread.ManagedThreadId == mMainThreadId;
		}
	}
}
