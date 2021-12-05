using api.Models.EntityModel.AdvanceTransactionEntities;
using api.Models.RepositoryModel.AdvanceRequestRepositories;
using api.Models.RepositoryModel.TransactionRepositories;
using api.Models.ResultModel.AdvanceRequestResults;

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

        public async Task<ProcessRequestResult> ProcessRequest(ICollection<long> transactionNSUs)
        {
            var openRequest = await _requestRepository.FindOpenRequest();
            if(openRequest != null) return new ProcessRequestResult(false, "ALREADY_HAVE_OPEN_REQUEST", null);

            var requestedTransactions = await _transactionRepository.ListByNSUs(transactionNSUs);
            if(!requestedTransactions.Any()) return new ProcessRequestResult(false, "NO_TRANSACTIONS_FOUND", null);

            foreach (var transaction in requestedTransactions)
            {
                if (transaction.AdvanceTransactionRequest != null) return new ProcessRequestResult(false, "TRANSACTION_ALREADY_REQUEST", null);
            }

            var advanceRequest = AdvanceTransactionRequest.Request(requestedTransactions);
            await _requestRepository.Create(advanceRequest);

            return new ProcessRequestResult(true, string.Empty, advanceRequest);
        }
    }
}
