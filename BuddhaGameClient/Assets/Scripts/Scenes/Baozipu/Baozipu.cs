using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class Baozipu : StandardWorldScene
	{
		private SceneInfo mSceneInfo;

		public override WorldSceneInfo getSceneInfo()
		{
			if (mSceneInfo == null)
			{
				mSceneInfo = new SceneInfo();
			}
			return mSceneInfo;
		}

		public class SceneInfo : WorldSceneInfo
		{
			public override string getSceneName()
			{
				return "Baozipu";
			}

			public override string getSceneLocalName()
			{
				return "包子铺";
			}
		}
	}
}
