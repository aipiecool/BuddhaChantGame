package model.scence;

import java.util.HashMap;
import java.util.List;

public class ScencesManager {

    private static ScencesManager sInstance;

    private List<GameScene> mGameScenes;
    private HashMap<String, GameScene> mName2GameScene = new HashMap<>();

    private ScencesManager() {
        mGameScenes = new ScencesLoader().load();
        for(GameScene gameScene : mGameScenes){
            mName2GameScene.put(gameScene.getSceneInfo().name, gameScene);
        }
    }

    public static ScencesManager get(){
        if(sInstance == null){
            synchronized (ScencesManager.class){
                if(sInstance == null){
                    sInstance = new ScencesManager();
                }
            }
        }
        return sInstance;
    }

    public GameScene getScenceIdByName(String name){
        return mName2GameScene.get(name);
    }
}
