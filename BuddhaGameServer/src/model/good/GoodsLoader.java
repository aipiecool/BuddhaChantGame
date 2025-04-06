package model.good;

import model.good.entity.rom.Goods;
import model.good.entity.rom.GoodsInfo;
import utils.FileUtils;
import utils.JsonUtils;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

public class GoodsLoader {

    public List<Goods> load(){
        List<Goods> goods = new ArrayList<>();
        loadGoods(goods, "assets/goods/");
        return goods;
    }

    private void loadGoods(List<Goods> goods, String path){
        File[] files = FileUtils.listOfFolder(path);
        for(File file : files){
            if(file.isFile()){
                GoodsInfo goodsInfo = JsonUtils.unserialize(FileUtils.readTxt(file.getAbsolutePath()), GoodsInfo.class);
                goods.add(new Goods(goodsInfo));
            }else if(file.isDirectory()){
                loadGoods(goods, file.getAbsolutePath());
            }
        }
    }

}
