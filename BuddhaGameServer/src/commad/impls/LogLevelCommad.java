package commad.impls;

import commad.Commad;
import log.Log;

import java.util.InputMismatchException;
import java.util.Scanner;

public class LogLevelCommad extends Commad {
    @Override
    public String getCommad() {
        return "loglevel";
    }

    @Override
    public String getSimpleCommad() {
        return "logl";
    }

    @Override
    public String getHelpMessage() {
        return "设置输出日志的等级 0:debug, 1:info, 2:warn";
    }

    @Override
    public void process(Scanner parms) throws InputMismatchException {
        int level = parms.nextInt();
        Log.setOutputLevel(level);
    }
}
