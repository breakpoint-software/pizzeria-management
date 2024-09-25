using AutoMapper;
using Models.Domain;

namespace ChicksGold.Server.Models.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderRequest, Order>().ReverseMap();
            CreateMap<OrderResponse, Order>().ReverseMap();
            CreateMap<OrderDetailResponse, OrderDetail>().ReverseMap().ForMember(src => src.Pizza, opt => opt.MapFrom(e => e.Pizza.Name));
            CreateMap<OrderDetailRequest, OrderDetail>().ReverseMap();
        }
    }
}
