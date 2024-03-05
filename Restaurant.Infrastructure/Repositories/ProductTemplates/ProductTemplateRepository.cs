using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Interfaces;
using Restaurant.Application.IRepositories.ProductTemplates;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Infrastructure.Repositories.ProductTemplates
{
    public class ProductTemplateRepository : GenericRepository<ProductTemplate>, IProductTemplateRepository
    {
        private readonly MixFoodContext _dbContext;
        public ProductTemplateRepository(MixFoodContext context, ICurrentTime timeService, IClaimsService claimsService) : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }
        public async Task<List<ProductTemplate>> GetThreeProductsPerCategoryAsync(string? size = null)
        {
            // Retrieve all products and categories
            var products = await _dbContext.ProductTemplates.ToListAsync();
            var categories = await _dbContext.Categories.ToListAsync();

            // Group products by category
            var productsByCategory = products.GroupBy(p => p.CategoryId);

            // Select first three products for each category
            var productsPerCategory = new List<ProductTemplate>();
            if(string.IsNullOrEmpty(size))
            {
                productsPerCategory = products;
            }
            else
            {
                foreach (var category in categories)
                {
                    var categoryProducts = productsByCategory.FirstOrDefault(group => group.Key == category.Id);
                    if (categoryProducts != null)
                    {
                        productsPerCategory.AddRange(categoryProducts.Take(Convert.ToInt32(size)));
                    }
                }
            }
            return productsPerCategory;
        }
    }
}
