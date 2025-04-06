package model.player.persistence;

import global.GlobalConfig;
import log.Log;
import model.player.PlayerInfo;
import utils.JsonUtils;
import okhttp3.*;

import java.io.IOException;

public class PlayerDatabasePersistence extends  PlayerPersistence{

    OkHttpClient mClient;
    String mUrlBase = GlobalConfig.HTTP_SEVER_IP + "/login/";

    public PlayerDatabasePersistence() {
        mClient = new OkHttpClient();
    }

    @Override
    public void save(PlayerInfo info) throws IOException {
        String url = mUrlBase + "saveUserPlayerInfo";
        String json = JsonUtils.serialize(info);
        RequestBody requestBody = new FormBody.Builder()
                .add("userId", String.valueOf(info.userId))
                .add("password", info.password)
                .add("playerInfo", json)
                .build();
        Request request = new Request.Builder().url(url).post(requestBody).build();
        Response response = mClient.newCall(request).execute();
        if(response.code() == 200){
            String body = response.body().string();
            try {
                //SimpleJsonEntity entity = JsonUtils.unserialize(body, SimpleJsonEntity.class);
                Log.input().info("保存玩家" + info.username + "信息:" + body);
            }catch (Exception e){
                Log.input().warn("保存玩家" + info.username + "信息错误:" + body);
            }
        }

    }

    @Override
    public PlayerInfo load(int userId, String username, String password) throws IOException {
        String url = mUrlBase + "queryUserPlayerInfo";
        RequestBody requestBody = new FormBody.Builder()
                .add("userId", String.valueOf(userId))
                .add("password", password)
                .build();
        Request request = new Request.Builder().url(url).post(requestBody).build();
        Response response = mClient.newCall(request).execute();
        String json = response.body().string();
        if(!json.isEmpty()){
            try {
                PlayerInfo playerInfo = JsonUtils.unserialize(json, PlayerInfo.class);
                if(playerInfo.userId > 0 &&  playerInfo.password != null) {
                    return playerInfo;
                }
            }catch (Exception e){
                //SimpleJsonEntity entity = JsonUtils.unserialize(json, SimpleJsonEntity.class);
                Log.input().warn("读取玩家" + username + "信息错误:" + json);
            }
        }
        PlayerInfo playerInfo = new PlayerInfo(userId, username, password);
        return playerInfo;
    }

    private class SimpleJsonEntity{
        public int code;
        public String data;
    }
}
