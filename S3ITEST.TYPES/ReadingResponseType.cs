using GraphQL.Types;
using S3ITEST.DATAACCESS.Repositories;
using S3ITEST.DB.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace S3ITEST.TYPES
{
    public class ReadingResponseType:ObjectGraphType
    {
        public ReadingResponseType()
        {
            Field<StringGraphType>("BuildingName");
            Field<StringGraphType>("ObjectName"); 
            Field<StringGraphType>("DataFieldName"); 
            Field<ListGraphType<IntGraphType>>("Timestamp"); 
            Field<ListGraphType<DecimalGraphType>>("Value");
        }
        

    }
}
