package commad.impls;

import commad.Commad;

import java.util.InputMismatchException;
import java.util.Scanner;

public class ExitCommad extends Commad {
    @Override
    public String getCommad() {
        return "stop";
    }

    @Override
    public String getSimpleCommad() {
        return null;
    }

    @Override
    public String getHelpMessage() {
        return "退出服务器程序";
    }

    @Override
    public void process(Scanner parms) throws InputMismatchException {
        System.exit(1);
    }
}
