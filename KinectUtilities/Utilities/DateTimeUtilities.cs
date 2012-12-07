using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectUtilities
{
    public static class DateTimeUtilities
    {
        public static DateTime ToDateTime(long ticks)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime dateTime = start.AddMilliseconds(ticks).ToLocalTime();

            return dateTime;
        }
    }
}
