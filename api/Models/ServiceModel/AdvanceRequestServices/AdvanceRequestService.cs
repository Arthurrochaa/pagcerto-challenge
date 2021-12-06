using api.Models.EntityModel.AdvanceTransactionEntities;
using api.Models.Enums;
using api.Models.RepositoryModel.AdvanceRequestRepositories;
using api.Models.ServiceModel.TransactionServices;

namespace api.Models.ServiceModel.AdvanceRequestServices
{
    public class AdvanceRequestService : IAdvanceRequestService
    {
        private readonly ITransactionService _transactionService;
        private readonly IAdvanceRequestRepository _requestRepository;

        public AdvanceRequestService(ITransactionService transactionService, IAdvanceRequestRepository requestRepository)
        {
            _transactionService = transactionService;
            _requestRepository = requestRepository;
        }

        public async Task<(bool successful, string error)> UpdateTransactions(long requestId, bool approve, ICollection<long> transactionNSUs)
        {
            var advanceRequestDb = await _requestRepository.FindById(requestId);
            if (advanceRequestDb == null) return (false, "REQUEST_NOT_FOUND");

            var transactionsToUpdate = advanceRequestDb.RequestedTransactions
                .Where(rt => transactionNSUs.Contains(rt.NSU)).ToList();

            foreach (var transaction in transactionsToUpdate)
            {
                if (approve)
                {
                    var anticipated = await _transactionService.AnticipateById(transaction.NSU);
                    if (anticipated)
                    {
                        decimal? anticipatedAmount = transaction.TransactionInstallments
                            .Where(ti => ti.AnticipatedAmount != null)
                            .Select(ti => ti.AnticipatedAmount).Sum();

                        advanceRequestDb.AnticipatedAmount += anticipatedAmount ?? 0;
                    }
                }
                else await _transactionService.RefuseAnticipationById(transaction.NSU);
            }

            var isOpen = advanceRequestDb.RequestedTransactions
                .Any(rt => rt.AcquirerConfirmation == AcquirerStatus.None);

            if (!isOpen)
            {
                var totalTransactions = advanceRequestDb.RequestedTransactions.Count;
                var totalApproved = advanceRequestDb.RequestedTransactions
                    .Count(ar => ar.AcquirerConfirmation == AcquirerStatus.Approved);

                bool allApproved = (totalTransactions == totalApproved);
                bool hasAnyApproved = (totalApproved > 0);

                advanceRequestDb.AnalysisStatus = allApproved
                    ? AnalysisStatus.Approved
                    : hasAnyApproved
                        ? AnalysisStatus.PartiallyApproved : AnalysisStatus.Disapproved;
                advanceRequestDb.AnalysisEndedAt = DateTime.UtcNow;
            }

            await _requestRepository.Update(advanceRequestDb);

            return (true, string.Empty);
        }

        public async Task<ICollection<AdvanceTransactionRequest>> ListRequests(AnalysisStatus? analysisStatus)
            => await _requestRepository.ListByStatus(analysisStatus);

        public async Task<(bool successful, string error, AdvanceTransactionRequest? advanceRequest)> ProcessRequest(ICollection<long> transactionNSUs)
        {
            var openRequest = await _requestRepository.FindOpenRequest();
            if (openRequest != null) return (false, "ALREADY_HAVE_OPEN_REQUEST", null);

            var requestedTransactions = await _transactionService.ListByNSUs(transactionNSUs);
            if (!requestedTransactions.Any()) return (false, "NO_TRANSACTIONS_FOUND", null);

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
