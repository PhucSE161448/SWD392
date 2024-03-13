using Restaurant.Application.Services;
using Application.ViewModels.AccountDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Application.ViewModels.AccountDTO;

namespace Restaurant.Application.Interfaces.Accounts
{
    public interface IAccountService
    {
        Task<ServiceResponse<IEnumerable<AccountDTO>>> GetAccountAsync();
        Task<ServiceResponse<AccountDTO>> CreateAccountAsync(CreatedAccountDTO createdAccountDTO);
        Task<ServiceResponse<AccountDTO>> UpdateUserAsync(int id, AccountDTO accountDTO);
        Task<ServiceResponse<bool>> DeleteUserAsync(int id);
        Task<ServiceResponse<string>> ChangePasswordAsync(int userId, ChangePasswordDTO changePasswordDto);
        Task<ServiceResponse<bool>> UpdateIsDelete(int id, bool? isDeleted);
        Task<ServiceResponse<UpdateProfileAccountDTO>> UpdateProfileAsync(int id, UpdateProfileAccountDTO accountDTO);
        Task<ServiceResponse<AccountDTO>> GetAccountByIdAsync(int id);
    }
}
