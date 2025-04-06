using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BuddhaGame
{
	public class TaskCell : DataListCell<GameTask>
    {
        public Text mainTitle;
        public Text minorTitle; 

        protected override void onCreate(GameTask data)
        {        
            mainTitle.text = data.mainTitle;
            minorTitle.text = data.minorTitle;
        }
    }
}
