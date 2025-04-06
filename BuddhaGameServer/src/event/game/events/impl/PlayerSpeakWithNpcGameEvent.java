package event.game.events.impl;

import event.game.events.PlayerGameEvent;
import model.npc.Npc;
import model.player.Player;

public class PlayerSpeakWithNpcGameEvent extends PlayerGameEvent {

    private Npc mNpc;

    public PlayerSpeakWithNpcGameEvent(Player player, Npc npc) {
        super(player);
        mNpc = npc;
    }

    public Npc getNpc() {
        return mNpc;
    }
}
