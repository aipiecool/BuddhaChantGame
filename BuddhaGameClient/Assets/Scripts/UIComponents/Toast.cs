using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BuddhaGame
{
	public class Toast : UIHideable
	{
		private static Toast sInstance;
		private Text toast_text;		

		private void OnEnable()
        {
			sInstance = this;		
			toast_text = GetComponentInChildren<Text>();			
		}

		public void show(string message, int second)
        {
			ThreadLooper.get().autoRunMainThread(()=>{
				show();
				toast_text.text = message;
				Delayer.get().delayer(() => {
					hide();
				}, second);
			});			
		}	

		public static Toast get()
        {
			return sInstance;
		}
	}
}
