using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class RuntimeController
	{
		private float mTimer;
		private float mRunSpeed;
		public RuntimeController(float runSpeed)
		{
			mRunSpeed = runSpeed;
		}

		public void setRunSpped(float second)
        {
			mRunSpeed = second;

		}
		public void run(Runable runable)
		{
			mTimer -= Time.deltaTime;
			if (mTimer <= 0)
			{
				mTimer = mRunSpeed;
				runable();
			}
		}
	}
}
