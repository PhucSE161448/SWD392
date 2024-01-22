using Restaurant.Application.Interfaces;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Repositories
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<Account> GetUserByEmailAndPasswordHash(string email, string passwordHash);

        Task<bool> CheckEmailNameExited(string username);
        Task<Account> GetUserByConfirmationToken(string token);
        Task<IEnumerable<Account>> SearchAccountByNameAsync(string name);
        Task<IEnumerable<Account>> SearchAccountByRoleNameAsync(string roleName);
        Task<IEnumerable<Account>> GetSortedAccountAsync();
    }
}
