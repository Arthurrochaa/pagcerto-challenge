using api.Infrastructure.Data;
using api.Models.EntityModel.AdvanceRequestEntities;
using api.Models.EntityModel.AdvanceTransactionEntities;
using Microsoft.EntityFrameworkCore;

namespace api.Models.RepositoryModel.AdvanceRequestRepositories
{
    public class AdvanceRequestRepository : IAdvanceRequestRepository
    {
        private readonly TransactionContext _context;

        public AdvanceRequestRepository(TransactionContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(AdvanceTransactionRequest advanceRequest)
        {
            if (advanceRequest == null) return false;

            _context.AdvanceTransactionRequests.Add(advanceRequest);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<AdvanceTransactionRequest?> FindById(long advanceRequestId)
        {
            try
            {
                return await _context.AdvanceTransactionRequests
                    .WhereId(advanceRequestId)
                    .IncludeTransactions()
                    .SingleOrDefaultAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<AdvanceTransactionRequest?> FindOpenRequest()
        {
            try
            {
                return await _context.AdvanceTransactionRequests
                    .WhereNotEnded()
                    .SingleOrDefaultAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task Update(AdvanceTransactionRequest advanceRequest)
        {
            _context.AdvanceTransactionRequests.Update(advanceRequest);
            await _context.SaveChangesAsync();
        }
    }
}
