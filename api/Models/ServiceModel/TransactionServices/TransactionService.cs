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

        public async Task<TransactionProcessResult> Process(Transaction transaction, string firstCardDigits)
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
            if(!successful) return new TransactionProcessResult(successful, "FAILED_TO_PERSIST", null);

            return new TransactionProcessResult(successful, string.Empty, transaction);
        }

        private bool ApproveCreditCard(string firstCardDigits) 
            => firstCardDigits != TransactionRules.InvalidFirstDigits;

    }
}
