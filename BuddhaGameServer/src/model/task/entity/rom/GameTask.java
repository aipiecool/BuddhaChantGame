package model.task.entity.rom;

import model.task.condition.TaskCondition;
import model.task.condition.TaskConditionFactory;

import java.util.ArrayList;
import java.util.List;

public class GameTask {

    private TaskInfo mTaskInfo;
    private List<GameChildTask> mGameChildTasks = new ArrayList<>();
    private List<TaskCondition> mActiveConditon;
    private TaskCondition mGetCondition;

    public GameTask(TaskInfo taskInfo) {
        mTaskInfo = taskInfo;

        mActiveConditon = new ArrayList<>();
        for(TaskInfo.TaskActiveCondition condition : mTaskInfo.condition){
            mActiveConditon.add(TaskConditionFactory.get().createCondition(condition));
        }
        mGetCondition = TaskConditionFactory.get().createCondition(mTaskInfo.getCondition);

        for(TaskInfo.ChildTask childTask : mTaskInfo.childTask){
            mGameChildTasks.add(new GameChildTask(this, childTask));
        }
    }

    public TaskInfo getTaskInfo() {
        return mTaskInfo;
    }

    public List<GameChildTask> getGameChildTasks() {
        return mGameChildTasks;
    }

    public List<TaskCondition> getActiveConditon() {
        return mActiveConditon;
    }

    public TaskCondition getGetCondition() {
        return mGetCondition;
    }
}
