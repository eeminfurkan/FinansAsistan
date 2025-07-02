using FinansAsistan.Api.Dtos;
using MediatR;
using System.Collections.Generic;

namespace FinansAsistan.Api.Features.Categories.Queries
{
    // Bu istek, geriye bir Kategori DTO listesi döndürecek.
    public class GetAllCategoriesQuery : IRequest<IReadOnlyList<CategoryDto>>
    {
        // Tümünü getireceği için parametreye ihtiyacı yok, içi boş.
    }
}