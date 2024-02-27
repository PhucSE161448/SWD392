using Restaurant.Application.Services;
using Application.ViewModels.AccountDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Interfaces.Accounts
{
    public interface IAccountService
    {
        Task<ServiceResponse<IEnumerable<AccountDTO>>> GetAccountAsync();
        Task<ServiceResponse<AccountDTO>> CreateAccountAsync(CreatedAccountDTO createdAccountDTO);
        Task<ServiceResponse<AccountDTO>> UpdateUserAsync(int id, AccountDTO accountDTO);
        Task<ServiceResponse<bool>> DeleteUserAsync(int id);
        Task<ServiceResponse<string>> ChangePasswordAsync(int userId, ChangePasswordDTO changePasswordDto);
        Task<ServiceResponse<IEnumerable<AccountDTO>>> SearchAccountByNameAsync(string name);
        Task<ServiceResponse<IEnumerable<AccountDTO>>> SearchAccountByRoleNameAsync(string name);
        Task<ServiceResponse<IEnumerable<AccountDTO>>> GetSortedAccountsAsync();
    }
}
