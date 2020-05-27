using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S3ITEST.API.Queries
{
    public class RootQuery: ObjectGraphType
    {
        public RootQuery()
        {
            Name = "RootQuery";
            Field<BuildingQuery>("bQuery", resolve: context => new { });
            Field<ObjectQuery>("objQuery", resolve: context => new { });
            Field<DataFieldQuery>("dfieldQuery", resolve: context => new { });
            Field<ReadingQuery>("readingQuery", resolve: context => new { });
        }
    }
}
