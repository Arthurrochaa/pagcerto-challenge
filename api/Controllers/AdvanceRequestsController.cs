using api.Models.Enums;
using api.Models.ResultModel;
using api.Models.ResultModel.AdvanceRequestResults;
using api.Models.ServiceModel.AdvanceRequestServices;
using api.Models.ViewModel.AdvanceRequestViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/v1/advance-requests")]
    [ApiController]
    public class AdvanceRequestsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> ListRequests([FromQuery] AnalysisStatus? analysisStatus, [FromServices] IAdvanceRequestService advanceRequestService)
        {
            var requests = await advanceRequestService.ListRequests(analysisStatus);
            return new ListAdvanceRequestResult(requests);
        }

        [HttpPost, Route("request")]
        public async Task<IActionResult> RequestAdvance([FromBody] AdvanceRequestViewModel model, [FromServices] IAdvanceRequestService advanceRequestService)
        {
            var requestResult = await advanceRequestService.ProcessRequest(model.TransactionNSUs.ToList());
            if (!requestResult.successful) return new BaseErrorResult(requestResult.error);

            return new AdvanceRequestSuccessResult(requestResult.advanceRequest!);
        }

        [HttpPut, Route("start-analysis/{requestId:long}")]
        public async Task<IActionResult> StartAnalysis([FromRoute] long requestId, [FromServices] IAdvanceRequestService advanceRequestService)
        {
            var startResult = await advanceRequestService.StartAnalysis(requestId);
            if (!startResult.successful) return new BaseNotFoundResult(startResult.error);

            return new AdvanceRequestSuccessResult(startResult.advanceRequest!);
        }
    }
}
