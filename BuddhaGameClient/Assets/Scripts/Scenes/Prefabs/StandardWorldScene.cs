using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public abstract class StandardWorldScene : WorldScene
	{
		private void Start()
		{
			new OtherPlayersManager();
			new NpcsManager();			
			onStart();
		}

		protected virtual void onStart()
        {

        }
	}
}
