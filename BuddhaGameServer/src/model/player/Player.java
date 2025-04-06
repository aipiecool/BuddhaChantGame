package model.player;

import event.server.events.impl.ClientNeedFlowTextServerEvent;
import model.good.entity.running.RunningGoods;
import model.task.entity.rom.GameChildTask;
import model.task.entity.rom.GameTask;
import model.task.entity.running.RunningGameTask;
import model.task.entity.running.RunningTaskInfo;
import network.socket.SocketAddress;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;

public class Player {

    private final static int sMAX_LIFETIME = 6;

    private PlayerInfo mInfo;
    private SocketAddress mAddress;
    private int mLastSceneId = 0;
    private int mLifetime = 6;
    private boolean mIsIntoworld = false;
    private int mChantCount = 0;
    private int mChantWordId = 0;
    private List<GameChildTask> mActiveAndNotGetChildTasks = new ArrayList<>();
    public final Object mActiveAndNotGetChildTasksLock = new Object();
    private List<RunningGameTask> mRunningGameTasks = new ArrayList<>();
    public final Object mRunningGameTasksLock = new Object();
    private List<ClientNeedFlowTextServerEvent> mFlowTextEvent = new LinkedList<>();
    private RunningGoods mRunningGoods;


    public Player(PlayerInfo info, SocketAddress address) {
        mInfo = info;
        mAddress = address;
        for(RunningTaskInfo runningTaskInfo : info.runningTaskInfos) {
            mRunningGameTasks.add(new RunningGameTask(runningTaskInfo));
        }
        mRunningGoods = new RunningGoods(info.goods);
    }

    public void putChantBuffer(int wordId, int count){
        mChantWordId = wordId;
        mChantCount += count;
    }

    public int getChantCountAndRest() {
        int ret = mChantCount;
        mChantCount = 0;
        return ret;
    }

    public int getChantWordId() {
        return mChantWordId;
    }

    public PlayerInfo getInfo() {
        return mInfo;
    }

    public SocketAddress getAddress() {
        return mAddress;
    }

    public void setPosition(int sceneId, int enterId, float positionX, float positionY, int direction) {
        if(mLastSceneId != sceneId){
            PlayersManager.get().playerChangeScene(this, mLastSceneId, sceneId);
        }
        mInfo.sceneId = sceneId;
        mInfo.enterId = enterId;
        mInfo.positionX = positionX;
        mInfo.positionY = positionY;
        mInfo.direction = direction;
        mLastSceneId = sceneId;
    }

    public boolean isIntoworld() {
        return mIsIntoworld;
    }

    public void setIntoworld(boolean intoworld) {
        mIsIntoworld = intoworld;
    }

    public int getLifetime() {
        return mLifetime;
    }

    public void resetLifetime() {
        this.mLifetime = sMAX_LIFETIME;
    }

    public void decLifetime() {
        this.mLifetime--;
    }

    public List<GameChildTask> getActiveAndNotGetChildTasks() {
        synchronized (mActiveAndNotGetChildTasksLock) {
            return mActiveAndNotGetChildTasks;
        }
    }

    public void setActiveAndNotGetChildTasks(List<GameChildTask> activeAndNotGetChildTasks) {
        synchronized (mActiveAndNotGetChildTasksLock) {
            mActiveAndNotGetChildTasks = activeAndNotGetChildTasks;
        }
    }

    public void getAChildTask(Iterator<GameChildTask> iterator, GameChildTask task) {
        synchronized (mActiveAndNotGetChildTasksLock){
            iterator.remove();
        }
        synchronized (mRunningGameTasksLock) {
            mRunningGameTasks.add(new RunningGameTask(task));
        }
    }

    public void completeAChildTask(Iterator<RunningGameTask> iterator, RunningGameTask task) {
        iterator.remove();
        mInfo.completeChildTasksId.add(task.getChildTaskInfo().taskId);
    }

    public void completeAParentTask(GameTask task) {
        mInfo.completeTasksId.add(task.getTaskInfo().taskId);
    }

    public List<RunningGameTask> getRunningGameTasks() {
        return mRunningGameTasks;
    }

    public void addFlowTextEvent(ClientNeedFlowTextServerEvent event) {
        mFlowTextEvent.add(event);
    }

    public List<ClientNeedFlowTextServerEvent> getFlowTextEvent() {
        return mFlowTextEvent;
    }

    public void removeAllFlowTextEvent() {
        mFlowTextEvent.clear();
    }

    public RunningGoods getRunningGoods() {
        return mRunningGoods;
    }
}
