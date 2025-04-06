using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
    public delegate void OnLevelCallback(LevelResponseInfo flowTexts);
    public class PortraitModel
    {
        private SocketPackageNet mSocket;

        static PortraitModel()
        {
            ServerPackageManager.get().registerServerPackage(new SimpleServerResponsePackage("LevelResponse"));
        }

        public PortraitModel()
        {
            mSocket = NetworkFactory.getSocketNet();
        }

        public void requestLevel(OnLevelCallback callback)
        {
            SimpleClientRequestPackage package = new SimpleClientRequestPackage("LevelRequest");
            mSocket.sendRequest(new SocketRequest(package, (response) => {
                if (response.isSuccess() && response.getCode() == 1)
                {
                    LevelResponseInfo levelInfo = JsonUtils.unserialize<LevelResponseInfo>(response.getMessage());
                    callback(levelInfo);
                }
                else
                {
                    Dialog.get().show("请求服务失败");
                }
            }));
        }
    }

    public class LevelResponseInfo
    {
        public int level;
        public int exp;
        public int maxExp;
    }
}
