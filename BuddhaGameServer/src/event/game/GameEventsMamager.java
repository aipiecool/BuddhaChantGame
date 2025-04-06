package event.game;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.*;

public class GameEventsMamager {

    private static GameEventsMamager sInstance;

    private final List<GameEventListener<?>> mGameEventListeners = new ArrayList<>();

    private final ExecutorService mExecutorService;

    private GameEventsMamager() {
        mExecutorService = new ThreadPoolExecutor(0, 100,
                60L, TimeUnit.SECONDS,
                new ArrayBlockingQueue<>(100));
    }

    public static GameEventsMamager get(){
        if(sInstance == null){
            synchronized (GameEventsMamager.class){
                if(sInstance == null){
                    sInstance = new GameEventsMamager();
                }
            }
        }
        return sInstance;
    }

    public void addListener(GameEventListener<?> listener){
        mGameEventListeners.add(listener);
    }

    public void sendEvent(GameEvent event){
        for(GameEventListener<?> listener : mGameEventListeners){
            listener.receive(event);
        }
    }

    public void sendEventAsyn(GameEvent event) {
        for(GameEventListener<?> listener : mGameEventListeners){
            mExecutorService.execute(() -> listener.receive(event));
        }
    }
}
