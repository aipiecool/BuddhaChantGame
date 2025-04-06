using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BuddhaGame
{
	public class MainMenuView : MonoBehaviour
	{
		public Button login_button;
		public Button tutorial_button;
		public Button quit_button;
		public Text username_text;
		public Text version_text;
		public SpriteRenderer mainPlayer;
		private MainMenuPresent mPresent;
		

		private void Start()
        {
			Screen.sleepTimeout = SleepTimeout.NeverSleep;

			login_button.onClick.AddListener(onLoginButtonClick);
			tutorial_button.onClick.AddListener(onTutorialButtonClick);
			quit_button.onClick.AddListener(onQuitButtonClick);

			mPresent = new MainMenuPresent(this);
			mainPlayer.gameObject.SetActive(false);

			version_text.text = "版本:v" + GameGlobal.GAME_VERSION_NAME;

			AndroidUtils.backToMenu();
			//registerUser("{ \"nickname\":\"寻元念佛\", \"userCode\":\"7706d2dc2da6837340effd985dc620b6\", \"userId\":\"31\" }");		
		}

		public void setCharacterInfo(CharacterInfoEntityWithScene entity)
        {
            if (entity.isCreated)
            {
				CharacterInfo characterInfo = new CharacterInfo(entity);
				characterInfo.render(mainPlayer);
				mainPlayer.gameObject.SetActive(true);
				UserRecorder.get().setCharacterInfo(characterInfo);
			}            
        }

		public void setUsername(String username)
        {
			username_text.text = username;
		}

        public void setLoginButtonText(string text)
        {
			login_button.GetComponentInChildren<Text>().text = text;
		}

		public void setLoginButtonActive(bool active)
        {
			login_button.gameObject.SetActive(active);

		}

		private void onLoginButtonClick()
        {
			Loading.get().show();
			mPresent.onLoginButtonClick();
		}

		private void onTutorialButtonClick()
		{
			Dialog.get().show("进入小镇后，对着手机念颂佛号即可自动识别\n\n更详细的教程还在制作中\n如有疑问可以加入qq群讨论");
		}

		private void onQuitButtonClick()
		{
			AndroidUtils.quitButtonClick();
			//Application.Quit();
		}

		public void registerAndroidDrive(String paras)
		{
			Log.input().debug("Receive registerAndroidDrive");
			GameGlobal.IS_MOBILE = true;
		}

		public void registerUser(String userJson)
		{
			UserEntityJson entity = JsonUtils.unserialize<UserEntityJson>(userJson);
			String userId = entity.userId;
			String username = entity.nickname;
			String password = entity.userCode;
			Log.input().debug("Receive registerUser, userId:" + userId + ",username:" + username + ", password:" + password);
			mPresent.loginByUserId(userId, username, password);
		}

		public class UserEntityJson
		{
			public String userId;
			public String userCode;
			public String nickname;

          
        }
	}
}
