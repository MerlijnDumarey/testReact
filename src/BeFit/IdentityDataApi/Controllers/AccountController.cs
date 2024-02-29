using IdentityDataApi.DTOs;
using IdentityDataApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace IdentityDataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        //lector can add student by email to db
        public async Task<IActionResult> Create(AccountCreateDto accountCreateDto)
        {
            var response = await _accountService.Create(accountCreateDto.Email);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response.Items.FirstOrDefault());
        }

        [HttpGet]
        public async Task<IActionResult> Login(string token)
        {
            return Ok(await _accountService.Login(token));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(AccountDeleteDto accountDeleteDto)
        {
            var response = await _accountService.Delete(accountDeleteDto.Email);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response.Items.FirstOrDefault());
        }
    }
}
