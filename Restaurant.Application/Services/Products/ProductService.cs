using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Interfaces.Products;
using Restaurant.Application.ViewModels.ProductDTO;
using Restaurant.Application.ViewModels.ProductTemplateDTO;
using Restaurant.Application.ViewModels.TemplateStepsDTO;
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
      
        public async Task<ServiceResponse<ProductsDTO>> CreateProductAsync(CreatedProductDTO CreatedProductDTO)
        {
            var response = new ServiceResponse<ProductsDTO>();
            try
            {
                var productTemplate = await _unitOfWork.ProductTemplateRepository.GetAsync(x => x.Id == CreatedProductDTO.ProductTemplateId);
                var (isSuccess, Product) = await _unitOfWork.ProductRepository.CreateProductAsync(CreatedProductDTO, _mapper.Map<ProductTemplateDTO>(productTemplate));
               
                if (isSuccess)
                {
                    var p = await _unitOfWork.ProductRepository.GetAsync(x => x.Id == Product.Id, includeProperties: "IngredientProducts");
                    var pro = _mapper.Map<ProductsDTO>(p);
                    response.Data = pro;
                    response.Success = true;
                    response.Message = "Product created successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Create Product failed";
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

        public async Task<ServiceResponse<IEnumerable<ProductsDTO>>> GetAllProductAsync(string? name = null)
        {
            var _response = new ServiceResponse<IEnumerable<ProductsDTO>>();
            try
            {
                var products = await _unitOfWork.ProductRepository.GetProductsByUserId(name);
                if (products.Count != 0)
                {
                    _response.Success = true;
                    _response.Message = "Product retrieved successfully";
                    _response.Data = products;
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
        public async Task<ServiceResponse<ProductsDTO>> GetProductAsync(int id)
        {
            var _response = new ServiceResponse<ProductsDTO>();
            try
            {
                var products = await _unitOfWork.ProductRepository.GetProduct(id);

                if (products != null)
                {
                    _response.Success = true;
                    _response.Message = "Product retrieved successfully";
                    _response.Data = products;
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

        public async Task<ServiceResponse<ProductsDTO>> UpdateProductAsync(int id, ProductsDTO ProductDTO)
        {
            var response = new ServiceResponse<ProductsDTO>();
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
                    response.Data = _mapper.Map<ProductsDTO>(product);
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
