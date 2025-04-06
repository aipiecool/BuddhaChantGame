using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public abstract class WorldScene : MonoBehaviour
	{
        private static WorldScene sCurrentScene;
        private static int sCurrentSceneId;
        private static string sCurrentSceneName;
        private static string sCurrentSceneLocalName;

        public List<Enter> enters;

        private List<UnityScriptRunable> mRunables = new List<UnityScriptRunable>();    

        private void Awake()
        {
            sCurrentScene = this;
            sCurrentSceneId = getSceneInfo().getSceneId();
            sCurrentSceneName = getSceneInfo().getSceneName();
            sCurrentSceneLocalName = getSceneInfo().getSceneLocalName();
        }

        protected void registerRunable(UnityScriptRunable runable)
		{
			mRunables.Add(runable);
		}

        private void Update()
        {
            foreach(UnityScriptRunable runable in mRunables)
            {
                runable.Update();
            }
        }        

        public abstract WorldSceneInfo getSceneInfo();

        public static WorldScene getCurrentWordScene()
        {
            return sCurrentScene;
        }

        public static int getCurrentSceneId()
        {
            return sCurrentSceneId;
        }

        public static string getCurrentSceneName()
        {
            return sCurrentSceneName;
        }
        public static string getCurrentSceneLocalName()
        {
            return sCurrentSceneLocalName;
        }
    }
}
