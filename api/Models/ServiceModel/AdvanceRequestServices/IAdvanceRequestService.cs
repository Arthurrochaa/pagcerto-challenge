using api.Models.EntityModel.AdvanceTransactionEntities;
using api.Models.ResultModel.AdvanceRequestResults;

namespace api.Models.ServiceModel.AdvanceRequestServices
{
    public interface IAdvanceRequestService
    {
        public Task<(bool successful, string error, AdvanceTransactionRequest? advanceRequest)> ProcessRequest(ICollection<long> transactionNSUs);
        public Task<(bool successful, string error, AdvanceTransactionRequest? advanceRequest)> StartAnalysis(long requestId);
    }
}
