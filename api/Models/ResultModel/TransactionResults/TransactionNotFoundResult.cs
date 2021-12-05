using api.Models.EntityModel.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace api.Models.ResultModel
{
    public class TransactionNotFoundResult : IActionResult
    {
        const string Message = "TRANSACTION_NOT_FOUND";

        public Task ExecuteResultAsync(ActionContext context)
        {
            return new NotFoundObjectResult(new { Message }).ExecuteResultAsync(context);
        }
    }
}
