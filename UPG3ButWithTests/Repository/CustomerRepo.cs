using UPG3ButWithTests.Repository.Interfaces;
using Dapper;
using System.Data;
using UPG3ButWithTests.Models;
using System.Reflection;
using UPG3ButWithTests.DTO;

namespace UPG3ButWithTests.Repository
{
    public class CustomerRepo (IDBContext context) : ICustomerRepo
    {
        private readonly IDBContext _context = context;

        public void InsertLogin(string loginKey, string admin)
        {
            try
            {
                using IDbConnection db = _context.GetConnection();
                DynamicParameters parameters = new();
                parameters.Add("@LoginKey", loginKey);
                parameters.Add("@Admin", admin);
                db.Execute("InsertLogin", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        public void InsertCustomer(
            string Gender,
            string Givenname,
            string Surname, 
            string Streetaddress,
            string City,
            string Zipcode,
            string Country,
            string CountryCode,
            DateTime Birthday,
            string Telephonecountrycode,
            string Telephonenumber,
            string Emailaddress,
            int LoginId)
        {
            try
            {
                using IDbConnection db = _context.GetConnection();
                DynamicParameters parameters = new();
                parameters.Add("@Gender", Gender);
                parameters.Add("@Givenname", Givenname);
                parameters.Add("@Surname", Surname);
                parameters.Add("@Streetaddress", Streetaddress);
                parameters.Add("@City", City);
                parameters.Add("@Zipcode", Zipcode);
                parameters.Add("@Country", Country);
                parameters.Add("@CountryCode", CountryCode);
                parameters.Add("@Birthday", Birthday);
                parameters.Add("@Telephonecountrycode", Telephonecountrycode);
                parameters.Add("@Telephonenumber", Telephonenumber);
                parameters.Add("@Emailaddress", Emailaddress);
                parameters.Add("@LoginId", LoginId);
                db.Execute("InsertCustomer", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public Customers GetInsertedCustomer(string Givenname, string Surname, DateTime Birthday)
        {
            try
            {
                using IDbConnection db = _context.GetConnection();
                DynamicParameters parameters = new();
                parameters.Add("@Givenname", Givenname);
                parameters.Add("@Surname", Surname);
                parameters.Add("@Birthday", Birthday);
                return db.QueryFirstOrDefault<Customers>("GetInsertedCustomer", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void InsertAccount(
            string Frequency,
            decimal Balance,
            int AccountTypesId,
            int CustomerId)
        {
            try
            {
                using IDbConnection db = _context.GetConnection();
                DynamicParameters parameters = new();
                parameters.Add("@Frequency", Frequency);
                parameters.Add("@Created", DateTime.UtcNow);
                parameters.Add("@Balance", Balance);
                parameters.Add("@AccountTypesId", AccountTypesId);
                parameters.Add("@CustomerId", CustomerId);
                db.Execute("InsertAccount", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void GiveLoan(GiveLoanDTO giveLoanDTO)
        {
            try
            {
                using IDbConnection db = _context.GetConnection();
                DynamicParameters parameters = new();
                parameters.Add("@AccountId", giveLoanDTO.AccountId);
                parameters.Add("@Date", giveLoanDTO.Date);
                parameters.Add("@Amount", giveLoanDTO.Amount);
                parameters.Add("@Duration", giveLoanDTO.Duration);
                parameters.Add("@Payments", giveLoanDTO.Payments);
                parameters.Add("@Status", giveLoanDTO.Status);
                db.Execute("InsertLoan", parameters, commandType: CommandType.StoredProcedure);
            } 
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Accounts GetAccount(int accountId)
        {
            try
            {
                using IDbConnection db = _context.GetConnection();
                DynamicParameters parameters = new();
                parameters.Add("@AccountId", accountId);
                return db.QueryFirstOrDefault<Accounts>("GetAccountByAccountId", parameters, commandType: CommandType.StoredProcedure);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        public List<Accounts> GetAccounts(int customerId)
        {
            try
            {
                using IDbConnection db = _context.GetConnection();
                DynamicParameters parameters = new();
                parameters.Add("@CustomerId", customerId);
                return db.Query<Accounts>("GetAccountsByCustomerId", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
            catch( Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void TransferMoneyBetweenAccounts(int from, int to, decimal amount)
        {
            try
            {
                using IDbConnection db = _context.GetConnection();
                DynamicParameters parameters = new();
                parameters.Add("@TransferMoney", amount);
                parameters.Add("@FromAccountId", from);
                parameters.Add("@ToAccountId", to);
                db.Execute("TransferMoneyBetweenAccounts", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Customers GetCustomerByLoginId(int loginId)
        {
            try
            {
                using IDbConnection db = _context.GetConnection();
                DynamicParameters parameters = new();
                parameters.Add("@LoginId", loginId);
                return db.QueryFirstOrDefault<Customers>("GetCustomerByLoginId", parameters, commandType: CommandType.StoredProcedure);
            }
            catch
            (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Transactions> GetTransactionsOnAccount(int accountId)
        {
            try
            {
                using IDbConnection db = _context.GetConnection();
                DynamicParameters parameters = new();
                parameters.Add("@AccountId", accountId);
                return db.Query<Transactions>("GetTransactionsOnAccount", parameters, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void InsertAnotherAccount(int accountTypesId, int customerId)
        {
            try
            {
                using IDbConnection db = _context.GetConnection();
                DynamicParameters parameters = new();
                parameters.Add("@Frequency", "Monthly");
                parameters.Add("@Created", DateTime.UtcNow);
                parameters.Add("@Balance", 0);
                parameters.Add("@AccountTypesId", accountTypesId);
                parameters.Add("@CustomerId", customerId);
                db.Execute("InsertAccount", parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void InsertTransaction(int from, int to, decimal amount, decimal balance)
        {
            try
            {
                using IDbConnection db = _context.GetConnection();
                DynamicParameters parameters = new();
                parameters.Add("@AccountId", from);
                parameters.Add("@Date", DateTime.UtcNow);
                parameters.Add("@Type", "Debit");
                parameters.Add("@Operation", "Transer to another account");
                parameters.Add("@Amount", amount);
                parameters.Add("@Balance", balance);
                parameters.Add("@Symbol", "Monthly");
                parameters.Add("@Bank", null);
                parameters.Add("@Account", to);
                db.Execute("InsertTransaction", parameters, commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex) 
            {
                throw new Exception (ex.Message);
            }
        }
    }
}
