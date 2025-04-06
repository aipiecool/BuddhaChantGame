package model.task.reward;

import model.task.entity.rom.TaskInfo;
import model.task.reward.impl.ExpReward;
import model.task.reward.impl.GoodsReward;

import java.lang.reflect.Constructor;
import java.lang.reflect.InvocationTargetException;
import java.util.HashMap;

public class TaskRewardFactory {
    private static TaskRewardFactory sInstance;
    private final HashMap<String, Class<? extends TaskReward>> mNameRewardMap = new HashMap<>();

    private TaskRewardFactory() {
        registerReward(ExpReward.class);
        registerReward(GoodsReward.class);
    }

    private void registerReward(Class<? extends TaskReward> reward){
        String simpleName = reward.getSimpleName();
        String name = simpleName.substring(0, simpleName.length() - 6);
        mNameRewardMap.put(name, reward);
    }

    public TaskReward createReward(TaskInfo.ChildTask.TaskReward taskRewardInfo) {
        Class<? extends TaskReward> aClass = mNameRewardMap.get(taskRewardInfo.rewardType);
        try {
            Constructor<?> constructor = aClass.getConstructor(TaskInfo.ChildTask.TaskReward.class);
            TaskReward taskReward = (TaskReward)constructor.newInstance(taskRewardInfo);
            taskRewardInfo.rewardName = taskReward.getRewardName();
            return taskReward;
        } catch (NoSuchMethodException | InstantiationException | IllegalAccessException | InvocationTargetException e) {
            e.printStackTrace();
        }
        return null;
    }

    public static TaskRewardFactory get(){
        if(sInstance == null){
            synchronized (TaskRewardFactory.class){
                if(sInstance == null){
                    sInstance = new TaskRewardFactory();
                }
            }
        }
        return sInstance;
    }
}
