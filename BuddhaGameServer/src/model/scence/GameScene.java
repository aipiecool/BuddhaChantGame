package model.scence;

import utils.EncodeUtils;

public class GameScene {

    private SceneInfo mSceneInfo;
    private int mSceneId;

    public GameScene(SceneInfo sceneInfo) {
        mSceneInfo = sceneInfo;
        mSceneId = EncodeUtils.string2HashCode(mSceneInfo.name);
    }

    public SceneInfo getSceneInfo() {
        return mSceneInfo;
    }

    public int getSceneId() {
        return mSceneId;
    }
}
