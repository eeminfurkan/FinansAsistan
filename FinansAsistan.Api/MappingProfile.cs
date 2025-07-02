using AutoMapper;
using FinansAsistan.Api.Dtos;
using FinansAsistan.Api.Features.Categories.Commands;
using FinansAsistan.Api.Features.Transactions.Commands;
using FinansAsistan.Core.Entities;

namespace FinansAsistan.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ===== Category Mappings =====
            // Entity to DTO
            CreateMap<Category, CategoryDto>();

            // DTO to Command
            CreateMap<CreateCategoryDto, CreateCategoryCommand>();
            CreateMap<UpdateCategoryDto, UpdateCategoryCommand>();

            // Command to Entity
            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<UpdateCategoryCommand, Category>();


            // ===== Transaction Mappings =====
            // Entity to DTO
            CreateMap<Transaction, TransactionDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            // DTO to Command
            CreateMap<CreateTransactionDto, CreateTransactionCommand>();
            CreateMap<UpdateTransactionDto, UpdateTransactionCommand>();

            // Command to Entity
            CreateMap<CreateTransactionCommand, Transaction>();
            CreateMap<UpdateTransactionCommand, Transaction>();
        }
    }
}