using Application.ViewModels.AccountDTO;
using Restaurant.Application.Services;
using Restaurant.Application.ViewModels.ProductDTO;
using Restaurant.Application.ViewModels.ProductTemplateDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Interfaces.Products
{
    public interface IProductService
    {
        Task<ServiceResponse<List<GetProductDTO>>> GetAllProductAsync(string? name = null);
        Task<ServiceResponse<ProductsDTO>> GetProductAsync(int id);
        Task<ServiceResponse<ProductsDTO>> CreateProductAsync(CreatedProductDTO CreatedProductDTO);
        Task<ServiceResponse<ProductsDTO>> UpdateProductAsync(int id, ProductsDTO ProductDTO);
        Task<ServiceResponse<bool>> DeleteProductAsync(int id);
    }
}
