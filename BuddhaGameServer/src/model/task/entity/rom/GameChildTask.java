package model.task.entity.rom;

import model.task.condition.TaskCondition;
import model.task.condition.TaskConditionFactory;
import model.task.reward.TaskReward;
import model.task.reward.TaskRewardFactory;

import java.util.ArrayList;
import java.util.List;

public class GameChildTask {

    private TaskInfo.ChildTask mChildTaskInfo;
    private GameTask mParentTask;
    private List<TaskCondition> mActiveConditon = new ArrayList<>();
    private List<TaskReward> mTaskReward = new ArrayList<>();
    private TaskCondition mGetCondition;

    public GameChildTask(GameTask parentTask, TaskInfo.ChildTask childTaskInfo) {
        mParentTask = parentTask;
        mChildTaskInfo = childTaskInfo;

        for(TaskInfo.TaskActiveCondition condition : mChildTaskInfo.condition){
            mActiveConditon.add(TaskConditionFactory.get().createCondition(condition));
        }

        for(TaskInfo.ChildTask.TaskReward reward : mChildTaskInfo.rewards){
            mTaskReward.add(TaskRewardFactory.get().createReward(reward));
        }
    }

    public GameTask getParentTask() {
        return mParentTask;
    }

    public TaskInfo.ChildTask getChildTaskInfo() {
        return mChildTaskInfo;
    }

    public List<TaskCondition> getActiveConditon() {
        return mActiveConditon;
    }

    public List<TaskReward> getTaskReward() {
        return mTaskReward;
    }

    public boolean isCompleteParentTask() {
        return mParentTask.getTaskInfo().completeChildTaskId.equals(mChildTaskInfo.taskId);
    }
}
