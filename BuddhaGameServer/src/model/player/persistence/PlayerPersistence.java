package model.player.persistence;

import model.player.PlayerInfo;

import java.io.IOException;

public abstract class PlayerPersistence {

    private static PlayerPersistence sInstance;

    public static PlayerPersistence get(){
        if(sInstance == null){
            synchronized (PlayerPersistence.class){
                if(sInstance == null){
                    sInstance = new PlayerDatabasePersistence();
                }
            }
        }
        return sInstance;
    }

    public abstract void save(PlayerInfo info) throws IOException;
    public abstract PlayerInfo load(int userId, String username, String password) throws IOException;
}
