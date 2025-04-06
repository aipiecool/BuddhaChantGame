package model.task.action.impl;

import model.chantable.Chantable;
import model.chantable.ChantablesManager;
import event.game.events.PlayerGameEvent;
import event.game.events.impl.PlayerChantGameEvent;
import model.task.action.TaskAction;
import model.task.entity.running.RunningTaskInfo;

public class ChantOrWriteAction extends TaskAction {

    private final Chantable mChantable;
    private final int mMaxCount;

    public ChantOrWriteAction(RunningTaskInfo.RunningTaskActionInfo taskActionInfo) {
        super(taskActionInfo);
        mChantable = ChantablesManager.get().getChantableByName(taskActionInfo.romInfo.triggerObject);
        mMaxCount = taskActionInfo.romInfo.requireReplaceCount;
        registerGameEvent(PlayerChantGameEvent.class);
    }

    @Override
    public String getActionLocalName() {
        return "念诵" + mChantable.getInfo().localName + "佛号" + mMaxCount + "遍";
    }

    @Override
    public boolean onGameEvent(PlayerGameEvent event) {
        PlayerChantGameEvent gameEvent = (PlayerChantGameEvent)event;
        if(gameEvent.getChantable().equals(mChantable)){
            addActionCountOne();
            return isComplete();
        }
        return false;
    }
}
