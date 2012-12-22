using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolBox.Functions
{
    public static class DateTimeUtilities
    {
        public static DateTime ToDateTime(long ticks)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime dateTime = start.AddMilliseconds(ticks).ToLocalTime();

            return dateTime;
        }

        public static int DifferenceInMilliseconds(DateTime now, DateTime next)
        {
            // RBakerFlag -> Returns the difference in seconds accurate to hours.
            return (next.Hour * 24 * 60 * 1000 + next.Minute * 60 * 1000 + next.Second * 1000 + next.Millisecond)
                - (now.Hour * 24 * 60 * 1000 + now.Minute * 60 * 1000 + now.Second * 1000 + now.Millisecond);
        }
    }
}
