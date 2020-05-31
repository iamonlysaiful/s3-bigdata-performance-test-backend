﻿using System;

namespace S3ITEST.DB.EntityModels
{
    public partial class Reading
    {
        public short BuildingId { get; set; }
        public byte ObjectId { get; set; }
        public byte DataFieldId { get; set; }
        public decimal Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}