using BankingAPI.DTOs;
using BankingAPI.Models;

namespace BankingAPI.Services.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<CreateAccountResponseDTO>> CreateAccount(CreateAccountDTO dto);
        Task<BaseResponse<AccountBalanceDTO>> GetAccountBalance(string accountNumber);
        Task<BaseResponse<string>> Deposit(DepositDTO dto);
        Task<BaseResponse<string>> Transfer(TransferDTO dto);
        Task<BaseResponse<string>> Withdraw(WithdrawDTO dto);
    }
}
