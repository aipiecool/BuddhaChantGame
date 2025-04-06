package log.outputs;

public class MultipleLogOutput extends LogOutput{

    private LogOutput[] mLogOutputs;

    public MultipleLogOutput(LogOutput...args) {
        mLogOutputs = args;
    }

    @Override
    public void write(int level, String message) {
        if(mLogOutputs != null){
            for(LogOutput output : mLogOutputs){
                output.write(level, message);
            }
        }
    }
}
