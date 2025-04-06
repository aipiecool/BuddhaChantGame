package model.good.entity.running;

import log.Log;
import model.good.entity.rom.Goods;

import java.util.HashMap;
import java.util.HashSet;

public class RunningGoods {

    private final HashSet<RunningGoodsInfo> mRunningGoodsInfo;
    private final HashMap<Goods, RunningGoodsInfo> mGoods2InfoMap = new HashMap<>();
    private final Object mLock = new Object();

    public RunningGoods(HashSet<RunningGoodsInfo> goods) {
        mRunningGoodsInfo = goods;
        for(RunningGoodsInfo info : goods){
            mGoods2InfoMap.put(info.goods, info);
        }
    }

    public void addGoods(Goods goods, int count) {
        synchronized (mLock){
            RunningGoodsInfo runningGoodsInfo = mGoods2InfoMap.get(goods);
            if(runningGoodsInfo != null){
                runningGoodsInfo.count += count;
            }else {
                runningGoodsInfo = new RunningGoodsInfo(goods, count);
                mRunningGoodsInfo.add(runningGoodsInfo);
                mGoods2InfoMap.put(goods, runningGoodsInfo);
            }
        }
    }

    public void decGoods(Goods goods, int count) {
        synchronized (mLock){
            RunningGoodsInfo runningGoodsInfo = mGoods2InfoMap.get(goods);
            if(runningGoodsInfo != null){
                runningGoodsInfo.count -= count;
                if(runningGoodsInfo.count <= 0){
                    mRunningGoodsInfo.remove(runningGoodsInfo);
                    mGoods2InfoMap.remove(goods);
                }
            }else {
                Log.input().warn("try decGoods a Null Object");
            }
        }
    }
}
