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
        Task<ServiceResponse<IEnumerable<CategoryDTO>>> GetAllCategoryAsync();
        Task<ServiceResponse<CategoryDTO>> CreateCategoryAsync(CategoryDTO CategoryDto);
        Task<ServiceResponse<CategoryDTO>> UpdateCategoryAsync(int id, CategoryDTO CategoryDto);
        Task<ServiceResponse<bool>> DeleteCategoryAsync(int id);
    }
}
