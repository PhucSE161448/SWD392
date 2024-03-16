using System.Data.Common;
using Restaurant.Application.Commons;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Utils;
using Application.ViewModels.AccountDTO;
using AutoMapper;
using Restaurant.Domain.Entities;
using Restaurant.Application.Interfaces.Accounts;
using Restaurant.Application.ViewModels.AccountDTO;
using Restaurant.Application.ViewModels.NewsDTO;

namespace Restaurant.Application.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<AccountDTO>> CreateAccountAsync(CreatedAccountDTO createdAccountDTO)
        {
            var response = new ServiceResponse<AccountDTO>();

            var exist = await _unitOfWork.AccountRepository.CheckEmailNameExited(createdAccountDTO.Email);
            if (exist)
            {
                response.Success = false;
                response.Message = "Email is existed";
                return response;
            }
            try
            {
                var account = _mapper.Map<Account>(createdAccountDTO);
                account.Password = HashPassword.HashWithSHA256(createdAccountDTO.Password);

                account.Status = "true";
                await _unitOfWork.AccountRepository.AddAsync(account);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var accountDTO = _mapper.Map<AccountDTO>(account);
                    response.Data = accountDTO; // Chuyển đổi sang AccountDTO
                    response.Success = true;
                    response.Message = "User created successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error saving the user.";
                }
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteUserAsync(int id)
        {
            var response = new ServiceResponse<bool>();

            var exist = await _unitOfWork.AccountRepository.GetByIdAsync(id);
            if (exist == null)
            {
                response.Success = false;
                response.Message = "Account is not existed";
                return response;
            }

            try
            {
                _unitOfWork.AccountRepository.SoftRemove(exist);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "Account deleted successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error deleting the account.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<AccountDTO>>> GetAccountAsync()
        {
            var _response = new ServiceResponse<IEnumerable<AccountDTO>>();

            try
            {
                var accounts = await _unitOfWork.AccountRepository.GetAllAsync();

                var accountDTOs = new List<AccountDTO>();

                foreach (var acc in accounts)
                {
                    if (acc.IsConfirmed == true)
                    {
                        accountDTOs.Add(_mapper.Map<AccountDTO>(acc));
                    }
                   

                }

                if (accountDTOs.Count != 0)
                {
                    _response.Success = true;
                    _response.Message = "Account retrieved successfully";
                    _response.Data = accountDTOs;
                }
                else
                {
                    _response.Success = true;
                    _response.Message = "Not have Account";
                }

            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }

            return _response;
        }

        public async Task<ServiceResponse<AccountDTO>> UpdateUserAsync(int id, AccountDTO accountDTO)
        {
            var response = new ServiceResponse<AccountDTO>();

            try
            {
                var existingUser = await _unitOfWork.AccountRepository.GetByIdAsync(id);

                if (existingUser == null)
                {
                    response.Success = false;
                    response.Message = "Account not found.";
                    return response;
                }

                if ((bool)existingUser.IsDeleted)
                {
                    response.Success = false;
                    response.Message = "Account is deleted in system";
                    return response;
                }

                // Map accountDT0 => existingUser
                var updated = _mapper.Map(accountDTO, existingUser);
                updated.Password = HashPassword.HashWithSHA256(accountDTO.Password);

                _unitOfWork.AccountRepository.Update(updated);

                var updatedUserDto = _mapper.Map<AccountDTO>(updated);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Data = updatedUserDto;
                    response.Success = true;
                    response.Message = "Account updated successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error updating the account.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<string>> ChangePasswordAsync(int userId, ChangePasswordDTO changePasswordDto)
        {
            var response = new ServiceResponse<string>();

            // Kiểm tra xem người dùng có tồn tại không
            var user = await _unitOfWork.AccountRepository.GetByIdAsync(userId);
            if (user == null)
            {
                response.Success = false;
                response.Message = "Account not found";
                return response;
            }

            // Xác minh mật khẩu cũ
            var hashedOldPassword = HashPassword.HashWithSHA256(changePasswordDto.OldPassword);
            if (user.Password != hashedOldPassword)
            {
                response.Success = false;
                response.Message = "Incorrect old password";
                return response;
            }

            // Kiểm tra mật khẩu mới và xác nhận mật khẩu mới (nếu cần)
            if (changePasswordDto.NewPassword != changePasswordDto.ConfirmNewPassword)
            {
                response.Success = false;
                response.Message = "New password and confirmation do not match";
                return response;
            }
             
            // Băm mật khẩu mới
            var hashedNewPassword = HashPassword.HashWithSHA256(changePasswordDto.NewPassword);

            // Lưu mật khẩu mới vào cơ sở dữ liệu
            user.Password = hashedNewPassword;
            var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

            if (!isSuccess)
            {
                response.Success = true;
                response.Message = "Password changed fail.";
                return response;
            }

            response.Success = true;
            response.Message = "Password changed successfully.";
            return response;

        }

     

        public async Task<ServiceResponse<bool>> UpdateIsDelete(int id, bool? isDeleted)
        {
            var response = new ServiceResponse<bool>();

            var exist = await _unitOfWork.AccountRepository.GetByIdAsync(id);
            if (exist == null)
            {
                response.Success = false;
                response.Message = "Account is not existed";
                return response;
            }

            try
            {
                if(isDeleted.HasValue)
                {
                    exist.IsDeleted = isDeleted;
                }
                _unitOfWork.AccountRepository.Update(exist);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "Account update successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error update the account.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<UpdateProfileAccountDTO>> UpdateProfileAsync(int id, UpdateProfileAccountDTO accountDTO)
        {
            var response = new ServiceResponse<UpdateProfileAccountDTO>();
            var exist = await _unitOfWork.AccountRepository.GetByIdAsync(id);
            if (exist == null)
            {
                response.Success = false;
                response.Message = "account not found";
                return response;
            }
            try
            {
                var acc = _mapper.Map(accountDTO, exist);
                _unitOfWork.AccountRepository.Update(acc);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "Account updated successfully";
                    response.Data = _mapper.Map<UpdateProfileAccountDTO>(acc);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Account News failed";
                }
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }

        public async Task<ServiceResponse<AccountDTO>> GetAccountByIdAsync(int id)
        {
            var response = new ServiceResponse<AccountDTO>();
            try
            {
                var acc = await _unitOfWork.AccountRepository.GetAsync(x => x.Id == id);
                if (acc != null)
                {
                    response.Success = true;
                    response.Message = "Account retrieved successfully";
                    response.Data = _mapper.Map<AccountDTO>(acc);
                }
                else
                {
                    response.Success = true;
                    response.Message = "Account not found";
                }
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = null;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }
            return response;
        }

        public async Task<ServiceResponse<ProfileAccountDTO>> GetAccountProfileById(int id)
        {
            var response = new ServiceResponse<ProfileAccountDTO>();
            try
            {
                var acc = await _unitOfWork.AccountRepository.GetAsync(x => x.Id == id);
                if (acc != null)
                {
                    response.Success = true;
                    response.Message = "Account retrieved successfully";
                    response.Data = _mapper.Map<ProfileAccountDTO>(acc);
                }
                else
                {
                    response.Success = true;
                    response.Message = "Account not found";
                }
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = null;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }
            return response;
        }
    }
}
