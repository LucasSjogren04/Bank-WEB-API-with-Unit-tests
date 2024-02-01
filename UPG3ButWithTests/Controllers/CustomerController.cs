using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UPG3ButWithTests.Models;
using UPG3ButWithTests.Services.Interfaces;

namespace UPG3ButWithTests.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController (ICustomerService customerSerice) : ControllerBase
    {
        private readonly ICustomerService _customerSerice = customerSerice;

        [HttpPut("TransferMoney")]
        [Authorize(Roles = "Customer")]
        public IActionResult TranserMoney(int from, int to, decimal amount)
        {
            string loginIdS = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int loginId = int.Parse(loginIdS);
            
            string result = _customerSerice.TransferMoney(from, to, amount, loginId);
            if (result == "Money transefered, success!")
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("AccountOverview")]
        [Authorize(Roles = "Customer")]
        public IActionResult GetAccountOverview()
        {
            string loginIdS = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int loginId = int.Parse(loginIdS);

            string result = _customerSerice.GetAccountOverview(loginId);
            if (result != "You don't have any accounts" ||  result != "Unauthorized") 
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("GetTransactionsOnAccount")]
        [Authorize(Roles ="Customer")]
        public IActionResult GetTransactionsOnAccount(int accountId) 
        {
            string loginIdS = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int loginId = int.Parse(loginIdS);

            string result = _customerSerice.GetTransactionsOnAccount(loginId, accountId);
            if(result.Contains("Transactions on account"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost]
        [Authorize(Roles ="Customer")]
        public IActionResult CreateAnotherAccount(int accountTypesId)
        {
            string loginIdS = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int loginId = int.Parse(loginIdS);

            string result = _customerSerice.CreateAnotherAccount(loginId, accountTypesId);
            if (result == "Account created!")
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
