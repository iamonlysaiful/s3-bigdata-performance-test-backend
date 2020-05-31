using GraphQL.Types;

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
