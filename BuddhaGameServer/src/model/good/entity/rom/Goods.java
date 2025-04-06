package model.good.entity.rom;

public class Goods {
    private GoodsInfo mInfo;

    public Goods(GoodsInfo info) {
        mInfo = info;
    }

    public GoodsInfo getInfo() {
        return mInfo;
    }

    @Override
    public int hashCode() {
        return mInfo.hashCode();
    }

    @Override
    public boolean equals(Object obj) {
        if(obj instanceof Goods){
            Goods other = (Goods)obj;
            return other.getInfo().name.equals(mInfo.name);
        }
        return false;
    }
}
