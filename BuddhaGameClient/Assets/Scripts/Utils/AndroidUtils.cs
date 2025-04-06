using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class AndroidUtils 
	{
		public static void setOpenRecord(bool open)
        {         
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityApi");
            jc.CallStatic("setOpenRecord", open);
        }

        public static void backToMenu()
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityApi");
            jc.CallStatic("backToMenu");
        }

        public static void quitButtonClick()
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityApi");
            jc.CallStatic("quitButtonClick");
        }
    }
}
