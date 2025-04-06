package controller.login.actions;

import network.socket.actions.ClientPackage;
import network.socket.actions.unpackage.ClientUnpackage;
import model.player.Player;
import model.player.PlayersManager;

public class Heartbeat extends ClientPackage {

    public Heartbeat(ClientUnpackage unpackage) {
        super(unpackage);
    }

    public Heartbeat() {
    }

    @Override
    public void process() throws Exception {
        Player player = PlayersManager.get().getPlayer(mUnpackage.getAddress());
        if(player != null){
            player.resetLifetime();
        }
    }

    @Override
    public String getHeaderString() {
        return "Heartbeat";
    }

    @Override
    public ClientPackage create(ClientUnpackage unpackage) throws Exception {
        return new Heartbeat(unpackage);
    }
}
