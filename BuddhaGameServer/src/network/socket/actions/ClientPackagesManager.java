package network.socket.actions;

import network.socket.actions.unpackage.ClientUnpackage;

import java.util.HashMap;

public class ClientPackagesManager {

    private static ClientPackagesManager sInstance;
    private HashMap<Integer, ClientPackage> mClientPackages = new HashMap<>();

    private ClientPackagesManager(){

    }

    public static ClientPackagesManager get(){
        if(sInstance == null) {
            synchronized (ClientPackagesManager.class) {
                if (sInstance == null) {
                    sInstance = new ClientPackagesManager();
                }
            }
        }
        return sInstance;
    }

    public void registerClientPackage(ClientPackage clientPackage)
    {
        mClientPackages.put(clientPackage.getHeader(), clientPackage);

    }

    public ClientPackage createInstanceByUnpackage(ClientUnpackage unpackage) throws Exception {
        ClientPackage clientPackage = searchClientPackage(unpackage);
        if(clientPackage != null)
        {
            return clientPackage.create(unpackage);
        }
        return null;
    }

    private ClientPackage searchClientPackage(ClientUnpackage unpackage)
    {
        return mClientPackages.get(unpackage.getHeader());
    }
}
