package model.npc;

import model.scence.GameScene;
import model.scence.ScencesManager;

import java.util.HashMap;
import java.util.HashSet;
import java.util.List;

public class NpcsManager {

    private static NpcsManager sInstance;

    private List<Npc> mNpcs;
    private HashMap<String, Npc> mName2Npcs = new HashMap<>();
    private HashMap<Integer, HashSet<Npc>> mSceneId2Npcs = new HashMap<>();

    public NpcsManager() {
        mNpcs = new NpcLoader().load();
        for(Npc npc : mNpcs){
            GameScene scene = ScencesManager.get().getScenceIdByName(npc.getNpcInfo().scene);
            HashSet<Npc> npcsSet = mSceneId2Npcs.computeIfAbsent(scene.getSceneId(), k -> new HashSet<>());
            npcsSet.add(npc);
            mName2Npcs.put(npc.getNpcInfo().name, npc);
        }
    }

    public static NpcsManager get(){
        if(sInstance == null){
            synchronized (NpcsManager.class){
                if(sInstance == null){
                    sInstance = new NpcsManager();
                }
            }
        }
        return sInstance;
    }

    public List<Npc> getNpcs() {
        return mNpcs;
    }

    public HashSet<Npc> getNpcsBySceneId(int senceId){
        return mSceneId2Npcs.get(senceId);
    }

    public Npc getNpcsByName(String npcName) {
        return mName2Npcs.get(npcName);
    }
}
