package model.chantable;

import utils.FileUtils;
import utils.JsonUtils;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

public class ChantableLoader {

    public List<Chantable> load(){
        List<Chantable> chantables = new ArrayList<>();
        loadChantables(chantables, "assets/chantables/");
        return chantables;
    }

    private void loadChantables(List<Chantable> chantables, String path){
        File[] files = FileUtils.listOfFolder(path);
        for(File file : files){
            if(file.isFile()){
                ChantableInfo chantableInfo = JsonUtils.unserialize(FileUtils.readTxt(file.getAbsolutePath()), ChantableInfo.class);
                chantables.add(new Chantable(chantableInfo));
            }else if(file.isDirectory()){
                loadChantables(chantables, file.getAbsolutePath());
            }
        }
    }

}
