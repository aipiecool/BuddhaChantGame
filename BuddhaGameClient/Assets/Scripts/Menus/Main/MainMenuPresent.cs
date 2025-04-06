using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BuddhaGame
{
	public class MainMenuPresent
	{
		private MainMenuView mView;
		private MainMenuModel mModel;
		private bool mIsLogin;
		private bool mIsCreated;
		private CharacterInfoEntityWithScene mCharacterInfo;

		public MainMenuPresent(MainMenuView view)
        {
			mView = view;
			mModel = new MainMenuModel();
			refresh();
		}		

		private void refresh()
        {
			mIsLogin = UserRecorder.get().isLogin();
			if (mIsLogin)
			{
				ThreadLooper.get().runMainThread(() =>
				{
					mView.setUsername("");
					mView.setLoginButtonActive(false);
				});
				Loading.get().show();
				mModel.requestLogin((entity) => {
					ThreadLooper.get().runMainThread(() =>
					{
						Loading.get().hide();
						mCharacterInfo = entity;
						mView.setCharacterInfo(entity);
						mIsCreated = entity.isCreated;
						mView.setLoginButtonActive(true);
						mView.setLoginButtonText(mIsCreated ? "进入小镇" : "创建角色");
						mView.setUsername(UserRecorder.get().getUsername());
					});
				},
				(message) => {
					ThreadLooper.get().runMainThread(() =>
					{
						Loading.get().hide();
						Dialog.get().show(message);
					});
				});
			}
			else
			{
				ThreadLooper.get().runMainThread(() =>
				{
					mView.setUsername("");
					mView.setLoginButtonText("登录");
				});
			}
		}

		public void onLoginButtonClick()
        {
			if (mIsLogin)
			{
                if (mIsCreated)
                {
					SceneSwitchParameters.sTargetEnter = mCharacterInfo.enterId;
					WorldSceneInfo senceInfo = WorldScenesInfoManager.get().getSceneInfoById(mCharacterInfo.sceneId);
					if (senceInfo != null)
					{
						SceneManager.LoadScene(senceInfo.getSceneName());
					}
					else
					{
						SceneManager.LoadScene("Nianfotang");
					}
				}
                else
                {
					SceneManager.LoadScene("CharacterCreator");
				}		
			}
			else
			{
				SceneManager.LoadScene("Login");
			}
		}

		public bool isLogin()
        {
			return mIsLogin;

		}

		public void loginByUserId(string userId, string username, string password)
        {
			Loading.get().show();
			mView.setLoginButtonActive(false);
			mModel.requestLoginByUserId(userId, username, password, ()=>{
				refresh();
			}, ()=>
			{
				Loading.get().hide();
				mView.setLoginButtonActive(true);
			});

		}
    }
}
