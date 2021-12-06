using api.Models.EntityModel.AdvanceTransactionEntities;
using api.Models.Enums;
using api.Models.ResultModel.AdvanceRequestResults;

namespace api.Models.ServiceModel.AdvanceRequestServices
{
    public interface IAdvanceRequestService
    {
        public Task<(bool successful, string error, AdvanceTransactionRequest? advanceRequest)> ProcessRequest(ICollection<long> transactionNSUs);
        public Task<(bool successful, string error, AdvanceTransactionRequest? advanceRequest)> StartAnalysis(long requestId);
        public Task<ICollection<AdvanceTransactionRequest>> ListRequests(AnalysisStatus? analysisStatus);
        public Task<(bool successful, string error)> UpdateTransactions(long requestId, bool approve, ICollection<long> transactionNSUs);
    }
}
