package model.good.entity.running;

import model.good.entity.rom.Goods;

public class RunningGoodsInfo {
    public Goods goods;
    public int count;

    public RunningGoodsInfo() {
    }

    public RunningGoodsInfo(Goods goods, int count) {
        this.goods = goods;
        this.count = count;
    }
}
