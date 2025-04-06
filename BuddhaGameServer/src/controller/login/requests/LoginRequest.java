package controller.login.requests;

import event.game.GameEventsMamager;
import event.game.events.impl.PlayerLoginGameEvent;
import global.GlobalConfig;
import log.Log;
import network.NetworkFactory;
import network.socket.actions.ClientPackage;
import network.socket.actions.ClientRequestPackage;
import network.socket.actions.prefabs.Server.SimpleServerResponsePackage;
import network.socket.actions.unpackage.ClientRequestJsonUnpackage;
import network.socket.actions.unpackage.ClientUnpackage;
import model.player.Player;
import model.player.PlayerInfo;
import model.player.PlayersManager;
import model.player.persistence.PlayerPersistence;

public class LoginRequest extends ClientRequestPackage {



    public LoginRequest(ClientUnpackage unpackage) throws Exception {
        super(unpackage);
    }

    public LoginRequest() {

    }

    @Override
    public void process() throws Exception {
        ClientRequestJsonUnpackage jsonUnpackage = new ClientRequestJsonUnpackage(mRequestUnpackage);
        int userId = Integer.parseInt(jsonUnpackage.getValue("userId"));
        String username = jsonUnpackage.getValue("username");
        String password = jsonUnpackage.getValue("password");
        int version = -1;
        try {
            version = Integer.parseInt(jsonUnpackage.getValue("version"));
        }catch (Exception e){

        }
        if(version != GlobalConfig.CLIENT_VERSION){
            NetworkFactory.getSocketNet().sendPackage(new SimpleServerResponsePackage(
                    jsonUnpackage.getAddress() ,
                    jsonUnpackage.getPackageId(),
                    "LoginResponse",
                    -1,
                    "无法登录服务器，当前版本过低\n请更新到最新的版本("+GlobalConfig.CLIENT_VERSION_NAME+")"));
            Log.input().info(username + " 以低版本("+ version +")进行登录");
            return;
        }
        Player player = PlayersManager.get().getPlayer(userId);
        if(player == null){
            PlayerInfo playerInfo = PlayerPersistence.get().load(userId, username,password);
            player = new Player(playerInfo, mUnpackage.getAddress());
            PlayersManager.get().addPlayer(player);
        }else {
            if(player.getAddress().equals(mUnpackage.getAddress())) {
                player.setIntoworld(false);
                Log.input().info(username + " 退出到了主界面/重新加入了游戏");
            }else {
                NetworkFactory.getSocketNet().sendPackage(new SimpleServerResponsePackage(
                        jsonUnpackage.getAddress() ,
                        jsonUnpackage.getPackageId(),
                        "LoginResponse",
                        -2,
                        "该账号已在其他设备上登录"));
                Log.input().info(username + " 尝试在其他设备上重复登录");
                return;
            }
        }
        NetworkFactory.getSocketNet().sendPackage(new SimpleServerResponsePackage(
                jsonUnpackage.getAddress() ,
                jsonUnpackage.getPackageId(),
                "LoginResponse",
                1,
                "登录成功"));
        GameEventsMamager.get().sendEvent(new PlayerLoginGameEvent(player));
    }

    @Override
    public String getHeaderString() {
        return "LoginRequest";
    }

    @Override
    public ClientPackage create(ClientUnpackage unpackage) throws Exception {
        return new LoginRequest(unpackage);
    }
}
