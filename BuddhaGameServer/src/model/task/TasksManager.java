package model.task;

import event.game.events.PlayerGameEvent;
import model.player.Player;
import model.task.condition.TaskCondition;
import model.task.entity.rom.GameChildTask;
import model.task.entity.rom.GameTask;
import model.task.entity.running.RunningGameTask;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class TasksManager {

    private static TasksManager sInstance;
    private final List<GameTask> mTasks;
    private final List<GameChildTask> mChildTasks = new ArrayList<>();
    private final Map<String, GameChildTask> mTaskId2GameChildTasks = new HashMap<>();

    private TasksManager() {
        mTasks = new TaskLoader().load();
        for(GameTask gameTask : mTasks){
            mChildTasks.addAll(gameTask.getGameChildTasks());
        }
        for(GameChildTask gameChildTask : mChildTasks){
            mTaskId2GameChildTasks.put(gameChildTask.getChildTaskInfo().taskId, gameChildTask);
        }
    }

    public static TasksManager get(){
        if(sInstance == null){
            synchronized (TasksManager.class){
                if(sInstance == null){
                    sInstance = new TasksManager();
                }
            }
        }
        return sInstance;
    }

    public List<GameTask> getPlayerActiveAndNotGetTask(Player player){
        List<GameTask> activiteTask = new ArrayList<>();
        for(GameTask gameTask : mTasks){
            boolean isPass = true;
            List<TaskCondition> activeConditon = gameTask.getActiveConditon();
            for(TaskCondition condition : activeConditon){
                if(!condition.isPass(player)){
                    isPass = false;
                    break;
                }
            }
            if(isPass){
                boolean isGeted = false;
                for (RunningGameTask runtimeTask : player.getRunningGameTasks()) {
                    if (runtimeTask.getChildTaskInfo().parentTask.taskId.equals(gameTask.getTaskInfo().taskId)) {
                        isGeted = true;
                        break;
                    }
                }
                if(!isGeted) {
                    activiteTask.add(gameTask);
                }
            }
        }
        return activiteTask;
    }

    public List<GameChildTask> getPlayerActiveAndNotGetChildTask(Player player, PlayerGameEvent event) {
        List<GameChildTask> activiteChildTask = new ArrayList<>();
        List<GameTask> playerActiveAndNotGetTask = getPlayerActiveAndNotGetTask(player);
        for(GameTask gameTask : playerActiveAndNotGetTask){
            for(GameChildTask childTask : gameTask.getGameChildTasks()){
                boolean isPass = true;
                List<TaskCondition> activeConditon = childTask.getActiveConditon();
                for(TaskCondition condition : activeConditon){
                    if(!condition.isPass(event)){
                        isPass = false;
                        break;
                    }
                }
                if(isPass){
                    boolean isGeted = false;
                    for (RunningGameTask runtimeTask : player.getRunningGameTasks()) {
                        if (runtimeTask.getChildTaskInfo().taskId.equals(childTask.getChildTaskInfo().taskId)) {
                            isGeted = true;
                            break;
                        }
                    }
                    if(!isGeted) {
                        activiteChildTask.add(childTask);
                    }
                }
            }
        }
        return activiteChildTask;
    }

    public GameChildTask getChildGameTaskById(String childTaskId) {
        return mTaskId2GameChildTasks.get(childTaskId);
    }
}
