using S3ITEST.DB.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace S3ITEST.DATAACCESS.Repositories
{
    public interface IBuildingRepositories
    {
        IEnumerable<Building> GetAll();
        Building GetBuildingById(int id);
    }

    public class BuildingRepositories : BaseRepositories, IBuildingRepositories
    {
        private readonly DapperDBContext _dbContext;

        public BuildingRepositories(DapperDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Building> GetAll()
        {
            try
            {
                var sql = @"SELECT     
                           *
                           FROM Building
                           Order by id ASC";

                var data = GetData<Building>(sql, null, _dbContext).ToList();

                return data;
            }
            catch (Exception ex)
            {
                return new List<Building>();
            }
        }
        public Building GetBuildingById(int id)
        {
            try
            {
                var sql = @"SELECT     
                           *
                           FROM Building
                           WHERE Id=@BuildingId";

                var data = GetData<Building>(sql, new { BuildingId = id }, _dbContext).FirstOrDefault();

                return data;
            }
            catch (Exception ex)
            {
                return new Building();
            }
        }
    }
}
