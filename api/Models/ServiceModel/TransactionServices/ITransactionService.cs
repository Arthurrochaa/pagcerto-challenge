using api.Models.EntityModel.Transactions;
using api.Models.ResultModel;
using api.Models.ViewModel.TransactionViewModels;

namespace api.Models.ServiceModel.TransactionServices
{
    public interface ITransactionService
    {
        public Task<TransactionProcessResult> Process(Transaction transaction, string firstCardDigits);
    }
}
