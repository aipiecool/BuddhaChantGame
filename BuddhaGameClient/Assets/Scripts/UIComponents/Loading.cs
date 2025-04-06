using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BuddhaGame
{
	public class Loading : UIHideable
	{
		private static Loading sInstance;
		private Text mText;
		private RectTransform mImageRectTransform;	
		private void OnEnable()
        {
			sInstance = this;
			mText = GetComponent<Text>();
			mImageRectTransform = transform.GetChild(0).GetComponent<RectTransform>();			
		}

		public static Loading get()
		{
			return sInstance;
		}	

		public void setText(string t)
        {
			ThreadLooper.get().autoRunMainThread(() => {
				mText.text = t;
			});
			
        }

        private void Update()
        {
			mImageRectTransform.Rotate(new Vector3(0, 0, -1));
		}
    }
}
