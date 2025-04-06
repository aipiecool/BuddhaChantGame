package model.task.entity.running;

import model.task.entity.rom.TaskInfo;

import java.util.List;

public class RunningTaskInfo {
    public String childTaskId;
    public List<RunningTaskActionInfo> taskActions;

    public static class RunningTaskActionInfo {
        public TaskInfo.ChildTask.TaskAction romInfo;
        public String actionLocalName;
        public int replaceCount;
    }
}
