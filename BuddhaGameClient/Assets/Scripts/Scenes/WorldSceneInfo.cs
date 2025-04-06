using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public abstract class WorldSceneInfo 
	{	
        public abstract string getSceneName();
        public abstract string getSceneLocalName();

        public int getSceneId()
        {
            return EncodeUtils.string2HashCode(getSceneName());
        }

        public override int GetHashCode()
        {
            return getSceneId();
        }

        public override bool Equals(object obj)
        {
            WorldSceneInfo other = obj as WorldSceneInfo;
            return other.getSceneName().Equals(getSceneName());
        }
    }
}
