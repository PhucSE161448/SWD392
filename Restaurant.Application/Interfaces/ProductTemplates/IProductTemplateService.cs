using Restaurant.Application.Services;
using Restaurant.Application.ViewModels.ProductTemplateDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Interfaces.ProductTemplates
{
    public interface IProductTemplateService
    {
        Task<ServiceResponse<IEnumerable<ProductTemplateDTO>>> GetAllProductTemplateAsync(string? size = null);
        Task<ServiceResponse<ProductTemplateDTO>> GetProductTemplateAsync(int id);
        Task<ServiceResponse<ProductTemplateDTO>> CreateProductTemplateAsync(ProductTemplateCreateDTO CreatedProductTemplateDTO, string url);
        Task<ServiceResponse<ProductTemplateDTO>> UpdateProductTemplateAsync(int id, ProductTemplateUpdateDTO ProductTemplateDTO,string url);
        Task<ServiceResponse<bool>> DeleteProductTemplateAsync(int id);
        Task<ServiceResponse<IEnumerable<ProductTemplateDTO>>> SearchProductTemplateByNameAsync(string name);
        Task<ServiceResponse<IEnumerable<ProductTemplateDTO>>> GetSortedProductTemplateAsync();
    }
}
