using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BuddhaGame
{
	public class FlowSpriteLabel : MonoBehaviour
	{
		private static GameObject sSpriteLabelsParent;
		private static Font sFont;
		
		private LinkedList<FlowLable> mFlowlabels = new LinkedList<FlowLable>();
		private RuntimeController mRuntimeController = new RuntimeController(0.01f);

		private void Awake()
		{
			if (sSpriteLabelsParent == null)
			{
				sSpriteLabelsParent = GameObject.Find("SpriteLabels");
				sFont = Resources.Load("SourceHanSerifCN-Regular") as Font;
			}
		}

		private void Start()
		{
			
		}

		public void addText(string text) 
		{
			FlowLable flowLable = new FlowLable(this);
			flowLable.setText(text);
			flowLable.setSize(500, 1);
			flowLable.setOffset(0, 0.1f);
			flowLable.setColor(Color.yellow);
			mFlowlabels.AddLast(flowLable);
		}

        private void Update()
        {            
			mRuntimeController.run(()=> {
				lock (mFlowlabels)
				{
					LinkedListNode<FlowLable> node = mFlowlabels.First;
					while (node != null)
					{
						FlowLable flowLable = node.Value;	
                        if (flowLable.isDestory())
                        {
							mFlowlabels.Remove(flowLable);
                        }
                        else
                        {
							flowLable.moveUp();
							flowLable.update();
						}
						node = node.Next;
					}
				}			
			});			
        }

		internal void destory()
		{
			lock (mFlowlabels)
			{
				LinkedListNode<FlowLable> node = mFlowlabels.First;
				while (node != null)
				{
					FlowLable flowLable = node.Value;
					flowLable.destory();
					mFlowlabels.Remove(flowLable);
					node = node.Next;
				}
			}
		}

		public class FlowLable
        {
			private FlowSpriteLabel mOuter;
			private Transform mOuterTransform;
			private GameObject mGameObject;
			private RectTransform mRectTransform;
			private Vector3 mOffset = new Vector3(0, 0);
			private Text mText;
			private Color mColor = Color.white;
			private bool mIsDestory = false;
			private FlowSpriteLabelChild mFlowLabelChild;

			public FlowLable(FlowSpriteLabel outer)
            {
				mOuter = outer;
				mOuterTransform = outer.transform;
				mGameObject = new GameObject("sprite_label");
				mGameObject.transform.position = new Vector3(int.MaxValue, 0, 0);
				mGameObject.transform.parent = sSpriteLabelsParent.transform;
				mRectTransform = mGameObject.AddComponent<RectTransform>();
				mText = mGameObject.AddComponent<Text>();
				mText.font = sFont;
				mText.alignment = TextAnchor.MiddleCenter;
				mText.fontStyle = FontStyle.Bold;
				mText.fontSize = 35;
				mFlowLabelChild = mGameObject.AddComponent<FlowSpriteLabelChild>();
				mFlowLabelChild.initialize(this, mOuter, mOuterTransform);
			}

			public void moveUp()
            {
				
				float colorA = mColor.a - 0.01f;
				if(colorA <= 0)
                {
					mIsDestory = true;
					Destroy(mGameObject);
				}
                else
                {
					mColor = new Color(mColor.r, mColor.g, mColor.b, colorA);
					mText.color = mColor;
					mOffset = new Vector3(mOffset.x, mOffset.y + 0.002f, 0);					
				}				
			}

			public bool isDestory()
			{
				return mIsDestory;
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
				mText.color = color;
				mColor = color;
			}

			public void setAlignment(TextAnchor textAnchor)
			{
				mText.alignment = textAnchor;
			}

			public void setText(string str)
			{
				mText.text = str;
			}

			public void update()
			{
				if (mGameObject != null)
				{
					mGameObject.transform.localScale = new Vector3(2, 2, 1);
					mGameObject.transform.position = Camera.main.WorldToScreenPoint(mOuterTransform.position + mOffset);					
				}
			}

			public void destory()
            {
				mIsDestory = true;
				Destroy(mGameObject);
			}
        }

        private void OnDestroy()
        {
			destory();
        }
    }

}
