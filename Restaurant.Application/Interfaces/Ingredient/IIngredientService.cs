using Restaurant.Application.Services;
using Restaurant.Application.ViewModels.IngredientsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Interfaces.Ingredient
{
    public interface IIngredientService
    {
        Task<ServiceResponse<IEnumerable<IngredientDTO>>> GetAllIngredientAsync();
        Task<ServiceResponse<IngredientDTO>> CreateIngredientAsync(IngredientAddDTO CreatedIngredientDTO, string url);
        Task<ServiceResponse<IngredientDTO>> UpdateIngredientAsync(int id, IngredientUpdateDTO IngredientDTO, string url);
        Task<ServiceResponse<bool>> DeleteIngredientAsync(int id);
        Task<ServiceResponse<IEnumerable<IngredientDTO>>> SearchIngredientByNameAsync(string name);
        Task<ServiceResponse<IEnumerable<IngredientDTO>>> GetSortedIngredientAsync();
    }
}
