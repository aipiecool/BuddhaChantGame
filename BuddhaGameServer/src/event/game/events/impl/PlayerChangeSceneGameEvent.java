package event.game.events.impl;

import event.game.events.PlayerGameEvent;
import model.player.Player;

public class PlayerChangeSceneGameEvent extends PlayerGameEvent {

    private int mLastSceneId;
    private int mToSceneId;

    public PlayerChangeSceneGameEvent(Player player, int lastSceneId, int toSceneId) {
        super(player);
        mLastSceneId = lastSceneId;
        mToSceneId = toSceneId;
    }

    public int getLastSceneId() {
        return mLastSceneId;
    }

    public int getToSceneId() {
        return mToSceneId;
    }
}
