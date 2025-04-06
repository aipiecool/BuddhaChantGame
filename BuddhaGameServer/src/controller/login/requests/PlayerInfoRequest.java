package controller.login.requests;

import network.NetworkFactory;
import network.socket.actions.ClientPackage;
import network.socket.actions.ClientRequestPackage;
import network.socket.actions.prefabs.Server.SimpleServerResponsePackage;
import network.socket.actions.unpackage.ClientUnpackage;
import model.player.Player;
import model.player.PlayerInfo;
import model.player.PlayersManager;
import utils.BytesUtils;
import utils.JsonUtils;

public class PlayerInfoRequest extends ClientRequestPackage {

    private int mUserId;

    public PlayerInfoRequest(ClientUnpackage unpackage) throws Exception {
        super(unpackage);
        byte[] body = mRequestUnpackage.getBody();
        mUserId = BytesUtils.bytes2Int(body);
    }

    public PlayerInfoRequest() {
    }

    @Override
    public void process() throws Exception {
        Player player = PlayersManager.get().getPlayer(mUserId);
        if(player != null){
            OtherPlayerInfo playerInfo = new OtherPlayerInfo(player.getInfo());
            String json = JsonUtils.serialize(playerInfo);
            SimpleServerResponsePackage simpleServerResponsePackage = new SimpleServerResponsePackage(
                    mUnpackage.getAddress(),
                    mRequestUnpackage.getPackageId(),
                    "PlayerInfoResponse",
                    1,
                    json);
            NetworkFactory.getSocketNet().sendPackage(simpleServerResponsePackage);
        }
    }

    @Override
    public String getHeaderString() {
        return "PlayerInfoRequest";
    }

    @Override
    public ClientPackage create(ClientUnpackage unpackage) throws Exception {
        return new PlayerInfoRequest(unpackage);
    }

    public class OtherPlayerInfo{
        public int userId;
        public String username;
        public PlayerInfo.CharacterInfo characterInfo;

        public OtherPlayerInfo() {
        }

        public OtherPlayerInfo(PlayerInfo playerInfo) {
            this.userId = playerInfo.userId;
            this.username = playerInfo.username;
            this.characterInfo = playerInfo.characterInfo;
        }
    }
}
