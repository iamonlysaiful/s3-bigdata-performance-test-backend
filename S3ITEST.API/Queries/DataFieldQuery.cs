using GraphQL.Types;
using S3ITEST.DATAACCESS.Repositories;
using S3ITEST.TYPES;

namespace S3ITEST.API.Queries
{
    public class DataFieldQuery : ObjectGraphType
    {
        public DataFieldQuery(IDataFieldRepositories dataFieldRepositories)
        {
            Name = "DataFieldQuery";
            Field<ListGraphType<DataFieldType>>("datafields", resolve: context => dataFieldRepositories.GetAll());
            Field<DataFieldType>("datafield", arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }), resolve: context =>
            {
                var id = context.GetArgument<int>("id");
                return dataFieldRepositories.GetDataFieldById(id);
            });
          
        }

    }
}
