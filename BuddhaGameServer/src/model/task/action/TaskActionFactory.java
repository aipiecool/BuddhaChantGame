package model.task.action;

import model.task.action.impl.ChantOrWriteAction;
import model.task.action.impl.SpeakWithNpcAction;
import model.task.entity.running.RunningTaskInfo;

import java.lang.reflect.Constructor;
import java.lang.reflect.InvocationTargetException;
import java.util.HashMap;

public class TaskActionFactory {
    private static TaskActionFactory sInstance;
    private final HashMap<String, Class<? extends TaskAction>> mNameActionMap = new HashMap<>();

    private TaskActionFactory() {
        registerAction(ChantOrWriteAction.class);
        registerAction(SpeakWithNpcAction.class);
    }

    private void registerAction(Class<? extends TaskAction> action){
        String simpleName = action.getSimpleName();
        String name = simpleName.substring(0, simpleName.length() - 6);
        mNameActionMap.put(name, action);
    }

    public TaskAction createAction(RunningTaskInfo.RunningTaskActionInfo taskActionInfo) {
        Class<? extends TaskAction> aClass = mNameActionMap.get(taskActionInfo.romInfo.triggerEvent);
        try {
            Constructor<?> constructor = aClass.getConstructor(RunningTaskInfo.RunningTaskActionInfo.class);
            TaskAction taskAction = (TaskAction)constructor.newInstance(taskActionInfo);
            taskActionInfo.actionLocalName = taskAction.getActionLocalName();
            return taskAction;
        } catch (NoSuchMethodException | InstantiationException | IllegalAccessException | InvocationTargetException e) {
            e.printStackTrace();
        }
        return null;
    }

    public static TaskActionFactory get(){
        if(sInstance == null){
            synchronized (TaskActionFactory.class){
                if(sInstance == null){
                    sInstance = new TaskActionFactory();
                }
            }
        }
        return sInstance;
    }
}
