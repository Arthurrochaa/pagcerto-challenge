using api.Models.EntityModel.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace api.Models.ResultModel.TransactionResults
{
    public class ListTransactionResult : IActionResult 
    {
        public ListTransactionResult(ICollection<Transaction> transactions)
        {
            Transactions = transactions.Select(t => new TransactionSuccessResult(t)).ToList();
            Count = transactions.Count; 
        }

        public ICollection<TransactionSuccessResult> Transactions { get; set; }
        public int Count { get; set; }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return new OkObjectResult(this).ExecuteResultAsync(context);
        }
    }
}
