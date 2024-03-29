﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Interfaces.Categories;
using Restaurant.Application.ViewModels.CategoryDTO;
using Restaurant.Domain.Entities;

namespace Restaurant.WebAPI.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetCategoryList()
        {
            var result = await _categoryService.GetAllCategoryAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var result = await _categoryService.GetCategoryAsync(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] AddUpdateCategoryDTO createdCategoryDTO)
        {
            var result = await _categoryService.CreateCategoryAsync(createdCategoryDTO);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }

        /*[Authorize(Roles = "admin")]*/
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] AddUpdateCategoryDTO CategoryDTO)
        {
            var result = await _categoryService.UpdateCategoryAsync(id, CategoryDTO);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        //[Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpPut("Status/{id}")]
        public async Task<IActionResult> UpdateIsDelete(int id, [FromQuery] bool? isDeleted)
        {
            var updatedUser = await _categoryService.UpdateIsDelete(id, isDeleted);
            if (!updatedUser.Success)
            {
                return NotFound(updatedUser);
            }
            return Ok(updatedUser);
        }
    }
}
