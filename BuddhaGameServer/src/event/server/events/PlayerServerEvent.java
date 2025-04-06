package event.server.events;

import event.server.ServerEvent;
import model.player.Player;

public class PlayerServerEvent extends ServerEvent {
    protected Player mPlayer;

    public PlayerServerEvent(Player player) {
        mPlayer = player;
    }

    public Player getPlayer() {
        return mPlayer;
    }
}
