using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class NpcsManager 
	{
		private static Hashtable sNpcPrefabsMap = new Hashtable();

		private NpcModel mModel = new NpcModel();
		private Transform mNpcParentTransform;

		public NpcsManager()
        {
			mNpcParentTransform = GameObject.Find("/Npc").transform;
			mModel.requestScenceNpc(onScenceNpcResponse);
		}

		void onScenceNpcResponse(List<SceneNpcInfoResponse> responseNpcInfos)
        {
			foreach(SceneNpcInfoResponse sceneNpcInfo in responseNpcInfos)
            {
				GameObject npcPrefbas = sNpcPrefabsMap[sceneNpcInfo.name] as GameObject;
				if(npcPrefbas == null)
                {
					npcPrefbas = Resources.Load("Character/Npc/Prefabs/" + sceneNpcInfo.name) as GameObject;
					sNpcPrefabsMap.Add(sceneNpcInfo.name, npcPrefbas);
				}
				GameObject npcGameObject = GameObject.Instantiate(npcPrefbas);
				npcGameObject.transform.SetParent(mNpcParentTransform);
				Npc npc = npcGameObject.GetComponent<Npc>();
				npc.initializeByResponseSceneNpcInfo(sceneNpcInfo);
			}
        }
	}
}
