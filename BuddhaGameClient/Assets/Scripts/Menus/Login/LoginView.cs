using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BuddhaGame
{
	public class LoginView : MonoBehaviour
	{
        public InputField username_input;
        public InputField password_input;
        public Button login_button;
        public Button register_button;
        public Button back_button;
        HttpNet mHttpNet;

        private void Start()
        {
            mHttpNet = NetworkFactory.getHttpNet();
            login_button.onClick.AddListener(onLoginButtonClick);
            register_button.onClick.AddListener(onRegisterButtonClick);
            back_button.onClick.AddListener(onBackButtonClick);
            loadUsernameAndPassword();
        }

        private void loadUsernameAndPassword()
        {
            username_input.text = PlayerPrefs.GetString("username", "");
            password_input.text = PlayerPrefs.GetString("password", "");            
        }
        
        private void onLoginButtonClick()
        {
            string username = username_input.text;
            string password = password_input.text;
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                Toast.get().show("用户名或密码不能为空", 5);
                return;
            }
            HttpRouteRequest request = new HttpRouteRequest("/login/", "loginByPassword");
            request.addFormData("username", username);
            request.addFormData("password", password);
            Loading.get().show();
            mHttpNet.post(request, (response) =>
            {
                Loading.get().hide();
                HttpJsonResponse<LoginEntity> jsonResponse = new HttpJsonResponse<LoginEntity>(response);
                if (jsonResponse.code == 1)
                {
                    Toast.get().show("登录成功", 5);
                    saveUsernameAndPassword(username, password);
                    UserRecorder.get().login(jsonResponse.data.ID, jsonResponse.data.username, jsonResponse.data.password);
                    onBackButtonClick();
                }
                else
                {
                    Toast.get().show(jsonResponse.message, 5);
                }
            });
        }

        private void saveUsernameAndPassword(string username, string password)
        {
            PlayerPrefs.SetString("username", username);
            PlayerPrefs.SetString("password", password);
        }

        private void onRegisterButtonClick()
        {
            string username = username_input.text;
            string password = password_input.text;
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                Toast.get().show("用户名或密码不能为空", 5);
                return;
            }
            HttpRouteRequest request = new HttpRouteRequest("/login/", "registerByPassword");
            request.addFormData("username", username);
            request.addFormData("password", password);
            Loading.get().show();
            mHttpNet.post(request, (response) =>
            {
                Loading.get().hide();
                HttpJsonResponse<LoginEntity> jsonResponse = new HttpJsonResponse<LoginEntity>(response);
                if(jsonResponse.code == 1)
                {
                    Toast.get().show("注册成功", 5);
                    saveUsernameAndPassword(username, password);
                    UserRecorder.get().login(jsonResponse.data.ID, jsonResponse.data.username, jsonResponse.data.password);
                    onBackButtonClick();
                }
                else
                {
                    Toast.get().show(jsonResponse.message, 5);
                }
            });
        }

        private void onBackButtonClick()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
