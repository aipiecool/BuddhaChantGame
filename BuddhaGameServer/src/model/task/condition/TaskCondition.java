package model.task.condition;

import event.game.events.PlayerGameEvent;
import model.player.Player;
import model.task.entity.rom.TaskInfo;

public abstract class TaskCondition {


    public TaskCondition(TaskInfo.TaskActiveCondition condition) {

    }

    public abstract boolean isPass(PlayerGameEvent gameEvent);

    public abstract boolean isPass(Player player);

}
