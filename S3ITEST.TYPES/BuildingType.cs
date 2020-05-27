using GraphQL.Types;
using S3ITEST.DB.EntityModels;

namespace S3ITEST.TYPES
{
    public class BuildingType : ObjectGraphType<Building>
    {
        public BuildingType()
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name);
            Field(x => x.Location);
        }
    }
}
