using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class BuddhaNames
	{
		public static string[] sNames = new string[] { "南无阿弥陀佛", "南无观世音菩萨", "南无地藏王菩萨", "南无药师琉璃光如来" };

        public static string getNameById(int wordId)
        {
            if(wordId < sNames.Length)
            {
                return sNames[wordId];
            }
            return sNames[0];
        }
    }
}
