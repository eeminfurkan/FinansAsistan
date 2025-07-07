using FinansAsistan.Shared.Dtos;
using MediatR;

namespace FinansAsistan.Api.Features.Categories.Commands
{
    // Bu komut, bir Kategori oluşturma isteğini temsil eder
    // ve geriye yeni oluşturulan CategoryDto'yu döndürecek.
    public class CreateCategoryCommand : IRequest<CategoryDto>
    {
        public string Name { get; set; }
    }
}