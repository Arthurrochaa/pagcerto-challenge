using api.Models.EntityModel.AdvanceTransactionEntities;
using api.Models.Enums;

namespace api.Models.RepositoryModel.AdvanceRequestRepositories
{
    public interface IAdvanceRequestRepository
    {
        public Task<bool> Create(AdvanceTransactionRequest advanceRequest);
        public Task<AdvanceTransactionRequest?> FindById(long advanceRequestId);
        public Task<AdvanceTransactionRequest?> FindOpenRequest();
        public Task Update(AdvanceTransactionRequest advanceRequest);
        public Task<ICollection<AdvanceTransactionRequest>> ListByStatus(AnalysisStatus? analysisStatus);
    }
}
