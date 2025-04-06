package event.game.events;

import event.game.GameEvent;
import model.player.Player;

public class PlayerGameEvent extends GameEvent {
    protected Player mPlayer;

    public PlayerGameEvent(Player player) {
        mPlayer = player;
    }

    public Player getPlayer() {
        return mPlayer;
    }
}
