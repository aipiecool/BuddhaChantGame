package log.outputs;

public abstract class LogOutput {
    public abstract void write(int level, String message);
}
