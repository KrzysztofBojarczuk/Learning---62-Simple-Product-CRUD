using AutoMapper;
using Products.Dtos;
using Products.Models;

namespace Products.Automapper
{
    public class ToDoMappingProfiles : Profile
    {
        public ToDoMappingProfiles()
        {
            CreateMap<ProductCreateDto, Product>();
            CreateMap<Product, ProductGetDto>();
        }
    }
}
