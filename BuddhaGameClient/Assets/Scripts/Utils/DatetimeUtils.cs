using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuddhaGame
{
    public class DatetimeUtils
    {
        public static string getDatatime()
        {
            return DateTime.Now.ToLocalTime().ToString();
        }
    }
}
