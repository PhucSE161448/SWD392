using Application.ViewModels.AccountDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Interfaces.Accounts;
using Restaurant.Application.ViewModels.AccountDTO;

namespace Restaurant.WebAPI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccountList()
        {
            var User = await _accountService.GetAccountAsync();
            return Ok(User);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountAsyncById(int id) 
        {
            var User = await _accountService.GetAccountByIdAsync(id);
            return Ok(User);
        }
        [HttpGet("Profile/{id}")]
        public async Task<IActionResult> GetAccountProfileById(int id)
        {
            var User = await _accountService.GetAccountProfileById(id);
            return Ok(User);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreatedAccountDTO createdAccountDTO)
        {
            var createdAccount = await _accountService.CreateAccountAsync(createdAccountDTO);

            if (!createdAccount.Success)
            {
                return BadRequest(createdAccount);
            }
            else
            {
                return Ok(createdAccount);
            }
        }

        [HttpPut("Profile/{id}")]
        public async Task<IActionResult> UpdateProfile(int id, [FromBody] UpdateProfileAccountDTO accountDTO)
        {
            var updateProfile = await _accountService.UpdateProfileAsync(id, accountDTO);
            if (!updateProfile.Success)
            {
                return NotFound(updateProfile);
            }
            return Ok(updateProfile);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] AccountsDTO accountDTO)
        {
            var updatedUser = await _accountService.UpdateUserAsync(id, accountDTO);
            if (!updatedUser.Success)
            {
                return NotFound(updatedUser);
            }
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deletedUser = await _accountService.DeleteUserAsync(id);
            if (!deletedUser.Success)
            {
                return NotFound(deletedUser);
            }
            return Ok(deletedUser);
        }

        [HttpPut("Status/{id}")]
        public async Task<IActionResult> UpdateIsDelete(int id,[FromQuery] bool? isDeleted)
        {
            var updatedUser = await _accountService.UpdateIsDelete(id, isDeleted);
            if (!updatedUser.Success)
            {
                return NotFound(updatedUser);
            }
            return Ok(updatedUser);
        }
    }
}
