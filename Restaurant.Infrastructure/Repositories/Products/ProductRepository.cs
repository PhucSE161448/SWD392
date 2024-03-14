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
        private readonly IClaimsService _claimsService;
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
            _claimsService = claimsService;
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

        public async Task<List<ProductDTO>> GetProductsByUserId()
        {

            var products = await _dbContext.Products
                .Include(p => p.IngredientProducts)
                    .ThenInclude(ip => ip.Ingredient)
                .Where(p => p.IsDeleted == false) 
                .ToListAsync();
            List<ProductDTO> productDTOs = new List<ProductDTO>();
            if(products != null)
            {
                productDTOs = products.Select(product => new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    ProductTemplateId = product.ProductTemplateId,
                    Ingredients = product.IngredientProducts.Select(ip => ip.Ingredient.Name).ToList()
                }).ToList();
            }
            return productDTOs;
        }

        public async Task<(bool success, Product product)> CreateProductAsync(CreatedProductDTO pro, ProductTemplateDTO productTemplate)
        {
            try
            {
                var temId =  _dbContext.TemplateSteps.FirstOrDefault(x => x.ProuctTemplateId == pro.ProductTemplateId);
                var product = _mapper.Map<Product>(pro);
                product.Name = productTemplate.Name;
                if(pro.Ingredients.ContainsKey(4) && pro.Ingredients.Count == 1)
                {
                    product.Price = productTemplate.Price;
                }
                product.CreatedDate = DateTime.Now;
                product.CreatedBy = _claimsService.GetCurrentUserId;
               

                foreach (var ingredientTypeEntry in pro.Ingredients)
                {
                    var ingredientTypeId = ingredientTypeEntry.Key;
                    var ingredientIds = ingredientTypeEntry.Value;

                    var maxLimit = await _dbContext.IngredientTypeTemplateSteps
                                                .Where(its => its.IngredientTypeId == ingredientTypeId && its.TemplateStepId == temId.Id)
                                                .Select(its => its.QuantityMax)
                                                .FirstOrDefaultAsync();

                    foreach (var ingredientId in ingredientIds)
                    {
                        var ingredientPrice = await _dbContext.Ingredients
                                           .Where(i => i.Id == ingredientId)
                                           .Select(i => i.Price)
                                           .FirstOrDefaultAsync();
                        if (maxLimit != 0 && ingredientIds.Count > maxLimit)
                        {
                            // Max limit exceeded for this ingredient type
                            return (false, null);
                        }

                        var ingredientProduct = new IngredientProduct
                        {
                            ProductId = product.Id,
                            IngredientId = ingredientId,
                            Quantity = 1,
                        };
                        product.Price = ingredientPrice;
                          await _dbContext.IngredientProducts.AddAsync(ingredientProduct);
                    }
                }
                await _dbContext.Products.AddAsync(product);
                await _dbContext.SaveChangesAsync();
                return (true, product);
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }
        public decimal CalculateTotalPrice(IEnumerable<ProductDTO> products)
        {
            decimal totalPrice = 0;

            foreach (var product in products)
            {
                totalPrice += product.Price ;
            }

            return totalPrice;
        }
    }
}
