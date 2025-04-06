package controller.realtime.actions.task;

import controller.realtime.actions.server.OtherPlayersPosition;
import network.NetworkFactory;
import network.socket.SocketPackageNet;
import model.player.Player;
import model.player.PlayersManager;
import utils.BytesUtils;

import java.io.UnsupportedEncodingException;
import java.util.Collection;
import java.util.HashMap;
import java.util.List;

public class PositionPostTask implements Runnable{

    HashMap<Integer, byte[]> mPositionCache = new HashMap<>();
    private final PlayersManager mPlayersManager;
    private final SocketPackageNet mSocket;

    public PositionPostTask() {
        mPlayersManager = PlayersManager.get();
        mSocket = NetworkFactory.getSocketNet();
    }

    @Override
    public void run() {
        while (true){
            mPositionCache.clear();
            try {
                synchronized (PlayersManager.sPlayerLock) {
                    Collection<Player> onlinePlayers = mPlayersManager.getOnlinePlayers();
                    for (Player player : onlinePlayers) {
                        int sceneId = player.getInfo().sceneId;
                        boolean intoworld = player.isIntoworld();
                        if (sceneId == 0 || !intoworld) {
                            continue;
                        }
                        byte[] body = mPositionCache.get(sceneId);
                        if (body == null) {
                            List<Player> playersByScenes = mPlayersManager.getPlayersByScenes(sceneId);
                            if (playersByScenes != null) {
                                int size = playersByScenes.size();
                                body = new byte[8 + size * 16];
                                BytesUtils.fillBytesWithInt(body, sceneId, 0);
                                BytesUtils.fillBytesWithInt(body, size, 4);
                                int index = 0;
                                for (Player playerOfScene : playersByScenes) {
                                    if (playerOfScene.isIntoworld()) {
                                        int userId = playerOfScene.getInfo().userId;
                                        float posX = playerOfScene.getInfo().positionX;
                                        float posY = playerOfScene.getInfo().positionY;
                                        int dir = playerOfScene.getInfo().direction;
                                        int start = 8 + index * 16;
                                        BytesUtils.fillBytesWithInt(body, userId, start);
                                        BytesUtils.fillBytesWithFloat(body, posX, start + 4);
                                        BytesUtils.fillBytesWithFloat(body, posY, start + 8);
                                        BytesUtils.fillBytesWithInt(body, dir, start + 12);
                                        index++;
                                    }
                                }
                                mPositionCache.put(sceneId, body);
                            }
                        }
                        if (body != null) {
                            OtherPlayersPosition serverPackage = new OtherPlayersPosition(player.getAddress(), body);
                            mSocket.sendPackage(serverPackage);
                        }
                    }
                }
                Thread.sleep(1000);
            } catch (InterruptedException | UnsupportedEncodingException e) {
                e.printStackTrace();
            }
        }
    }
}
