package model.task.condition;

import model.task.condition.impl.*;
import model.task.entity.rom.TaskInfo;

import java.lang.reflect.Constructor;
import java.lang.reflect.InvocationTargetException;
import java.util.HashMap;

public class TaskConditionFactory {

    private static TaskConditionFactory sInstance;
    private final HashMap<String, Class<? extends TaskCondition>> mNameConditionMap = new HashMap<>();

    private TaskConditionFactory(){
        registerCondition(AutoGetCondition.class);
        registerCondition(UncompleteTaskCondition.class);
        registerCondition(UncompleteChildTaskCondition.class);
        registerCondition(CompleteChildTaskCondition.class);
        registerCondition(CompleteTaskCondition.class);
        registerCondition(SpeakWithNpcCondition.class);
    }

    private void registerCondition(Class<? extends TaskCondition> condition){
        String simpleName = condition.getSimpleName();
        String name = simpleName.substring(0, simpleName.length() - 9);
        mNameConditionMap.put(name, condition);
    }

    public TaskCondition createCondition(TaskInfo.TaskActiveCondition condition) {
        Class<? extends TaskCondition> aClass = mNameConditionMap.get(condition.triggerEvent);
        try {
            Constructor<?> constructor = aClass.getConstructor(TaskInfo.TaskActiveCondition.class);
            return (TaskCondition)constructor.newInstance(condition);
        } catch (NoSuchMethodException | InstantiationException | IllegalAccessException | InvocationTargetException e) {
            e.printStackTrace();
        }
        return null;
    }

    public static TaskConditionFactory get(){
        if(sInstance == null){
            synchronized (TaskConditionFactory.class){
                if(sInstance == null){
                    sInstance = new TaskConditionFactory();
                }
            }
        }
        return sInstance;
    }
}
