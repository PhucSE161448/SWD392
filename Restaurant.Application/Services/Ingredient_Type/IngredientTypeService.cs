using AutoMapper;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Interfaces.Ingredient_Type;
using Restaurant.Application.ViewModels.Ingredient_TypeDTO;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Services.Ingredient_Type
{
    public class IngredientTypeService : IIngredientTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public IngredientTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<IngredientTypeDTO>> CreateIngredientTypeAsync(IngredientTypeDTO IngredientTypeDto)
        {
            var response = new ServiceResponse<IngredientTypeDTO>();
            try
            {
                var IngredientType = _mapper.Map<IngredientType>(IngredientTypeDto);
                await _unitOfWork.IngredientTypeRepository.AddAsync(IngredientType);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var IngredientTypeDTO = _mapper.Map<IngredientTypeDTO>(IngredientType);
                    response.Data = IngredientTypeDTO;
                    response.Success = true;
                    response.Message = "IngredientType created successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Create IngredientType failed";
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

        public async Task<ServiceResponse<bool>> DeleteIngredientTypeAsync(int id)
        {
            var response = new ServiceResponse<bool>();
            var exist = await _unitOfWork.IngredientTypeRepository.GetByIdAsync(id);
            if (exist == null)
            {
                response.Success = false;
                response.Message = "IngredientType not found";
                return response;
            }
            try
            {
                _unitOfWork.IngredientTypeRepository.SoftRemove(exist);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "IngredientType deleted successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "IngredientType product failed";
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

        public async Task<ServiceResponse<IEnumerable<IngredientTypeDTO>>> GetAllIngredientTypeAsync()
        {
            var _response = new ServiceResponse<IEnumerable<IngredientTypeDTO>>();
            try
            {
                var IngredientTypes = await _unitOfWork.IngredientTypeRepository.GetAllAsync();
                var IngredientTypeDTOs = new List<IngredientTypeDTO>();
                foreach (var pro in IngredientTypes)
                {
                    if ((bool)!pro.IsDeleted)
                    {
                        IngredientTypeDTOs.Add(_mapper.Map<IngredientTypeDTO>(pro));
                    }
                }
                if (IngredientTypeDTOs.Count != 0)
                {
                    _response.Success = true;
                    _response.Message = "IngredientType retrieved successfully";
                    _response.Data = IngredientTypeDTOs;
                }
                else
                {
                    _response.Success = true;
                    _response.Message = "IngredientType not found";
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

        public async Task<ServiceResponse<IngredientTypeDTO>> UpdateIngredientTypeAsync(int id, IngredientTypeDTO IngredientTypeDTO)
        {
            var response = new ServiceResponse<IngredientTypeDTO>();
            var exist = await _unitOfWork.IngredientTypeRepository.GetAsync(x => x.Id == id);

            if (exist == null)
            {
                response.Success = false;
                response.Message = "IngredientType not found";
                return response;
            }
            try
            {
                var IngredientType = _mapper.Map(IngredientTypeDTO, exist);
                _unitOfWork.IngredientTypeRepository.Update(IngredientType);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "IngredientType updated successfully";
                    response.Data = _mapper.Map<IngredientTypeDTO>(IngredientType);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Update IngredientType failed";
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
