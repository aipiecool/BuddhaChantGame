package event.game.events.impl;

import model.npc.Npc;
import model.player.Player;

public class PlayerSpeakStartWithNpcGameEvent extends PlayerSpeakWithNpcGameEvent {



    public PlayerSpeakStartWithNpcGameEvent(Player player, Npc npc) {
        super(player, npc);
    }
}
