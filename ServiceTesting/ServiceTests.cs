using Moq;
using UPG3ButWithTests.DTO;
using UPG3ButWithTests.Models;
using UPG3ButWithTests.Repository.Interfaces;
using UPG3ButWithTests.Services;

namespace UPG3ButWithTests
{
    public class ServiceTester
    {
        [Fact]
        public void CustomerService_InsertLoan_ReturnsSuccess()
        {
            var customerRepo = new Mock<ICustomerRepo>();
            var loginRepo = new Mock<ILoginRepo>();

            var giveLoanDTO = new GiveLoanDTO() { AccountId = 1, Amount = 1000, Payments = 0 };

            var service = new CustomerService(customerRepo.Object, loginRepo.Object);

            string result = service.GiveLoan(giveLoanDTO);

            Assert.Equal("Loan given, success!", result);
        }

        [Fact]
        public void CustomerService_TransferMoney_ReturnsAuthorized_Because_A_customer_with_the_inserted_loginid_doesnt_exist()
        {
            var customerRepo = new Mock<ICustomerRepo>();
            var loginRepo = new Mock<ILoginRepo>();

            int loginId = 834;
            int from = 5623;
            int to = 1231;
            decimal amount = 4000;
            Customers emptyCustomer = null;

            customerRepo.Setup(repo => repo.GetCustomerByLoginId(loginId)).Returns(emptyCustomer);

            var service = new CustomerService(customerRepo.Object, loginRepo.Object);

            string result = service.TransferMoney(from, to, amount, loginId);

            Assert.Equal("Unauthorized", result);
        }

        [Fact]
        public void CustomerService_GetAccountOverview_ReturnsStringWithAccountOverview()
        {
            var customerRepo = new Mock<ICustomerRepo>();
            var loginRepo = new Mock<ILoginRepo>();

            int loginId = 245;
            var actingCustomer = new Customers() { CustomerId = 50, LoginId = 245 };
            var customersAccount = new Accounts() { AccountId = 23, CustomerId = 50, Balance = 200, AccountTypesId = 1, };

            var customersAccounts = new List<Accounts>
            {
                customersAccount
            };

            customerRepo.Setup(repo => repo.GetCustomerByLoginId(loginId)).Returns(actingCustomer);
            customerRepo.Setup(repo => repo.GetAccounts(actingCustomer.CustomerId)).Returns(customersAccounts);

            var service = new CustomerService(customerRepo.Object, loginRepo.Object);

            string overview = service.GetAccountOverview(loginId);

            Assert.Contains("Your accounts: ", overview);
        }
    }
}