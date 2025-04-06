package network.socket;

import log.Log;
import network.socket.actions.*;
import network.socket.actions.unpackage.ClientRequestUnpackage;
import network.socket.actions.unpackage.ClientUnpackage;
import utils.BytesUtils;

import java.io.UnsupportedEncodingException;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;

public class SocketPackageNet {

    private final SocketNet mSocketNet;
    protected List<PackageListener> mPackageListeners;
    private final LinkedList<ServerResponsePackage> mResponsePackagesCache = new LinkedList<>();

    public SocketPackageNet(SocketNet socketNet) {
        mSocketNet = socketNet;
        mSocketNet.addReceiver(new SocketReceiveCallback());
        mPackageListeners = new LinkedList<>();
        new Thread(new ResponsePackageCheckThread()).start();
    }

    public void release()
    {
        mSocketNet.release();
    }

    public void sendPackage(ServerPackage pkg) throws UnsupportedEncodingException {
        sendPackage(pkg, true);
    }

    public void sendPackage(ServerPackage pkg, boolean addToCache) throws UnsupportedEncodingException {
        mSocketNet.sendBytes(pkg.getBytes(), pkg.getAddress());
        if(addToCache && pkg instanceof ServerResponsePackage){
            Log.input().warn("发送:" + pkg.toString() + ", to:" + pkg.getAddress().toString() + ", header:" + BytesUtils.bytes2Int(pkg.getHeader()));
            ServerResponsePackage responsePackage = (ServerResponsePackage) pkg;
            responsePackage.setLifeTime(60);
            if(!mResponsePackagesCache.contains(pkg)){
                synchronized (mResponsePackagesCache) {
                    mResponsePackagesCache.add(responsePackage);
                }
            }
        }
    }

    private ServerResponsePackage searchInResponseCache(ClientRequestUnpackage unpackage){
        SocketAddress address = unpackage.getAddress();
        int packageId = unpackage.getPackageId();
        synchronized (mResponsePackagesCache){
            for(ServerResponsePackage cache : mResponsePackagesCache){
                if(cache.getAddress().equals(address) && cache.getPackageId() == packageId){
                    return cache;
                }
            }
        }
        return null;
    }

    private boolean interceptByResponseCache(ClientPackage clientPackage) throws UnsupportedEncodingException {
        if(clientPackage instanceof ClientRequestPackage){
            ClientRequestPackage clientRequestPackage = (ClientRequestPackage) clientPackage;
            ServerResponsePackage cache = searchInResponseCache(clientRequestPackage.getRequestUnpackage());
            if(cache != null){
                Log.input().debug("使用响应缓存回复");
                sendPackage(cache, false);
                return true;
            }
        }
        return false;
    }

    public void addPackageListeners(PackageListener listener)
    {
        mPackageListeners.add(listener);

    }

    public void removePackageListeners(PackageListener listener)
    {
        mPackageListeners.remove(listener);
    }

    private class ResponsePackageCheckThread implements Runnable{

        @Override
        public void run() {
            while (true) {
                synchronized (mResponsePackagesCache) {
                    Iterator<ServerResponsePackage> iterator = mResponsePackagesCache.iterator();
                    while (iterator.hasNext()){
                        ServerResponsePackage responsePackage = iterator.next();
                        int lifetime = responsePackage.getLifetime();
                        if(lifetime <= 0){
                            iterator.remove();
                        }else {
                            responsePackage.setLifeTime(lifetime - 1);
                        }
                    }
                }
                try {
                    Thread.sleep(1000);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
            }
        }
    }

    private class SocketReceiveCallback implements SocketListener {

        @Override
        public void onReceive(SocketAddress address, byte[] bytes) throws Exception {
            ClientUnpackage unpackage = new ClientUnpackage(bytes, address);
            ClientPackage packageInstance = ClientPackagesManager.get().createInstanceByUnpackage(unpackage);
            if(packageInstance != null) {
                if(!interceptByResponseCache(packageInstance)) {
                    Log.input().debug("收到:" + packageInstance.toString() + ", from:" + address.toString());
                    for (PackageListener l : mPackageListeners) {
                        if (l.isSubscribe(packageInstance)) {
                            l.onReceive(packageInstance);
                        }
                    }
                }
            }
        }

        @Override
        public void onError(Exception e) {
            e.printStackTrace();
        }
    }

}
