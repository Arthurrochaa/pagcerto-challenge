using api.Models.EntityModel.AdvanceTransactionEntities;
using api.Models.Enums;
using api.Models.RepositoryModel.AdvanceRequestRepositories;
using api.Models.RepositoryModel.TransactionRepositories;

namespace api.Models.ServiceModel.AdvanceRequestServices
{
    public class AdvanceRequestService : IAdvanceRequestService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAdvanceRequestRepository _requestRepository;

        public AdvanceRequestService(ITransactionRepository transactionRepository, IAdvanceRequestRepository requestRepository)
        {
            _transactionRepository = transactionRepository;
            _requestRepository = requestRepository;
        }

        public async Task<ICollection<AdvanceTransactionRequest>> ListRequests(AnalysisStatus? analysisStatus)
            => await _requestRepository.ListByStatus(analysisStatus);

        public async Task<(bool successful, string error, AdvanceTransactionRequest? advanceRequest)> ProcessRequest(ICollection<long> transactionNSUs)
        {
            var openRequest = await _requestRepository.FindOpenRequest();
            if(openRequest != null) return (false, "ALREADY_HAVE_OPEN_REQUEST", null);

            var requestedTransactions = await _transactionRepository.ListByNSUs(transactionNSUs);
            if(!requestedTransactions.Any()) return (false, "NO_TRANSACTIONS_FOUND", null);

            foreach (var transaction in requestedTransactions)
            {
                if (transaction.AdvanceTransactionRequest != null) return (false, "TRANSACTION_ALREADY_REQUEST", null);
            }

            var advanceRequest = AdvanceTransactionRequest.Request(requestedTransactions);
            await _requestRepository.Create(advanceRequest);

            return (true, string.Empty, advanceRequest);
        }

        public async Task<(bool successful, string error, AdvanceTransactionRequest? advanceRequest)> StartAnalysis(long requestId)
        {
            var advanceRequestDb = await _requestRepository.FindById(requestId);
            if (advanceRequestDb == null) return (false, "REQUEST_NOT_FOUND", null);

            if (advanceRequestDb.AnalysisStartedAt != null) return (false, "ANALYZE_ALREADY_STARTED", advanceRequestDb);

            advanceRequestDb.StartAnalysis();
            await _requestRepository.Update(advanceRequestDb);

            return (true, string.Empty, advanceRequestDb);
        }
    }
}
