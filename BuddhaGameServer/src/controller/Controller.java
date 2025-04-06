package controller;

import network.NetworkFactory;
import network.socket.actions.ClientPackage;
import network.socket.actions.PackageListener;
import network.socket.SocketPackageNet;

public abstract class Controller {

    protected final ControllerPackageListener mPackageListener;
    protected final SocketPackageNet mSocket;

    public Controller() {
        mSocket = NetworkFactory.getSocketNet();
        mPackageListener = new ControllerPackageListener();
        mSocket.addPackageListeners(mPackageListener);
    }

    public static class ControllerPackageListener extends PackageListener {

        @Override
        public void onReceive(ClientPackage clientPackage) throws Exception {
            clientPackage.process();
        }
    }
}
