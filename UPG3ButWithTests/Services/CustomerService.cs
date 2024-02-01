using UPG3ButWithTests.DTO;
using UPG3ButWithTests.Repository.Interfaces;
using UPG3ButWithTests.Services.Interfaces;
using UPG3ButWithTests.Models;
using System.Security.Claims;

namespace UPG3ButWithTests.Services
{
    public class CustomerService(ICustomerRepo customerRepo, ILoginRepo loginRepo) : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo = customerRepo;
        private readonly ILoginRepo _loginRepo = loginRepo;

        public string CreateAnotherAccount(int loginId, int accountTypesId)
        {
            Customers actingCustomer = _customerRepo.GetCustomerByLoginId(loginId);
            if (actingCustomer != null)
            {
                _customerRepo.InsertAnotherAccount(accountTypesId, actingCustomer.CustomerId);
                return "Account created!";
            }
            return "UnAuthorized";
        }

        public string CreateCreateCustomer(CreateCustomerDTO createCustomerDTO)
        {
            //Inserts the LoginInformation
            _customerRepo.InsertLogin(createCustomerDTO.LoginKey, createCustomerDTO.Admin);

            //Gets the LoginId of the newly created login
            Login createdLogin = _loginRepo.Login(createCustomerDTO.LoginKey);

            //Inserts into the CustomerTable
            _customerRepo.InsertCustomer(
                createCustomerDTO.Gender,
                createCustomerDTO.Givenname,
                createCustomerDTO.Surname,
                createCustomerDTO.Streetaddress,
                createCustomerDTO.City,
                createCustomerDTO.Zipcode,
                createCustomerDTO.Country,
                createCustomerDTO.CountryCode,
                createCustomerDTO.Birthday,
                createCustomerDTO.Telephonecountrycode,
                createCustomerDTO.Telephonenumber,
                createCustomerDTO.Emailaddress,
                createdLogin.LoginId);

            //Gets the CustomerId of the newly created customer
            Customers createdCustomer = _customerRepo.GetInsertedCustomer(createCustomerDTO.Givenname, createCustomerDTO.Surname, createCustomerDTO.Birthday);

            Console.WriteLine(createdCustomer.CustomerId);

            _customerRepo.InsertAccount(
                createCustomerDTO.Frequency,
                createCustomerDTO.Balance,
                createCustomerDTO.AccountTypesId,
                createdCustomer.CustomerId);

            return "Customer Has been successfully created";
        }

        public string GetAccountOverview(int loginId)
        {
            Customers actingCustomer = _customerRepo.GetCustomerByLoginId(loginId);
            if (actingCustomer != null)
            {
                List<Accounts> customersAccounts = _customerRepo.GetAccounts(actingCustomer.CustomerId);
                if(customersAccounts != null)
                {
                    string overview = "Your accounts: \n";
                    foreach(Accounts customersAccount in customersAccounts)
                    {
                        overview += (customersAccount.AccountId + " " + customersAccount.Balance
                            + " " + customersAccount.AccountTypesId + "\n \n");
                    }
                    return overview;
                }
                return "You don't have any accounts";
            }
            return "Unauthorized";
        }

        public string GetTransactionsOnAccount(int loginId, int accountId)
        {
            Customers actingCustomer = _customerRepo.GetCustomerByLoginId(loginId);
            if (actingCustomer != null)
            {
                List<Accounts> customersAccounts = _customerRepo.GetAccounts(actingCustomer.CustomerId);
                foreach (Accounts customersAccount in customersAccounts)
                {
                    if (customersAccount.AccountId == accountId)
                    {
                        List<Transactions> transactinsOnAccount = _customerRepo.GetTransactionsOnAccount(customersAccount.AccountId);
                        string transactionsToString = "Transactions on account: \n";
                        if (transactinsOnAccount != null)
                        {
                            foreach (Transactions transaction in transactinsOnAccount)
                            {
                                transactionsToString += (transaction.Date + "\n" +
                                    transaction.Type + "\n" +
                                    transaction.Operation + "\n" +
                                    transaction.Amount + "\n" +
                                    transaction.Balance + "\n" +
                                    transaction.Symbol + "\n" +
                                    transaction.Bank + "\n" +
                                    transaction.Account + "\n \n");
                            }
                            return transactionsToString;
                        } 
                        return transactionsToString;
                    }
                    return "You do not own that account";
                }
                return "You do not own that account";
            }
            return "Unathorized";
        }

        public string GiveLoan(GiveLoanDTO giveLoanDTO)
        {
            if (giveLoanDTO != null)
            {
                _customerRepo.GiveLoan(giveLoanDTO);
                return "Loan given, success!";
            }
            return "Loan not given, failure!";
        }

        public string TransferMoney(int from, int to, decimal amount, int loginId)
        {
            Customers actingCustomer = _customerRepo.GetCustomerByLoginId(loginId);
            if (actingCustomer != null)
            {
                Accounts fromAccount = _customerRepo.GetAccount(from);
                if (fromAccount.CustomerId == actingCustomer.CustomerId)
                {
                    if (fromAccount != null)
                    {
                        if (fromAccount.Balance > amount)
                        {
                            _customerRepo.TransferMoneyBetweenAccounts(from, to, amount);
                            _customerRepo.InsertTransaction(from, to, amount, fromAccount.Balance);
                            return "Money transefered, success!";
                        }
                        return "You don't have that much money tranfer";
                    }
                    return "The account you are trying to send money from doesn't exist!";
                }
                return "Unauthorized";
            }
            return "Unauthorized";
        }
    }
}
