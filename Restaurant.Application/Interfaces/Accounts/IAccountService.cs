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
        Task<ServiceResponse<IEnumerable<AccountsDTO>>> GetAccountAsync();
        Task<ServiceResponse<AccountsDTO>> CreateAccountAsync(CreatedAccountDTO createdAccountDTO);
        Task<ServiceResponse<AccountsDTO>> UpdateUserAsync(int id, AccountsDTO accountDTO);
        Task<ServiceResponse<bool>> DeleteUserAsync(int id);
        Task<ServiceResponse<string>> ChangePasswordAsync(int userId, ChangePasswordDTO changePasswordDto);
        Task<ServiceResponse<bool>> UpdateIsDelete(int id, bool? isDeleted);
        Task<ServiceResponse<UpdateProfileAccountDTO>> UpdateProfileAsync(int id, UpdateProfileAccountDTO accountDTO);
        Task<ServiceResponse<AccountsDTO>> GetAccountByIdAsync(int id);
        Task<ServiceResponse<ProfileAccountDTO>> GetAccountProfileById(int id);

    }
}
