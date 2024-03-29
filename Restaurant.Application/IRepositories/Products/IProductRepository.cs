﻿using Restaurant.Application.ViewModels.ProductDTO;
using Restaurant.Application.ViewModels.ProductTemplateDTO;
using Restaurant.Application.ViewModels.TemplateStepsDTO;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.IRepositories.Products
{
    public interface IProductRepository : IGenericRepository<Product>
    {

        Task<(bool success, Product product)> CreateProductAsync(CreatedProductDTO pro, ProductTemplateDTO productTemplate);
        Task<ProductsDTO> GetProduct(int id);
        Task<List<ProductsDTO>> GetProductsByUserId(string? name );
    }
}
