package controller.realtime.actions.client;

import network.socket.actions.ClientPackage;
import network.socket.actions.unpackage.ClientUnpackage;
import model.player.Player;
import model.player.PlayersManager;
import utils.BytesUtils;

public class PlayerMove extends ClientPackage {

    private float mPositionX;
    private float mPositionY;
    private int mDirection;
    private int mSceneId;
    private int mEnterId;

    public PlayerMove(ClientUnpackage unpackage) throws Exception {
        super(unpackage);
        byte[] body = unpackage.getBody();
        mSceneId = BytesUtils.bytes2Int(BytesUtils.subbytes(body, 0, 4));
        mEnterId = BytesUtils.bytes2Int(BytesUtils.subbytes(body, 4, 8));
        mPositionX = BytesUtils.bytes2Float(BytesUtils.subbytes(body, 8, 12));
        mPositionY = BytesUtils.bytes2Float(BytesUtils.subbytes(body, 12, 16));
        mDirection = BytesUtils.bytes2Int(BytesUtils.subbytes(body, 16, 20));
    }

    public PlayerMove() {
    }

    @Override
    public void process() throws Exception {
        Player player = PlayersManager.get().getPlayer(mUnpackage.getAddress());
        if(player != null){
            player.setIntoworld(true);
            player.resetLifetime();
            player.setPosition(mSceneId, mEnterId, mPositionX, mPositionY, mDirection);
            //System.out.println("scene:" + mSceneId + ", pos(" + mPositionX + "," + mPositionY + ")");
        }

    }

    @Override
    public String getHeaderString() {
        return "PlayerMove";
    }

    @Override
    public ClientPackage create(ClientUnpackage unpackage) throws Exception {
        return new PlayerMove(unpackage);
    }
}
