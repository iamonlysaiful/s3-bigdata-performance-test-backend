using S3ITEST.DB.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace S3ITEST.DATAACCESS.Repositories
{
    public interface IDataFieldRepositories
    {
        IEnumerable<DataField> GetAll();
        DataField GetDataFieldById(int id);
    }

    public class DataFieldRepositories :BaseRepositories, IDataFieldRepositories
    {
        private readonly DapperDBContext _dbContext;
        public DataFieldRepositories(DapperDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<DataField> GetAll()
        {
            try
            {
                var sql = @"SELECT     
                           *
                           FROM DataField
                           Order by id ASC";

                var data = GetData<DataField>(sql, null, _dbContext).ToList();

                return data;
            }
            catch (Exception ex)
            {
                return new List<DataField>();
            }
        }

        public DataField GetDataFieldById(int id)
        {
            try
            {
                var sql = @"SELECT     
                           *
                           FROM DataField
                           WHERE Id=@DataFieldId";

                var data = GetData<DataField>(sql, new { DataFieldId = id }, _dbContext).FirstOrDefault();

                return data;
            }
            catch (Exception ex)
            {
                return new DataField();
            }
        }
    }
}
