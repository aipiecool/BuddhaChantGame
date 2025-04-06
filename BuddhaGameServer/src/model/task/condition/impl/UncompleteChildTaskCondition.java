package model.task.condition.impl;

import event.game.events.PlayerGameEvent;
import model.player.Player;
import model.task.condition.TaskCondition;
import model.task.entity.rom.TaskInfo;

public class UncompleteChildTaskCondition extends TaskCondition {

    private final String mConditionTaskId;

    public UncompleteChildTaskCondition(TaskInfo.TaskActiveCondition condition) {
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
        if(player.getInfo().completeChildTasksId != null){
            return !player.getInfo().completeChildTasksId.contains(mConditionTaskId);
        }
        return true;
    }
}
