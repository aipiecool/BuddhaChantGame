package model.task;


import model.task.entity.rom.GameTask;
import model.task.entity.rom.TaskInfo;
import utils.FileUtils;
import utils.JsonUtils;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

public class TaskLoader {
    private static TaskLoader sInstance;

    public TaskLoader() {

    }

    public List<GameTask> load(){
        List<GameTask> tasks = new ArrayList<>();
        loadTask(tasks, "assets/tasks/");
        return tasks;
    }

    private void loadTask(List<GameTask> tasks, String path){
        File[] files = FileUtils.listOfFolder(path);
        for(File file : files){
            if(file.isFile() && file.getName().equals("task.json")){
                tasks.add(loadOneTask(path));
            }else if(file.isDirectory()){
                loadTask(tasks, file.getAbsolutePath());
            }
        }
    }

    private GameTask loadOneTask(String path){
        TaskInfo taskInfo = JsonUtils.unserialize(FileUtils.readTxt(path + "/task.json"), TaskInfo.class);
        File[] files = FileUtils.listOfFolder(path);
        for(File file : files){
            if(file.isFile() && !file.getName().equals("task.json")){
                TaskInfo.ChildTask childTask = JsonUtils.unserialize(FileUtils.readTxt(file.getAbsolutePath()), TaskInfo.ChildTask.class);
                childTask.parentTask = taskInfo;
                taskInfo.childTask.add(childTask);
            }
        }
        return new GameTask(taskInfo);
    }


}
