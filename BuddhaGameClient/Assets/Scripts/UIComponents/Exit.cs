using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BuddhaGame
{
	public class Exit : MonoBehaviour
	{
		private Button mButton;
		private void Start()
        {
			mButton = GetComponent<Button>();
			mButton.onClick.AddListener(onExitButtonClick);
		}

		private void onExitButtonClick()
        {
			Dialog.get().show("确定返回到主菜单吗?", ()=> {
				Loading.get().show();
				SceneManager.LoadScene("MainMenu");
			});
        }
	}
}
