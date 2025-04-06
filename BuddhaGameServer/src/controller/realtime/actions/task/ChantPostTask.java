package controller.realtime.actions.task;

import controller.realtime.actions.server.OtherPlayersChant;
import network.NetworkFactory;
import network.socket.SocketPackageNet;
import model.player.Player;
import model.player.PlayersManager;
import utils.BytesUtils;

import java.io.UnsupportedEncodingException;
import java.util.Collection;
import java.util.HashMap;
import java.util.List;

public class ChantPostTask implements Runnable{

    HashMap<Integer, byte[]> mChantCache = new HashMap<>();
    private final PlayersManager mPlayersManager;
    private final SocketPackageNet mSocket;

    public ChantPostTask() {
        mPlayersManager = PlayersManager.get();
        mSocket = NetworkFactory.getSocketNet();
    }

    @Override
    public void run() {
        while (true){
            mChantCache.clear();
            try {
                synchronized (PlayersManager.sPlayerLock) {
                    Collection<Player> onlinePlayers = mPlayersManager.getOnlinePlayers();
                    for (Player player : onlinePlayers) {
                        int sceneId = player.getInfo().sceneId;
                        boolean intoworld = player.isIntoworld();
                        if (sceneId == 0 || !intoworld) {
                            continue;
                        }
                        byte[] body = mChantCache.get(sceneId);
                        if (body == null) {
                            List<Player> playersByScenes = mPlayersManager.getPlayersByScenes(sceneId);
                            if (playersByScenes != null) {
                                int size = playersByScenes.size();
                                body = new byte[8 + size * 12];
                                BytesUtils.fillBytesWithInt(body, sceneId, 0);
                                BytesUtils.fillBytesWithInt(body, size, 4);
                                int index = 0;
                                for (Player playerOfScene : playersByScenes) {
                                    if (playerOfScene.isIntoworld()) {
                                        int userId = playerOfScene.getInfo().userId;
                                        int wordId = playerOfScene.getChantWordId();
                                        int chantCount = playerOfScene.getChantCountAndRest();
                                        int start = 8 + index * 12;
                                        BytesUtils.fillBytesWithInt(body, userId, start);
                                        BytesUtils.fillBytesWithInt(body, wordId, start + 4);
                                        BytesUtils.fillBytesWithInt(body, chantCount, start + 8);
                                        index++;
                                    }
                                }
                                mChantCache.put(sceneId, body);
                            }
                        }
                        if (body != null) {
                            OtherPlayersChant serverPackage = new OtherPlayersChant(player.getAddress(), body);
                            mSocket.sendPackage(serverPackage);
                        }
                    }
                }
                Thread.sleep(5000);
            } catch (InterruptedException | UnsupportedEncodingException e) {
                e.printStackTrace();
            }
        }
    }
}
