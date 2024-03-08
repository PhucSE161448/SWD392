using Restaurant.Application.Interfaces.Store;
using Restaurant.Application.IRepositories.Accounts;
using Restaurant.Application.IRepositories.Categories;
using Restaurant.Application.IRepositories.Ingredient_Type;
using Restaurant.Application.IRepositories.Ingredients;
using Restaurant.Application.IRepositories.New;
using Restaurant.Application.IRepositories.Nutritions;
using Restaurant.Application.IRepositories.Products;
using Restaurant.Application.IRepositories.ProductTemplates;
using Restaurant.Application.IRepositories.Stores;
using Restaurant.Application.IRepositories.TemplateSteps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public IAccountRepository AccountRepository { get; }
        public IProductRepository ProductRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IProductTemplateRepository ProductTemplateRepository { get; }
        public IIngredientTypeRepository IngredientTypeRepository { get; }
        public INewsRepository NewsRepository { get; }
        public IIngredientRepository IngredientRepository { get; }
        public INutritionRepository NutritionRepository { get; }
        public IStoreRepository StoreRepository { get; }
        public ITemplateStepRepository TemplateStepRepository { get; }
        public Task<int> SaveChangeAsync();
    }
}
