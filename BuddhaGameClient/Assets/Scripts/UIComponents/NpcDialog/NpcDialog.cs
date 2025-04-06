using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BuddhaGame
{
	public class NpcDialog : UIHideable
	{
		private static NpcDialog sInstance;

		public Button nextButton;
		public Text npc_name_text;
		public SpeakText content_text; 
		public Text continue_text;

		private int mPage;
		private bool mSpeakFinished;
		private NpcSpeakInfo mSpeakInfo;

		private void Start()
        {
			sInstance = this;
			nextButton.onClick.AddListener(onNextButtonClick);
			content_text.setSpeakFinishCallback(onSpeakFinished);
		}

		public static NpcDialog get()
        {
			return sInstance;
		}

		public void show(Npc npc)
        {
			ThreadLooper.get().autoRunMainThread(() =>
			{
				npc_name_text.text = npc.getLocalName() + "：";
				content_text.clear();
				show();
			});
		}

		public void show(NpcSpeakInfo speakInfo)
        {
			ThreadLooper.get().autoRunMainThread(() =>
			{
				mPage = 0;
				mSpeakFinished = false;
				mSpeakInfo = speakInfo;
				npc_name_text.text = speakInfo.npcLocalName + "：";
				content_text.setText(speakInfo.speakTexts[0]);
				show();
			});
        }

        public override void hide()
        {
            base.hide();
			MessagesManager.get().requestFlowText();
        }


        private void onNextButtonClick()
        {
            if (mSpeakFinished)
            {
				mSpeakFinished = false;
				mPage++;
				if (mPage < mSpeakInfo.speakTexts.Length)
                {
					content_text.setText(mSpeakInfo.speakTexts[mPage]);
				}
                else
                {
					hide();
                }				
			}
            else
            {
				content_text.showAll();
			}
        }

		private void onSpeakFinished()
        {
			mSpeakFinished = true;

		}
	}

	public class NpcSpeakInfo
	{
		private static char[] sSplitChars = new char[] { '^' };

		public string npcLocalName { get; }
		public string[] speakTexts { get; }

		public NpcSpeakInfo(Npc npc, string speakText)
        {
			npcLocalName = npc.getLocalName();
			speakTexts = speakText.Split(sSplitChars);
		}
	}
}
