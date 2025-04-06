package network.socket;

import java.util.LinkedList;
import java.util.List;
import java.util.concurrent.*;

public abstract class SocketNet {
    protected int mPort;
    protected List<SocketListener> mReceivers;


    private ThreadPoolExecutor mExecutor;

    protected SocketNet(int port) {
        mPort = port;
        mReceivers = new LinkedList<>();
        mExecutor = new ThreadPoolExecutor(
                64,
                Integer.MAX_VALUE,
                1,
                TimeUnit.MINUTES,
                new LinkedBlockingQueue<Runnable>(8),
                Executors.defaultThreadFactory(),
                new ThreadPoolExecutor.DiscardPolicy()
        );
    }

    public abstract void sendBytes(byte[] data, SocketAddress socketAddress);

    public abstract void release();

    public void addReceiver(SocketListener r){
        mReceivers.add(r);
    }

    public void removeReceiver(SocketListener r){
        mReceivers.remove(r);
    }

    protected void notify(SocketAddress address, byte[] bytes) throws Exception {
        for(SocketListener l : mReceivers){
            mExecutor.execute(() -> {
                try {
                    l.onReceive(address, bytes);
                } catch (Exception e) {
                    e.printStackTrace();
                }
            });
        }
    }

    protected void notifyError(Exception e){
        for(SocketListener l : mReceivers){
            mExecutor.execute(() -> {
                l.onError(e);
            });
        }
    }


}
