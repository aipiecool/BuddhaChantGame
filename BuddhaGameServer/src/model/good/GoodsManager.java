package model.good;

import model.good.entity.rom.Goods;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class GoodsManager {

    private static GoodsManager sInstance;

    private final List<Goods> mGoods;
    private final Map<String, Goods> mName2Good = new HashMap<>();

    private GoodsManager() {
        mGoods = new GoodsLoader().load();
        for(Goods goods : mGoods){
            mName2Good.put(goods.getInfo().name, goods);
        }
    }

    public Goods getGoodByName(String name){
        return mName2Good.get(name);
    }

    public static GoodsManager get(){
        if(sInstance == null){
            synchronized (GoodsManager.class){
                if(sInstance == null){
                    sInstance = new GoodsManager();
                }
            }
        }
        return sInstance;
    }
}
