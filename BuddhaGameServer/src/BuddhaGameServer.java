import commad.CommadsManager;
import controller.goods.GoodsController;
import controller.login.LoginController;
import controller.message.MessageController;
import controller.npc.NpcController;
import controller.realtime.RealtimeController;
import controller.task.TaskController;
import global.GlobalConfig;
import log.Log;
import model.task.TasksManager;

public class BuddhaGameServer {
    public BuddhaGameServer() {
        Log.input().info("开启服务\n客户端版本号:" + GlobalConfig.CLIENT_VERSION + "\n客户端版本名称:" + GlobalConfig.CLIENT_VERSION_NAME);
        Log.input().info("登录服务器地址:" + GlobalConfig.HTTP_SEVER_IP);

        new CommadsManager();
        TasksManager.get();
    }

    public void start(){
        startControllers();
    }

    private void startControllers(){
        new LoginController();
        new RealtimeController();
        new TaskController();
        new NpcController();
        new GoodsController();
        new MessageController();
    }


}
