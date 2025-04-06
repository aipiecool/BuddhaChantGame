package model.task.action;

import event.game.events.PlayerGameEvent;
import model.task.entity.running.RunningTaskInfo;

import java.util.HashSet;
import java.util.Set;

public abstract class TaskAction {

    protected RunningTaskInfo.RunningTaskActionInfo mTaskActionInfo;
    private final Set<Class<? extends PlayerGameEvent>> mRegisterGameEvents = new HashSet<>();

    public TaskAction(RunningTaskInfo.RunningTaskActionInfo taskActionInfo) {
        mTaskActionInfo = taskActionInfo;
    }

    protected void registerGameEvent(Class<? extends PlayerGameEvent> clazz){
        mRegisterGameEvents.add(clazz);
    }

    /*
    返回该动作是否已经完成
     */
    public boolean processGameEvent(PlayerGameEvent event){
        if(mRegisterGameEvents.contains(event.getClass())){
            return onGameEvent(event);
        }
        return isComplete();
    }

    protected void addActionCountOne(){
        mTaskActionInfo.replaceCount++;
    }

    public abstract String getActionLocalName();

    /*
    返回该动作是否已经完成
     */
    protected abstract boolean onGameEvent(PlayerGameEvent event);

    protected boolean isComplete(){
        return mTaskActionInfo.replaceCount >= mTaskActionInfo.romInfo.requireReplaceCount;
    }
}
