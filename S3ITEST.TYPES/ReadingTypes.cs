using GraphQL.Types;
using S3ITEST.DB.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace S3ITEST.TYPES
{
    public class ReadingTypes : ObjectGraphType<Reading>
    {
        public ReadingTypes()
        {
            Field(x => x.BuildingId, type: typeof(IdGraphType));
            Field(x => x.ObjectId, type: typeof(IdGraphType));
            Field(x => x.DataFieldId, type: typeof(IdGraphType));
            Field(x => x.Value);
            Field(x => x.Timestamp, type: typeof(DateTimeGraphType));
            Field<IntGraphType>("Unix",
               resolve: context =>
               {
                   return Convert.ToInt64((context.Source.Timestamp - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds);
               });
        }
    }
}
