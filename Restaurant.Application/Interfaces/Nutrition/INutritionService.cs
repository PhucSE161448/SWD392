using Restaurant.Application.Services;
using Restaurant.Application.ViewModels.NutritionsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Interfaces.Nutrition
{
    public interface INutritionService
    {
        Task<ServiceResponse<IEnumerable<NutritionDTO>>> GetAllNutritionAsync();
        Task<ServiceResponse<NutritionDTO>> GetNutritionAsync(int id);
        Task<ServiceResponse<NutritionDTO>> CreateNutritionAsync(NutritionAddDTO CreatedNutritionDTO, string url);
        Task<ServiceResponse<NutritionDTO>> UpdateNutritionAsync(int id, NutritionUpdateDTO NutritionDTO, string url);
        Task<ServiceResponse<bool>> DeleteNutritionAsync(int id);
        Task<ServiceResponse<IEnumerable<NutritionDTO>>> SearchNutritionByNameAsync(string name);
        Task<ServiceResponse<IEnumerable<NutritionDTO>>> GetSortedNutritionAsync();
        Task<ServiceResponse<IEnumerable<NutritionDTO>>> GetNutritionByIngredientIdAsync(int id);
        Task<ServiceResponse<NutritionDTO>> GetNutritionByIngredientId(int ingredientId);
    }
}
