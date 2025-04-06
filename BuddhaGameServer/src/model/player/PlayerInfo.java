package model.player;

import model.good.entity.running.RunningGoodsInfo;
import model.task.entity.running.RunningTaskInfo;

import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;

public class PlayerInfo {

    public int userId;
    public String username;
    public String password;
    public int sceneId;
    public int enterId;
    public float positionX;
    public float positionY;
    public int direction;
    public CharacterInfo characterInfo;
    public long lastSaveDatatime;
    public HashSet<String> completeTasksId;
    public HashSet<String> completeChildTasksId;
    public List<RunningTaskInfo> runningTaskInfos;
    public HashSet<RunningGoodsInfo> goods;
    public int exp;

    public PlayerInfo() {
    }

    public PlayerInfo(int userId, String username, String password) {
        this.userId = userId;
        this.username = username;
        this.password = password;
        sceneId = 0;
        enterId = 0;
        characterInfo = new CharacterInfo();
        positionX = 0;
        positionY = 0;
        completeTasksId = new HashSet<>();
        completeChildTasksId = new HashSet<>();
        runningTaskInfos = new ArrayList<>();
        goods = new HashSet<>();
        exp = 0;
    }

    public static class CharacterInfo
    {
        public boolean isCreated;
        public int gender;
        public float colorR;
        public float colorG;
        public float colorB;

        public CharacterInfo() {
            isCreated = false;
        }
    }


}
