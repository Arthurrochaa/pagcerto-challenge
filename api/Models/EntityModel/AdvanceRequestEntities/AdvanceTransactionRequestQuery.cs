using api.Models.EntityModel.AdvanceTransactionEntities;
using api.Models.Enums;
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

        public static IQueryable<AdvanceTransactionRequest> WhereAnalysisStatus(this IQueryable<AdvanceTransactionRequest> advanceRequests, AnalysisStatus? analysisStatus)
        {
            if(analysisStatus == null) return advanceRequests;

            return advanceRequests.Where(ar => ar.AnalysisStatus == analysisStatus);
        }
    }
}
