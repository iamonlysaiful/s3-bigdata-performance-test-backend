using GraphQL;
using GraphQL.Types;
using S3ITEST.API.Queries;

namespace S3ITEST.API.Schema
{
    public class S3ITESTSchema:GraphQL.Types.Schema
    {
        public S3ITESTSchema(IDependencyResolver resolver)
            :base(resolver)
        {
            Query = resolver.Resolve<RootQuery>();
            
        }
    }
}
