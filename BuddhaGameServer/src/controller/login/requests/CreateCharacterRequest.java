package controller.login.requests;

import network.NetworkFactory;
import network.socket.actions.ClientPackage;
import network.socket.actions.ClientRequestPackage;
import network.socket.actions.prefabs.Server.SimpleServerResponsePackage;
import network.socket.actions.unpackage.ClientRequestJsonUnpackage;
import network.socket.actions.unpackage.ClientUnpackage;
import model.player.Player;
import model.player.PlayersManager;

public class CreateCharacterRequest extends ClientRequestPackage {

    public CreateCharacterRequest(ClientUnpackage unpackage) throws Exception {
        super(unpackage);
    }

    public CreateCharacterRequest() {
    }

    @Override
    public void process() throws Exception {
        ClientRequestJsonUnpackage jsonUnpackage = new ClientRequestJsonUnpackage(mRequestUnpackage);
        Player player = PlayersManager.get().getPlayer(mUnpackage.getAddress());
        if(player != null){
            int gender = Integer.parseInt(jsonUnpackage.getValue("gender"));
            float colorR = Float.parseFloat(jsonUnpackage.getValue("colorR"));
            float colorG = Float.parseFloat(jsonUnpackage.getValue("colorG"));
            float colorB = Float.parseFloat(jsonUnpackage.getValue("colorB"));
            player.getInfo().characterInfo.gender = gender;
            player.getInfo().characterInfo.colorR = colorR;
            player.getInfo().characterInfo.colorG = colorG;
            player.getInfo().characterInfo.colorB = colorB;
            player.getInfo().characterInfo.isCreated = true;
            PlayersManager.get().savePlayer(player);
            NetworkFactory.getSocketNet().sendPackage(new SimpleServerResponsePackage(
                    jsonUnpackage.getAddress() ,
                    jsonUnpackage.getPackageId(),
                    "CreateCharacterResponse",
                    1,
                    "创建成功"));
        }
    }

    @Override
    public String getHeaderString() {
        return "CreateCharacterRequest";
    }

    @Override
    public ClientPackage create(ClientUnpackage unpackage) throws Exception {
        return new CreateCharacterRequest(unpackage);
    }
}
