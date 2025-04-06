package event.server.events.impl;

import event.server.events.PlayerServerEvent;
import model.player.Player;

public class ClientNeedFlowTextServerEvent extends PlayerServerEvent {

    private String mMessage;
    private String mSound;

    public ClientNeedFlowTextServerEvent(Player player, String message, String sound) {
        super(player);
        mMessage = message;
        mSound = sound;
    }

    public String getMessage() {
        return mMessage;
    }

    public String getSound() {
        return mSound;
    }
}
