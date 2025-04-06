using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BuddhaGame
{
	public class Dialog : UIHideable
	{
		private static Dialog sInstance;

		public Text message_text;
		public Button yes_button;
		public Button no_button;

		private Text mYesButtonText;
		private Text mNoButtonText;
		private Runable mYesRunable;
		private Runable mNoRunable;

		private void OnEnable()
        {
			sInstance = this;
			mYesButtonText = yes_button.GetComponentInChildren<Text>();
			mNoButtonText = no_button.GetComponentInChildren<Text>();
			yes_button.onClick.AddListener(onYesButtonClick);
			no_button.onClick.AddListener(onNoButtonClick);
		}

		private void onYesButtonClick()
        {
			if(mYesRunable != null)
            {
				mYesRunable();
			}
			hide();
        }

		private void onNoButtonClick()
		{
			if (mNoRunable != null)
			{
				mNoRunable();
			}
			hide();
		}

		public static Dialog get()
		{
			return sInstance;
		}

		public void show(string message, Runable yesRunable = null, Runable noRunable = null, string yesButtonText = "确认", string noButtonText = "取消")
        {
			ThreadLooper.get().autoRunMainThread(() => {
				message_text.text = message;
				mYesButtonText.text = yesButtonText;
				mNoButtonText.text = noButtonText;
				mYesRunable = yesRunable;
				mNoRunable = noRunable;
				show();
			});
			
		}
	}
}
