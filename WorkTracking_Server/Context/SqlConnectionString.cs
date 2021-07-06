
namespace WorkTracking_Server.Context
{
    public class SqlConnectionString
    {
        public string ConnectionString { get; set; }

        public SqlConnectionString(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
