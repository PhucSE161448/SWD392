﻿using AutoMapper;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Interfaces.Categories;
using Restaurant.Application.ViewModels.CategoryDTO;
using Restaurant.Application.ViewModels.CategoryDTO;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<CategoryDto>> CreateCategoryAsync(AddUpdateCategoryDTO CategoryDto)
        {
            var response = new ServiceResponse<CategoryDto>();
            var exist = await _unitOfWork.CategoryRepository.CheckCategoryExited(CategoryDto.Name);
            if (exist)
            {
                response.Success = false;
                response.Message = "Category already exist";
                return response;
            }
            try
            {
                var category = _mapper.Map<Category>(CategoryDto);
                await _unitOfWork.CategoryRepository.AddAsync(category);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    var categoryDTO = _mapper.Map<CategoryDto>(category);
                    response.Data = categoryDTO;
                    response.Success = true;
                    response.Message = "Category created successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Create Category failed";
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

        public async Task<ServiceResponse<bool>> DeleteCategoryAsync(int id)
        {
            var response = new ServiceResponse<bool>();
            var exist = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (exist == null)
            {
                response.Success = false;
                response.Message = "Category not found";
                return response;
            }
            try
            {
                _unitOfWork.CategoryRepository.SoftRemove(exist);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "Category deleted successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Category product failed";
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

        public async Task<ServiceResponse<IEnumerable<CategoryDto>>> GetAllCategoryAsync()
        {
            var _response = new ServiceResponse<IEnumerable<CategoryDto>>();
            try
            {
                var categorys = await _unitOfWork.CategoryRepository.GetAllAsync();
                var CategoryDTOs = new List<CategoryDto>();
                foreach (var pro in categorys)
                {
                    CategoryDTOs.Add(_mapper.Map<CategoryDto>(pro));
                }
                if (CategoryDTOs.Count != 0)
                {
                    _response.Success = true;
                    _response.Message = "Category retrieved successfully";
                    _response.Data = CategoryDTOs;
                }
                else
                {
                    _response.Success = true;
                    _response.Message = "Category not found";
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
        public async Task<ServiceResponse<CategoryDto>> GetCategoryAsync(int id)
        {
            var _response = new ServiceResponse<CategoryDto>();
            try
            {
                var categorys = await _unitOfWork.CategoryRepository.GetAsync(x => x.Id == id);
                if (categorys != null)
                {
                    _response.Success = true;
                    _response.Message = "Category retrieved successfully";
                    _response.Data = _mapper.Map<CategoryDto>(categorys);
                }
                else
                {
                    _response.Success = true;
                    _response.Message = "Category not found";
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
        public async Task<ServiceResponse<CategoryDto>> UpdateCategoryAsync(int id, AddUpdateCategoryDTO CategoryDTO)
        {
            var response = new ServiceResponse<CategoryDto>();
            var exist = await _unitOfWork.CategoryRepository.GetAsync(x => x.Id == id);

            if (exist == null)
            {
                response.Success = false;
                response.Message = "Category not found";
                return response;
            }
            try
            {
                var category = _mapper.Map(CategoryDTO, exist);
                _unitOfWork.CategoryRepository.Update(category);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "Category updated successfully";
                    response.Data = _mapper.Map<CategoryDto>(category);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Update Category failed";
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

            var exist = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (exist == null)
            {
                response.Success = false;
                response.Message = "Category is not existed";
                return response;
            }
            try
            {
                if (isDeleted.HasValue)
                {
                    exist.IsDeleted = isDeleted;
                }
                _unitOfWork.CategoryRepository.Update(exist);

                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    response.Success = true;
                    response.Message = "Category update successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Error update the Category.";
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
