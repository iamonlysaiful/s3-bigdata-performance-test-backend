using GraphQL.Types;

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
