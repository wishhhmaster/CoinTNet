using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinWisdomAPI
{
    public static class UnixTime
    {
        static DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static UInt32 Now
        {
            get
            {
                return GetFromDateTime(DateTime.UtcNow);
            }
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
