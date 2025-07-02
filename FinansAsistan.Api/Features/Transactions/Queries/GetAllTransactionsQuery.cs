using FinansAsistan.Api.Dtos; // TransactionDto'yu kullanmak için
using MediatR;
using System.Collections.Generic;

namespace FinansAsistan.Api.Features.Transactions.Queries
{
    // Bu bir "istektir" (Request) ve geriye IReadOnlyList<TransactionDto> tipinde bir "cevap" döndürecektir.
    public class GetAllTransactionsQuery : IRequest<IReadOnlyList<TransactionDto>>
    {
        // Bu sorgunun parametresi olmadığı için içi boş.
    }
}    