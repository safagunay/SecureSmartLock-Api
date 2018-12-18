using System;

namespace LockerApi.Services
{
    public static class DateService
    {
        public static DateTime getCurrentUTC()
        {
            return DateTime.UtcNow;
        }

        public static DateTime getCurrentSystemTime()
        {
            return DateTime.Now;
        }

        public static bool isExpiredUTC(DateTime? dateTime)
        {
            if (!dateTime.HasValue)
                return false;
            return getCurrentUTC() > dateTime.Value;
        }

        public static bool isExpiredUTC(DateTime dateTime)
        {
            return getCurrentUTC() > dateTime;
        }
    }
}