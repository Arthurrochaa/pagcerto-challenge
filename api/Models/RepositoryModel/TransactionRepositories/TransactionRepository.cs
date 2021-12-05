using api.Infrastructure.Data;
using api.Models.EntityModel.TransactionEntities;
using api.Models.EntityModel.Transactions;
using Microsoft.EntityFrameworkCore;

namespace api.Models.RepositoryModel.TransactionRepositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionContext _context;

        public TransactionRepository(TransactionContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Transaction transaction)
        {
            if(transaction == null) return false;

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            
            return true;
        }

        public async Task<Transaction?> FindByNSU(long nsu)
        {
            try
            {
                return await _context.Transactions
                    .WhereNSU(nsu)
                    .IncludeInstallments()
                    .SingleOrDefaultAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<ICollection<Transaction>> ListApproved()
            => await _context.Transactions.WhereApproved()
            .IncludeInstallments()
            .ToListAsync();
    }
}
