using api.Models.EntityModel.Transactions;
using Microsoft.EntityFrameworkCore;

namespace api.Models.EntityModel.TransactionEntities
{
    public static class TransactionQuery
    {
        public static IQueryable<Transaction> IncludeInstallments(this IQueryable<Transaction> transactions)
        {
            return transactions.Include(transaction => transaction.TransactionInstallments);
        }

        public static IQueryable<Transaction> WhereNSU(this IQueryable<Transaction> transactions, long transactionNsu)
        {
            return transactions.Where(transaction => transaction.NSU == transactionNsu);
        }

        public static IQueryable<Transaction> WhereApproved(this IQueryable<Transaction> transactions)
        {
            return transactions.Where(transaction => transaction.ApprovalDate != null);
        }
    }
}
