using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UPG3ButWithTests.Models;
using UPG3ButWithTests.DTO;
using UPG3ButWithTests.Repository.Interfaces;
using UPG3ButWithTests.Services.Interfaces;

namespace UPG3ButWithTests.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController (ICustomerService customerService) : ControllerBase
    {
        private readonly ICustomerService _customerService = customerService;
        
        [HttpPost("CreateCustomer")]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateCustomer(CreateCustomerDTO createCustomerDTO)
        {
            string result = _customerService.CreateCreateCustomer(createCustomerDTO);
            if (result == "Customer Has been successfully created")
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("GiveLoanToCustomer")]
        [Authorize(Roles ="Admin")]
        public IActionResult GiveLoan(GiveLoanDTO giveLoanDTO)
        {
            string result = _customerService.GiveLoan(giveLoanDTO);
            if (result == "Loan given, success!")
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
