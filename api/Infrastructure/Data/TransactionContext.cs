using api.Models.EntityModel.AdvanceTransactionEntities;
using api.Models.EntityModel.TransactionInstallmentModels;
using api.Models.EntityModel.TransactionModels;
using api.Models.EntityModel.Transactions;
using Microsoft.EntityFrameworkCore;

namespace api.Infrastructure.Data
{
    public class TransactionContext : DbContext
    {
        public TransactionContext(DbContextOptions<TransactionContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionInstallment> TransactionInstallments { get; set; }
        public DbSet<AdvanceTransactionRequest> AdvanceTransactionRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>().Map();
            modelBuilder.Entity<TransactionInstallment>().Map();
            modelBuilder.Entity<AdvanceTransactionRequest>().Map();
        }
    }
}
