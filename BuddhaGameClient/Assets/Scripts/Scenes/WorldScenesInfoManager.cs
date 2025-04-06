using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class WorldScenesInfoManager 
	{
		private static WorldScenesInfoManager sInstance;
		private static object sSingletonLock = new object();
        List<WorldSceneInfo> mWorldScenes = new List<WorldSceneInfo>();
        Hashtable mSceneId2WorldScenes = new Hashtable();
        Hashtable mSceneName2WorldScenes = new Hashtable();

        private WorldScenesInfoManager()
        {
            registerSceneInfo(new NianfotangScene.SceneInfo());
            registerSceneInfo(new PrajnaTownScene.SceneInfo());
        }

        private void registerSceneInfo(WorldSceneInfo info)
        {
            mWorldScenes.Add(info);
            mSceneId2WorldScenes.Add(info.getSceneId(), info);
            mSceneName2WorldScenes.Add(info.getSceneName(), info);
        }

        public WorldSceneInfo getSceneInfoById(int id)
        {
            return mSceneId2WorldScenes[id] as WorldSceneInfo;
        }

        public WorldSceneInfo getSceneInfoById(string name)
        {
            return mSceneName2WorldScenes[name] as WorldSceneInfo;
        }

        public static WorldScenesInfoManager get()
        {
            if (sInstance == null)
            {
                lock (sSingletonLock)
                {
                    if (sInstance == null)
                    {
                        sInstance = new WorldScenesInfoManager();
                    }
                }
            }
            return sInstance;
        }
    }
}
