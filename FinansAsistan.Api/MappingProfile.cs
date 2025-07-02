using AutoMapper;
using FinansAsistan.Api.Dtos;
using FinansAsistan.Api.Features.Categories.Commands;
using FinansAsistan.Api.Features.Transactions.Commands; // Command'leri tanımak için eklendi
using FinansAsistan.Core.Entities;

namespace FinansAsistan.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Category için mevcut map'ler
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryDto, Category>();

            // Transaction için olan map'ler
            CreateMap<Transaction, TransactionDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<CreateTransactionDto, Transaction>();
            CreateMap<UpdateTransactionDto, Transaction>();
            CreateMap<UpdateTransactionDto, UpdateTransactionCommand>();
            CreateMap<UpdateTransactionCommand, Transaction>();
            CreateMap<CreateCategoryCommand, Category>();


            // ---- EKSİK OLAN VE ŞİMDİ EKLENEN MAP ----
            // CreateTransactionCommand'den Transaction entity'sine dönüşüm kuralı
            CreateMap<CreateTransactionCommand, Transaction>();
        }
    }
}