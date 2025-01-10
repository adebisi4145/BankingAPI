using System.ComponentModel.DataAnnotations;

namespace BankingAPI.DTOs
{
    public class TransferDTO
    {
        [Required]
        public string SendersAccountNumber { get; set; } = null!;
        [Required]
        public string ReceiversAccountNumber { get; set; } = null!;
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Transfer amount must be greater than zero")]
        public decimal Amount { get; set; }
    }
}
