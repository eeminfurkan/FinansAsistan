using MediatR;
using System;

namespace FinansAsistan.Api.Features.Transactions.Commands
{
    public class UpdateTransactionCommand : IRequest
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public int CategoryId { get; set; }
    }
}