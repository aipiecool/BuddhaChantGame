using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BuddhaGame
{
	public class SpriteLabel : MonoBehaviour
	{
		private static GameObject sSpriteLabelsParent;
		private static Font sFont;

		private GameObject mLabelObject;
		private RectTransform mRectTransform;
		private Text mLabelText;
		private Runable mInitializedCallback;
		private Vector3 mOffset = new Vector3(0, 0);
		private bool mStarted = false;

		private void Awake()
        {
            if(sSpriteLabelsParent == null)
            {
				sSpriteLabelsParent = GameObject.Find("SpriteLabels");
				sFont = Resources.Load("SourceHanSerifCN-Regular") as Font;
			}
        }

        private void Start()
        {
			mLabelObject = new GameObject("sprite_label");
			mLabelObject.transform.parent = sSpriteLabelsParent.transform;
			mRectTransform = mLabelObject.AddComponent<RectTransform>();
			mLabelText = mLabelObject.AddComponent<Text>();
			mLabelText.font = sFont;
			mLabelText.alignment = TextAnchor.MiddleCenter;
			mLabelText.fontStyle = FontStyle.Bold;
			mLabelText.fontSize = 35;
            mInitializedCallback?.Invoke();
			mStarted = true;
		}		

        public void setInitialzedCallback(Runable runable)
		{
			mInitializedCallback = runable;
            if (mStarted)
            {
				runable();

			}
		}

		public void setOffset(float x, float y)
        {
			mOffset = new Vector3(x, y, 0);
		}

		public void setSize(int width, int maxLine)
        {
			mRectTransform.sizeDelta = new Vector2(width, maxLine * 60);
		}

		public void setColor(Color color)
		{
			mLabelText.color = color;
		}

		public void setAlignment(TextAnchor textAnchor)
        {
			mLabelText.alignment = textAnchor;
		}

		public void setText(string text)
        {
			mLabelText.text = text;
		}

        private void Update()
		{
			if(mLabelObject != null)
            {
				mLabelObject.transform.localScale = new Vector3(2, 2, 1);
				mLabelObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + mOffset);
			}			
		}

		public void destory()
        {
			Destroy(mLabelObject);
        }
    }
}
