package controller.login.requests;

import network.NetworkFactory;
import network.socket.actions.ClientPackage;
import network.socket.actions.ClientRequestPackage;
import network.socket.actions.prefabs.Server.SimpleJsonServerResponsePackage;
import network.socket.actions.unpackage.ClientUnpackage;
import model.player.Player;
import model.player.PlayersManager;

public class CharacterInfoRequest extends ClientRequestPackage {

    public CharacterInfoRequest(ClientUnpackage unpackage) throws Exception {
        super(unpackage);
    }

    public CharacterInfoRequest() {
    }

    @Override
    public void process() throws Exception {
        Player player = PlayersManager.get().getPlayer(mUnpackage.getAddress());
        if(player != null){
            SimpleJsonServerResponsePackage responsePackage = new SimpleJsonServerResponsePackage(
                    mUnpackage.getAddress(),
                    mRequestUnpackage.getPackageId(),
                    "CharacterInfoResponse",
                    1);
            responsePackage.addKeyValue("isCreated", String.valueOf(player.getInfo().characterInfo.isCreated));
            responsePackage.addKeyValue("gender", String.valueOf(player.getInfo().characterInfo.gender));
            responsePackage.addKeyValue("colorR", String.valueOf(player.getInfo().characterInfo.colorR));
            responsePackage.addKeyValue("colorG", String.valueOf(player.getInfo().characterInfo.colorG));
            responsePackage.addKeyValue("colorB", String.valueOf(player.getInfo().characterInfo.colorB));
            responsePackage.addKeyValue("sceneId", String.valueOf(player.getInfo().sceneId));
            responsePackage.addKeyValue("enterId", String.valueOf(player.getInfo().enterId));
            responsePackage.complete();
            NetworkFactory.getSocketNet().sendPackage(responsePackage);
        }
    }

    @Override
    public String getHeaderString() {
        return "CharacterInfoRequest";
    }

    @Override
    public ClientPackage create(ClientUnpackage unpackage) throws Exception {
        return new CharacterInfoRequest(unpackage);
    }
}
