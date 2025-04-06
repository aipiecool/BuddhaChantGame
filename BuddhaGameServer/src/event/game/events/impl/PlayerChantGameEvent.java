package event.game.events.impl;

import model.chantable.Chantable;
import event.game.events.PlayerGameEvent;
import model.player.Player;

public class PlayerChantGameEvent extends PlayerGameEvent {

    private Chantable mChantable;

    public PlayerChantGameEvent(Player player, Chantable chantable) {
        super(player);
        mChantable = chantable;
    }

    public Chantable getChantable() {
        return mChantable;
    }
}
