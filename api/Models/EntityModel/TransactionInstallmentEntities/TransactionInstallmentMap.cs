using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Models.EntityModel.TransactionInstallmentModels
{
    public static class TransactionInstallmentMap
    {
        public static void Map(this EntityTypeBuilder<TransactionInstallment> entity)
        {
            entity.ToTable(nameof(TransactionInstallment));

            entity.HasKey(p => p.ID);

            entity.Property(p => p.ParcelNumber).HasColumnName("ParcelNumber").IsRequired();
            entity.Property(p => p.GrossAmount).HasColumnName("GrossAmount").HasColumnType("decimal(8,2)").IsRequired();
            entity.Property(p => p.NetAmount).HasColumnName("NetAmount").HasColumnType("decimal(8,2)").IsRequired();
            entity.Property(p => p.AnticipatedAmount).HasColumnName("AnticipatedAmount").HasColumnType("decimal(8,2)");
            entity.Property(p => p.ExpectedReceiptDate).HasColumnName("ExpectedReceiptDate").IsRequired();
            entity.Property(p => p.TransferDate).HasColumnName("TransferDate");
            
            entity.HasOne(ti => ti.Transaction)
                .WithMany(i => i.TransactionInstallments)
                .HasForeignKey(ti => ti.TransactionNSU);
        }
    }
}
