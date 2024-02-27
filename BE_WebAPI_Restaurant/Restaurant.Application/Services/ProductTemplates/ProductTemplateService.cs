﻿using AutoMapper;
using Restaurant.Application.Interfaces.ProductTemplates;
using Restaurant.Application.Interfaces;
using Restaurant.Application.ViewModels.ProductTemplateDTO;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Services.ProductTemplates
{
    public class ProductTemplateService : IProductTemplateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductTemplateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<ProductTemplateDTO>> CreateProductTemplateAsync(ProductTemplateCreateDTO CreatedProductTemplateDTO)
        {
            var response = new ServiceResponse<ProductTemplateDTO>();
            try
            {

                var ProductTemplate = _mapper.Map<ProductTemplate>(CreatedProductTemplateDTO);
                await _unitOfWork.ProductTemplateRepository.AddAsync(ProductTemplate);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var ProductTemplateDTO = _mapper.Map<ProductTemplateDTO>(ProductTemplate);
                    response.Data = ProductTemplateDTO;
                    response.Success = true;
                    response.Message = "ProductTemplate created successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Create ProductTemplate failed";
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

        public async Task<ServiceResponse<bool>> DeleteProductTemplateAsync(int id)
        {
            var response = new ServiceResponse<bool>();
            var exist = await _unitOfWork.ProductTemplateRepository.GetByIdAsync(id);
            if (exist == null)
            {
                response.Success = false;
                response.Message = "ProductTemplate not found";
                return response;
            }
            try
            {
                _unitOfWork.ProductTemplateRepository.SoftRemove(exist);
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
                    response.Message = "ProductTemplate deleted successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Delete ProductTemplate failed";
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

        public async Task<ServiceResponse<IEnumerable<ProductTemplateDTO>>> GetAllProductTemplateAsync()
        {
            var _response = new ServiceResponse<IEnumerable<ProductTemplateDTO>>();
            try
            {
                var ProductTemplates = await _unitOfWork.ProductTemplateRepository.GetAllAsync();
                var ProductTemplateDTOs = new List<ProductTemplateDTO>();
                foreach (var pro in ProductTemplates)
                {
                    if ((bool)!pro.IsDeleted)
                    {
                        ProductTemplateDTOs.Add(_mapper.Map<ProductTemplateDTO>(pro));
                    }
                }
                if (ProductTemplateDTOs.Count != 0)
                {
                    _response.Success = true;
                    _response.Message = "ProductTemplate retrieved successfully";
                    _response.Data = ProductTemplateDTOs;
                }
                else
                {
                    _response.Success = true;
                    _response.Message = "ProductTemplate not found";
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

        public async Task<ServiceResponse<IEnumerable<ProductTemplateDTO>>> GetSortedProductTemplateAsync()
        {
            var response = new ServiceResponse<IEnumerable<ProductTemplateDTO>>();
            try
            {
                var ProductTemplates = await _unitOfWork.ProductTemplateRepository.GetAllAsync();
                var ProductTemplateDTOs = new List<ProductTemplateDTO>();
                foreach (var pro in ProductTemplates)
                {
                    if ((bool)!pro.IsDeleted)
                    {
                        ProductTemplateDTOs.Add(_mapper.Map<ProductTemplateDTO>(pro));
                    }
                }
                if (ProductTemplateDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "ProductTemplate retrieved successfully";
                    response.Data = ProductTemplateDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "ProductTemplate not found";
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

        public async Task<ServiceResponse<IEnumerable<ProductTemplateDTO>>> SearchProductTemplateByNameAsync(string name)
        {
            var response = new ServiceResponse<IEnumerable<ProductTemplateDTO>>();
            try
            {
                var ProductTemplates = await _unitOfWork.ProductTemplateRepository.GetAllAsync();
                var ProductTemplateDTOs = new List<ProductTemplateDTO>();
                foreach (var pro in ProductTemplates)
                {
                    if ((bool)!pro.IsDeleted)
                    {
                        ProductTemplateDTOs.Add(_mapper.Map<ProductTemplateDTO>(pro));
                    }
                }
                if (ProductTemplateDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "ProductTemplate retrieved successfully";
                    response.Data = ProductTemplateDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "ProductTemplate not found";
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

        public async Task<ServiceResponse<ProductTemplateDTO>> UpdateProductTemplateAsync(int id, ProductTemplateUpdateDTO ProductTemplateDTO)
        {
            var response = new ServiceResponse<ProductTemplateDTO>();
            var exist = await _unitOfWork.ProductTemplateRepository.GetByIdAsync(id);
            if (exist == null)
            {
                response.Success = false;
                response.Message = "ProductTemplate not found";
                return response;
            }
            try
            {
                if (ProductTemplateDTO.ImageUrl == null)
                {
                    ProductTemplateDTO.ImageUrl = exist.ImageUrl;
                }
                var ProductTemplate = _mapper.Map(ProductTemplateDTO, exist);
                _unitOfWork.ProductTemplateRepository.Update(ProductTemplate);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "ProductTemplate updated successfully";
                    response.Data = _mapper.Map<ProductTemplateDTO>(ProductTemplate);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Update ProductTemplate failed";
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