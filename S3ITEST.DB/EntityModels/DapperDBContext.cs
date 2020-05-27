using System;
using System.Collections.Generic;
using System.Text;

namespace S3ITEST.DB.EntityModels
{
    public sealed class DapperDBContext
    {
        public DapperDBContext(string value) => Value = value;

        public string Value { get; }
    }
}
