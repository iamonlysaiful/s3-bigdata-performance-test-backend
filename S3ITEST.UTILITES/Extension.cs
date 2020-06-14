using System;
using System.Collections.Generic;
using System.Globalization;

namespace S3ITEST.UTILITES
{
    public static class Extension
    {
        public class PageInfo<T>
        {
            public int TotalCount { get; private set; }
            public int Page { get; private set; }
            public int Size { get; private set; }
            public int PageCount { get; private set; }
            public DateTime StartDate { get; private set; }
            public DateTime EndDate { get; private set; }
            public IEnumerable<T> List { get; private set; }
           
            public PageInfo(IEnumerable<T> list, double totalCount, int size)
            {
                List = list;
                TotalCount = Convert.ToInt32(Math.Ceiling(totalCount));
                PageCount = Convert.ToInt32(Math.Ceiling(TotalCount / Convert.ToDouble(size)));
                Size = size;
            }

            public PageInfo(int page, int size, string startTime, string endTime)
            {
                StartDate = DateTime.ParseExact(startTime, "dd-MM-yyyyhh-mm-ss-tt", CultureInfo.InvariantCulture);
                EndDate = DateTime.ParseExact(endTime, "dd-MM-yyyyhh-mm-ss-tt", CultureInfo.InvariantCulture);
                TotalCount = DayDifference(StartDate, EndDate);
                Size = size > TotalCount ? TotalCount : size;
                StartDate = StartDate.AddDays((page * Size) - Size);
                int daycount = DayDifference(StartDate, EndDate);
                EndDate = StartDate.AddDays(Size < daycount ? Size : daycount);
            }

            public PageInfo(string startTime, string endTime)
            {
                StartDate = DateTime.ParseExact(startTime, "dd-MM-yyyyhh-mm-ss-tt", CultureInfo.InvariantCulture);
                EndDate = DateTime.ParseExact(endTime, "dd-MM-yyyyhh-mm-ss-tt", CultureInfo.InvariantCulture);
                TotalCount = DayDifference(StartDate, EndDate);
            }
        }

        public static int DayDifference(DateTime startDate, DateTime endDate)
        {
            return Convert.ToInt32(Math.Floor((endDate - startDate).TotalDays)); ;
        }

        public static long DatTimeToUnix(DateTime datetime)
        {
            return Convert.ToInt64((datetime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
        }
    }
}
