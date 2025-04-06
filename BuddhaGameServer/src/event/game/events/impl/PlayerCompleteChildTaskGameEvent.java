package event.game.events.impl;

import event.game.events.PlayerGameEvent;
import model.player.Player;
import model.task.entity.rom.GameChildTask;

public class PlayerCompleteChildTaskGameEvent extends PlayerGameEvent {

    private GameChildTask mCompleteTask;

    public PlayerCompleteChildTaskGameEvent(Player player, GameChildTask completeTask) {
        super(player);
        mCompleteTask = completeTask;
    }

    public GameChildTask getCompleteTask() {
        return mCompleteTask;
    }
}
