using AutoMapper;
using ApiVM = Shop.Api.Contract;
using DbVM = Shop.DataLayer.Models;

namespace Shop.WebApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApiVM.OrderCreate, DbVM.Order>();
            CreateMap<ApiVM.Order, DbVM.Order>().ReverseMap();
        }
    }
}