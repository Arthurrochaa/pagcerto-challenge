using api.Models.EntityModel.AdvanceTransactionEntities;
using api.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace api.Models.ResultModel.AdvanceRequestResults
{
    public class AdvanceRequestSuccessResult : IActionResult
    {
        public AdvanceRequestSuccessResult(AdvanceTransactionRequest advanceRequest)
        {
            ID = advanceRequest.ID;
            RequestedAt = advanceRequest.RequestedAt;
            AnalysisStatus = advanceRequest.AnalysisStatus;
            RequestedAmount = advanceRequest.RequestedAmount;
        }

        public long ID { get; set; }
        public DateTime RequestedAt { get; set; }
        public AnalysisStatus AnalysisStatus { get; set; }
        public decimal RequestedAmount { get; set; }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return new OkObjectResult(this).ExecuteResultAsync(context);
        }
    }
}
