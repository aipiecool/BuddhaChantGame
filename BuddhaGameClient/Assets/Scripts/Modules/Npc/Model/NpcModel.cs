using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
    public delegate void ScenceNpcResponseCallback(List<SceneNpcInfoResponse> responseNpcInfos);
    public delegate void NpcSpeakResponseCallback(NpcSpeakResponse npcSpeakResponse);


	public class NpcModel
	{
        private SocketPackageNet mSocket;

        static NpcModel()
        {
			ServerPackageManager.get().registerServerPackage(new SimpleServerResponsePackage("SceneNpcResponse"));
			ServerPackageManager.get().registerServerPackage(new SimpleServerResponsePackage("NpcSpeakResponse"));
        }

		public NpcModel()
        {
            mSocket = NetworkFactory.getSocketNet();
        }

        public void requestScenceNpc(ScenceNpcResponseCallback callback)
        {
            SimpleClientRequestPackage package = new SimpleClientRequestPackage("SceneNpcRequest", WorldScene.getCurrentSceneId());
            mSocket.sendRequest(new SocketRequest(package, (response) => {
                if (response.isSuccess() && response.getCode() == 1)
                {
                    List<SceneNpcInfoResponse> responseNpcInfos = JsonUtils.unserialize<List<SceneNpcInfoResponse>>(response.getMessage());
                    ThreadLooper.get().runMainThread(()=>
                    {
                        callback(responseNpcInfos);
                    });                    
                }
                else
                {
                    Dialog.get().show("请求服务失败");
                }
            }));
        }

        public void requestNpcSpeak(Npc npc, NpcSpeakResponseCallback callback)
        {            
            SimpleClientJsonRequestPackage package = new SimpleClientJsonRequestPackage("NpcSpeakRequest");
            package.addKeyValue("npcName", npc.getName());
            mSocket.sendRequest(new SocketRequest(package, (response) => {
                if (response.isSuccess() && response.getCode() == 1)
                {
                    NpcSpeakResponse npcSpeakResponse = JsonUtils.unserialize<NpcSpeakResponse>(response.getMessage());
                    ThreadLooper.get().runMainThread(() =>
                    {
                        callback(npcSpeakResponse);
                    });
                }
                else
                {
                    Dialog.get().show("请求服务失败");
                }
            }));
        }
    }

    public class SceneNpcInfoResponse
    {
        public string name;
        public string localName;
        public string firstSpeak;
        public string secondSpeak;
        public float positionX;
        public float positionY;
    }

    public class NpcSpeakResponse
    {
        public string firstSpeak;
        public string secondSpeak;
    }
}
