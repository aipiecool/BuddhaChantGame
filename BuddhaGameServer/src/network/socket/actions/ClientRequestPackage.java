package network.socket.actions;

import network.socket.actions.unpackage.ClientRequestUnpackage;
import network.socket.actions.unpackage.ClientUnpackage;

public abstract class ClientRequestPackage extends ClientPackage{

    protected ClientRequestUnpackage mRequestUnpackage;

    public ClientRequestPackage(ClientUnpackage unpackage) throws Exception {
        super(unpackage);
        mRequestUnpackage  = new ClientRequestUnpackage(mUnpackage);
    }

    public ClientRequestPackage() {
    }

    public ClientRequestUnpackage getRequestUnpackage() {
        return mRequestUnpackage;
    }
}
