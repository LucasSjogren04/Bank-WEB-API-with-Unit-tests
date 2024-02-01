namespace UPG3ButWithTests.Models
{
    public class Accounts
    {
        public int AccountId { get; set; }
        public string Frequency { get; set;}
        public DateTime Created { get; set;}
        public decimal Balance { get; set;}
        public int AccountTypesId { get; set;}
        public int CustomerId { get; set; }
    }
}
