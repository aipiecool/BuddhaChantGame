package model.good.entity.rom;

public class GoodsInfo {
    public String name;
    public String localName;
    public String info;
    public int order;

    @Override
    public int hashCode() {
        return localName.hashCode();
    }

    @Override
    public boolean equals(Object obj) {
        if(obj instanceof GoodsInfo){
            GoodsInfo other = (GoodsInfo)obj;
            return other.name.equals(name);
        }
        return false;
    }
}
