package event.game.events.impl;

import event.game.events.PlayerGameEvent;
import model.player.Player;

public class PlayerLoginGameEvent extends PlayerGameEvent {

    public PlayerLoginGameEvent(Player player) {
        super(player);
    }

}
