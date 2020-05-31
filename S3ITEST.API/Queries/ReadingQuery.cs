using GraphQL.Types;
using S3ITEST.DATAACCESS.Repositories;
using S3ITEST.TYPES;
using System.Collections.Generic;
using System.Linq;
using S3ITEST.DB.ViewModel;
using S3ITEST.UTILITES;

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
                   var buildingId = context.GetArgument<int>("buildingId");
                   var objectId = context.GetArgument<int>("objectId");
                   var datafieldId = context.GetArgument<int>("datafieldId");
                   var startTime = context.GetArgument<string>("startTime");
                   var endTime = context.GetArgument<string>("endTime");

                   var obj = new ReadingViewModel();
                   obj.BuildingName = buildingRepositories.GetBuildingById(buildingId).Name;
                   obj.DataFieldName = dataFieldRepositories.GetDataFieldById(datafieldId).Name;
                   obj.ObjectName= objectRepositories.GetObjectById(objectId).Name;
                   List<long> unixList = new List<long>();
                   List<decimal> values = new List<decimal>();
                   var readingDatas = readingRepositories.GetReadingDataDBFilter(buildingId, objectId, datafieldId, startTime, endTime).OrderBy(x=>x.Timestamp);
                   foreach (var item in readingDatas)
                   {
                       unixList.Add(Extension.DatTimeToUnix(item.Timestamp));
                       values.Add(item.Value);
                   }
                   obj.Timestamp = unixList;
                   obj.Value = values;

                   return obj;

               });
        }

    }
}
