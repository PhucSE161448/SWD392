using Restaurant.Application.Services;
using Restaurant.Application.ViewModels.Ingredient_TypeDTO;
using Restaurant.Application.ViewModels.ProductTemplateDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Interfaces.Ingredient_Type
{
    public interface IIngredientTypeService
    {
        Task<ServiceResponse<IEnumerable<IngredientTypeDTO>>> GetAllIngredientTypeAsync();
        Task<ServiceResponse<IngredientTypeDTO>> GetIngredientTypeAsync(int id);
        Task<ServiceResponse<IngredientTypeDTO>> CreateIngredientTypeAsync(AddUpdateIngredientTypeDTO IngredientTypeDto);
        Task<ServiceResponse<IngredientTypeDTO>> UpdateIngredientTypeAsync(int id, AddUpdateIngredientTypeDTO IngredientTypeDto);
        Task<ServiceResponse<bool>> DeleteIngredientTypeAsync(int id);
        Task<ServiceResponse<bool>> UpdateIsDelete(int id, bool? isDeleted);
    }
}
