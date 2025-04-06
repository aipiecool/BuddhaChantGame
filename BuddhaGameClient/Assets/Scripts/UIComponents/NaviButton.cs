using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BuddhaGame
{
	public class NaviButton : MonoBehaviour
	{
		public UIHideable relationComponet;        

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(onButtonClick);
        }

        private void onButtonClick()
        {
            relationComponet.show();
        }
    }
}
