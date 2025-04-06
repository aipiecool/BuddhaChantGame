using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BuddhaGame
{
	public class PortraitView : MonoBehaviour
	{
		private static PortraitView sInstance;

		public RectTransform exp_bar;
		public Text exp_text;
		public Text level_text;
		public Button menu_button;

		private PortraitModel mPortraitModel = new PortraitModel();
		private float mExpBarStartWidth;

		private void Awake()
        {
			sInstance = this;
			mExpBarStartWidth = exp_bar.sizeDelta.x;
			requestUpdate();
			setLevel(0);
			setExp(0, 100);
		}

		public static PortraitView get()
		{
			return sInstance;
		}

		public void requestUpdate()
        {
			mPortraitModel.requestLevel((levelInfo)=>
			{
				ThreadLooper.get().runMainThread(() =>
				{
					setLevel(levelInfo.level);
					setExp(levelInfo.exp, levelInfo.maxExp);
				});			
			});
		}

		public void setLevel(int level)
		{
			level_text.text = "等级lv." + level;
		}

		public void setExp(int exp, int maxExp)
        {
			if (exp_text != null && exp_bar != null)
			{
				exp_text.text = "经验值:" + exp + "/" + maxExp;
				float barWidth = ((float)exp / maxExp) * mExpBarStartWidth;
				exp_bar.sizeDelta = new Vector2(barWidth, exp_bar.sizeDelta.y);
			}
		}
	}
}
