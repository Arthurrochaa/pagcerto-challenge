using api.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Models.EntityModel.AdvanceTransactionEntities
{
    public static class AdvanceTransactionRequestMap
    {
        public static void Map(this EntityTypeBuilder<AdvanceTransactionRequest> entity)
        {
            entity.ToTable(nameof(AdvanceTransactionRequest));

            entity.HasKey(p => p.ID);

            entity.Property(p => p.RequestedAt).HasColumnName("RequestedAt").IsRequired();
            entity.Property(p => p.AnalysisStartedAt).HasColumnName("AnalysisStartedAt");
            entity.Property(p => p.AnalysisEndedAt).HasColumnName("AnalysisEndedAt");
            entity.Property(p => p.RequestedAmount).HasColumnName("RequestedAmount").HasColumnType("decimal(8,2)").IsRequired();
            entity.Property(p => p.AnticipatedAmount).HasColumnName("AnticipatedAmount").HasColumnType("decimal(8,2)").IsRequired();

            entity.Property(p => p.AnalysisStatus)
                .HasColumnName("AnalysisStatus")
                .HasConversion(
                    v => v.ToString(),
                    v => (AnalysisStatus)Enum.Parse(typeof(AnalysisStatus), v))
                .IsRequired();

            entity.HasMany(atr => atr.RequestedTransactions)
                .WithOne(t => t.AdvanceTransactionRequest)
                .HasForeignKey(t => t.AdvanceTransactionRequestID);
        }
    }
}
