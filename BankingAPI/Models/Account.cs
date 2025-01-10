namespace BankingAPI.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string AccountName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string AccountNumber { get; set; } = null!;
        public decimal AccountBalance { get; set; } = 0;
    }
}
