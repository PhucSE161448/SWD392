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
        private readonly IProductRepository _productRepository;

        public UnitOfWork(MixFoodContext foodContext, IAccountRepository accountRepository, IProductRepository productRepository)
        {
            _foodContext = foodContext;
            _accountRepository = accountRepository;
            _productRepository = productRepository;
        }
        public IAccountRepository AccountRepository => _accountRepository;
        public IProductRepository ProductRepository => _productRepository;
        
        public async Task<int> SaveChangeAsync()
        {
            return await _foodContext.SaveChangesAsync();
        }
    }
}
