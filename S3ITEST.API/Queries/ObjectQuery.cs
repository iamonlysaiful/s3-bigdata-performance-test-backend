using GraphQL.Types;
using S3ITEST.DATAACCESS.Repositories;
using S3ITEST.TYPES;
using System.Linq;

namespace S3ITEST.API.Queries
{
    public class ObjectQuery : ObjectGraphType
    {
        public ObjectQuery(IObjectRepositories objectRepositories)
        {
            Name = "objects";
            Field<ListGraphType<ObjectType>>("objects", resolve: context => objectRepositories.GetAll());
            Field<ObjectType>("object", arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }), resolve: context =>
            {
                var id = context.GetArgument<int>("id");
                return objectRepositories.GetObjectById(id);
            });
        }

    }
}
