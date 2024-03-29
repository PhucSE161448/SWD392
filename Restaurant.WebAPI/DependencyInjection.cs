﻿using Restaurant.Application.Interfaces;
using Restaurant.Application.Interfaces.Help;
using Restaurant.Application.Services.Help;
using Restaurant.Application.ViewModels.Help;
using Restaurant.Infrastructure;
using Restaurant.WebAPI.Services;
using System.Diagnostics;

namespace Restaurant.WebAPI
{
    public static class DependencyInjection
    {
      
        public static IServiceCollection AddWebAPIService(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IClaimsService, ClaimsService>();
          
           
            services.AddHttpContextAccessor();
            return services;
        }
      
    }

}
