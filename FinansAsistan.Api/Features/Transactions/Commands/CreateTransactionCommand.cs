using FinansAsistan.Api.Dtos;
using MediatR;
using System;

namespace FinansAsistan.Api.Features.Transactions.Commands
{
    // Bu komut, geriye yeni oluşturulan TransactionDto'yu döndürecek.
    public class CreateTransactionCommand : IRequest<TransactionDto>
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public int CategoryId { get; set; }
    }
}