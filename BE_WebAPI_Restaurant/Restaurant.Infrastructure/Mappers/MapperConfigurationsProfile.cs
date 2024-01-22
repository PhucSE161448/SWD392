using AutoMapper;
using Restaurant.Application.Commons;
using Restaurant.Domain.Entities;
using Application.ViewModels.AccountDTO;
using Application.ViewModels.RegisterAccountDTO;

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


        }
    }
}
