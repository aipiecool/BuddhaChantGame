package model.task.condition.impl;

import event.game.events.PlayerGameEvent;
import event.game.events.impl.PlayerSpeakWithNpcGameEvent;
import model.player.Player;
import model.task.condition.TaskCondition;
import model.task.entity.rom.TaskInfo;

public class SpeakWithNpcCondition extends TaskCondition {

    private final String mNpcName;

    public SpeakWithNpcCondition(TaskInfo.TaskActiveCondition condition) {
        super(condition);
        mNpcName = condition.triggerObject;
    }

    @Override
    public boolean isPass(PlayerGameEvent gameEvent) {
        if(gameEvent instanceof PlayerSpeakWithNpcGameEvent){
            PlayerSpeakWithNpcGameEvent event = ((PlayerSpeakWithNpcGameEvent) gameEvent);
            return event.getNpc().getNpcInfo().name.equals(mNpcName);
        }
        return false;
    }

    @Override
    public boolean isPass(Player player) {
        return false;
    }
}
