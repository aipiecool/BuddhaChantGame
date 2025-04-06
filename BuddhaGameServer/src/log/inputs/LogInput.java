package log.inputs;

import log.outputs.LogOutput;

public abstract class LogInput {

    protected LogOutput mLogOutput;

    public LogInput(LogOutput logOutput) {
        mLogOutput = logOutput;    }

    public void debug(String message){
        write(0, message);
    }
    public void info(String message){
        write(1, message);
    }
    public void warn(String message){
        write(2, message);
    }

    protected abstract void write(int level, String message);
}
