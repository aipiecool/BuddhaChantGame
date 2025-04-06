package network.socket.actions;

import java.util.HashSet;

public abstract class PackageListener {
    private HashSet<ClientPackage> mSubscribePackages = new HashSet<>();

    public void subscribePackages(ClientPackage clientPackage)
    {
        mSubscribePackages.add(clientPackage);
    }

    public void unsubscribePackages(ClientPackage clientPackage)
    {
        mSubscribePackages.add(clientPackage);
    }

    public boolean isSubscribe(ClientPackage clientPackage)
    {
        return mSubscribePackages.contains(clientPackage);
    }

    public abstract void onReceive(ClientPackage clientPackage) throws Exception;
}
