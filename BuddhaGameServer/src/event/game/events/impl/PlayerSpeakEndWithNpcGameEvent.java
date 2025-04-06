package event.game.events.impl;

import model.npc.Npc;
import model.player.Player;

public class PlayerSpeakEndWithNpcGameEvent extends PlayerSpeakWithNpcGameEvent {

    public PlayerSpeakEndWithNpcGameEvent(Player player, Npc npc) {
        super(player, npc);
    }
}
