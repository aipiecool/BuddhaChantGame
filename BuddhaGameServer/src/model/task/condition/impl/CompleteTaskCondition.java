package model.task.condition.impl;

import event.game.events.PlayerGameEvent;
import model.player.Player;
import model.task.condition.TaskCondition;
import model.task.entity.rom.TaskInfo;

public class CompleteTaskCondition extends TaskCondition {

    private final String mConditionTaskId;

    public CompleteTaskCondition(TaskInfo.TaskActiveCondition condition) {
        super(condition);
        mConditionTaskId = condition.triggerObject;
    }

    @Override
    public boolean isPass(PlayerGameEvent gameEvent) {
        Player player = gameEvent.getPlayer();
        return isPass(player);
    }

    @Override
    public boolean isPass(Player player) {
        if(player.getInfo().completeTasksId != null){
            return player.getInfo().completeTasksId.contains(mConditionTaskId);
        }
        return false;
    }
}
