using BankingAPI.DTOs;
using BankingAPI.Models;
using BankingAPI.Repositories.Interfaces;
using BankingAPI.Services.Interfaces;

namespace BankingAPI.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<BaseResponse<CreateAccountResponseDTO>> CreateAccount(CreateAccountDTO dto)
        {
            bool emailExists = await _accountRepository.EmailExists(dto.Email);
            if (emailExists)
                return new BaseResponse<CreateAccountResponseDTO> { Status = false, Message = "Email already exists" };

            var account = new Account
            {
                AccountName = dto.AccountName,
                Email = dto.Email,
                AccountNumber = await GenerateAccountNumber(),
                AccountBalance = 0
            };

            await _accountRepository.AddAccount(account);

            var responseDto = new CreateAccountResponseDTO
            {
                AccountNumber = account.AccountNumber,
                AccountName = account.AccountName,
                Email = account.Email,
                AccountBalance = account.AccountBalance
            };

            return new BaseResponse<CreateAccountResponseDTO> { Data = responseDto, Status = true, Message = "Account created successfully" };
        }

        public async Task<BaseResponse<string>> Deposit(DepositDTO dto)
        {
            var account = await _accountRepository.GetAccountByNumber(dto.AccountNumber);
            if (account == null)
                return new BaseResponse<string> { Status = false, Message = "Account not found" };

            account.AccountBalance += dto.Amount;
            await _accountRepository.UpdateAccount(account);

            return new BaseResponse<string> { Status = true, Message = $"Successfully deposited ₦{dto.Amount:N} to account {dto.AccountNumber}" };
        }

        public async Task<BaseResponse<AccountBalanceDTO>> GetAccountBalance(string accountNumber)
        {
            var account = await _accountRepository.GetAccountByNumber(accountNumber);

            if (account == null)
                return new BaseResponse<AccountBalanceDTO> { Status = false, Message = "Account not found" };

            var accountBalanceDto = new AccountBalanceDTO
            {
                AccountNumber = account.AccountNumber,
                AccountBalance = $"₦{account.AccountBalance:N}"
            };


            return new BaseResponse<AccountBalanceDTO>
{
                Data = accountBalanceDto,
                Status = true,
                Message = "Account balance successfully retrieved"
            };
        }

        public async Task<BaseResponse<string>> Transfer(TransferDTO dto)
        {
            var sender = await _accountRepository.GetAccountByNumber(dto.SendersAccountNumber);
            var receiver = await _accountRepository.GetAccountByNumber(dto.ReceiversAccountNumber);

            if (sender == null)
                return new BaseResponse<string> { Status = false, Message = "Senders account not found" };
            else if (receiver == null)
                return new BaseResponse<string> { Status = false, Message = "Receivers account not found" };

            if (sender.AccountBalance < dto.Amount + 100)
                return new BaseResponse<string> { Status = false, Message = "Insufficient funds" };

            sender.AccountBalance -= dto.Amount;
            receiver.AccountBalance += dto.Amount;

            await _accountRepository.UpdateAccount(sender);
            await _accountRepository.UpdateAccount(receiver);

            return new BaseResponse<string> { Status = true, Message = $"Successfully transferred ₦{dto.Amount:N} to {dto.ReceiversAccountNumber}" };
        }

        public async Task<BaseResponse<string>> Withdraw(WithdrawDTO dto)
        {
            var account = await _accountRepository.GetAccountByNumber(dto.AccountNumber);
            if (account == null)
                return new BaseResponse<string> { Status = false, Message = "Account not found" };

            if (account.AccountBalance < dto.Amount + 100)
                return new BaseResponse<string> { Status = false, Message = "Insufficient funds" };

            account.AccountBalance -= dto.Amount;
            await _accountRepository.UpdateAccount(account);

            return new BaseResponse<string> { Status = true, Message = "Withdrawal successful" };

        }
        private async Task<string> GenerateAccountNumber()
        {
            string accountNumber;
            do
            {
                accountNumber = new Random().Next(1000000000, 1999999999).ToString();
            } while (await _accountRepository.GetAccountByNumber(accountNumber) != null);

            return accountNumber;
        }
    }
}
