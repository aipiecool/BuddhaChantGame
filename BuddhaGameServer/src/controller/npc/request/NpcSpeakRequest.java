package controller.npc.request;

import event.game.GameEventsMamager;
import event.game.events.impl.PlayerSpeakEndWithNpcGameEvent;
import event.game.events.impl.PlayerSpeakStartWithNpcGameEvent;
import model.npc.NpcsManager;
import model.player.Player;
import model.player.PlayersManager;
import model.task.entity.rom.GameChildTask;
import model.task.entity.rom.TaskInfo;
import model.task.entity.running.RunningGameTask;
import network.NetworkFactory;
import network.socket.actions.ClientPackage;
import network.socket.actions.ClientRequestPackage;
import network.socket.actions.prefabs.Server.SimpleServerResponsePackage;
import network.socket.actions.unpackage.ClientRequestJsonUnpackage;
import network.socket.actions.unpackage.ClientUnpackage;
import utils.JsonUtils;

import java.util.List;

public class NpcSpeakRequest extends ClientRequestPackage {

    private String mNpcName;

    public NpcSpeakRequest(ClientUnpackage unpackage) throws Exception {
        super(unpackage);
        ClientRequestJsonUnpackage jsonUnpackage = new ClientRequestJsonUnpackage(mRequestUnpackage);
        mNpcName = jsonUnpackage.getValue("npcName");
    }

    public NpcSpeakRequest() {

    }

    @Override
    public void process() throws Exception {
        Player player = PlayersManager.get().getPlayer(mUnpackage.getAddress());
        if(player != null){
            GameEventsMamager.get().sendEvent(new PlayerSpeakStartWithNpcGameEvent(player, NpcsManager.get().getNpcsByName(mNpcName)));
            NpcSpeakResponse npcSpeakResponse = new NpcSpeakResponse();
            List<GameChildTask> activeAndNotGetChildTasks = player.getActiveAndNotGetChildTasks();
            boolean find = false;
            synchronized (player.mActiveAndNotGetChildTasksLock) {
                for (GameChildTask gameChildTask : activeAndNotGetChildTasks) {
                    for (TaskInfo.NpcSpeak npcSpeak : gameChildTask.getChildTaskInfo().unCompleteNpcSpeak) {
                        if (npcSpeak.npcName.equals(mNpcName)) {
                            npcSpeakResponse.firstSpeak = npcSpeak.npcFirstSpeak;
                            npcSpeakResponse.secondSpeak = npcSpeak.npcSecondSpeak;
                            find = true;
                            break;
                        }
                    }
                    if (find) {
                        break;
                    }
                }
            }

            if(!find) {
                List<RunningGameTask> runningGameTasks = player.getRunningGameTasks();
                synchronized (player.mRunningGameTasksLock) {
                    for (RunningGameTask runningGameTask : runningGameTasks) {
                        for (TaskInfo.NpcSpeak npcSpeak : runningGameTask.getChildTaskInfo().completeNpcSpeak) {
                            if (npcSpeak.npcName.equals(mNpcName)) {
                                npcSpeakResponse.firstSpeak = npcSpeak.npcFirstSpeak;
                                npcSpeakResponse.secondSpeak = npcSpeak.npcSecondSpeak;
                                find = true;
                                break;
                            }
                        }
                        if (find) {
                            break;
                        }
                    }
                }
            }


            if(!find){
                npcSpeakResponse.firstSpeak = "";
                npcSpeakResponse.secondSpeak = "";
            }
            String json = JsonUtils.serialize(npcSpeakResponse);
            NetworkFactory.getSocketNet().sendPackage(new SimpleServerResponsePackage(
                    mRequestUnpackage.getAddress(),
                    mRequestUnpackage.getPackageId(),
                    "NpcSpeakResponse",
                    1,
                    json
            ));
            GameEventsMamager.get().sendEvent(new PlayerSpeakEndWithNpcGameEvent(player, NpcsManager.get().getNpcsByName(mNpcName)));
        }
    }

    @Override
    public String getHeaderString() {
        return "NpcSpeakRequest";
    }

    @Override
    public ClientPackage create(ClientUnpackage unpackage) throws Exception {
        return new NpcSpeakRequest(unpackage);
    }

    public static class NpcSpeakResponse
    {
        public String firstSpeak;
        public String secondSpeak;
    }
}
