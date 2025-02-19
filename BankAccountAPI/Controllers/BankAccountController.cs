using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankAccountAPI.Controllers
{
    [ApiController]
    [Route("api/bank")]
    public class BankAccountController : ControllerBase
    {
        private static readonly List<BankAccount> Accounts = new List<BankAccount>
        {
            new BankAccount { AccountNumber = "1234567890", AccountHolder = "John Doe", Balance = 1000 },
            new BankAccount { AccountNumber = "0987654321", AccountHolder = "Jane Doe", Balance = 2000 },
            new BankAccount { AccountNumber = "1122334455", AccountHolder = "Alice Smith", Balance = 500 }
        };

        private bool IsValidAccountNumber(string accountNumber)
        {
            return Regex.IsMatch(accountNumber, "^\\d{10}$");
        }

        [HttpPost("create")]
        public IActionResult CreateAccount([FromBody] BankAccount account)
        {
            if (account == null || string.IsNullOrWhiteSpace(account.AccountNumber) || !IsValidAccountNumber(account.AccountNumber) || string.IsNullOrWhiteSpace(account.AccountHolder))
                return BadRequest(new { Error = "Invalid account details" });

            if (Accounts.Any(a => a.AccountNumber == account.AccountNumber))
                return Conflict(new { Error = "Account already exists" });

            Accounts.Add(account);
            return CreatedAtAction(nameof(GetBalance), new { accountNumber = account.AccountNumber }, new { Success = true, Message = "Account created successfully", Account = account });
        }

        [HttpPost("deposit")]
        public IActionResult Deposit([FromBody] TransactionRequest request)
        {
            if (request == null || !IsValidAccountNumber(request.AccountNumber) || request.Amount <= 0)
                return BadRequest(new { Error = "Invalid request parameters" });

            var account = Accounts.FirstOrDefault(a => a.AccountNumber == request.AccountNumber);
            if (account == null)
                return NotFound(new { Error = "Account not found" });

            account.Balance += request.Amount;
            return Ok(new { Message = "Deposit successful", Balance = account.Balance });
        }

        [HttpPost("withdraw")]
        public IActionResult Withdraw([FromBody] TransactionRequest request)
        {
            if (request == null || !IsValidAccountNumber(request.AccountNumber) || request.Amount <= 0)
                return BadRequest(new { Error = "Invalid request parameters" });

            var account = Accounts.FirstOrDefault(a => a.AccountNumber == request.AccountNumber);
            if (account == null)
                return NotFound(new { Error = "Account not found" });

            if (account.Balance < request.Amount)
                return BadRequest(new { Error = "Insufficient balance" });

            account.Balance -= request.Amount;
            return Ok(new { Message = "Withdrawal successful", Balance = account.Balance });
        }

        [HttpGet("balance/{accountNumber}")]
        public IActionResult GetBalance(string accountNumber)
        {
            if (!IsValidAccountNumber(accountNumber))
                return BadRequest(new { Error = "Invalid account number" });

            var account = Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
            if (account == null)
                return NotFound(new { Error = "Account not found" });

            return Ok(new { Balance = account.Balance });
        }
    }

    public class BankAccount
    {
        public required string AccountNumber { get; set; }
        public required string AccountHolder { get; set; }
        public decimal Balance { get; set; } = 0;
    }

    public class TransactionRequest
    {
        public required string AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
