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
