package controller.npc.request;

import network.NetworkFactory;
import network.socket.actions.ClientPackage;
import network.socket.actions.ClientRequestPackage;
import network.socket.actions.prefabs.Server.SimpleServerResponsePackage;
import network.socket.actions.unpackage.ClientUnpackage;
import model.npc.Npc;
import model.npc.NpcInfo;
import model.npc.NpcsManager;
import model.player.Player;
import model.player.PlayersManager;
import utils.BytesUtils;
import utils.JsonUtils;

import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;

public class SceneNpcRequest extends ClientRequestPackage {

    private int mScenceId;

    public SceneNpcRequest(ClientUnpackage unpackage) throws Exception {
        super(unpackage);
        mScenceId = BytesUtils.bytes2Int(mRequestUnpackage.getBody());
    }

    public SceneNpcRequest() {
    }

    @Override
    public void process() throws Exception {
        Player player = PlayersManager.get().getPlayer(mUnpackage.getAddress());
        if(player != null){
            List<ResponseSceneNpcInfo> responseSceneNpcInfos = new ArrayList<>();
            HashSet<Npc> npcsBySceneId = NpcsManager.get().getNpcsBySceneId(mScenceId);
            for(Npc npc : npcsBySceneId){
                ResponseSceneNpcInfo responseInfo = new ResponseSceneNpcInfo(npc.getNpcInfo());
                responseSceneNpcInfos.add(responseInfo);
            }
            String json = JsonUtils.serialize(responseSceneNpcInfos);
            NetworkFactory.getSocketNet().sendPackage(new SimpleServerResponsePackage(
                    mRequestUnpackage.getAddress(),
                    mRequestUnpackage.getPackageId(),
                    "SceneNpcResponse",
                    1,
                    json
                    ));
        }
    }

    @Override
    public String getHeaderString() {
        return "SceneNpcRequest";
    }

    @Override
    public ClientPackage create(ClientUnpackage unpackage) throws Exception {
        return new SceneNpcRequest(unpackage);
    }

    public static class ResponseSceneNpcInfo{
        String name;
        String localName;
        String firstSpeak;
        String secondSpeak;
        float positionX;
        float positionY;

        public ResponseSceneNpcInfo(NpcInfo npcInfo) {
            name = npcInfo.name;
            localName = npcInfo.localName;
            firstSpeak = npcInfo.defaultSpeak;
            secondSpeak = npcInfo.defaultSpeak;
            positionX = npcInfo.positionX;
            positionY = npcInfo.positionY;
        }
    }
}
