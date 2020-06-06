
namespace S3ITEST.DB.EntityModels
{
    public sealed class DapperDBContext
    {
        public DapperDBContext(string connection) => Connection = connection;
        public string Connection { get; }
    }
}
