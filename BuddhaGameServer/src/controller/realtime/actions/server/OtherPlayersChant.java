package controller.realtime.actions.server;

import network.socket.actions.ServerPackage;
import network.socket.SocketAddress;

import java.io.UnsupportedEncodingException;

public class OtherPlayersChant extends ServerPackage {

    private byte[] mBody;

    public OtherPlayersChant(SocketAddress address, byte[] body) {
        super(address);
        mBody = body;
    }

    @Override
    public String getHeaderString() {
        return "OtherPlayersChant";
    }

    @Override
    protected byte[] getBody() throws UnsupportedEncodingException {
        return mBody;
    }
}
