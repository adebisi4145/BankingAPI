using BankingAPI.Context;
using BankingAPI.Models;
using BankingAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.Repositories.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankingDbContext _dbContext;
        public AccountRepository(BankingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Account> AddAccount(Account account)
        {
            await _dbContext.Accounts.AddAsync(account);
            await _dbContext.SaveChangesAsync();
            return account;
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _dbContext.Accounts.AnyAsync(c=>c.Email == email);
        }

        public async Task<Account?> GetAccountByNumber(string accountNumber)
        {
            return await _dbContext.Accounts.FirstOrDefaultAsync(c => c.AccountNumber ==  accountNumber);
        }

        public async Task<Account> UpdateAccount(Account account)
        {
             _dbContext.Accounts.Update(account);
            await _dbContext.SaveChangesAsync();
            return account;
        }
    }
}
