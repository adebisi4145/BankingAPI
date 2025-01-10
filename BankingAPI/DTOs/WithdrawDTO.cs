using System.ComponentModel.DataAnnotations;

namespace BankingAPI.DTOs
{
    public class WithdrawDTO
    {
        [Required]
        public string AccountNumber { get; set; } = null!;
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Withdrawal amount must be greater than zero")]
        public decimal Amount { get; set; }
    }
}
