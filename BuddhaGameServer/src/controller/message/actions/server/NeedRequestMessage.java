package controller.message.actions.server;

import network.socket.SocketAddress;
import network.socket.actions.ServerPackage;
import utils.BytesUtils;

import java.io.UnsupportedEncodingException;

public class NeedRequestMessage extends ServerPackage {

    private byte[] mBody;

    public NeedRequestMessage(SocketAddress address, String messageType) throws UnsupportedEncodingException {
        super(address);
        mBody = BytesUtils.string2Bytes(messageType);
    }

    @Override
    public String getHeaderString() {
        return "NeedRequestMessage";
    }

    @Override
    protected byte[] getBody() throws UnsupportedEncodingException {
        return mBody;
    }
}
