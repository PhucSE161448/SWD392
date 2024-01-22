using Restaurant.Application.Interfaces;
using Application.ViewModels.AccountDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] AccountDTO accountDTO)
        {
            var updatedUser = await _accountService.UpdateUserAsync(id, accountDTO);
            if (!updatedUser.Success)
            {
                return NotFound(updatedUser);
            }
            return Ok(updatedUser);
        }

        [Authorize(Roles = "admin")]
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

        [Authorize]
        [HttpPost("change-password/{userId}")]
        public async Task<IActionResult> ChangePassword(int userId, [FromBody] ChangePasswordDTO changePasswordDto)
        {
            var result = await _accountService.ChangePasswordAsync(userId, changePasswordDto);

            if (result.Success)
            {
                return Ok(new { Message = result.Message });
            }
            else
            {
                return BadRequest(new { Message = result.Message });
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{name}")]
        public async Task<IActionResult> SearchByName(string name)
        {
            var result = await _accountService.SearchAccountByNameAsync(name);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [Authorize(Roles = "admin")]
        [HttpGet("{role}")]
        public async Task<IActionResult> SearchByRole([FromRoute] string role)
        {
            var result = await _accountService.SearchAccountByRoleNameAsync(role);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetSortedAccount()
        {
            var result = await _accountService.GetSortedAccountsAsync();

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }
    }
}
