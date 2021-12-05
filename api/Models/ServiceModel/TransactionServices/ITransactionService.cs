using api.Models.EntityModel.Transactions;
using api.Models.ResultModel;
using api.Models.ViewModel.TransactionViewModels;

namespace api.Models.ServiceModel.TransactionServices
{
    public interface ITransactionService
    {
        public Task<(bool successful, string error, Transaction? transaction)> Process(Transaction transaction, string firstCardDigits);
        public Task<Transaction?> FindByNSU(long transactionNSU);
        public Task<ICollection<Transaction>> ListApprovedTransactions();

    }
}
