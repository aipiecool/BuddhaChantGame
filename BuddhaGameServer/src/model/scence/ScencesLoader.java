package model.scence;

import utils.FileUtils;
import utils.JsonUtils;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

public class ScencesLoader {
    public List<GameScene> load(){
        List<GameScene> scenes = new ArrayList<>();
        loadNpcs(scenes, "assets/scenes/");
        return scenes;
    }

    private void loadNpcs(List<GameScene> scenes, String path){
        File[] files = FileUtils.listOfFolder(path);
        for(File file : files){
            if(file.isFile()){
                SceneInfo sceneInfo = JsonUtils.unserialize(FileUtils.readTxt(file.getAbsolutePath()), SceneInfo.class);
                scenes.add(new GameScene(sceneInfo));
            }else if(file.isDirectory()){
                loadNpcs(scenes, file.getAbsolutePath());
            }
        }
    }
}
