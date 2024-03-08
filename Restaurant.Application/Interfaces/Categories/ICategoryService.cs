using Restaurant.Application.Services;
using Restaurant.Application.ViewModels.CategoryDTO;
using Restaurant.Application.ViewModels.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Interfaces.Categories
{
    public interface ICategoryService
    {
        Task<ServiceResponse<IEnumerable<CategoryDto>>> GetAllCategoryAsync();
        Task<ServiceResponse<CategoryDto>> GetCategoryAsync(int id);
        Task<ServiceResponse<CategoryDto>> CreateCategoryAsync(AddUpdateCategoryDTO CategoryDto);
        Task<ServiceResponse<CategoryDto>> UpdateCategoryAsync(int id, AddUpdateCategoryDTO CategoryDto);
        Task<ServiceResponse<bool>> DeleteCategoryAsync(int id);
    }
}
