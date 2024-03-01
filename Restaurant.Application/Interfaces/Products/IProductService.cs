using Application.ViewModels.AccountDTO;
using Restaurant.Application.Services;
using Restaurant.Application.ViewModels.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Interfaces.Products
{
    public interface IProductService
    {
        Task<ServiceResponse<IEnumerable<ProductDTO>>> GetAllProductAsync();
        Task<ServiceResponse<ProductDTO>> CreateProductAsync(CreatedProductDTO CreatedProductDTO);
        Task<ServiceResponse<ProductDTO>> UpdateProductAsync(int id, ProductDTO ProductDTO);
        Task<ServiceResponse<bool>> DeleteProductAsync(int id);
        Task<ServiceResponse<IEnumerable<ProductDTO>>> SearchProductByNameAsync(string name);
        Task<ServiceResponse<IEnumerable<ProductDTO>>> GetSortedProductAsync();
    }
}
