using api.Models.EntityModel.AdvanceTransactionEntities;

namespace api.Models.ResultModel.AdvanceRequestResults
{
    public class ProcessRequestResult : BaseResult
    {
        public ProcessRequestResult(bool success, string error, AdvanceTransactionRequest? advanceRequest) : base(success, error)
        {
            AdvanceRequest = advanceRequest;
        }

        public AdvanceTransactionRequest? AdvanceRequest { get; set; }
    }
}
