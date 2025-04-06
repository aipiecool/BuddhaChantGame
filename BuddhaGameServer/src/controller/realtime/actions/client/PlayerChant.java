package controller.realtime.actions.client;

import model.chantable.Chantable;
import model.chantable.ChantablesManager;
import event.game.GameEventsMamager;
import event.game.events.impl.PlayerChantGameEvent;
import network.socket.actions.ClientPackage;
import network.socket.actions.unpackage.ClientUnpackage;
import model.player.Player;
import utils.BytesUtils;
import model.player.PlayersManager;

public class PlayerChant extends ClientPackage {

    private int mWordId;
    private int mCount;

    public PlayerChant(ClientUnpackage unpackage) throws Exception {
        super(unpackage);
        byte[] body = unpackage.getBody();
        mWordId = BytesUtils.bytes2Int(BytesUtils.subbytes(body, 0, 4));
        mCount = BytesUtils.bytes2Int(BytesUtils.subbytes(body, 4, 8));
    }

    public PlayerChant() {
    }

    @Override
    public void process() throws Exception {
        Player player = PlayersManager.get().getPlayer(mUnpackage.getAddress());
        if(player != null){
            player.putChantBuffer(mWordId, mCount);
            Chantable chantable = ChantablesManager.get().getChantableById(mWordId);
            GameEventsMamager.get().sendEvent(new PlayerChantGameEvent(player, chantable));
        }
    }

    @Override
    public String getHeaderString() {
        return "PlayerChant";
    }

    @Override
    public ClientPackage create(ClientUnpackage unpackage) throws Exception {
        return new PlayerChant(unpackage);
    }
}
