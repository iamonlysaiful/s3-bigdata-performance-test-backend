using S3ITEST.DB.EntityModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace S3ITEST.DATAACCESS.Repositories
{
    public interface IReadingRepositories
    {
        IQueryable<Reading> GetReadingDataDBFilter(int buildingId, int objectId, int datafieldId, string startTime, string endTime);
        IEnumerable<Reading> GetReadingData();
    }
    public class ReadingRepositories : BaseRepositories, IReadingRepositories
    {
        private readonly DapperDBContext _dbContext;
        public ReadingRepositories(DapperDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Reading> GetReadingData()
        {
            try
            {
                var sql = @"SELECT     
                           *
                           FROM Readingv2";
                var data = GetData<Reading>(sql, null, _dbContext).ToList();
                return data;
            }
            catch (Exception ex)
            {
                return new List<Reading>();
            }
        }
        public IQueryable<Reading> GetReadingDataDBFilter(int buildingId, int objectId, int datafieldId, string startTime, string endTime)
        {
            try
            {
                DateTime starttime = DateTime.ParseExact(startTime, "dd-MM-yyyyhh-mm-ss-tt", CultureInfo.InvariantCulture);
                DateTime endtime = DateTime.ParseExact(endTime, "dd-MM-yyyyhh-mm-ss-tt", CultureInfo.InvariantCulture);
                var sql = @"SELECT *
                            FROM Readingv2
                            WHERE Timestamp BETWEEN @StartTime AND @EndTime
                            AND BuildingId=@BuildingId
                            AND ObjectId=@ObjectId
                            AND DataFieldId=@DataFieldId";
                var data = GetData<Reading>(
                sql,
                new
                {
                    StartTime = starttime,
                    EndTime = endtime,
                    BuildingId = buildingId,
                    ObjectId = objectId,
                    DataFieldId = datafieldId
                },
                _dbContext).AsQueryable();
                return data;
            }
            catch (Exception ex)
            {
                return new List<Reading>().AsQueryable();
            }


        }
    }
}
