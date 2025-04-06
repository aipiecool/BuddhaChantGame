package model.chantable;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class ChantablesManager {

    private static ChantablesManager sInstance;

    private final List<Chantable> mChantables;
    private final Map<String, Chantable> mName2Chantable = new HashMap<>();
    private final Map<Integer, Chantable> mId2Chantable = new HashMap<>();

    private ChantablesManager() {
        mChantables = new ChantableLoader().load();
        for(Chantable chantable : mChantables){
            mName2Chantable.put(chantable.getInfo().name, chantable);
            mId2Chantable.put(chantable.getInfo().id, chantable);
        }
    }

    public Chantable getChantableByName(String name){
        return mName2Chantable.get(name);
    }

    public Chantable getChantableById(int id){
        return mId2Chantable.get(id);
    }

    public static ChantablesManager get(){
        if(sInstance == null){
            synchronized (ChantablesManager.class){
                if(sInstance == null){
                    sInstance = new ChantablesManager();
                }
            }
        }
        return sInstance;
    }
}
