using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
	public class VectorUtils 
	{
		public static int getDirectionByVector2(Vector2 v1, Vector2 v2)
		{
			Vector2 from = v2 - v1;
			Vector2 to = Vector2.one;
			float dot = Vector2.Dot(from, new Vector2(-1, 1));
			float angle = Vector2.Angle(from, to);
			angle = dot > 0 ? -angle : angle;	
			if(angle > -180 && angle <= -90)
            {
				return 0;
            }else if(angle >-90 && angle <= 0)
            {
				return 1;
            }else if(angle > 0 && angle <= 90)
            {
				return 2;
            }
			else if (angle > 90 && angle <= 180)
			{
				return 3;
			}
			return 0;
		}
	}
}
