using AutoMapper;
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
        private readonly string _currentUserId;
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
            _currentUserId = claimsService.GetCurrentUserId;
        }


        public async Task<ProductsDTO> GetProduct(int id)
        {
            var product = await _dbContext.Products
                .Include(p => p.IngredientProducts)
                    .ThenInclude(ip => ip.Ingredient)
                .FirstOrDefaultAsync(p => p.Id == id);

            var productDTO = new ProductsDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ProductTemplateId = product.ProductTemplateId,
                Quantity = product.Quantity,
                Ingredients = product.IngredientProducts.Select(ip => ip.Ingredient.Name).ToList()
            };

            return productDTO;
        }

        public async Task<List<ProductsDTO>> GetProductsByUserId(string? name)
        {
            List<Product> products = new List<Product>();
            if(!string.IsNullOrEmpty(name))
            {
                products = await _dbContext.Products
               .Include(p => p.IngredientProducts)
                   .ThenInclude(ip => ip.Ingredient)
               .Where(p => p.IsDeleted == false && p.CreatedBy == name)
               .ToListAsync();
            }
            else
            {
                products = await _dbContext.Products
               .Include(p => p.IngredientProducts)
                   .ThenInclude(ip => ip.Ingredient)
               .Where(p => p.IsDeleted == false)
               .ToListAsync();
            }
            List<ProductsDTO> productDTOs = new List<ProductsDTO>();
            if (products != null)
            {
                productDTOs = products.Select(product => new ProductsDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    ProductTemplateId = product.ProductTemplateId,
                    Quantity = product.Quantity,
                    Ingredients = product.IngredientProducts.Select(ip => ip.Ingredient.Name).ToList()
                }).ToList();
            }
            return productDTOs;
        }

      
        public async Task<(bool success, Product product)> CreateProductAsync(CreatedProductDTO pro, ProductTemplateDTO productTemplate)
        {
            try
            {
                var temId = _dbContext.TemplateSteps.FirstOrDefault(x => x.ProuctTemplateId == pro.ProductTemplateId);
                var product = _mapper.Map<Product>(pro);
                product.Name = productTemplate.Name;
                product.Price = productTemplate.Price * pro.Quantity;
                product.CreatedBy = _currentUserId;
                product.CreatedDate = DateTime.Now;
                await _dbContext.Products.AddAsync(product);
                await _dbContext.SaveChangesAsync();
                foreach (var ingredientTypeEntry in pro.Ingredients)
                {
                    var ingredientTypeId = ingredientTypeEntry.Key;
                    var ingredientIds = ingredientTypeEntry.Value;

                    var minLimit = await _dbContext.IngredientTypeTemplateSteps
                                                .Where(its => its.IngredientTypeId == ingredientTypeId && its.TemplateStepId == temId.Id)
                                                .Select(its => its.QuantityMin)
                                                .FirstOrDefaultAsync();
                    if (ingredientIds.Count < minLimit)
                    {
                        return (false, null);
                    }

                    foreach (var ingredientId in ingredientIds)
                    {
                        var ingredientPrice = await _dbContext.Ingredients
                                           .Where(i => i.Id == ingredientId)
                                           .Select(i => i.Price)
                                           .FirstOrDefaultAsync();
                       

                        var ingredientProduct = new IngredientProduct
                        {
                            Product = product,
                            IngredientId = ingredientId,
                            Quantity = pro.Quantity,
                        };
                        product.Price += ingredientPrice;
                        await _dbContext.IngredientProducts.AddAsync(ingredientProduct);
                    }
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
