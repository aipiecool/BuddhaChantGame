package controller.task.request;

import model.player.Player;
import model.player.PlayersManager;
import model.task.entity.running.RunningGameTask;
import model.task.entity.running.RunningTaskInfo;
import model.task.reward.TaskReward;
import network.NetworkFactory;
import network.socket.actions.ClientPackage;
import network.socket.actions.ClientRequestPackage;
import network.socket.actions.prefabs.Server.SimpleServerResponsePackage;
import network.socket.actions.unpackage.ClientUnpackage;
import utils.JsonUtils;

import java.util.ArrayList;
import java.util.List;

public class TaskDetalRequest extends ClientRequestPackage {

    public TaskDetalRequest(ClientUnpackage unpackage) throws Exception {
        super(unpackage);
    }

    public TaskDetalRequest() {
    }

    @Override
    public void process() throws Exception {
        Player player = PlayersManager.get().getPlayer(mUnpackage.getAddress());
        if(player != null){
            List<ResponseRuntimeTaskInfo> result = new ArrayList<>();
            synchronized (player.mRunningGameTasksLock) {
                for (RunningGameTask gameTask : player.getRunningGameTasks()) {
                    ResponseRuntimeTaskInfo taskInfo = new ResponseRuntimeTaskInfo();
                    taskInfo.mainTitle = gameTask.getChildTaskInfo().parentTask.taskName;
                    taskInfo.minorTitle = gameTask.getChildTaskInfo().taskName;
                    taskInfo.tapInfo = gameTask.getChildTaskInfo().taskTap;
                    taskInfo.taskActions = new ArrayList<>();
                    for (RunningTaskInfo.RunningTaskActionInfo runningTaskActionInfo : gameTask.getRunningTaskActionInfos()) {
                        ResponseRuntimeTaskInfo.TaskAction taskAction = new ResponseRuntimeTaskInfo.TaskAction();
                        taskAction.actionName = runningTaskActionInfo.actionLocalName;
                        taskAction.requireReplaceCount = runningTaskActionInfo.romInfo.requireReplaceCount;
                        taskAction.replaceCount = runningTaskActionInfo.replaceCount;
                        taskAction.isComplete = taskAction.replaceCount >= taskAction.requireReplaceCount;
                        taskInfo.taskActions.add(taskAction);
                    }
                    taskInfo.taskReward = new ArrayList<>();
                    for (TaskReward taskRewardInfo : gameTask.getChildTask().getTaskReward()) {
                        ResponseRuntimeTaskInfo.TaskReward taskReward = new ResponseRuntimeTaskInfo.TaskReward();
                        taskReward.rewardName = taskRewardInfo.getRewardName();
                        taskReward.rewardCount = taskRewardInfo.getRewardCount();
                        taskInfo.taskReward.add(taskReward);
                    }
                    result.add(taskInfo);
                }
            }
            String json = JsonUtils.serialize(result);
            NetworkFactory.getSocketNet().sendPackage(new SimpleServerResponsePackage(
                    mRequestUnpackage.getAddress(),
                    mRequestUnpackage.getPackageId(),
                    "TaskDetalResponse",
                    1,
                    json
            ));
        }
    }

    @Override
    public String getHeaderString() {
        return "TaskDetalRequest";
    }

    @Override
    public ClientPackage create(ClientUnpackage unpackage) throws Exception {
        return new TaskDetalRequest(unpackage);
    }

    private static class ResponseRuntimeTaskInfo{
        String mainTitle;
        String minorTitle;
        String tapInfo;
        List<TaskAction> taskActions;
        List<TaskReward> taskReward;

        public static class TaskAction
        {
            String actionName;
            int replaceCount;
            int requireReplaceCount;
            boolean isComplete;
        }

        public static class TaskReward
        {
            String rewardName;
            int rewardCount;
        }
    }
}
