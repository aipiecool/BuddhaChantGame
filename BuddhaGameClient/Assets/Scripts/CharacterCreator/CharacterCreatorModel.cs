using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class CharacterCreatorModel 
	{
        private SocketPackageNet mSocket;

        static CharacterCreatorModel()
        {
            ServerPackageManager.get().registerServerPackage(new SimpleServerResponsePackage("CreateCharacterResponse"));
        }

        public CharacterCreatorModel()
        {
            mSocket = NetworkFactory.getSocketNet();
        }

        public void sendCharacterInfo(int mGenderSelectorIndex, Color color, Runable callback)
        {
            SimpleClientJsonRequestPackage package = new SimpleClientJsonRequestPackage("CreateCharacterRequest");
            package.addKeyValue("gender", mGenderSelectorIndex);
            package.addKeyValue("colorR", color.r);
            package.addKeyValue("colorG", color.g);
            package.addKeyValue("colorB", color.b);           
            mSocket.sendRequest(new SocketRequest(package, (response) => {
                ThreadLooper.get().runMainThread(() =>
                {
                    if (response.isSuccess() && response.getCode() == 1)
                    {
                        callback();
                    }
                    else
                    {
                        Dialog.get().show("请求服务失败");
                        Loading.get().hide();
                    }
                });               
            }));
        }
    }
}
