using UPG3ButWithTests.DTO;
using UPG3ButWithTests.Models;

namespace UPG3ButWithTests.Repository.Interfaces
{
    public interface ICustomerRepo
    {
        void InsertLogin(string loginKey, string admin);
        void InsertCustomer(
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
            int LoginId);

        Customers GetInsertedCustomer(string Givenname, string Surname, DateTime Birthday);
        void InsertAccount(
            string Frequency,
            decimal Balance,
            int AccountTypesId,
            int CustomerId);
        void GiveLoan(GiveLoanDTO giveLoanDTO);
        Accounts GetAccount(int from);
        Customers GetCustomerByLoginId(int loginId);
        void TransferMoneyBetweenAccounts(int from, int to, decimal amount);
        List<Accounts> GetAccounts(int customerId);
        List<Transactions> GetTransactionsOnAccount(int accountId);
        void InsertAnotherAccount(int accountTypesId, int customerId);
        void InsertTransaction(int from, int to, decimal amount, decimal balance);
    }
}
