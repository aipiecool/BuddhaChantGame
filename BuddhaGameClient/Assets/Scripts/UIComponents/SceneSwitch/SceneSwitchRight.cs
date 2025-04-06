using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class SceneSwitchRight : SceneSwitchDoor
	{
        protected override void setPosition(float i)
        {
            mRectTransform = GetComponent<RectTransform>();
            mRectTransform.offsetMin = new Vector2(540 + i * 540, 0);
            mRectTransform.offsetMax = new Vector2(540 + i * 540, 0);
        }
    }
}
