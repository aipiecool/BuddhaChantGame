using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BuddhaGame
{
	public class Selector : MonoBehaviour
	{
		public List<Button> buttons;
		public Sprite selectedSprite; 
		public Sprite unselectedSprite;
		private List<SelectorButton> mSelectorButton = new List<SelectorButton>();
		private void Start()
        {
			for (int i = 0; i < buttons.Count; i++)
			{
				mSelectorButton.Add(new SelectorButton(buttons[i], i));
			}
		}

		public void addCallback(SelectorCallback callback)
        {
			for (int i = 0; i < buttons.Count; i++)
			{
				mSelectorButton[i].setCallback(callback);
				mSelectorButton[i].setCallback(onButtonClick);
			}
		}

		private void onButtonClick(int index)
        {
			for (int i = 0; i < buttons.Count; i++)
			{
				if(i == index)
                {
					mSelectorButton[i].setSprite(selectedSprite);
                }
                else
                {
					mSelectorButton[i].setSprite(unselectedSprite);
				}
			}
		}

		public class SelectorButton
		{		
			private Button mButton;
			private int mIndex;

			public SelectorButton(Button button, int index)
			{
				mButton = button;
				mIndex = index;
			}

			public void setCallback(SelectorCallback callback)
			{
				mButton.onClick.AddListener(()=> {
					callback(mIndex);
				});
			}

			public void setSprite(Sprite sprite)
            {
				mButton.image.sprite = sprite;

			}
		}
	}


	public delegate void SelectorCallback(int index);
}
