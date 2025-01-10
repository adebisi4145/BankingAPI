using BankingAPI.Models;

namespace BankingAPI.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> AddAccount(Account account);
        Task<Account?> GetAccountByNumber(string accountNumber);
        Task<bool> EmailExists(string email);
        Task<Account> UpdateAccount(Account account);
    }
}
