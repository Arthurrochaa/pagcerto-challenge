using api.Models.EntityModel.Transactions;
using api.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Models.EntityModel.TransactionModels
{
    public static class TransactionMap
    {
        public static void Map(this EntityTypeBuilder<Transaction> entity)
        {
            entity.ToTable(nameof(Transaction));

            entity.HasKey(p => p.NSU);

            entity.Property(p => p.CreatedAt).HasColumnName("CreatedAt").IsRequired();
            entity.Property(p => p.ApprovalDate).HasColumnName("ApprovalDate");
            entity.Property(p => p.DisapprovalDate).HasColumnName("DisapprovalDate");
            entity.Property(p => p.Anticipated).HasColumnName("Anticipated").IsRequired();
            entity.Property(p => p.GrossAmount).HasColumnName("GrossAmount").HasColumnType("decimal(8,2)").IsRequired();
            entity.Property(p => p.NetAmount).HasColumnName("NetAmount").HasColumnType("decimal(8,2)").IsRequired();
            entity.Property(p => p.FixedRateCharged).HasColumnName("FixedRateCharged").HasColumnType("decimal(8,2)").IsRequired();
            entity.Property(p => p.InstallmentsNumber).HasColumnName("InstallmentsNumber").IsRequired();
            entity.Property(p => p.LastCardDigits).HasColumnName("LastCardDigits").IsRequired();

            entity.Property(p => p.AcquirerConfirmation)
                .HasColumnName("AcquirerConfirmation")
                .HasConversion(
                    v => v.ToString(),
                    v => (AcquirerStatus)Enum.Parse(typeof(AcquirerStatus), v))
                .IsRequired();

            entity.HasMany(t => t.TransactionInstallments)
                .WithOne(ti => ti.Transaction)
                .HasForeignKey(ti => ti.TransactionNSU);
        }
    }
}
