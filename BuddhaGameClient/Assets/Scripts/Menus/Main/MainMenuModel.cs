using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public delegate void CharacterInfoSuccess(CharacterInfoEntityWithScene entity);
	public delegate void CharacterInfoError(string message);
	public delegate void LoginByUserIdSuccess();
	public delegate void LoginByUserIdError();

	public class MainMenuModel 
	{
		private MainMenuPresent mPresent;
		private SocketPackageNet mSocket;
		private HttpNet mHttpNet;

		static MainMenuModel()
        {
			ServerPackageManager.get().registerServerPackage(new SimpleServerResponsePackage("LoginResponse"));
			ServerPackageManager.get().registerServerPackage(new SimpleServerResponsePackage("CharacterInfoResponse"));
		}

		public MainMenuModel()
        {
			mSocket = NetworkFactory.getSocketNet();
			mHttpNet = NetworkFactory.getHttpNet();
		}

		public void requestLogin(CharacterInfoSuccess success, CharacterInfoError error)
		{
			SimpleClientJsonRequestPackage package = new SimpleClientJsonRequestPackage("LoginRequest");
			package.addKeyValue("userId", UserRecorder.get().getUserId());
			package.addKeyValue("username", UserRecorder.get().getUsername());
			package.addKeyValue("password", UserRecorder.get().getPassword());
			package.addKeyValue("version", GameGlobal.GAME_VERSION);
			mSocket.sendRequest(new SocketRequest(package, (response) => {
						if (response.isSuccess() && response.getCode() == 1)
						{
							requestCharacterInfo(success, error);
                        }
                        else
                        {
							error(response.getMessage());
						}
					}));
		}

		private void requestCharacterInfo(CharacterInfoSuccess success, CharacterInfoError error)
        {
			mSocket.sendRequest(new SocketRequest(new SimpleClientRequestPackage("CharacterInfoRequest"), (response) => {
				if (response.isSuccess() && response.getCode() == 1)
				{
					string jsonString = response.getMessage();
                    try
                    {
						CharacterInfoEntityWithScene entity = JsonUtils.unserialize<CharacterInfoEntityWithScene>(jsonString);
						success(entity);
					}
                    catch (Exception)
                    {
						error("请求角色信息错误:\n" + jsonString);
                    }
				}
				else
				{
					error("请求服务失败");
				}
			}));
		}

		public void requestLoginByUserId(String userId, String username, String password, LoginByUserIdSuccess success, LoginByUserIdError error)
        {
			HttpRouteRequest request = new HttpRouteRequest("game/Login/", "loginByUserId.html");
			request.addFormData("userId", userId);
			request.addFormData("username", username);
			request.addFormData("password", password);
			Loading.get().show();
			mHttpNet.post(request, (response) =>
			{
				Loading.get().hide();
				HttpJsonResponse<LoginEntity> jsonResponse = new HttpJsonResponse<LoginEntity>(response);
				if (jsonResponse.code == 1)
				{
					//Toast.get().show("登录成功", 5);					
					UserRecorder.get().login(jsonResponse.data.ID, jsonResponse.data.username, jsonResponse.data.password);
					success();
				}
				else
				{
					Toast.get().show(jsonResponse.message, 5);
					error();
				}
			});
		}
	}


}
