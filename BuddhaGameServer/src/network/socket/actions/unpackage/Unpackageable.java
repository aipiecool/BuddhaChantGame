package network.socket.actions.unpackage;

import network.socket.SocketAddress;

public interface Unpackageable {
    int getHeader();
    byte[] getBody();
    SocketAddress getAddress();
}
