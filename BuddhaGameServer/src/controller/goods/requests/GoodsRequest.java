package controller.goods.requests;

import model.good.entity.rom.GoodsInfo;
import model.good.entity.running.RunningGoodsInfo;
import model.player.Player;
import model.player.PlayersManager;
import network.NetworkFactory;
import network.socket.actions.ClientPackage;
import network.socket.actions.ClientRequestPackage;
import network.socket.actions.prefabs.Server.SimpleServerResponsePackage;
import network.socket.actions.unpackage.ClientUnpackage;
import utils.JsonUtils;

import java.util.ArrayList;
import java.util.List;

public class GoodsRequest extends ClientRequestPackage {

    public GoodsRequest(ClientUnpackage unpackage) throws Exception {
        super(unpackage);
    }

    public GoodsRequest() {
    }

    @Override
    public void process() throws Exception {
        Player player = PlayersManager.get().getPlayer(mUnpackage.getAddress());
        if(player != null){
            List<GoodsResponseInfo> result = new ArrayList<>();
            for(RunningGoodsInfo goodsInfo : player.getInfo().goods){
                GoodsResponseInfo responseInfo = new GoodsResponseInfo();
                GoodsInfo info = goodsInfo.goods.getInfo();
                responseInfo.name = info.name;
                responseInfo.localName = info.localName;
                responseInfo.info = info.info;
                responseInfo.order = info.order;
                responseInfo.count = goodsInfo.count;
                result.add(responseInfo);
            }
            String json = JsonUtils.serialize(result);
            NetworkFactory.getSocketNet().sendPackage(new SimpleServerResponsePackage(
                    mRequestUnpackage.getAddress(),
                    mRequestUnpackage.getPackageId(),
                    "GoodsResponse",
                    1,
                    json
            ));
        }
    }

    @Override
    public String getHeaderString() {
        return "GoodsRequest";
    }

    @Override
    public ClientPackage create(ClientUnpackage unpackage) throws Exception {
        return new GoodsRequest(unpackage);
    }

    private static class GoodsResponseInfo{
        public String name;
        public String localName;
        public String info;
        public int count;
        public int order;
    }
}
