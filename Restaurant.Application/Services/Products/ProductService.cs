﻿using AutoMapper;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Interfaces.Products;
using Restaurant.Application.ViewModels.ProductDTO;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<ProductDTO>> CreateProductAsync(CreatedProductDTO CreatedProductDTO)
        {
            var response = new ServiceResponse<ProductDTO>();
            var exist = await _unitOfWork.ProductRepository.CheckProductExited(CreatedProductDTO.Id);
            if (exist)
            {
                response.Success = false;
                response.Message = "Product already exist";
                return response;
            }
            try
            {
                var product = _mapper.Map<Product>(CreatedProductDTO);
                await _unitOfWork.ProductRepository.AddAsync(product);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var productDTO = _mapper.Map<ProductDTO>(product);
                    response.Data = productDTO;
                    response.Success = true;
                    response.Message = "Product created successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Create product failed";
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

        public async Task<ServiceResponse<bool>> DeleteProductAsync(int id)
        {
            var response = new ServiceResponse<bool>();
            var exist = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (exist == null)
            {
                response.Success = false;
                response.Message = "Product not found";
                return response;
            }
            try
            {
                _unitOfWork.ProductRepository.SoftRemove(exist);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "Product deleted successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Delete product failed";
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

        public async Task<ServiceResponse<IEnumerable<ProductDTO>>> GetAllProductAsync()
        {
            var _response = new ServiceResponse<IEnumerable<ProductDTO>>();
            try
            {
                var products = await _unitOfWork.ProductRepository.GetAllAsync();
                var productDTOs = new List<ProductDTO>();
                foreach (var pro in products)
                {
                    if ((bool)!pro.IsDeleted)
                    {
                        productDTOs.Add(_mapper.Map<ProductDTO>(pro));
                    }
                }
                if (productDTOs.Count != 0)
                {
                    _response.Success = true;
                    _response.Message = "Product retrieved successfully";
                    _response.Data = productDTOs;
                }
                else
                {
                    _response.Success = true;
                    _response.Message = "Product not found";
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

        public async Task<ServiceResponse<IEnumerable<ProductDTO>>> GetSortedProductAsync()
        {
            var response = new ServiceResponse<IEnumerable<ProductDTO>>();
            try
            {
                var products = await _unitOfWork.ProductRepository.GetSortedProductAsync();
                var productDTOs = new List<ProductDTO>();
                foreach (var pro in products)
                {
                    if ((bool)!pro.IsDeleted)
                    {
                        productDTOs.Add(_mapper.Map<ProductDTO>(pro));
                    }
                }
                if (productDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Product retrieved successfully";
                    response.Data = productDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Product not found";
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

        public async Task<ServiceResponse<IEnumerable<ProductDTO>>> SearchProductByNameAsync(string name)
        {
            var response = new ServiceResponse<IEnumerable<ProductDTO>>();
            try
            {
                var products = await _unitOfWork.ProductRepository.SearchProductByNameAsync(name);
                var productDTOs = new List<ProductDTO>();
                foreach (var pro in products)
                {
                    if ((bool)!pro.IsDeleted)
                    {
                        productDTOs.Add(_mapper.Map<ProductDTO>(pro));
                    }
                }
                if (productDTOs.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Product retrieved successfully";
                    response.Data = productDTOs;
                }
                else
                {
                    response.Success = true;
                    response.Message = "Product not found";
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

        public async Task<ServiceResponse<ProductDTO>> UpdateProductAsync(int id, ProductDTO ProductDTO)
        {
            var response = new ServiceResponse<ProductDTO>();
            var exist = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (exist == null)
            {
                response.Success = false;
                response.Message = "Product not found";
                return response;
            }
            try
            {
                var product = _mapper.Map<Product>(ProductDTO);
                _unitOfWork.ProductRepository.Update(product);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "Product updated successfully";
                    response.Data = _mapper.Map<ProductDTO>(product);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Update product failed";
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