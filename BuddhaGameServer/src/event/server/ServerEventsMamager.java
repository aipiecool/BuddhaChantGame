package event.server;

import java.util.ArrayList;
import java.util.List;

public class ServerEventsMamager {

    private static ServerEventsMamager sInstance;

    private final List<ServerEventListener<?>> mServerEventListeners = new ArrayList<>();

    private ServerEventsMamager() {

    }

    public static ServerEventsMamager get(){
        if(sInstance == null){
            synchronized (ServerEventsMamager.class){
                if(sInstance == null){
                    sInstance = new ServerEventsMamager();
                }
            }
        }
        return sInstance;
    }

    public void addListener(ServerEventListener<?> listener){
        mServerEventListeners.add(listener);
    }

    public void sendEvent(ServerEvent event){
        for(ServerEventListener<?> listener : mServerEventListeners){
            listener.receive(event);
        }
    }
}
