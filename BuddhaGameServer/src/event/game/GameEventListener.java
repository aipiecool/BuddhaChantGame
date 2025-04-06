package event.game;

public abstract class GameEventListener<T extends GameEvent> {

    private final Class<? extends T>[] mRegisterEventClasses;

    @SafeVarargs
    public GameEventListener(Class<? extends T> ...tClass) {
        mRegisterEventClasses = tClass;
    }

    protected abstract void onReceive(T event);

    public void receive(GameEvent gameEvent) {
        for(Class<? extends T> clazz : mRegisterEventClasses) {
            if (clazz.isInstance(gameEvent)) {
                onReceive(clazz.cast(gameEvent));
                return;
            }
        }
    }
}
