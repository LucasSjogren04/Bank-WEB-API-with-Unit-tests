using UPG3ButWithTests.DTO;

namespace UPG3ButWithTests.Services.Interfaces
{
    public interface ICustomerService
    {
        string CreateAnotherAccount(int loginId, int accountTypesId);
        string CreateCreateCustomer(CreateCustomerDTO createCustomerDTO);
        string GetAccountOverview(int loginId);
        string GetTransactionsOnAccount(int loginId, int accountId);
        string GiveLoan(GiveLoanDTO giveLoanDTO);
        string TransferMoney(int from, int to, decimal amount, int loginId);
    }
}
