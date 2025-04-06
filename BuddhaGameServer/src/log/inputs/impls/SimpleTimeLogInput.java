package log.inputs.impls;

import log.inputs.LogInput;
import log.outputs.LogOutput;
import utils.DatetimeUtils;

public class SimpleTimeLogInput extends LogInput {


    public SimpleTimeLogInput(LogOutput logOutput) {
        super(logOutput);
    }

    @Override
    protected void write(int level, String message) {
        mLogOutput.write(level, DatetimeUtils.getDatatime() + " " + message);
    }


}
