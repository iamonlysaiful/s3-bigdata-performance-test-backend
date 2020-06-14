using S3ITEST.DB.EntityModels;
using S3ITEST.DB.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using static S3ITEST.UTILITES.Extension;

namespace S3ITEST.DATAACCESS.Repositories
{
    public interface IReadingRepositories
    {
        PageInfo<ReadingViewModel> GetReadingData(int buildingId, int? objectId, int? datafieldId, string startTime, string endTime, int page, int size);
        IEnumerable<ReadingViewModel> GetReadingLazyData(int buildingId, int? objectId, int? datafieldId, string startTime, string endTime);
    }
    public class ReadingRepositories : BaseRepositories, IReadingRepositories
    {
        private readonly DapperDBContext _dbContext;
        private readonly IBuildingRepositories _buildingRepositories;
        public ReadingRepositories(DapperDBContext dbContext, IBuildingRepositories buildingRepositories)
        {
            _dbContext = dbContext;
            _buildingRepositories = buildingRepositories;
        }

        public IEnumerable<ReadingViewModel> GetReadingLazyData(int buildingId, int? objectId, int? datafieldId, string startTime, string endTime)
        {
            PageInfo<ReadingViewModel> req;
            IEnumerable<ReadingViewModel> response;

            req = new PageInfo<ReadingViewModel>(startTime, endTime);
            try
            {
                IEnumerable<Reading> filtered;
                var sql = "";
                object sqlParams = null;
                if (objectId != null && datafieldId != null)
                {
                    //             sql = @"SELECT obj.Name || ' ' || df.Name DatapointName ,
                    //                     rv.Timestamp,
                    //                     rv.Value
                    //                     FROM Readingv2 rv
                    //left join Object obj on obj.Id=rv.ObjectId
                    //inner join DataField df on df.Id =rv.DataFieldId
                    //                     WHERE Timestamp BETWEEN @StartTime AND @EndTime
                    //                     AND BuildingId=@BuildingId
                    //                     AND ObjectId=@ObjectId
                    //                     AND DataFieldId=@DataFieldId";

                    if (req.TotalCount >= 365)
                    {
                        sql = @"SELECT  obj.Name || ' ' || df.Name DatapointName ,
							                date_trunc('month', rv.Timestamp::timestamp) AS Timestamp,
                                            avg(rv.Value) as Value
                                            FROM Readingv2 rv
							                left join Object obj on obj.Id=rv.ObjectId
							                inner join DataField df on df.Id =rv.DataFieldId
                                            WHERE Timestamp BETWEEN @StartTime AND @EndTime
                                            AND BuildingId=@BuildingId
                                            AND ObjectId=@ObjectId
                                            AND DataFieldId=@DataFieldId
                                  group by 1,2";
                    }
                    if (req.TotalCount >= 30)
                    {
                        sql = @"SELECT  obj.Name || ' ' || df.Name DatapointName ,
							                date_trunc('week', rv.Timestamp::timestamp) AS Timestamp,
                                            avg(rv.Value) as Value
                                            FROM Readingv2 rv
							                left join Object obj on obj.Id=rv.ObjectId
							                inner join DataField df on df.Id =rv.DataFieldId
                                            WHERE Timestamp BETWEEN @StartTime AND @EndTime
                                            AND BuildingId=@BuildingId
                                            AND ObjectId=@ObjectId
                                            AND DataFieldId=@DataFieldId
                                  group by 1,2";
                    }
                    else
                    {
                        sql = @"SELECT obj.Name || ' ' || df.Name DatapointName,
                            rv.Timestamp,
                            rv.Value
                            FROM Readingv2 rv
							left join Object obj on obj.Id=rv.ObjectId
							inner join DataField df on df.Id =rv.DataFieldId
                            WHERE Timestamp BETWEEN @StartTime AND @EndTime
                            AND BuildingId=@BuildingId
                            AND ObjectId=@ObjectId
                            AND DataFieldId=@DataFieldId";
                    }

                    sqlParams = new
                    {
                        StartTime = req.StartDate,
                        EndTime = req.EndDate,
                        BuildingId = buildingId,
                        ObjectId = objectId,
                        DataFieldId = datafieldId
                    };
                }
                else if (objectId == null && datafieldId != null)
                {
                    if (req.TotalCount >= 365)
                    {
                        sql = @"SELECT  obj.Name || ' ' || df.Name DatapointName ,
							                date_trunc('month', rv.Timestamp::timestamp) AS Timestamp,
                                            avg(rv.Value) as Value
                                            FROM Readingv2 rv
							                left join Object obj on obj.Id=rv.ObjectId
							                inner join DataField df on df.Id =rv.DataFieldId
                                            WHERE Timestamp BETWEEN @StartTime AND @EndTime
                                            AND BuildingId=@BuildingId
                                            AND DatafieldId=@DataFieldId
                                  group by 1,2";
                    }
                    else if (req.TotalCount >= 30)
                    {
                        sql = @"SELECT  obj.Name || ' ' || df.Name DatapointName ,
							                date_trunc('week', rv.Timestamp::timestamp) AS Timestamp,
                                            avg(rv.Value) as Value
                                            FROM Readingv2 rv
							                left join Object obj on obj.Id=rv.ObjectId
							                inner join DataField df on df.Id =rv.DataFieldId
                                            WHERE Timestamp BETWEEN @StartTime AND @EndTime
                                            AND BuildingId=@BuildingId
                                            AND DatafieldId=@DataFieldId
                                  group by 1,2";
                    }
                    else
                    {
                        sql = @"SELECT obj.Name || ' ' || df.Name DatapointName,
                            rv.Timestamp,
                            rv.Value
                            FROM Readingv2 rv
							left join Object obj on obj.Id=rv.ObjectId
							inner join DataField df on df.Id =rv.DataFieldId
                            WHERE Timestamp BETWEEN @StartTime AND @EndTime
                            AND BuildingId=@BuildingId
                            AND DataFieldId=@DataFieldId";
                    }

                    sqlParams = new
                    {
                        StartTime = req.StartDate,
                        EndTime = req.EndDate,
                        BuildingId = buildingId,
                        DataFieldId = datafieldId
                    };
                }
                else if (objectId != null && datafieldId == null)
                {
                    if (req.TotalCount >= 365)
                    {
                        sql = @"SELECT  obj.Name || ' ' || df.Name DatapointName ,
							                date_trunc('month', rv.Timestamp::timestamp) AS Timestamp,
                                            avg(rv.Value) as Value
                                            FROM Readingv2 rv
							                left join Object obj on obj.Id=rv.ObjectId
							                inner join DataField df on df.Id =rv.DataFieldId
                                            WHERE Timestamp BETWEEN @StartTime AND @EndTime
                                            AND BuildingId=@BuildingId
                                            AND ObjectId=@ObjectId
                                  group by 1,2";
                    }
                    if (req.TotalCount >= 30)
                    {
                        sql = @"SELECT  obj.Name || ' ' || df.Name DatapointName ,
							                date_trunc('week', rv.Timestamp::timestamp) AS Timestamp,
                                            avg(rv.Value) as Value
                                            FROM Readingv2 rv
							                left join Object obj on obj.Id=rv.ObjectId
							                inner join DataField df on df.Id =rv.DataFieldId
                                            WHERE Timestamp BETWEEN @StartTime AND @EndTime
                                            AND BuildingId=@BuildingId
                                            AND ObjectId=@ObjectId
                                  group by 1,2";
                    }
                    else
                    {
                        sql = @"SELECT obj.Name || ' ' || df.Name DatapointName ,
                            rv.Timestamp,
                            rv.Value
                            FROM Readingv2 rv
							left join Object obj on obj.Id=rv.ObjectId
							inner join DataField df on df.Id =rv.DataFieldId
                            WHERE Timestamp BETWEEN @StartTime AND @EndTime
                            AND BuildingId=@BuildingId
                            AND ObjectId=@ObjectId";
                    }

                    sqlParams = new
                    {
                        StartTime = req.StartDate,
                        EndTime = req.EndDate,
                        BuildingId = buildingId,
                        ObjectId = objectId
                    };
                }
                else
                {
                    if (req.TotalCount >= 365)
                    {
                        sql = @"SELECT  obj.Name || ' ' || df.Name DatapointName ,
							                date_trunc('month', rv.Timestamp::timestamp) AS Timestamp,
                                            avg(rv.Value) as Value
                                            FROM Readingv2 rv
							                left join Object obj on obj.Id=rv.ObjectId
							                inner join DataField df on df.Id =rv.DataFieldId
                                            WHERE Timestamp BETWEEN @StartTime AND @EndTime
                                            AND BuildingId=@BuildingId
                                  group by 1,2";
                    }
                    if (req.TotalCount >= 30)
                    {
                        sql = @"SELECT  obj.Name || ' ' || df.Name DatapointName ,
							                date_trunc('week', rv.Timestamp::timestamp) AS Timestamp,
                                            avg(rv.Value) as Value
                                            FROM Readingv2 rv
							                left join Object obj on obj.Id=rv.ObjectId
							                inner join DataField df on df.Id =rv.DataFieldId
                                            WHERE Timestamp BETWEEN @StartTime AND @EndTime
                                            AND BuildingId=@BuildingId
                                  group by 1,2";
                    }
                    else
                    {
                        sql = @"SELECT obj.Name || ' ' || df.Name DatapointName ,
                            rv.Timestamp,
                            rv.Value
                            FROM Readingv2 rv
							left join Object obj on obj.Id=rv.ObjectId
							inner join DataField df on df.Id =rv.DataFieldId
                            WHERE Timestamp BETWEEN @StartTime AND @EndTime
                            AND BuildingId=@BuildingId";
                    }

                    sqlParams = new
                    {
                        StartTime = req.StartDate,
                        EndTime = req.EndDate,
                        BuildingId = buildingId,
                        ObjectId = objectId
                    };
                }

                filtered = GetData<Reading>(sql, sqlParams, _dbContext).AsQueryable();
                //=================================================
                List<ReadingViewModel> listReadings = new List<ReadingViewModel>();
                List<string> dataPoints = filtered.Select(x => x.DatapointName).Distinct().ToList();
                for (int i = 0; i < dataPoints.Count; i++)
                {
                    List<long> unixList = new List<long>();
                    List<decimal> values = new List<decimal>();
                    var datapointData = filtered.Where(x => x.DatapointName == dataPoints[i]).OrderBy(x => x.Timestamp);
                    var obj = new ReadingViewModel();
                    obj.BuildingName = _buildingRepositories.GetBuildingById(buildingId).Name;
                    foreach (var item in datapointData)
                    {
                        unixList.Add(DatTimeToUnix(Convert.ToDateTime(item.Timestamp)));
                        values.Add(Convert.ToDecimal(item.Value));
                    }
                    obj.DatapointName = dataPoints[i].ToString();
                    obj.Timestamp = unixList;
                    obj.Value = values;
                    listReadings.Add(obj);
                }
                //=================================================

                response = listReadings;
                //res = new PageInfo<ReadingViewModel>(response, req.TotalCount, req.Size);
                return response;
            }
            catch (Exception ex)
            {
                response = new List<ReadingViewModel>();
                return response; //= new PageInfo<ReadingViewModel>(response, 0, size);
            }
        }

        public PageInfo<ReadingViewModel> GetReadingData(int buildingId, int? objectId, int? datafieldId, string startTime, string endTime, int page, int size)
        {
            PageInfo<ReadingViewModel> res, req;
            IEnumerable<ReadingViewModel> response;

            req = new PageInfo<ReadingViewModel>(page, size, startTime, endTime);
            try
            {
                IEnumerable<Reading> filtered;
                var sql = "";
                object sqlParams = null;
                if (objectId != null && datafieldId != null)
                {
                    //             sql = @"SELECT obj.Name || ' ' || df.Name DatapointName ,
                    //                     rv.Timestamp,
                    //                     rv.Value
                    //                     FROM Readingv2 rv
                    //left join Object obj on obj.Id=rv.ObjectId
                    //inner join DataField df on df.Id =rv.DataFieldId
                    //                     WHERE Timestamp BETWEEN @StartTime AND @EndTime
                    //                     AND BuildingId=@BuildingId
                    //                     AND ObjectId=@ObjectId
                    //                     AND DataFieldId=@DataFieldId";

                    if (req.Size >= 365)
                    {
                        sql = @"SELECT  obj.Name || ' ' || df.Name DatapointName ,
							                date_trunc('month', rv.Timestamp::timestamp) AS Timestamp,
                                            avg(rv.Value) as Value
                                            FROM Readingv2 rv
							                left join Object obj on obj.Id=rv.ObjectId
							                inner join DataField df on df.Id =rv.DataFieldId
                                            WHERE Timestamp BETWEEN @StartTime AND @EndTime
                                            AND BuildingId=@BuildingId
                                            AND ObjectId=@ObjectId
                                            AND DataFieldId=@DataFieldId
                                  group by 1,2";
                    }
                    else
                    {
                        sql = @"SELECT obj.Name || ' ' || df.Name DatapointName,
                            rv.Timestamp,
                            rv.Value
                            FROM Readingv2 rv
							left join Object obj on obj.Id=rv.ObjectId
							inner join DataField df on df.Id =rv.DataFieldId
                            WHERE Timestamp BETWEEN @StartTime AND @EndTime
                            AND BuildingId=@BuildingId
                            AND ObjectId=@ObjectId
                            AND DataFieldId=@DataFieldId";
                    }

                    sqlParams = new
                    {
                        StartTime = req.StartDate,
                        EndTime = req.EndDate,
                        BuildingId = buildingId,
                        ObjectId = objectId,
                        DataFieldId = datafieldId
                    };
                }
                else if (objectId == null && datafieldId != null)
                {
                    if (req.Size >= 365)
                    {
                        sql = @"SELECT  obj.Name || ' ' || df.Name DatapointName ,
							                date_trunc('month', rv.Timestamp::timestamp) AS Timestamp,
                                            avg(rv.Value) as Value
                                            FROM Readingv2 rv
							                left join Object obj on obj.Id=rv.ObjectId
							                inner join DataField df on df.Id =rv.DataFieldId
                                            WHERE Timestamp BETWEEN @StartTime AND @EndTime
                                            AND BuildingId=@BuildingId
                                            AND DatafieldId=@DataFieldId
                                  group by 1,2";
                    }
                    else
                    {
                        sql = @"SELECT obj.Name || ' ' || df.Name DatapointName,
                            rv.Timestamp,
                            rv.Value
                            FROM Readingv2 rv
							left join Object obj on obj.Id=rv.ObjectId
							inner join DataField df on df.Id =rv.DataFieldId
                            WHERE Timestamp BETWEEN @StartTime AND @EndTime
                            AND BuildingId=@BuildingId
                            AND DataFieldId=@DataFieldId";
                    }

                    sqlParams = new
                    {
                        StartTime = req.StartDate,
                        EndTime = req.EndDate,
                        BuildingId = buildingId,
                        DataFieldId = datafieldId
                    };
                }
                else if (objectId != null && datafieldId == null)
                {
                    if (req.Size >= 365)
                    {
                        sql = @"SELECT  obj.Name || ' ' || df.Name DatapointName ,
							                date_trunc('month', rv.Timestamp::timestamp) AS Timestamp,
                                            avg(rv.Value) as Value
                                            FROM Readingv2 rv
							                left join Object obj on obj.Id=rv.ObjectId
							                inner join DataField df on df.Id =rv.DataFieldId
                                            WHERE Timestamp BETWEEN @StartTime AND @EndTime
                                            AND BuildingId=@BuildingId
                                            AND ObjectId=@ObjectId
                                  group by 1,2";
                    }
                    else
                    {
                        sql = @"SELECT obj.Name || ' ' || df.Name DatapointName ,
                            rv.Timestamp,
                            rv.Value
                            FROM Readingv2 rv
							left join Object obj on obj.Id=rv.ObjectId
							inner join DataField df on df.Id =rv.DataFieldId
                            WHERE Timestamp BETWEEN @StartTime AND @EndTime
                            AND BuildingId=@BuildingId
                            AND ObjectId=@ObjectId";
                    }

                    sqlParams = new
                    {
                        StartTime = req.StartDate,
                        EndTime = req.EndDate,
                        BuildingId = buildingId,
                        ObjectId = objectId
                    };
                }
                else
                {
                    if (req.Size >= 365)
                    {
                        sql = @"SELECT  obj.Name || ' ' || df.Name DatapointName ,
							                date_trunc('month', rv.Timestamp::timestamp) AS Timestamp,
                                            avg(rv.Value) as Value
                                            FROM Readingv2 rv
							                left join Object obj on obj.Id=rv.ObjectId
							                inner join DataField df on df.Id =rv.DataFieldId
                                            WHERE Timestamp BETWEEN @StartTime AND @EndTime
                                            AND BuildingId=@BuildingId
                                  group by 1,2";
                    }
                    else
                    {
                        sql = @"SELECT obj.Name || ' ' || df.Name DatapointName ,
                            rv.Timestamp,
                            rv.Value
                            FROM Readingv2 rv
							left join Object obj on obj.Id=rv.ObjectId
							inner join DataField df on df.Id =rv.DataFieldId
                            WHERE Timestamp BETWEEN @StartTime AND @EndTime
                            AND BuildingId=@BuildingId";
                    }

                    sqlParams = new
                    {
                        StartTime = req.StartDate,
                        EndTime = req.EndDate,
                        BuildingId = buildingId,
                        ObjectId = objectId
                    };
                }

                filtered = GetData<Reading>(sql, sqlParams, _dbContext).AsQueryable();
                //=================================================
                List<ReadingViewModel> listReadings = new List<ReadingViewModel>();
                List<string> dataPoints = filtered.Select(x => x.DatapointName).Distinct().ToList();
                for (int i = 0; i < dataPoints.Count; i++)
                {
                    List<long> unixList = new List<long>();
                    List<decimal> values = new List<decimal>();
                    var datapointData = filtered.Where(x => x.DatapointName == dataPoints[i]).OrderBy(x => x.Timestamp);
                    var obj = new ReadingViewModel();
                    obj.BuildingName = _buildingRepositories.GetBuildingById(buildingId).Name;
                    foreach (var item in datapointData)
                    {
                        unixList.Add(DatTimeToUnix(Convert.ToDateTime(item.Timestamp)));
                        values.Add(Convert.ToDecimal(item.Value));
                    }
                    obj.DatapointName = dataPoints[i].ToString();
                    obj.Timestamp = unixList;
                    obj.Value = values;
                    listReadings.Add(obj);
                }
                //=================================================

                response = listReadings;
                res = new PageInfo<ReadingViewModel>(response, req.TotalCount, req.Size);
                return res;
            }
            catch (Exception ex)
            {
                response = new List<ReadingViewModel>().AsQueryable();
                return res = new PageInfo<ReadingViewModel>(response, 0, size);
            }
        }

    }
}
