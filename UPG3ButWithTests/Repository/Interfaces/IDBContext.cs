using Microsoft.Data.SqlClient;

namespace UPG3ButWithTests.Repository.Interfaces
{
    public interface IDBContext
    {
        SqlConnection GetConnection();
    }
}
