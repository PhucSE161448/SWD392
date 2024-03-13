﻿using AutoMapper;
using Restaurant.Application.Commons;
using Restaurant.Domain.Entities;
using Application.ViewModels.AccountDTO;
using Application.ViewModels.RegisterAccountDTO;
using Restaurant.Application.ViewModels.ProductDTO;
using Restaurant.Application.ViewModels.CategoryDTO;
using Restaurant.Application.ViewModels.ProductTemplateDTO;
using Restaurant.Application.ViewModels.Ingredient_TypeDTO;
using Restaurant.Application.ViewModels.NewsDTO;
using Restaurant.Application.ViewModels.IngredientsDTO;
using Restaurant.Application.ViewModels.NutritionsDTO;
using Restaurant.Application.ViewModels.StoreDTO;
using Restaurant.Application.ViewModels.TemplateStepsDTO;
using Restaurant.Application.ViewModels.AccountDTO;

namespace Restaurant.Infrastructures.Mappers
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {

            CreateMap<Account, AccountDTO>().ReverseMap();
            CreateMap<CreatedAccountDTO, Account>();
            CreateMap<CreatedAccountDTO, AccountDTO>();
            CreateMap<UpdateProfileAccountDTO, Account>().ReverseMap();

            CreateMap<RegisterAccountDTO, Account>();
            CreateMap<RegisterAccountDTO, AccountDTO>();

            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<CreatedProductDTO, Product>();
            CreateMap<CreatedProductDTO, ProductDTO>();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<AddUpdateCategoryDTO, CategoryDto>().ReverseMap();
            CreateMap<AddUpdateCategoryDTO, Category>().ReverseMap();

            CreateMap<ProductTemplate, ProductTemplateDTO>().ReverseMap();
            CreateMap<ProductTemplateCreateDTO, ProductTemplate>();
            CreateMap<ProductTemplateCreateDTO, ProductTemplateDTO>();
            CreateMap<ProductTemplateUpdateDTO, ProductTemplate>().ReverseMap();

            CreateMap<NewsDTO, News>().ReverseMap();
            CreateMap<AddNewsDTO, News>().ReverseMap();

            CreateMap<IngredientDTO, Ingredient>().ReverseMap();
            CreateMap<IngredientAddDTO, Ingredient>();
            CreateMap<IngredientAddDTO, IngredientDTO>().ReverseMap();
            CreateMap<IngredientUpdateDTO, Ingredient>().ReverseMap();

            CreateMap<NutritionDTO, Nutrition>().ReverseMap();
            CreateMap<NutritionAddDTO, Nutrition>();
            CreateMap<NutritionAddDTO, NutritionDTO>().ReverseMap();
            CreateMap<NutritionUpdateDTO, Nutrition>().ReverseMap();

            CreateMap<IngredientType, IngredientTypeDTO>().ReverseMap();

            CreateMap<StoreDTO, Store>().ReverseMap();
            CreateMap<AddStoreDTO, Store>().ReverseMap();

            CreateMap<TemplateStepCreateDTO, TemplateStep>().ReverseMap();
            CreateMap<TemplateStepCreateDTO, TemplateStepDTO>().ReverseMap();
            CreateMap<TemplateStep, TemplateStepDTO>().ReverseMap();    
            CreateMap<TemplateStepDTO, TemplateStep>().ReverseMap();
            CreateMap<TemplateStepUpdateDTO, TemplateStep>().ReverseMap();
        }
    }
}
