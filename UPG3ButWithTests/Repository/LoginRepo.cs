using UPG3ButWithTests.Models;
using UPG3ButWithTests.Repository.Interfaces;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;

namespace UPG3ButWithTests.Repository
{
    public class LoginRepo (IDBContext context) : ILoginRepo
    {
        private readonly IDBContext _context = context;

        public Login Login(string loginKey)
        {
            try
            {
                using IDbConnection db = _context.GetConnection();
                DynamicParameters parameters = new();
                parameters.Add("@LoginKey", loginKey);
                return db.QueryFirstOrDefault<Login>("GetLoginId", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex) 
            { 
                throw new Exception(ex.Message);
            }

        }
    }
}
