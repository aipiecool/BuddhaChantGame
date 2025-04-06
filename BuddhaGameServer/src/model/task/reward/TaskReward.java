package model.task.reward;

import model.player.Player;
import model.task.entity.rom.TaskInfo;

public abstract class TaskReward {

    protected TaskInfo.ChildTask.TaskReward mTaskRewardInfo;
    protected int mRewardCount;

    public TaskReward(TaskInfo.ChildTask.TaskReward taskRewardInfo) {
        mTaskRewardInfo = taskRewardInfo;
        mRewardCount = mTaskRewardInfo.rewardCount;
    }

    public int getRewardCount(){
        return mRewardCount;
    }

    public abstract String getRewardName();

    public abstract void award(Player player);
}
