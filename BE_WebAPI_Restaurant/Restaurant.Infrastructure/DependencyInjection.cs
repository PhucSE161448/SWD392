using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Repositories;
using Restaurant.Application.Services;
using Restaurant.Domain;
using Restaurant.Infrastructures.Mappers;
using Restaurant.Infrastructures.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace Restaurant.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services,string databaseConnection)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();


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
