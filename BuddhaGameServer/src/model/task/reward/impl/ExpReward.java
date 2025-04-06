package model.task.reward.impl;

import model.player.Player;
import model.task.entity.rom.TaskInfo;
import model.task.reward.TaskReward;

public class ExpReward extends TaskReward {

    public ExpReward(TaskInfo.ChildTask.TaskReward taskRewardInfo) {
        super(taskRewardInfo);
    }

    @Override
    public String getRewardName() {
        return "经验";
    }

    @Override
    public void award(Player player) {
        player.getInfo().exp += mRewardCount;
    }
}
