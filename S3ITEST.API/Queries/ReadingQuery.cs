using GraphQL.Types;
using S3ITEST.DATAACCESS.Repositories;
using System.Collections.Generic;
using S3ITEST.TYPES;

namespace S3ITEST.API.Queries
{
    public class ReadingQuery : ObjectGraphType
    {
        public ReadingQuery(IReadingRepositories readingRepositories)
        {

            Name = "ReadingQuery";
            Field<ReadingResponseType>("readings",
               arguments: new QueryArguments(new List<QueryArgument>
               {
                    new QueryArgument<IdGraphType>{Name = "buildingId"},
                    new QueryArgument<IdGraphType>{Name = "objectId"},
                    new QueryArgument<IdGraphType>{Name = "datafieldId"},
                    new QueryArgument<StringGraphType>{Name = "startTime"},
                    new QueryArgument<StringGraphType>{Name = "endTime"},
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "page" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "size" },
               }),
               resolve: context =>
               {
                   var readingDatas = readingRepositories.GetReadingData(context.GetArgument<int>("buildingId"), context.GetArgument<int?>("objectId"), context.GetArgument<int?>("datafieldId"), context.GetArgument<string>("startTime"), context.GetArgument<string>("endTime"), context.GetArgument<int>("page"), context.GetArgument<int>("size"));
                   return readingDatas;
               });
            
            Field< ListGraphType<ReadingType>>("readingsLazyData",
               arguments: new QueryArguments(new List<QueryArgument>
               {
                    new QueryArgument<IdGraphType>{Name = "buildingId"},
                    new QueryArgument<IdGraphType>{Name = "objectId"},
                    new QueryArgument<IdGraphType>{Name = "datafieldId"},
                    new QueryArgument<StringGraphType>{Name = "startTime"},
                    new QueryArgument<StringGraphType>{Name = "endTime"},
               }),
               resolve: context =>
               {
                   var readingDatas = readingRepositories.GetReadingLazyData(context.GetArgument<int>("buildingId"), context.GetArgument<int?>("objectId"), context.GetArgument<int?>("datafieldId"), context.GetArgument<string>("startTime"), context.GetArgument<string>("endTime"));
                   return readingDatas;
               });
        }
    }
}
