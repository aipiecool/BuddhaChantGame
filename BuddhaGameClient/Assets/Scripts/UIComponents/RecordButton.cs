using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BuddhaGame
{
	public class RecordButton :MonoBehaviour
	{
		public Button record_button;
		public Image record_button_image;
		public RectTransform ringTransform;
		public Sprite activeSprite;
		public Sprite unactiveSprite;
		public float speed = 0.1f;

		private void Start()
        {
			record_button.onClick.AddListener(onButtonClick);
			updateButtonState();
			BuddhaChantInput.get().addCallback(chantCallback);
		}

		private void chantCallback(int wordId)
        {
			if (speed < 1f)
            {
				speed += 0.1f;
			}				
		}

		private void onButtonClick()
        {
			BuddhaChantInput.get().setOpenRecord(!BuddhaChantInput.get().getOpenRecord());
			updateButtonState();
		}

		private void updateButtonState()
        {
			bool open = BuddhaChantInput.get().getOpenRecord();
			record_button_image.sprite = open ? activeSprite : unactiveSprite;
			ringTransform.localScale = open ? Vector3.one : Vector3.zero;
		}

        private void FixedUpdate()
        {
			ringTransform.Rotate(new Vector3(0, 0, speed));
			if(speed > 0.1f)
            {
				speed -= 0.001f;
			}			
		}
    }
}
