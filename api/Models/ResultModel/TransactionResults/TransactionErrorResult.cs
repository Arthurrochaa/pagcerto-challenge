using Microsoft.AspNetCore.Mvc;

namespace api.Models.ResultModel
{
    public class TransactionErrorResult : IActionResult
    {
        public TransactionErrorResult(string error)
        {
            Error = error;
        }

        public string Error { get; set; }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return new UnprocessableEntityObjectResult(this).ExecuteResultAsync(context);
        }
    }
}
