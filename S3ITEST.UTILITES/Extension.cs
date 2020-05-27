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
    }
}
