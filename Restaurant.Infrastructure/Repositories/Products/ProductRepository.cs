using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Interfaces;
using Restaurant.Application.IRepositories.Products;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Infrastructure.Repositories.Products
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly MixFoodContext _dbContext;

        public ProductRepository(
            MixFoodContext context,
            ICurrentTime timeService,
            IClaimsService claimsService
        )
            : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }

        public Task<bool> CheckNameProductExited(string name)
        {
            return _dbContext.Products.AnyAsync(p => p.Name == name);
        }

        public Task<bool> CheckProductExited(int id)
        {
            return _dbContext.Products.AnyAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetSortedProductAsync()
        {

            return await _dbContext.Products.OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<IEnumerable<Product>> SearchProductByNameAsync(string name)
        {
            return await _dbContext.Products.Where(p => p.Name.Contains(name)).ToListAsync();
        }
    }
}
