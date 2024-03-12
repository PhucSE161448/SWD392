using Application.ViewModels.AccountDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Interfaces.Accounts;

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
        public async Task<IActionResult> UpdateIsDelete(int id, bool isDeleted)
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
