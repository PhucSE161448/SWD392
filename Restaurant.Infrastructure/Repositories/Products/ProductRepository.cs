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
                var product = _mapper.Map<Product>(pro);
                product.Name = productTemplate.Name;
                product.Price = productTemplate.Price;
                await _dbContext.Products.AddAsync(product);
                await _dbContext.SaveChangesAsync();
                foreach (var kvp in pro.Ingredients)
                {
                    var ingredientTypeId = kvp.Key;
                    foreach (var ingredientId in kvp.Value)
                    {
                        var ingredientProduct = new IngredientProduct
                        {
                            ProductId = product.Id,
                            IngredientId = ingredientId,
                            Quantity = 1,
                        };
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
