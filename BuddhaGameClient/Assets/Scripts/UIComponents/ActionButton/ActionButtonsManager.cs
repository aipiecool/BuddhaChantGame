using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class ActionButtonsManager :MonoBehaviour
	{
        private static ActionButtonsManager sInstance;

        public List<ActionButton> actionButtons;

		public void Awake()
        {
            sInstance = this;
        }

		public void show(Object caller, string text, Runable runable, int priority=0)
        {
            bool find = false;
			foreach(ActionButton button in actionButtons)
            {
                if(button.show(caller, text, runable, priority, false))
                {
                    find = true;
                    break;
                }
            }

            if (!find)
            {
                foreach (ActionButton button in actionButtons)
                {
                    if (button.show(caller, text, runable, priority, true))
                    {                       
                        break;
                    }
                }
            }
        }

		public void hide(Object caller)
        {
            foreach (ActionButton button in actionButtons)
            {
                if (button.hide(caller))
                {
                    break;
                }
            }
        }

        public static ActionButtonsManager get()
        {
            return sInstance;
        }
    }
}
