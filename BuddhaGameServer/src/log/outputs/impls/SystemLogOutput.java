package log.outputs.impls;

import log.Log;
import log.outputs.LogOutput;

public class SystemLogOutput extends LogOutput {
    @Override
    public void write(int level, String message) {
        if(level >= Log.sOutputLevel) {
            if (level == 2) {
                System.err.println(message);
            } else {
                System.out.println(message);
            }
        }
    }
}
