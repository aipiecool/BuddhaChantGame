package controller.task;

import controller.Controller;
import controller.task.request.TaskDetalRequest;
import event.game.GameEventListener;
import event.game.GameEventsMamager;
import event.game.events.PlayerGameEvent;
import event.game.events.impl.*;
import model.player.Player;
import model.task.TasksManager;
import model.task.entity.rom.GameChildTask;
import model.task.entity.running.RunningGameTask;

import java.util.Iterator;
import java.util.List;

public class TaskController extends Controller {


    public TaskController() {
        super();
        mPackageListener.subscribePackages(new TaskDetalRequest());

        //监听任务激活
        GameEventsMamager.get().addListener(new GameEventListener<PlayerGameEvent>(
                PlayerLoginGameEvent.class,
                PlayerSpeakWithNpcGameEvent.class,
                PlayerCompleteChildTaskGameEvent.class
            ) {
            @Override
            protected void onReceive(PlayerGameEvent event) {
                Player player = event.getPlayer();
                List<GameChildTask> playerActiveAndNotGetChildTask = TasksManager.get().getPlayerActiveAndNotGetChildTask(player, event);
                player.setActiveAndNotGetChildTasks(playerActiveAndNotGetChildTask);
            }
        });

        //监听任务获得
        GameEventsMamager.get().addListener(new GameEventListener<PlayerGameEvent>(
                PlayerSpeakEndWithNpcGameEvent.class,
                PlayerChangeSceneGameEvent.class,
                PlayerCompleteChildTaskGameEvent.class
            ) {
            @Override
            protected void onReceive(PlayerGameEvent event) {
                Player player = event.getPlayer();
                List<GameChildTask> activeAndNotGetChildTasks = player.getActiveAndNotGetChildTasks();
                synchronized (player.mActiveAndNotGetChildTasksLock) {
                    Iterator<GameChildTask> iterator = activeAndNotGetChildTasks.iterator();
                    while (iterator.hasNext()){
                        GameChildTask task = iterator.next();
                        if (task.getParentTask().getGetCondition().isPass(event)) {
                            player.getAChildTask(iterator, task);
                        }
                    }
                }
            }
        });

        //监听任务动作
        GameEventsMamager.get().addListener(new GameEventListener<PlayerGameEvent>(
                PlayerSpeakEndWithNpcGameEvent.class,
                PlayerChantGameEvent.class
                ) {
            @Override
            protected void onReceive(PlayerGameEvent event) {
                Player player = event.getPlayer();
                List<RunningGameTask> runningGameTasks = player.getRunningGameTasks();
                synchronized (player.mRunningGameTasksLock) {
                    Iterator<RunningGameTask> iterator = runningGameTasks.iterator();
                    while (iterator.hasNext()) {
                        RunningGameTask next = iterator.next();
                        next.checkAtion(iterator, event);
                    }
                }
            }
        });
    }


}
