using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BuddhaGame
{
	public class CloseButton : MonoBehaviour 
	{
        public UIHideable relationComponet;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(onClick);
        }

        private void onClick()
        {
            relationComponet.hide();
        }
    }
}
