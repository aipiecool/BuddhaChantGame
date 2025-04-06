package log;

import log.inputs.impls.SimpleTimeLogInput;
import log.inputs.LogInput;
import log.outputs.MultipleLogOutput;
import log.outputs.impls.SystemLogOutput;

public class Log {

    public static volatile int sOutputLevel = 1;

    private static final LogInput sInput = new SimpleTimeLogInput(new MultipleLogOutput(new SystemLogOutput()));

    public static LogInput input(){
        return sInput;
    }

    public static void setOutputLevel(int level){
        Log.input().info("改变日志输出等级:" + level);
        sOutputLevel = level;
    }
}
