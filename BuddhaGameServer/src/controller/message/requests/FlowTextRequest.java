package controller.message.requests;

import event.server.events.impl.ClientNeedFlowTextServerEvent;
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

public class FlowTextRequest extends ClientRequestPackage {

    public FlowTextRequest(ClientUnpackage unpackage) throws Exception {
        super(unpackage);
    }

    public FlowTextRequest() {

    }

    @Override
    public void process() throws Exception {
        Player player = PlayersManager.get().getPlayer(mUnpackage.getAddress());
        if(player != null){
            List<FlowTextResponseInfo> result = new ArrayList<>();
            List<ClientNeedFlowTextServerEvent> flowTextEvent = player.getFlowTextEvent();
            for(ClientNeedFlowTextServerEvent event : flowTextEvent){
                FlowTextResponseInfo info = new FlowTextResponseInfo();
                info.message = event.getMessage();
                info.sound = event.getSound();
                result.add(info);
            }
            player.removeAllFlowTextEvent();
            String json = JsonUtils.serialize(result);
            NetworkFactory.getSocketNet().sendPackage(new SimpleServerResponsePackage(
                    mRequestUnpackage.getAddress(),
                    mRequestUnpackage.getPackageId(),
                    "FlowTextResponse",
                    1,
                    json
            ));
        }
    }

    @Override
    public String getHeaderString() {
        return "FlowTextRequest";
    }

    @Override
    public ClientPackage create(ClientUnpackage unpackage) throws Exception {
        return new FlowTextRequest(unpackage);
    }

    private static class FlowTextResponseInfo{
        public String message;
        public String sound;
    }
}
