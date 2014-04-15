using System;

namespace BitstampAPI
{
    public static class UnixTimeHelper
    {
        static DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static UInt32 Now
        {
            get { return GetFromDateTime(DateTime.UtcNow); }
        }

        public static UInt32 GetFromDateTime(DateTime d)
        {
            var dif = d - unixEpoch;
            return (UInt32)dif.TotalSeconds;
        }

        public static DateTime ConvertToDateTime(UInt32 unixtime)
        {
            return unixEpoch.AddSeconds(unixtime);
        }
    }
}
