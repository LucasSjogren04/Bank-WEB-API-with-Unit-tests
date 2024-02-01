using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using UPG3ButWithTests.Repository.Interfaces;

namespace UPG3ButWithTests.Repository
{
    public class DBContext : IDBContext
    {
        private readonly string? _connString;
        public DBContext(IConfiguration config)
        {
            _connString = config.GetConnectionString("DefaultConnection");

        }
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connString);

        }
    }
}
