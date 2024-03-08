using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Interfaces.Store;
using Restaurant.Application.IRepositories.Accounts;
using Restaurant.Application.IRepositories.Categories;
using Restaurant.Application.IRepositories.Ingredient_Type;
using Restaurant.Application.IRepositories.Ingredients;
using Restaurant.Application.IRepositories.New;
using Restaurant.Application.IRepositories.Nutritions;
using Restaurant.Application.IRepositories.Orders;
using Restaurant.Application.IRepositories.Products;
using Restaurant.Application.IRepositories.ProductTemplates;
using Restaurant.Application.IRepositories.Stores;
using Restaurant.Application.IRepositories.TemplateSteps;
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
        private readonly IIngredientRepository _ingredientRepository;
        private readonly INutritionRepository _nutritionRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly ITemplateStepRepository _templateStepRepository;
        private readonly IOrderRepository _orderRepository;
        public UnitOfWork(MixFoodContext foodContext, IAccountRepository accountRepository, IProductRepository productRepository, ICategoryRepository categoryRepository, IProductTemplateRepository productTemplateRepository,
            IIngredientTypeRepository ingredientTypeRepository,INewsRepository newsRepository,
            IIngredientRepository ingredientRepository,INutritionRepository nutritionRepository,
            IStoreRepository storeRepository, ITemplateStepRepository templateStepRepository,
            IOrderRepository orderRepository)
        {
            _foodContext = foodContext;
            _accountRepository = accountRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _productTemplateRepository = productTemplateRepository;
            _ingredientTypeRepository = ingredientTypeRepository;
            _iNewsRepository = newsRepository;
            _ingredientRepository = ingredientRepository;
            _nutritionRepository = nutritionRepository;
            _storeRepository = storeRepository;
            _templateStepRepository = templateStepRepository;
            _orderRepository = orderRepository;
        }
        public IAccountRepository AccountRepository => _accountRepository;
        public IProductRepository ProductRepository => _productRepository;
        public ICategoryRepository CategoryRepository => _categoryRepository;
        public IProductTemplateRepository ProductTemplateRepository => _productTemplateRepository;
        public IIngredientTypeRepository IngredientTypeRepository => _ingredientTypeRepository;
        public INewsRepository NewsRepository => _iNewsRepository;
        public IIngredientRepository IngredientRepository => _ingredientRepository;
        public INutritionRepository NutritionRepository => _nutritionRepository;
        public ITemplateStepRepository TemplateStepRepository => _templateStepRepository;
        public IStoreRepository StoreRepository => _storeRepository;
        public IOrderRepository OrderRepository => _orderRepository;
        public async Task<int> SaveChangeAsync()
        {
            return await _foodContext.SaveChangesAsync();
        }
    }
}
