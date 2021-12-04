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
    }
}
