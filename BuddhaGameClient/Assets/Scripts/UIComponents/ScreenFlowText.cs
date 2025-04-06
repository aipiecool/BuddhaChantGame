using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BuddhaGame
{
	public class ScreenFlowText : MonoBehaviour
	{
        private static ScreenFlowText sInstance;
        private Text flow_text;
        private RectTransform flow_text_transform;
        private float mPositionY = 0;
        private float mColorA = 1;
        private bool mRunning = false;
        private Queue<ScreenFlowTextTask> mTaskQueue = new Queue<ScreenFlowTextTask>();

        private RuntimeController mRuntimeController;
        public void Awake()
        {
            sInstance = this;
            mRuntimeController = new RuntimeController(0.01f);
            flow_text = transform.GetChild(0).GetComponent<Text>();
            flow_text_transform = transform.GetChild(0).GetComponent<RectTransform>();
        }

        public static ScreenFlowText get()
        {
            return sInstance;
        }

        public void addText(string message, string sound = null)
        {
            mTaskQueue.Enqueue(new ScreenFlowTextTask(message, sound));            
        }

        private void Update()
        {

            mRuntimeController.run(() =>
            {
                if (mRunning)
                {
                    mPositionY += 1f;
                    mColorA -= 0.004f;
                    if (mColorA <= 0)
                    {
                        mRunning = false;
                        mPositionY = 0;
                        mColorA = 1;
                        flow_text.text = "";
                    }
                    flow_text_transform.anchoredPosition = new Vector2(0, mPositionY);
                    flow_text.color = new Color(flow_text.color.r, flow_text.color.g, flow_text.color.b, mColorA);
                }
                else if (mTaskQueue.Count > 0)
                {
                    ScreenFlowTextTask task = mTaskQueue.Dequeue();
                    flow_text.text = task.message;
                    mRunning = true;                 
                    SoundsManager.get().playSound(task.sound);
                }
            });           
        }

        private class ScreenFlowTextTask
        {
            public string message;
            public string sound;
            public ScreenFlowTextTask(string message, string sound)
            {
                this.message = message;
                this.sound = sound;
            }
        }
    }   
}
