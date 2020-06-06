using System;
using System.Collections.Generic;

namespace S3ITEST.DB.ViewModel
{
    public class ReadingViewModel
    {
        public string BuildingName { get; set; }
        public string DatapointName { get; set; }
        public List<Int64> Timestamp { get; set; }
        public List<decimal> Value { get; set; }
    }
}
