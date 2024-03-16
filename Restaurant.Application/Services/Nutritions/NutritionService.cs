using AutoMapper;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Interfaces.Nutrition;
using Restaurant.Application.ViewModels.NutritionsDTO;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Services.Nutritions
{
    public class NutritionService : INutritionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NutritionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<NutritionDTO>> CreateNutritionAsync(NutritionAddDTO CreatedNutritionDTO, string url)
        {
            var response = new ServiceResponse<NutritionDTO>();
            try
            {
                var Nutrition = _mapper.Map<Nutrition>(CreatedNutritionDTO);
                Nutrition.ImageUrl = url;
                await _unitOfWork.NutritionRepository.AddAsync(Nutrition);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var NutritionDTO = _mapper.Map<NutritionDTO>(Nutrition);
                    response.Data = NutritionDTO;
                    response.Success = true;
                    response.Message = "Nutrition created successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Create Nutrition failed";
                }

            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteNutritionAsync(int id)
        {
            var response = new ServiceResponse<bool>();
            var exist = await _unitOfWork.NutritionRepository.GetByIdAsync(id);
            if (exist == null)
            {
                response.Success = false;
                response.Message = "Nutrition not found";
                return response;
            }
            try
            {
                _unitOfWork.NutritionRepository.SoftRemove(exist);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                //Get the ImageLocalPath
                Uri uri = new Uri(exist.ImageUrl);
                string lastSegment = uri.Segments.LastOrDefault();
                string filePath = @"wwwroot\ProductImage\" + lastSegment;
                var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                FileInfo file = new FileInfo(directoryLocation);
                if (file.Exists)
                {
                    file.Delete();
                }

                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "Nutrition deleted successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Delete Nutrition failed";
                }
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<NutritionDTO>>> GetAllNutritionAsync()
        {
            var _response = new ServiceResponse<IEnumerable<NutritionDTO>>();
            try
            {
                var Nutritions = await _unitOfWork.NutritionRepository.GetAllAsync();
                var NutritionDTOs = new List<NutritionDTO>();
                foreach (var pro in Nutritions)
                {
                    if ((bool)!pro.IsDeleted)
                    {
                        NutritionDTOs.Add(_mapper.Map<NutritionDTO>(pro));
                    }
                }
                if (NutritionDTOs.Count != 0)
                {
                    _response.Success = true;
                    _response.Message = "Nutrition retrieved successfully";
                    _response.Data = NutritionDTOs;
                }
                else
                {
                    _response.Success = true;
                    _response.Message = "Nutrition not found";
                }
            }
            catch (DbException ex)
            {
                _response.Success = false;
                _response.Message = "Database error occurred.";
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }
            return _response;
        }
        public async Task<ServiceResponse<NutritionDTO>> GetNutritionByIngredientId(int ingredientId)
        {
            var response = new ServiceResponse<NutritionDTO>();
            if(ingredientId == 0)
            {
                response.Success = false;
                response.Message = "Ingredient not found";
            }
            try
            {
                var Nutritions = await _unitOfWork.NutritionRepository.GetAsync(x => x.IngredientId == ingredientId);
                if (Nutritions != null)
                {
                    response.Success = true;
                    response.Message = "Nutrition retrieved successfully";
                    response.Data = _mapper.Map<NutritionDTO>(Nutritions);
                }
                else
                {
                    response.Success = true;
                    response.Message = "Nutrition not found";
                }
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }
        public async Task<ServiceResponse<NutritionDTO>> GetNutritionAsync(int id)
        {
            var _response = new ServiceResponse<NutritionDTO>();
            try
            {
                var Nutritions = await _unitOfWork.NutritionRepository.GetAsync(x => x.Id == id);
                if (Nutritions !=  null)
                {
                    _response.Success = true;
                    _response.Message = "Nutrition retrieved successfully";
                    _response.Data = _mapper.Map<NutritionDTO>(Nutritions);
                }
                else
                {
                    _response.Success = true;
                    _response.Message = "Nutrition not found";
                }
            }
            catch (DbException ex)
            {
                _response.Success = false;
                _response.Message = "Database error occurred.";
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
            }
            return _response;
        }
        public async Task<ServiceResponse<IEnumerable<NutritionDTO>>> GetSortedNutritionAsync()
        {
            var response = new ServiceResponse<IEnumerable<NutritionDTO>>();
            try
            {
                var Nutritions = await _unitOfWork.NutritionRepository.GetAllAsync();
                var NutritionDTOs = new List<NutritionDTO>();
                foreach (var pro in Nutritions)
                {
                    if ((bool)!pro.IsDeleted)
                    {
                        NutritionDTOs.Add(_mapper.Map<NutritionDTO>(pro));
                    }
                }
                if (NutritionDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Nutrition retrieved successfully";
                    response.Data = NutritionDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Nutrition not found";
                }
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<NutritionDTO>>> SearchNutritionByNameAsync(string name)
        {
            var response = new ServiceResponse<IEnumerable<NutritionDTO>>();
            try
            {
                var Nutritions = await _unitOfWork.NutritionRepository.GetAllAsync();
                var NutritionDTOs = new List<NutritionDTO>();
                foreach (var pro in Nutritions)
                {
                    if ((bool)!pro.IsDeleted)
                    {
                        NutritionDTOs.Add(_mapper.Map<NutritionDTO>(pro));
                    }
                }
                if (NutritionDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Nutrition retrieved successfully";
                    response.Data = NutritionDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Nutrition not found";
                }
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }

        public async Task<ServiceResponse<NutritionDTO>> UpdateNutritionAsync(int id, NutritionUpdateDTO NutritionDTO, string url)
        {
            var response = new ServiceResponse<NutritionDTO>();
            var exist = await _unitOfWork.NutritionRepository.GetByIdAsync(id);
            if (exist == null)
            {
                response.Success = false;
                response.Message = "Nutrition not found";
                return response;
            }
            try
            {
                var Nutrition = _mapper.Map(NutritionDTO, exist);
                if (string.IsNullOrEmpty(url))
                {
                    Nutrition.ImageUrl = exist.ImageUrl;
                }
                else
                {
                    Nutrition.ImageUrl = url;
                }
                _unitOfWork.NutritionRepository.Update(Nutrition);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "Nutrition updated successfully";
                    response.Data = _mapper.Map<NutritionDTO>(Nutrition);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Update Nutrition failed";
                }
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }
        //create a method to get nutrition by id ingredient
        public async Task<ServiceResponse<IEnumerable<NutritionDTO>>> GetNutritionByIngredientIdAsync(int id)
        {
            var response = new ServiceResponse<IEnumerable<NutritionDTO>>();
            try
            {
                var Nutritions = await _unitOfWork.NutritionRepository.GetAllAsync();
                var NutritionDTOs = new List<NutritionDTO>();
                foreach (var pro in Nutritions)
                {
                    if ((bool)!pro.IsDeleted && pro.IngredientId == id)
                    {
                        NutritionDTOs.Add(_mapper.Map<NutritionDTO>(pro));
                    }
                }
                if (NutritionDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Nutrition retrieved successfully";
                    response.Data = NutritionDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Nutrition not found";
                }
            }
            catch (DbException ex)
            {
                response.Success = false;
                response.Message = "Database error occurred.";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;
        }
        public async Task<ServiceResponse<bool>> UpdateIsDelete(int id, bool? isDeleted)
        {
            var response = new ServiceResponse<bool>();

            var exist = await _unitOfWork.NutritionRepository.GetByIdAsync(id);
            if (exist == null)
            {
                response.Success = false;
                response.Message = "Account is not existed";
                return response;
            }

            try
            {
                if (isDeleted.HasValue)
                {
                    exist.IsDeleted = isDeleted;
                }
                _unitOfWork.NutritionRepository.Update(exist);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "Account update successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error update the account.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error";
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }
    }
}
