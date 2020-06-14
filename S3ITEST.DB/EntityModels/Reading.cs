using System;

namespace S3ITEST.DB.EntityModels
{
    public partial class Reading
    {
        public short BuildingId { get; set; }
        public byte ObjectId { get; set; }
        public byte DataFieldId { get; set; }
        public string DatapointName { get; set; }
        public decimal Value { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class ReadingConfig
    {
        public string ReadingTableName { get; set; }
    }
}
