package model.task.entity.running;

import event.game.GameEventsMamager;
import event.game.events.PlayerGameEvent;
import event.server.ServerEventsMamager;
import event.server.events.impl.ClientNeedFlowTextServerEvent;
import event.game.events.impl.PlayerCompleteChildTaskGameEvent;
import log.Log;
import model.player.Player;
import model.task.TasksManager;
import model.task.action.TaskAction;
import model.task.action.TaskActionFactory;
import model.task.entity.rom.GameChildTask;
import model.task.entity.rom.TaskInfo;
import model.task.reward.TaskReward;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

public class RunningGameTask {

    private final GameChildTask mChildTask;
    private final RunningTaskInfo mRunningTaskInfo;
    private final List<RunningTaskInfo.RunningTaskActionInfo> mRunningTaskActionInfos = new ArrayList<>();
    private final List<TaskAction> mRunningTaskActions = new ArrayList<>();
    private boolean mIsComplete = false;

    // 从TaskController实例化得来，表示新得到的任务
    public RunningGameTask(GameChildTask gameChildTask) {
        mChildTask = gameChildTask;
        for(TaskInfo.ChildTask.TaskAction taskAction : mChildTask.getChildTaskInfo().actions){
            RunningTaskInfo.RunningTaskActionInfo taskActionInfo = new RunningTaskInfo.RunningTaskActionInfo();
            taskActionInfo.romInfo = taskAction;
            taskActionInfo.replaceCount = 0;
            mRunningTaskActionInfos.add(taskActionInfo);
            mRunningTaskActions.add(TaskActionFactory.get().createAction(taskActionInfo));
        }

        mRunningTaskInfo = new RunningTaskInfo();
        mRunningTaskInfo.childTaskId = gameChildTask.getChildTaskInfo().taskId;
        mRunningTaskInfo.taskActions = mRunningTaskActionInfos;
    }
    // 从Player实例化得来
    public RunningGameTask(RunningTaskInfo runningTaskInfo) {
        mRunningTaskInfo = runningTaskInfo;
        mRunningTaskActionInfos.addAll(runningTaskInfo.taskActions);
        mChildTask = TasksManager.get().getChildGameTaskById(runningTaskInfo.childTaskId);
        for(RunningTaskInfo.RunningTaskActionInfo runningTaskActionInfo : runningTaskInfo.taskActions){
            mRunningTaskActions.add(TaskActionFactory.get().createAction(runningTaskActionInfo));
        }
    }

    public GameChildTask getChildTask() {
        return mChildTask;
    }

    public TaskInfo.ChildTask getChildTaskInfo() {
        return mChildTask.getChildTaskInfo();
    }

    public List<RunningTaskInfo.RunningTaskActionInfo> getRunningTaskActionInfos() {
        return mRunningTaskActionInfos;
    }

    public void checkAtion(Iterator<RunningGameTask> iterator, PlayerGameEvent event) {
        int completeCount = 0;
        for(TaskAction taskAction : mRunningTaskActions){
            if(taskAction.processGameEvent(event)){
                completeCount++;
            }
        }
        if(completeCount >= mRunningTaskActions.size()){
            //任务完成
            mIsComplete = true;
            onPlayerCompleteTask(iterator, event.getPlayer());
            Log.input().info(event.getPlayer().getInfo().username + "完成任务" + mChildTask.getChildTaskInfo().taskName);
        }
    }

    private void onPlayerCompleteTask(Iterator<RunningGameTask> iterator, Player player){
        StringBuilder flowTextStringBulder = new StringBuilder();
        flowTextStringBulder
                .append("任务”")
                .append(mChildTask.getChildTaskInfo().taskName)
                .append("“完成\n");
        List<TaskReward> taskReward = mChildTask.getTaskReward();
        for(TaskReward reward : taskReward){
            reward.award(player);
            flowTextStringBulder
                    .append("获得 ")
                    .append(reward.getRewardName())
                    .append("x")
                    .append(reward.getRewardCount())
                    .append("\n");
        }

        player.completeAChildTask(iterator, this);
        if(mChildTask.isCompleteParentTask()){
            player.completeAParentTask(mChildTask.getParentTask());
        }

        GameEventsMamager.get().sendEventAsyn(new PlayerCompleteChildTaskGameEvent(player, mChildTask));

        ServerEventsMamager.get().sendEvent(
                new ClientNeedFlowTextServerEvent(player, flowTextStringBulder.toString(), "get_reward"));
    }
}
