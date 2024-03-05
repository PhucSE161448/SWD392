using AutoMapper;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Interfaces.Ingredient;
using Restaurant.Application.ViewModels.IngredientsDTO;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Services.Ingredients
{
    public class IngredientService : IIngredientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public IngredientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<IngredientDTO>> CreateIngredientAsync(IngredientAddDTO CreatedIngredientDTO, string url)
        {
            var response = new ServiceResponse<IngredientDTO>();
            try
            {
                var Ingredient = _mapper.Map<Ingredient>(CreatedIngredientDTO);
                Ingredient.ImageUrl = url;
                await _unitOfWork.IngredientRepository.AddAsync(Ingredient);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var IngredientDTO = _mapper.Map<IngredientDTO>(Ingredient);
                    response.Data = IngredientDTO;
                    response.Success = true;
                    response.Message = "Ingredient created successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Create Ingredient failed";
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

        public async Task<ServiceResponse<bool>> DeleteIngredientAsync(int id)
        {
            var response = new ServiceResponse<bool>();
            var exist = await _unitOfWork.IngredientRepository.GetByIdAsync(id);
            if (exist == null)
            {
                response.Success = false;
                response.Message = "Ingredient not found";
                return response;
            }
            try
            {
                _unitOfWork.IngredientRepository.SoftRemove(exist);
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
                    response.Message = "Ingredient deleted successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Delete Ingredient failed";
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

        public async Task<ServiceResponse<IEnumerable<IngredientDTO>>> GetAllIngredientAsync()
        {
            var _response = new ServiceResponse<IEnumerable<IngredientDTO>>();
            try
            {
                var Ingredients = await _unitOfWork.IngredientRepository.GetAllAsync();
                var IngredientDTOs = new List<IngredientDTO>();
                foreach (var pro in Ingredients)
                {
                    if ((bool)!pro.IsDeleted)
                    {
                        IngredientDTOs.Add(_mapper.Map<IngredientDTO>(pro));
                    }
                }
                if (IngredientDTOs.Count != 0)
                {
                    _response.Success = true;
                    _response.Message = "Ingredient retrieved successfully";
                    _response.Data = IngredientDTOs;
                }
                else
                {
                    _response.Success = true;
                    _response.Message = "Ingredient not found";
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

        public async Task<ServiceResponse<IEnumerable<IngredientDTO>>> GetSortedIngredientAsync()
        {
            var response = new ServiceResponse<IEnumerable<IngredientDTO>>();
            try
            {
                var Ingredients = await _unitOfWork.IngredientRepository.GetAllAsync();
                var IngredientDTOs = new List<IngredientDTO>();
                foreach (var pro in Ingredients)
                {
                    if ((bool)!pro.IsDeleted)
                    {
                        IngredientDTOs.Add(_mapper.Map<IngredientDTO>(pro));
                    }
                }
                if (IngredientDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Ingredient retrieved successfully";
                    response.Data = IngredientDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Ingredient not found";
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

        public async Task<ServiceResponse<IEnumerable<IngredientDTO>>> SearchIngredientByNameAsync(string name)
        {
            var response = new ServiceResponse<IEnumerable<IngredientDTO>>();
            try
            {
                var Ingredients = await _unitOfWork.IngredientRepository.GetAllAsync();
                var IngredientDTOs = new List<IngredientDTO>();
                foreach (var pro in Ingredients)
                {
                    if ((bool)!pro.IsDeleted)
                    {
                        IngredientDTOs.Add(_mapper.Map<IngredientDTO>(pro));
                    }
                }
                if (IngredientDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Ingredient retrieved successfully";
                    response.Data = IngredientDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Ingredient not found";
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

        public async Task<ServiceResponse<IngredientDTO>> UpdateIngredientAsync(int id, IngredientUpdateDTO IngredientDTO, string url)
        {
            var response = new ServiceResponse<IngredientDTO>();
            var exist = await _unitOfWork.IngredientRepository.GetByIdAsync(id);
            if (exist == null)
            {
                response.Success = false;
                response.Message = "Ingredient not found";
                return response;
            }
            try
            {
                var Ingredient = _mapper.Map(IngredientDTO, exist);
                if (string.IsNullOrEmpty(url))
                {
                    Ingredient.ImageUrl = exist.ImageUrl;
                }
                else
                {
                    Ingredient.ImageUrl = url;
                }
                _unitOfWork.IngredientRepository.Update(Ingredient);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "Ingredient updated successfully";
                    response.Data = _mapper.Map<IngredientDTO>(Ingredient);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Update Ingredient failed";
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
    }
}

