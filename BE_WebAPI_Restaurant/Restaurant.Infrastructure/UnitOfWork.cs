using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MixFoodContext _foodContext;

        private readonly IAccountRepository _accountRepository;

        public UnitOfWork(MixFoodContext foodContext, IAccountRepository accountRepository)
        {
            _foodContext = foodContext;
            _accountRepository = accountRepository;
        }
        public IAccountRepository AccountRepository => _accountRepository;
        
        public async Task<int> SaveChangeAsync()
        {
            return await _foodContext.SaveChangesAsync();
        }
    }
}
