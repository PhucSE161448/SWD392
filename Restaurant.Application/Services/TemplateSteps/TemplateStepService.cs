using AutoMapper;
using PayPal.Api;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Interfaces.TemplateSteps;
using Restaurant.Application.ViewModels.ProductTemplateDTO;
using Restaurant.Application.ViewModels.TemplateStepsDTO;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Services.TemplateSteps
{
    public class TemplateStepService : ITemplateStepService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TemplateStepService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<TemplateStepDTO>> CreateTemplateStepAsync(TemplateStepCreateDTO CreatedTemplateStepDTO)
        {
            var response = new ServiceResponse<TemplateStepDTO>();
            try
            {
                var (isSuccess, templateStep) = await _unitOfWork.TemplateStepRepository.CreateTemplateAsync(CreatedTemplateStepDTO);
                if (isSuccess)
                {
                    var TemplateStepDTO = _mapper.Map<TemplateStepDTO>(templateStep);
                    response.Data = TemplateStepDTO;
                    response.Success = true;
                    response.Message = "TemplateStep created successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Create TemplateStep failed";
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

        public async Task<ServiceResponse<bool>> DeleteTemplateStepAsync(int id)
        {
            var response = new ServiceResponse<bool>();
            var exist = await _unitOfWork.TemplateStepRepository.GetByIdAsync(id);
            if (exist == null)
            {
                response.Success = false;
                response.Message = "TemplateStep not found";
                return response;
            }
            try
            {
                _unitOfWork.TemplateStepRepository.SoftRemove(exist);
                await _unitOfWork.TemplateStepRepository.DeleteTemplateAsync(exist.Id);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "TemplateStep deleted successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Delete TemplateStep failed";
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

        public async Task<ServiceResponse<IEnumerable<TemplateStepIngredientDTO>>> GetAllTemplateStepAsync(int? id)
        {
            var _response = new ServiceResponse<IEnumerable<TemplateStepIngredientDTO>>();
            try
            {
                var list = await _unitOfWork.TemplateStepRepository.GetTemplateStepsByProductId(id);

                if (list.Count() != 0)
                {
                    _response.Success = true;
                    _response.Message = "TemplateStep retrieved successfully";
                    _response.Data = list;
                }
                else
                {
                    _response.Success = true;
                    _response.Message = "TemplateStep not found";
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

        public async Task<ServiceResponse<TemplateStepIngredientDTO>> GetTemplateStepAsync(int? id)
        {
            var _response = new ServiceResponse<TemplateStepIngredientDTO>();
            try
            {
                var Templates = await _unitOfWork.TemplateStepRepository.GetTemplateStepByTemplateStepId(id);
                if (Templates != null)
                {
                    _response.Success = true;
                    _response.Message = "TemplateStep retrieved successfully";
                    _response.Data = Templates;
                }
                else
                {
                    _response.Success = true;
                    _response.Message = "TemplateStep not found";
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

        public async Task<ServiceResponse<TemplateStepDTO>> UpdateTemplateAsync(int id, TemplateStepUpdateDTO TemplateStepDTO)
        {
            var response = new ServiceResponse<TemplateStepDTO>();
            var exist = await _unitOfWork.TemplateStepRepository.GetByIdAsync(id);
            if (exist == null)
            {
                response.Success = false;
                response.Message = "TemplateStep not found";
                return response;
            }
            try
            {
                var (isSuccess, template) = await _unitOfWork.TemplateStepRepository.UpdateTemplateAsync(id , TemplateStepDTO);
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "TemplateStep updated successfully";
                    response.Data = _mapper.Map<TemplateStepDTO>(template); 
                }
                else
                {
                    response.Success = false;
                    response.Message = "Update TemplateStep failed";
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

