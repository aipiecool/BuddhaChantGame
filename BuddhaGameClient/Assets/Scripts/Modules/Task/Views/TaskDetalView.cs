using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BuddhaGame
{
	public class TaskDetalView : MonoBehaviour
	{
		public Text main_title;
		public Text minor_title;
		public Text detal_text;

		public void setTaskData(GameTask gameTask)
        {
			main_title.text = gameTask.mainTitle;
			minor_title.text = gameTask.minorTitle;
			string detalString = gameTask.tapInfo + "\n";		
			foreach (TaskAction taskAction in gameTask.taskActions)
            {
				bool isComplete = taskAction.isComplete;
				detalString += isComplete ? "    <color=#00ff00>": "    <color=#bdf655>";
				detalString += taskAction.actionName;
				if(taskAction.requireReplaceCount > 1)
                {
					detalString += "(" + taskAction.replaceCount + "/" + taskAction.requireReplaceCount + ")";
				}
                if (isComplete)
                {
					detalString += "  (完成)";
				}
				detalString += "</color>\n";
			}
			detalString += "\n任务完成奖励\n";
			foreach (TaskReward taskAction in gameTask.taskReward)
            {
				detalString += "    <color=#00ffff>";
				detalString += taskAction.rewardCount + "x" + taskAction.rewardName;
				detalString += "</color>\n";
			}
			detal_text.text = detalString;
		}

		public void clear()
        {
			main_title.text = "";
			minor_title.text = "";
			detal_text.text = "";
		}
    }
}
