package model.task.entity.rom;

import java.util.List;

public class TaskInfo {

    public String taskId;
    public String taskName;
    public List<ChildTask> childTask;
    public List<TaskActiveCondition> condition;
    public TaskActiveCondition getCondition;
    public String completeChildTaskId;

    public static class ChildTask {
        public String taskId;
        public TaskInfo parentTask;
        public String taskName;
        public String taskTap;
        public List<TaskAction> actions;
        public List<TaskActiveCondition> condition;
        public List<NpcSpeak> unCompleteNpcSpeak;
        public List<NpcSpeak> completeNpcSpeak;
        public List<NpcPosition> npcPositions;
        public List<TaskReward> rewards;

        public static class TaskAction{
            public int actionId;
            public String triggerEvent;
            public String triggerObject;
            public int requireReplaceCount;
        }

        public static class TaskReward{
            public String rewardName;
            public String rewardType;
            public String rewardObject;
            public int rewardCount;
        }
    }

    public static class TaskActiveCondition{
        public String triggerEvent;
        public String triggerObject;
        public int triggerCount;
    }

    public static class NpcSpeak{
        public String npcName;
        public String npcFirstSpeak;
        public String npcSecondSpeak;
    }

    public static class NpcPosition{
        public String npcName;
        public String sceneName;
        public float positionX;
        public float positionY;
    }
}
