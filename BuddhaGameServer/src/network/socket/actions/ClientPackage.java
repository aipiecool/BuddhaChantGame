package network.socket.actions;

import network.socket.actions.unpackage.ClientUnpackage;
import utils.EncodeUtils;

public abstract class ClientPackage {

    private int mHeader = 0;
    protected ClientUnpackage mUnpackage;

    public ClientPackage(ClientUnpackage unpackage)
    {
        mUnpackage = unpackage;
    }

    public ClientPackage()
    {
        ClientPackagesManager.get().registerClientPackage(this);
    }

    public abstract void process() throws Exception;

    public abstract String getHeaderString();

    public abstract ClientPackage create(ClientUnpackage unpackage) throws Exception;

    public int getHeader()
    {
        if (mHeader == 0)
        {
            mHeader = EncodeUtils.string2HashCode(getHeaderString());
        }
        return mHeader;
    }

    @Override
    public int hashCode() {
        return getHeader();
    }

    @Override
    public boolean equals(Object obj) {
        if(obj instanceof ClientPackage){
            return obj.hashCode() == hashCode();
        }
        return false;
    }
}
