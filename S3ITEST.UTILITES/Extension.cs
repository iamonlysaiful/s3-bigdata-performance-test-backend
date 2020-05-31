using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace S3ITEST.UTILITES
{
    public static class Extension
    {
        public static bool IsNullOrEmpty(string s)
        {
            return (s == null || s == String.Empty) ? true : false;
        }

        public static long DatTimeToUnix(DateTime datetime)
        {
            return Convert.ToInt64((datetime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
        }
    }
}
