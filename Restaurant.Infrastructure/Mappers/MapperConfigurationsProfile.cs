using AutoMapper;
using Restaurant.Application.Commons;
using Restaurant.Domain.Entities;
using Application.ViewModels.AccountDTO;
using Application.ViewModels.RegisterAccountDTO;
using Restaurant.Application.ViewModels.ProductDTO;
using Restaurant.Application.ViewModels.CategoryDTO;
using Restaurant.Application.ViewModels.ProductTemplateDTO;
using Restaurant.Application.ViewModels.Ingredient_TypeDTO;

namespace Restaurant.Infrastructures.Mappers
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {

            CreateMap<Account, AccountDTO>().ReverseMap();
            CreateMap<CreatedAccountDTO, Account>();
            CreateMap<CreatedAccountDTO, AccountDTO>();

            CreateMap<RegisterAccountDTO, Account>();
            CreateMap<RegisterAccountDTO, AccountDTO>();
            
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<CreatedProductDTO, Product>();
            CreateMap<CreatedProductDTO, ProductDTO>();

            CreateMap<Category,CategoryDTO>().ReverseMap();

            CreateMap<ProductTemplate, ProductTemplateDTO>().ReverseMap();
            CreateMap<ProductTemplateCreateDTO, ProductTemplate>();
            CreateMap<ProductTemplateCreateDTO, ProductTemplateDTO>();  
            CreateMap<ProductTemplateUpdateDTO, ProductTemplate>().ReverseMap();

            CreateMap<IngredientType,IngredientTypeDTO>().ReverseMap();
        }
    }
}
