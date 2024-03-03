using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Interfaces;
using Restaurant.Application.IRepositories.Accounts;
using Restaurant.Application.IRepositories.Categories;
using Restaurant.Application.IRepositories.Ingredient_Type;
using Restaurant.Application.IRepositories.New;
using Restaurant.Application.IRepositories.Products;
using Restaurant.Application.IRepositories.ProductTemplates;
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
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductTemplateRepository _productTemplateRepository;
        private readonly IIngredientTypeRepository _ingredientTypeRepository;
        private readonly INewsRepository _iNewsRepository;

        public UnitOfWork(MixFoodContext foodContext, IAccountRepository accountRepository, IProductRepository productRepository, ICategoryRepository categoryRepository, IProductTemplateRepository productTemplateRepository, IIngredientTypeRepository ingredientTypeRepository,INewsRepository newsRepository)
        {
            _foodContext = foodContext;
            _accountRepository = accountRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _productTemplateRepository = productTemplateRepository;
            _ingredientTypeRepository = ingredientTypeRepository;
            _iNewsRepository = newsRepository;
        }
        public IAccountRepository AccountRepository => _accountRepository;
        public IProductRepository ProductRepository => _productRepository;
        public ICategoryRepository CategoryRepository => _categoryRepository;
        public IProductTemplateRepository ProductTemplateRepository => _productTemplateRepository;
        public IIngredientTypeRepository IngredientTypeRepository => _ingredientTypeRepository;
        public INewsRepository NewsRepository => _iNewsRepository;
        public async Task<int> SaveChangeAsync()
        {
            return await _foodContext.SaveChangesAsync();
        }
    }
}
