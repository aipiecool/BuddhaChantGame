using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BuddhaGame
{
	public class TaskView : UIHideable
	{
		private static TaskView sInstance;
      
        public ListView taskList;
        public TaskDetalView subView;

        private TaskModel mTaskModel = new TaskModel();
        private DataAdapter<GameTask, TaskCell> mDataAdapter = new DataAdapter<GameTask, TaskCell>();

        private void Start()
        {
            sInstance = this;
            taskList.setAdapter(mDataAdapter);
            mDataAdapter.setOnClickCallback((cell, postion, data)=>
            {
                subView.setTaskData(data);
            });            
        }

        public static TaskView get()
        {
            return sInstance;
        }

		public override void show()
		{
            subView.clear();
            taskList.clearAllCell();
            mTaskModel.requestTaskDetal((gameTasks)=>
            {
                ThreadLooper.get().runMainThread(() =>
                {
                    mDataAdapter.setDatas(gameTasks);
                    if(gameTasks.Count > 0)
                    {
                        taskList.setActiveCell(0);
                        subView.setTaskData(gameTasks[0]);
                    }                  
                });                
            });
            base.show();
		}
	}
}
