using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
    public delegate void OnFlowTextCallback(List<FlowTextResponseInfo> flowTexts);
	public class MessageModel 
	{
        private SocketPackageNet mSocket;

        static MessageModel()
        {
            ServerPackageManager.get().registerServerPackage(new SimpleServerResponsePackage("FlowTextResponse"));            
        }

        public MessageModel()
        {
            mSocket = NetworkFactory.getSocketNet();
        }

        public void requestFlowText(OnFlowTextCallback callback)
        {
            SimpleClientRequestPackage package = new SimpleClientRequestPackage("FlowTextRequest");
            mSocket.sendRequest(new SocketRequest(package, (response) => {
                if (response.isSuccess() && response.getCode() == 1)
                {
                    List<FlowTextResponseInfo> gameTask = JsonUtils.unserialize<List<FlowTextResponseInfo>>(response.getMessage());
                    callback(gameTask);
                }
                else
                {
                    Dialog.get().show("请求服务失败");
                }
            }));
        }
    }

    public class FlowTextResponseInfo
    {
        public string message;
        public string sound;
    }
}
