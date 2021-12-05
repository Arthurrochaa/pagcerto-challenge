using api.Models.EntityModel.TransactionEntities;
using api.Models.EntityModel.Transactions;
using api.Models.RepositoryModel.TransactionRepositories;
using api.Models.ResultModel;

namespace api.Models.ServiceModel.TransactionServices
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        private const string FailedToPersist = "FAILED_TO_PERSIST";

        public async Task<Transaction?> FindByNSU(long transactionNSU)
            => await _transactionRepository.FindByNSU(transactionNSU);

        public async Task<(bool successful, string error, Transaction? transaction)> Process(Transaction transaction, string firstCardDigits)
        {
            var cardHasApproved = ApproveCreditCard(firstCardDigits);
            if (!cardHasApproved) transaction.Disapprove();
            else
            {
                transaction
                    .Approve()
                    .DiscountRate()
                    .GenerateParcels();
            }

            var successful = await _transactionRepository.Create(transaction);
            if(!successful) return (successful, FailedToPersist, null);

            return (successful, string.Empty, transaction);
        }

        private bool ApproveCreditCard(string firstCardDigits) 
            => firstCardDigits != TransactionRules.InvalidFirstDigits;

        public async Task<ICollection<Transaction>> ListApprovedTransactions()
            => await _transactionRepository.ListApproved();
    }
}
