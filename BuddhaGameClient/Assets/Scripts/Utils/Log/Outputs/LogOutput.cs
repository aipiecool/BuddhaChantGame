using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public abstract class LogOutput 
	{
		public abstract void write(int level, string message);
	}
}
