using GraphQL.Types;
using S3ITEST.DB.ViewModel;
using static S3ITEST.UTILITES.Extension;

namespace S3ITEST.TYPES
{
    public class ReadingResponseType : ObjectGraphType<PageInfo<ReadingViewModel>>
    {
        public ReadingResponseType()
        {
            Field<ListGraphType<ReadingType>>(
                "Data",
                resolve: context => context.Source.List
            );
            Field(xx => xx.PageCount);
            Field(xx => xx.Size);
            Field(xx => xx.TotalCount);
        }
    }
    public class ReadingType : ObjectGraphType
    {
        public ReadingType()
        {
            Field<StringGraphType>("BuildingName");
            Field<StringGraphType>("DatapointName");
            Field<ListGraphType<IntGraphType>>("Timestamp");
            Field<ListGraphType<DecimalGraphType>>("Value");
        }

    }
}
