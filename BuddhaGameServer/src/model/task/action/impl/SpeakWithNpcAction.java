package model.task.action.impl;

import event.game.events.PlayerGameEvent;
import event.game.events.impl.PlayerSpeakEndWithNpcGameEvent;
import event.game.events.impl.PlayerSpeakWithNpcGameEvent;
import model.npc.Npc;
import model.npc.NpcsManager;
import model.task.action.TaskAction;
import model.task.entity.running.RunningTaskInfo;

public class SpeakWithNpcAction extends TaskAction {

    private final Npc mNpc;

    public SpeakWithNpcAction(RunningTaskInfo.RunningTaskActionInfo taskActionInfo) {
        super(taskActionInfo);
        mNpc = NpcsManager.get().getNpcsByName(taskActionInfo.romInfo.triggerObject);
        registerGameEvent(PlayerSpeakEndWithNpcGameEvent.class);
    }

    @Override
    public String getActionLocalName() {
        return "与" + mNpc.getNpcInfo().localName + "对话";
    }

    @Override
    public boolean onGameEvent(PlayerGameEvent event) {
        PlayerSpeakWithNpcGameEvent gameEvent = (PlayerSpeakWithNpcGameEvent) event;
        if(gameEvent.getNpc().equals(mNpc)){
            addActionCountOne();
            return isComplete();
        }
        return false;
    }
}
