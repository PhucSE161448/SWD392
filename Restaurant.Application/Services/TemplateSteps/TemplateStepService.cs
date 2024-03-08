using AutoMapper;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Interfaces.TemplateSteps;
using Restaurant.Application.ViewModels.ProductTemplateDTO;
using Restaurant.Application.ViewModels.TemplateStepsDTO;
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
                var isSuccess = await _unitOfWork.TemplateStepRepository.CreateTemplateAsync(CreatedTemplateStepDTO) == true;
                if (isSuccess)
                {
                    var TemplateStepDTO = _mapper.Map<TemplateStepDTO>(CreatedTemplateStepDTO);
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

        public Task<ServiceResponse<bool>> DeleteTemplateStepAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<IEnumerable<TemplateStepDTO>>> GetAllTemplateStepAsync(string? size = null)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<TemplateStepDTO>> GetTemplateStepAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<TemplateStepDTO>> UpdateTemplateStepAsync(int id, TemplateStepUpdateDTO TemplateStepDTO)
        {
            throw new NotImplementedException();
        }
    }
}
