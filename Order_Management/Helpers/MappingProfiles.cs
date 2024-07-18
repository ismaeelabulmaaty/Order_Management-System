using AutoMapper;
using Order_Manag.Core.Entites;
using Order_Management.DTOS;

namespace Order_Management.Helpers
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<Order, OrderItemRequestDto>().ReverseMap();
        }

    }
}
