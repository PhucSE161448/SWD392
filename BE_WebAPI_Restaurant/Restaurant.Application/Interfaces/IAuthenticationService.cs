using Application.ViewModels.AccountDTO;
using Application.ViewModels.AuthenAccountDTO;
using Application.ViewModels.RegisterAccountDTO;
using Restaurant.Application.Services;

namespace Restaurant.Application.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<ServiceResponse<AccountDTO>> RegisterAsync(RegisterAccountDTO registerAccountDTO);
        public Task<ServiceResponse<string>> LoginAsync(AuthenAccountDTO accountDto);
    }
}