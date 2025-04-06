package event.server;

public abstract class ServerEventListener<T extends ServerEvent> {

    private final Class<? extends T>[] mRegisterEventClasses;

    @SafeVarargs
    public ServerEventListener(Class<? extends T> ...tClass) {
        mRegisterEventClasses = tClass;
    }

    protected abstract void onReceive(T event);

    public void receive(ServerEvent ServerEvent) {
        for(Class<? extends T> clazz : mRegisterEventClasses) {
            if (clazz.isInstance(ServerEvent)) {
                onReceive(clazz.cast(ServerEvent));
                return;
            }
        }
    }
}
