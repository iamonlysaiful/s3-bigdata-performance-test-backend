using Dapper;
using Npgsql;
using S3ITEST.DB.EntityModels;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace S3ITEST.DATAACCESS
{
    public abstract class BaseRepositories
    {
        //For PGSQL
        public List<T> GetData<T>(string sql, object obj, DapperDBContext _dbContext)
        {
            using (IDbConnection conn = new NpgsqlConnection(_dbContext.Connection))
            {
                var data = conn.Query<T>(sql, obj, commandTimeout: 1800).ToList();
                return data;
            }
        }
    }
}


