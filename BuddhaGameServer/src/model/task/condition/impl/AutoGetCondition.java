package model.task.condition.impl;


import event.game.events.PlayerGameEvent;
import model.player.Player;
import model.task.condition.TaskCondition;
import model.task.entity.rom.TaskInfo;


public class AutoGetCondition extends TaskCondition {

    public AutoGetCondition(TaskInfo.TaskActiveCondition condition) {
        super(condition);
    }

    @Override
    public boolean isPass(PlayerGameEvent gameEvent) {
        return true;
    }

    @Override
    public boolean isPass(Player player) {
        return true;
    }
}
