package controller.login.requests;

import model.player.Player;
import model.player.PlayersManager;
import network.NetworkFactory;
import network.socket.actions.ClientPackage;
import network.socket.actions.ClientRequestPackage;
import network.socket.actions.prefabs.Server.SimpleServerResponsePackage;
import network.socket.actions.unpackage.ClientUnpackage;
import utils.JsonUtils;

public class LevelRequest extends ClientRequestPackage {

    public LevelRequest(ClientUnpackage unpackage) throws Exception {
        super(unpackage);
    }

    public LevelRequest() {
    }

    @Override
    public void process() throws Exception {
        Player player = PlayersManager.get().getPlayer(mUnpackage.getAddress());
        if(player != null){
            int exp = player.getInfo().exp;
            int level1Exp = 10;
            double N = (double) exp/(2 * level1Exp) + 1;
            int level = (int) Math.floor(Math.log(N)/Math.log(2));
            int levelExp = (int) -(2 * (1 - Math.pow(2, level))) * level1Exp;
            int maxLevelExp = (int) Math.pow(2, level+1) * level1Exp;
            LevelResponseInfo result = new LevelResponseInfo();
            result.level = level + 1;
            result.exp = exp - levelExp;
            result.maxExp = maxLevelExp;
            String json = JsonUtils.serialize(result);
            NetworkFactory.getSocketNet().sendPackage(new SimpleServerResponsePackage(
                    mRequestUnpackage.getAddress(),
                    mRequestUnpackage.getPackageId(),
                    "LevelResponse",
                    1,
                    json
            ));
        }
    }

    @Override
    public String getHeaderString() {
        return "LevelRequest";
    }

    @Override
    public ClientPackage create(ClientUnpackage unpackage) throws Exception {
        return new LevelRequest(unpackage);
    }

    public class LevelResponseInfo
    {
        public int level;
        public int exp;
        public int maxExp;
    }
}
