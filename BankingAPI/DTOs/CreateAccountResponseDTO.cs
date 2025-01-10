namespace BankingAPI.DTOs
{
    public class CreateAccountResponseDTO
    {
        public string AccountNumber { get; set; } = null!;
        public string AccountName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public decimal AccountBalance { get; set; } = 0;
    }
}
