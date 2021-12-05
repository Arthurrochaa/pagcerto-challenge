using api.Models.EntityModel.AdvanceTransactionEntities;
using Microsoft.AspNetCore.Mvc;

namespace api.Models.ResultModel.AdvanceRequestResults
{
    public class ListAdvanceRequestResult : IActionResult
    {
        public ListAdvanceRequestResult(ICollection<AdvanceTransactionRequest> advanceRequests)
        {
            AdvanceRequests = advanceRequests;
            Count = advanceRequests.Count;
        }

        public ICollection<AdvanceTransactionRequest> AdvanceRequests { get; private set; } 
        public int Count { get; private set; }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return new OkObjectResult(this).ExecuteResultAsync(context);
        }
    }
}
