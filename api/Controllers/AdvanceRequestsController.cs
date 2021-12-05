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
        [HttpPost, Route("request")]
        public async Task<IActionResult> RequestAdvance([FromBody] AdvanceRequestViewModel model, [FromServices] IAdvanceRequestService advanceRequestService)
        {
            var requestResult = await advanceRequestService.ProcessRequest(model.TransactionNSUs.ToList());
            if (!requestResult.Success) return new BaseErrorResult(requestResult.Error);

            return new AdvanceRequestSuccessResult(requestResult.AdvanceRequest!);
        }
    }
}
