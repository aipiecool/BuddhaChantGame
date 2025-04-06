using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class DateUtils 
	{
		public static int getTimestampInt()
        {
			TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
			return Convert.ToInt32(ts.TotalSeconds);
		}
	}
}
