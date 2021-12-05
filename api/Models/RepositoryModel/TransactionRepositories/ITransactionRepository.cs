using api.Models.EntityModel.Transactions;

namespace api.Models.RepositoryModel.TransactionRepositories
{
    public interface ITransactionRepository
    {
        public Task<bool> Create(Transaction transaction);
        public Task<Transaction?> FindByNSU(long nsu);
        public Task<ICollection<Transaction>> ListApproved();
        public Task<ICollection<Transaction>> ListByNSUs(ICollection<long> transactionNSUs);
    }
}
