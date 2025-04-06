package model.npc;

import utils.FileUtils;
import utils.JsonUtils;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

public class NpcLoader {


    public NpcLoader() {
    }

    public List<Npc> load(){
        List<Npc> npcs = new ArrayList<>();
        loadNpcs(npcs, "assets/npcs/");
        return npcs;
    }

    private void loadNpcs(List<Npc> npcs, String path){
        File[] files = FileUtils.listOfFolder(path);
        for(File file : files){
            if(file.isFile()){
                NpcInfo npcInfo = JsonUtils.unserialize(FileUtils.readTxt(file.getAbsolutePath()), NpcInfo.class);
                npcs.add(new Npc(npcInfo));
            }else if(file.isDirectory()){
                loadNpcs(npcs, file.getAbsolutePath());
            }
        }
    }

}
