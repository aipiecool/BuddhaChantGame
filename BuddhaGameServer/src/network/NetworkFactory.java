package network;

import network.socket.SocketPackageNet;
import network.socket.udp.UDPSocketNet;

public class NetworkFactory {

    public static final int SERVER_UDP_PORT = 10100;

    private static SocketPackageNet sSocketNet;

    public static SocketPackageNet getSocketNet(){
        if(sSocketNet == null){
            synchronized (NetworkFactory.class){
                if(sSocketNet == null){
                    sSocketNet = new SocketPackageNet(new UDPSocketNet(SERVER_UDP_PORT));
                }
            }
        }
        return sSocketNet;
    }
}
