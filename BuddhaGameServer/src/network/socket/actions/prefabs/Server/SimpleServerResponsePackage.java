package network.socket.actions.prefabs.Server;

import network.socket.actions.ServerResponsePackage;
import network.socket.SocketAddress;

import java.io.UnsupportedEncodingException;

public class SimpleServerResponsePackage extends ServerResponsePackage {

    protected final String mHeaderString;

    public SimpleServerResponsePackage(SocketAddress address, int packageId, String headerString, int code, String message) throws UnsupportedEncodingException {
        super(address, packageId, code, message);
        mHeaderString = headerString;
    }

    @Override
    public String getHeaderString() {
        return mHeaderString;
    }
}
