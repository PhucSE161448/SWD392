using System.Data.Common;
using Restaurant.Application.Commons;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Utils;
using Application.ViewModels.AccountDTO;
using Application.ViewModels.AuthenAccountDTO;
using Application.ViewModels.RegisterAccountDTO;
using AutoMapper;
using Restaurant.Domain.Entities;
using Restaurant.Application.Interfaces.Authenticates;

namespace Restaurant.Application.Services.Authenticates
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentTime _currentTime;
        private readonly AppConfiguration _configuration;

        private readonly IMapper _mapper;

        public AuthenticationService(
            IUnitOfWork unitOfWork,
            ICurrentTime currentTime,
            AppConfiguration configuration,
            IMapper mapper
        )
        {
            _unitOfWork = unitOfWork;
            _currentTime = currentTime;
            _configuration = configuration;

            _mapper = mapper;
        }

        public async Task<ServiceResponse<string>> LoginAsync(AuthenAccountDTO authenAccountDto)
        {
            var response = new ServiceResponse<string>();
            try
            {
                var hashedPassword = HashPassword.HashWithSHA256(authenAccountDto.Password);
                var user = await _unitOfWork.AccountRepository.GetUserByEmailAndPasswordHash(
                    authenAccountDto.Email,
                    hashedPassword
                );

                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Invalid username or password";
                    return response;
                }
                if(user.IsDeleted == true)
                {
                    response.Success = true;
                    response.Message = "Account is deleted";
                    return response;
                }
                if (user.IsConfirmed == false)
                {
                    response.Success = true;
                    response.Message = "Please confirm via link in your email box";
                    return response;
                }
                var token = user.GenerateJsonWebToken(
                    _configuration,
                    _configuration.JWTSection.SecretKey,
                    _currentTime.GetCurrentTime()
                );

                response.Success = true;
                response.Message = "Login successfully.";
                response.Data = token;
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

        public async Task<ServiceResponse<AccountDTO>> RegisterAsync(RegisterAccountDTO registerAccountDTO)
        {
            var response = new ServiceResponse<AccountDTO>();

            try
            {
                var exist = await _unitOfWork.AccountRepository.CheckEmailNameExited(registerAccountDTO.Email);
                if (exist)
                {
                    response.Success = false;
                    response.Message = "Email is existed";
                    return response;
                }

                var account = _mapper.Map<Account>(registerAccountDTO);
                account.Password = HashPassword.HashWithSHA256(
                    registerAccountDTO.Password
                );
                // Tạo token ngẫu nhiên
                account.ConfirmationToken = Guid.NewGuid().ToString();

                account.Status = "true";
                account.Role = "Customer";
                await _unitOfWork.AccountRepository.AddAsync(account);
                var confirmationLink = $"https://localhost:7082/swagger/confirm?token={account.ConfirmationToken}";
                var emailSent = await SendEmail.SendConfirmationEmail(account.Email, confirmationLink);
                if (!emailSent)
                {
                    // Xử lý khi gửi email không thành công
                    response.Success = false;
                    response.Message = "Error sending confirmation email.";
                    return response;
                }
                else {
                    var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                    if (isSuccess)
                    {
                        var accountDTO = _mapper.Map<AccountDTO>(account);
                        response.Data = accountDTO; // Chuyển đổi sang AccountDTO
                        response.Success = true;
                        response.Message = "Register successfully.";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Error saving the account.";
                    }
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
    }
}
