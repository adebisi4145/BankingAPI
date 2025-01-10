using BankingAPI.DTOs;
using BankingAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Status = false, Message = "Invalid request data", Errors = ModelState });

            var response = await _accountService.CreateAccount(dto);

            if (!response.Status)
                return BadRequest(new { Status = false, Message = response.Message });

            return Ok(new { Status = true, Message = response.Message, Data = response.Data });
        }

        [HttpGet("balance/{accountNumber}")]
        public async Task<IActionResult> GetAccountBalance(string accountNumber)
        {
            var response = await _accountService.GetAccountBalance(accountNumber);

            if (!response.Status)
                return BadRequest(new { Status = false, Message = response.Message });

            return Ok(new { Status = true, Message = response.Message, Data = response.Data });
        }

        [HttpPut("deposit")]
        public async Task<IActionResult> Deposit([FromBody] DepositDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Status = false, Message = "Invalid request data", Errors = ModelState });

            var response = await _accountService.Deposit(dto);

            if (!response.Status)
                return BadRequest(new { Status = false, Message = response.Message });

            return Ok(new { Status = true, Message = response.Message });
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Status = false, Message = "Invalid request data", Errors = ModelState });

            var response = await _accountService.Transfer(dto);

            if (!response.Status)
                return BadRequest(new { Status = false, Message = response.Message });

            return Ok(new { Status = true, Message = response.Message });
        }

        [HttpPut("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Status = false, Message = "Invalid request data", Errors = ModelState });

            var response = await _accountService.Withdraw(dto);

            if (!response.Status)
                return BadRequest(new { Status = false, Message = response.Message });

            return Ok(new { Status = true, Message = response.Message });
        }
    }
}
