using AutoMapper;
using FinansAsistan.Api.Dtos;
using FinansAsistan.Core.Entities;

namespace FinansAsistan.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Kaynaktan -> Hedefe
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
        }
    }
}