using Dapper;
using Npgsql;
using S3ITEST.DB.EntityModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace S3ITEST.DATAACCESS
{
    public abstract class BaseRepositories
    {
        //For TSQL
        //public List<T> GetData<T>(string sql, object obj, DapperDBContext _dbContext) 
        //{
        //    using (IDbConnection conn = new SqlConnection(_dbContext.Value))
        //    {
        //        var data = conn.Query<T>(sql, obj, commandTimeout: 1800).ToList();
        //        return data;
        //    }
        //} 
        
        //For PGSQL
        public List<T> GetData<T>(string sql, object obj, DapperDBContext _dbContext)
        {
            using (IDbConnection conn = new NpgsqlConnection(_dbContext.Value))
            {
                var data = conn.Query<T>(sql, obj, commandTimeout: 1800).ToList();
                return data;
            }
        }
    }
}


