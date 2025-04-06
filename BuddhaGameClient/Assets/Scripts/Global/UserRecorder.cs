using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class UserRecorder 
	{
		private static UserRecorder sInstace;
		private static object sSingletonLock = new object();

        private string userId;
        private string username;
        private string password;
        private CharacterInfo characterInfo;
        private UserRecorder()
        {

        }

        public void login(string userId, string username, string password)
        {
            this.userId = userId;
            this.username = username;
            this.password = password;
        }

        public bool isLogin()
        {
            return !string.IsNullOrEmpty(userId);
        }

        public string getUserId()
        {
            return userId;
        }

        public string getUsername()
        {
            return username;
        }

        public void setCharacterInfo(CharacterInfo characterInfo)
        {
            this.characterInfo = characterInfo;
        }

        public CharacterInfo GetCharacterInfo()
        {
            return characterInfo;
        }

        public string getPassword()
        {
            return password;
        }

        public static UserRecorder get()
        {
            if (sInstace == null)
            {
                lock (sSingletonLock)
                {
                    if (sInstace == null)
                    {
                        sInstace = new UserRecorder();
                    }
                }
            }
            return sInstace;
        }
    }
}
