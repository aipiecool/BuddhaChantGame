using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class GameTask 
	{
        public string mainTitle;
        public string minorTitle;
        public string tapInfo;
        public List<TaskAction> taskActions;
        public List<TaskReward> taskReward;             
    }

    public class TaskAction
    {
        public string actionName;
        public int replaceCount;
        public int requireReplaceCount;
        public bool isComplete;
    }

    public class TaskReward
    {
        public string rewardName;
        public int rewardCount;
    }
}
