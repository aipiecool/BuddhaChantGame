package commad;

import commad.impls.ExitCommad;
import commad.impls.LogLevelCommad;
import commad.impls.ShowPlayersCommad;
import log.Log;

import java.util.ArrayList;
import java.util.InputMismatchException;
import java.util.List;
import java.util.Scanner;

public class CommadsManager {
    private final List<Commad> mCommads = new ArrayList<>();

    public CommadsManager() {
        mCommads.add(new HelpCommad());
        mCommads.add(new ExitCommad());
        mCommads.add(new ShowPlayersCommad());
        mCommads.add(new LogLevelCommad());

        new Thread(new InputRunable()).start();
    }

    private class InputRunable implements Runnable{

        @Override
        public void run() {
            while (true){
                Scanner input=new Scanner(System.in);
                String text = input.next();
                Log.input().info("输入命令:" + text);
                for(Commad commad : mCommads){
                    if(text.equalsIgnoreCase(commad.getCommad())
                            || (commad.getSimpleCommad() != null && text.equalsIgnoreCase(commad.getSimpleCommad()))){
                        try {
                            commad.process(input);
                        }catch (InputMismatchException e){
                            Log.input().info("命令参数不匹配:" + commad.getHelpMessage());
                        }
                    }
                }
            }
        }
    }

    private class HelpCommad extends Commad{

        @Override
        public String getCommad() {
            return "help";
        }

        @Override
        public String getSimpleCommad() {
            return null;
        }

        @Override
        public String getHelpMessage() {
            return "获取命令帮助信息";
        }

        @Override
        public void process(Scanner parms) throws InputMismatchException {
            StringBuilder stringBuilder = new StringBuilder();
            for(Commad commad : mCommads){
                stringBuilder.append(commad.getCommad());
                if(commad.getSimpleCommad() != null){
                    stringBuilder.append("(").append(commad.getSimpleCommad()).append(")");
                }
                if(commad.getHelpMessage() != null){
                    stringBuilder.append(":").append(commad.getHelpMessage());
                }
                stringBuilder.append("\n");
            }
            System.out.println(stringBuilder.toString());
        }
    }
}
