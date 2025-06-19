using App.Application.DTO;
using App.Core.Entities;
using AutoMapper;

namespace App.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<CreateProductDTO, Product>();
            CreateMap<UpdateProductDTO, Product>();
        }
    }
}
