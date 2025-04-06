using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{

    public delegate void OnTaskDetalCallback(List<GameTask> gameTask);

	public class TaskModel 
	{
        private SocketPackageNet mSocket;

        static TaskModel()
        {
            ServerPackageManager.get().registerServerPackage(new SimpleServerResponsePackage("TaskDetalResponse"));
        }

        public TaskModel()
        {
            mSocket = NetworkFactory.getSocketNet();
        }

        public void requestTaskDetal(OnTaskDetalCallback callback)
        {
            SimpleClientRequestPackage package = new SimpleClientRequestPackage("TaskDetalRequest");           
            mSocket.sendRequest(new SocketRequest(package, (response) => {
                if (response.isSuccess() && response.getCode() == 1)
                {
                    List<GameTask> gameTask = JsonUtils.unserialize<List<GameTask>>(response.getMessage());
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
