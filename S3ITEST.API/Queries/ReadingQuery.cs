using GraphQL.Types;
using S3ITEST.DATAACCESS.Repositories;
using S3ITEST.TYPES;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using S3ITEST.UTILITES;
using S3ITEST.DB.ResponseModel;

namespace S3ITEST.API.Queries
{
    public class ReadingQuery : ObjectGraphType
    {
        public ReadingQuery(
            IReadingRepositories readingRepositories,
            IBuildingRepositories buildingRepositories,
            IObjectRepositories objectRepositories,
            IDataFieldRepositories dataFieldRepositories
            )
        {


            Name = "ReadingQuery";
            Field<ReadingResponseType>("readings",
               arguments: new QueryArguments(new List<QueryArgument>
               {
                    new QueryArgument<IdGraphType>
                    {
                        Name = "buildingId"
                    },
                    new QueryArgument<IdGraphType>
                    {
                        Name = "objectId"
                    },
                    new QueryArgument<IdGraphType>
                    {
                        Name = "datafieldId"
                    },
                    new QueryArgument<StringGraphType>
                    {
                        Name = "startTime"
                    },
                    new QueryArgument<StringGraphType>
                    {
                        Name = "endTime"
                    }
               }),
               resolve: context =>
               {
                   var bid = context.GetArgument<int>("buildingId");
                   var oid = context.GetArgument<int>("objectId");
                   var did = context.GetArgument<int>("datafieldId");
                   var sTime = context.GetArgument<string>("startTime");
                   var eTime = context.GetArgument<string>("endTime");
                   var obj = new ReadingResponse();
                   obj.BuildingName = buildingRepositories.GetBuildingById(bid).Name;
                   obj.DataFieldName = dataFieldRepositories.GetDataFieldById(did).Name;
                   obj.ObjectName= objectRepositories.GetObjectById(oid).Name;
                   List<Int64> unixList = new List<Int64>();
                   List<decimal> values = new List<decimal>();
                   var readingDatas = readingRepositories.GetReadingDataDBFilter(bid, oid, did, sTime, eTime).OrderBy(x=>x.Timestamp);
                   foreach (var item in readingDatas)
                   {
                       unixList.Add(Convert.ToInt64((item.Timestamp - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds));
                       values.Add(item.Value == null ? 0 : item.Value);
                   }
                   obj.Timestamp = unixList;
                   obj.Value = values;
                   return obj;

               });
        }

    }
}
