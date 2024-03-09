﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Interfaces;
using Restaurant.Application.IRepositories.Products;
using Restaurant.Application.ViewModels.ProductDTO;
using Restaurant.Application.ViewModels.ProductTemplateDTO;
using Restaurant.Application.ViewModels.TemplateStepsDTO;
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
        private readonly IMapper _mapper;
        public ProductRepository(
            MixFoodContext context,
            ICurrentTime timeService,
            IClaimsService claimsService,
            IMapper mapper
        )
            : base(context, timeService, claimsService)
        {
            _dbContext = context;
            _mapper = mapper;
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
        public async Task<ProductDTO> GetProduct(int id)
        {
            var product = await _dbContext.Products
                .Include(p => p.IngredientProducts)
                    .ThenInclude(ip => ip.Ingredient) 
                .FirstOrDefaultAsync(p => p.Id == id);
                
            var productDTO = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ProductTemplateId = product.ProductTemplateId,
                Ingredients = product.IngredientProducts.Select(ip => ip.Ingredient.Name).ToList()
            };

            return productDTO;
        }
        public async Task<(bool success, Product product)> CreateProductAsync(CreatedProductDTO pro, ProductTemplateDTO productTemplate)
        {
            try
            {
                var product = _mapper.Map<Product>(pro);
                product.Name = productTemplate.Name;
                product.Price = productTemplate.Price;
                await _dbContext.Products.AddAsync(product);
                await _dbContext.SaveChangesAsync();
                foreach (var ingredientId in pro.IngredientId)
                {
                    var ingredientProduct = new IngredientProduct
                    {
                        ProductId = product.Id,
                        IngredientId = ingredientId,
                        Quantity = pro.Quantity,
                    };
                    await _dbContext.IngredientProducts.AddAsync(ingredientProduct);
                }
                await _dbContext.SaveChangesAsync();
                return (true, product);
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }
    }
}
