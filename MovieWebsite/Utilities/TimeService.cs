using System;

namespace MovieWebsite.Utilities
{
    public static class TimeService
    {
        public static String timeAgoString(DateTime pastTime)
        {
            var time = DateTime.Now.Subtract(pastTime);
            if (time.TotalHours >= 24)
                return $"{(int)time.TotalDays} days ago";
            return $"{(int)time.TotalHours} hours ago";
        }
    }
}