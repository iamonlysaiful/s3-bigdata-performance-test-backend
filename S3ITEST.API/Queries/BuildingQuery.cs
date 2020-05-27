using GraphQL.Types;
using S3ITEST.DATAACCESS.Repositories;
using S3ITEST.TYPES;
using System.Linq;

namespace S3ITEST.API.Queries
{
    public class BuildingQuery : ObjectGraphType
    {
        public BuildingQuery(IBuildingRepositories buildingRepository)
        {
            Name = "BuildingQuery";
            Field<ListGraphType<BuildingType>>("buildings", resolve: context => buildingRepository.GetAll());
            Field<BuildingType>("building", arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }), resolve: context =>
            {
                var id = context.GetArgument<int>("id");
                return buildingRepository.GetBuildingById(id);
            });
        }

    }
}
