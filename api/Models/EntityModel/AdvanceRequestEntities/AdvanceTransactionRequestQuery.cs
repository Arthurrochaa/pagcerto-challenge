using api.Models.EntityModel.AdvanceTransactionEntities;
using Microsoft.EntityFrameworkCore;

namespace api.Models.EntityModel.AdvanceRequestEntities
{
    public static class AdvanceTransactionRequestQuery
    {
        public static IQueryable<AdvanceTransactionRequest> IncludeTransactions(this IQueryable<AdvanceTransactionRequest> advanceRequests)
        {
            return advanceRequests.Include(ar => ar.RequestedTransactions);
        }

        public static IQueryable<AdvanceTransactionRequest> WhereId(this IQueryable<AdvanceTransactionRequest> advanceRequests, long advanceRequestId)
        {
            return advanceRequests.Where(ar => ar.ID == advanceRequestId);
        }

        public static IQueryable<AdvanceTransactionRequest> WhereNotEnded(this IQueryable<AdvanceTransactionRequest> advanceRequests)
        {
            return advanceRequests.Where(ar => ar.AnalysisEndedAt == null);
        }
    }
}
