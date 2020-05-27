using S3ITEST.DB.EntityModels;
using System.Collections.Generic;
using System.Linq;

namespace S3ITEST.DATAACCESS.Repositories
{
    public interface IObjectRepositories
    {
        IEnumerable<Object> GetAll();
        Object GetObjectById(int id);
    }

    public class ObjectRepositories :BaseRepositories, IObjectRepositories
    {
        private readonly DapperDBContext _dbContext;
        public ObjectRepositories(DapperDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Object> GetAll()
        {

            try
            {
                var sql = @"SELECT     
                           *
                           FROM Object
                           Order by id ASC";

                var data = GetData<Object>(sql, null, _dbContext).ToList();

                return data;
            }
            catch (System.Exception ex)
            {
                return new List<Object>();
            }
        }

        public Object GetObjectById(int id)
        {
            try
            {
                var sql = @"SELECT     
                           *
                           FROM Object
                           WHERE Id=@ObjectId";

                var data = GetData<Object>(sql, new { ObjectId = id }, _dbContext).FirstOrDefault();

                return data;
            }
            catch (System.Exception ex)
            {
                return new Object();
            }
        }
    }
}
