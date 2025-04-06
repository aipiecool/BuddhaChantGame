using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class Yaocaipu : StandardWorldScene
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
				return "Yaocaipu";
			}

			public override string getSceneLocalName()
			{
				return "药材铺";
			}
		}
	}
}
