﻿using Restaurant.Application.IRepositories.Accounts;
using Restaurant.Application.IRepositories.Categories;
using Restaurant.Application.IRepositories.Ingredient_Type;
using Restaurant.Application.IRepositories.Products;
using Restaurant.Application.IRepositories.ProductTemplates;
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
        public Task<int> SaveChangeAsync();
    }
}