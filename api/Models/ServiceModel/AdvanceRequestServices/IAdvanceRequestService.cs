using api.Models.EntityModel.AdvanceTransactionEntities;
using api.Models.ResultModel.AdvanceRequestResults;

namespace api.Models.ServiceModel.AdvanceRequestServices
{
    public interface IAdvanceRequestService
    {
        public Task<ProcessRequestResult> ProcessRequest(ICollection<long> transactionNSUs);
    }
}
