using GraphQL.Types;
using S3ITEST.DB.EntityModels;

namespace S3ITEST.TYPES
{
    public class ObjectType : ObjectGraphType<Object>
    {
        public ObjectType()
        {
            Field(x => x.Id, type: typeof(IdGraphType));
            Field(x => x.Name);
        }
    }
}
