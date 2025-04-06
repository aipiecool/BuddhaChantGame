package network.socket;

public interface SocketListener {

    void onReceive(SocketAddress address, byte[] bytes) throws Exception;

    void onError(Exception e);
}