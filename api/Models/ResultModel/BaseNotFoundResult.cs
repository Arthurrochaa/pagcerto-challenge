using Microsoft.AspNetCore.Mvc;

namespace api.Models.ResultModel
{
    public class BaseNotFoundResult : IActionResult
    {
        public BaseNotFoundResult(string message)
        {
            Message = message;
        }

        public string Message { get; set; }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return new NotFoundObjectResult(new { Message }).ExecuteResultAsync(context);
        }
    }
}
