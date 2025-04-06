using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public delegate void OnGoodsCallback(List<GameGoods> gameGoods);

	public class GoodsModel 
	{
		private SocketPackageNet mSocket;

		static GoodsModel()
		{
			ServerPackageManager.get().registerServerPackage(new SimpleServerResponsePackage("GoodsResponse"));
		}

		public GoodsModel()
        {
			mSocket = NetworkFactory.getSocketNet();
		}

        public void requestGoods(OnGoodsCallback callback)
        {
            SimpleClientRequestPackage package = new SimpleClientRequestPackage("GoodsRequest");
            mSocket.sendRequest(new SocketRequest(package, (response) => {
                if (response.isSuccess() && response.getCode() == 1)
                {
                    List<GameGoods> gameTask = JsonUtils.unserialize<List<GameGoods>>(response.getMessage());
                    callback(gameTask);
                }
                else
                {
                    Dialog.get().show("请求服务失败");
                }
            }));
        }
    }
}
