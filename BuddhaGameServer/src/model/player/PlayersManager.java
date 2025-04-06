package model.player;

import event.game.GameEventsMamager;
import event.game.events.impl.PlayerChangeSceneGameEvent;
import network.socket.SocketAddress;
import model.player.persistence.PlayerPersistence;
import utils.DatetimeUtils;

import log.Log;
import java.io.IOException;
import java.util.*;

public class PlayersManager {

    private static PlayersManager sInstance;
    public static final Object sPlayerLock = new Object();

    final private Map<Integer, Player> mUserId2Player = new Hashtable<>();
    final private Map<SocketAddress, Player> mAddress2Player = new Hashtable<>();
    final private Map<Integer, List<Player>> mScenesPlayerMap = new Hashtable<>();

    final private Thread mCheckOfflineThread;

    private PlayersManager() {
        mCheckOfflineThread = new Thread(new CheckOfflineRunable());
        mCheckOfflineThread.start();
    }

    public void savePlayer(Player player) throws IOException {
        player.getInfo().lastSaveDatatime = DatetimeUtils.getTimestampsLong();
        PlayerPersistence.get().save(player.getInfo());
    }

    public void addPlayer(Player player){
        synchronized (mUserId2Player) {
            mUserId2Player.put(player.getInfo().userId, player);
            mAddress2Player.put(player.getAddress(), player);
            Log.input().info(player.getInfo().username + "加入了游戏, 当前玩家数:" + mUserId2Player.size() + ", from:" + player.getAddress().toString()); ;
        }
    }

    public void removePlayer(Player player) throws IOException {
        //mUserId2Player.remove(model.player.getInfo().userId);
        mAddress2Player.remove(player.getAddress());
        List<Player> scenePlayers = mScenesPlayerMap.get(player.getInfo().sceneId);
        if(scenePlayers != null){
            scenePlayers.remove(player);
        }
        savePlayer(player);
        Log.input().info(player.getInfo().username  + "离开了游戏, 当前玩家数:" + mUserId2Player.size() + ", from:" + player.getAddress().toString());
    }

    public void playerChangeScene(Player player, int lastSceneId, int toSceneId){
        List<Player> lastScenePlayers = mScenesPlayerMap.get(lastSceneId);
        List<Player> toScenePlayers = mScenesPlayerMap.get(toSceneId);
        if(lastScenePlayers != null){
            lastScenePlayers.remove(player);
        }
        if(toScenePlayers == null){
            toScenePlayers = new LinkedList<>();
            mScenesPlayerMap.put(toSceneId, toScenePlayers);
        }
        if(!toScenePlayers.contains(player)){
            toScenePlayers.add(player);
        }
        GameEventsMamager.get().sendEvent(new PlayerChangeSceneGameEvent(player, lastSceneId, toSceneId));
    }

    public List<Player> getPlayersByScenes(int sceneId){
        return mScenesPlayerMap.get(sceneId);
    }

    public Collection<Player> getOnlinePlayers(){
        return mUserId2Player.values();
    }

    public Player getPlayer(int userId){
        return mUserId2Player.get(userId);
    }

    public Player getPlayer(SocketAddress address){
        return mAddress2Player.get(address);
    }

    public static PlayersManager get(){
        if(sInstance == null){
            synchronized (PlayersManager.class){
                if(sInstance == null){
                    sInstance = new PlayersManager();
                }
            }
        }
        return sInstance;
    }

    private class CheckOfflineRunable implements Runnable{

        @Override
        public void run() {
            while(true){
                try {
                    synchronized (sPlayerLock) {
                        Iterator<Map.Entry<Integer, Player>> it = mUserId2Player.entrySet().iterator();
                        while (it.hasNext()) {
                            Player player = it.next().getValue();
                            player.decLifetime();
                            if (player.getLifetime() < 0) {
                                it.remove();
                                removePlayer(player);
                            }
                        }
                    }
                    Thread.sleep(1000);
                } catch (InterruptedException | IOException e) {
                    e.printStackTrace();
                }
            }
        }
    }
}
