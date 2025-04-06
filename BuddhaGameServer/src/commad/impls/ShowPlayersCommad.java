package commad.impls;

import commad.Commad;
import model.player.Player;
import model.player.PlayersManager;

import java.util.Collection;
import java.util.InputMismatchException;
import java.util.Scanner;

public class ShowPlayersCommad extends Commad {
    @Override
    public String getCommad() {
        return "model/player";
    }

    @Override
    public String getSimpleCommad() {
        return "p";
    }

    @Override
    public String getHelpMessage() {
        return "获取全部在线玩家信息";
    }

    @Override
    public void process(Scanner parms) throws InputMismatchException {
        synchronized (PlayersManager.sPlayerLock){
            StringBuilder stringBuilder = new StringBuilder();
            Collection<Player> onlinePlayers = PlayersManager.get().getOnlinePlayers();
            stringBuilder.append("当前玩家数:").append(onlinePlayers.size());
            int lineCount = 0;
            for (Player player : onlinePlayers){
                if(lineCount % 5 == 0){
                    stringBuilder.append("\n");
                }
                stringBuilder.append(player.getInfo().username);
                stringBuilder.append("   ");
                lineCount++;
            }
            System.out.println(stringBuilder.toString());
        }
    }
}
