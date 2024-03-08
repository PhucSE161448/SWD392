using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Services;
using Restaurant.Infrastructures.Mappers;

using Restaurant.Application.Interfaces.Accounts;
using Restaurant.Application.Interfaces.Authenticates;
using Restaurant.Application.Interfaces.Products;
using Restaurant.Application.IRepositories.Accounts;
using Restaurant.Application.IRepositories.Products;
using Restaurant.Application.Services.Accounts;
using Restaurant.Application.Services.Authenticates;
using Restaurant.Application.Services.Products;
using Restaurant.Infrastructure.Repositories.Accounts;
using Restaurant.Infrastructure.Repositories.Products;
using Restaurant.Application.IRepositories.Categories;
using Restaurant.Infrastructure.Repositories.Categories;
using Restaurant.Application.Interfaces.Categories;
using Restaurant.Application.Services.Categories;
using Restaurant.Domain.Entities;
using Restaurant.Application.IRepositories.ProductTemplates;
using Restaurant.Infrastructure.Repositories.ProductTemplates;
using Restaurant.Application.Interfaces.ProductTemplates;
using Restaurant.Application.Services.ProductTemplates;
using Restaurant.Application.IRepositories.Ingredient_Type;
using Restaurant.Infrastructure.Repositories.Ingredient_Type;
using Restaurant.Application.Interfaces.Ingredient_Type;
using Restaurant.Application.Services.Ingredient_Type;
using Restaurant.Application.IRepositories.New;
using Restaurant.Infrastructure.Repositories.New;
using Restaurant.Application.Interfaces.News;
using Restaurant.Application.Services.Newss;
using Restaurant.Application.IRepositories.Ingredients;
using Restaurant.Infrastructure.Repositories.Ingredients;
using Restaurant.Application.Interfaces.Ingredient;
using Restaurant.Application.Services.Ingredients;
using Restaurant.Application.IRepositories.Nutritions;
using Restaurant.Infrastructure.Repositories.Nutritions;
using Restaurant.Application.Interfaces.Nutrition;
using Restaurant.Application.Services.Nutritions;
using Restaurant.Application.IRepositories.Stores;
using Restaurant.Infrastructure.Repositories.Stores;
using Restaurant.Application.Interfaces.Store;
using Restaurant.Application.Services.Stores;
using Restaurant.Application.IRepositories.TemplateSteps;
using Restaurant.Infrastructure.Repositories.TemplateSteps;
using Restaurant.Application.Interfaces.TemplateSteps;
using Restaurant.Application.Services.TemplateSteps;

namespace Restaurant.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services,string databaseConnection)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<ICategoryRepository,CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IProductTemplateRepository, ProductTemplateRepository>();
            services.AddScoped<IProductTemplateService, ProductTemplateService>();

            services.AddScoped<IIngredientTypeRepository, IngredientTypeRepository>();
            services.AddScoped<IIngredientTypeService,IngredientTypeService>();

            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<INewsService, NewsService>();

            services.AddScoped<IIngredientRepository,IngredientRepository>();
            services.AddScoped<IIngredientService,IngredientService>();
            
            services.AddScoped<INutritionRepository,NutritionRepository>();
            services.AddScoped<INutritionService,NutritionService>();

            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IStoreService, StoreService>();

            services.AddScoped<ITemplateStepRepository, TemplateStepRepository>();
            services.AddScoped<ITemplateStepService, TemplateStepService>();

            services.AddSingleton<ICurrentTime, CurrentTime>();
            services.AddDbContext<MixFoodContext>(options =>
            {
                options.UseSqlServer(databaseConnection);
            });
            services.AddAutoMapper(typeof(MapperConfigurationsProfile).Assembly);
            return services;
        }
    }
}
