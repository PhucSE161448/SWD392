using Application.ViewModels.AccountDTO;
using Application.ViewModels.AuthenAccountDTO;
using Application.ViewModels.RegisterAccountDTO;
using Restaurant.Application.Services;

namespace Restaurant.Application.Interfaces.Authenticates
{
    public interface IAuthenticationService
    {
        public Task<ServiceResponse<AccountsDTO>> RegisterAsync(RegisterAccountDTO registerAccountDTO);
        public Task<ServiceResponse<string>> LoginAsync(AuthenAccountDTO accountDto);
    }
}