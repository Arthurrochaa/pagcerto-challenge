using api.Models.EntityModel.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace api.Models.ResultModel
{
    public class TransactionSuccessResult : IActionResult
    {
        public TransactionSuccessResult(Transaction transaction)
        {
            NSU = transaction.NSU;
            CreatedAt = transaction.CreatedAt;
            GrossAmount = transaction.GrossAmount;
            NetAmount = transaction.NetAmount;
            FixedRateCharged = transaction.FixedRateCharged;
            InstallmentsNumber = transaction.InstallmentsNumber;
        }

        public long NSU { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal FixedRateCharged { get; set; }
        public int InstallmentsNumber { get; set; }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return new OkObjectResult(this).ExecuteResultAsync(context);
        }
    }
}
