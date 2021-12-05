using api.Models.EntityModel.Transactions;
using api.Models.Enums;

namespace api.Models.EntityModel.AdvanceTransactionEntities
{
    public class AdvanceTransactionRequest
    {
        public long ID { get; set; }
        public DateTime RequestedAt{ get; set; }
        public DateTime? AnalysisStartedAt { get; set; }
        public DateTime? AnalysisEndedAt { get; set; }
        public AnalysisStatus AnalysisStatus { get; set; }
        public decimal RequestedAmount { get; set; }
        public decimal AnticipatedAmount{ get; set; }

        public ICollection<Transaction> RequestedTransactions { get; set; }

        public static AdvanceTransactionRequest Request(ICollection<Transaction> transactions) => new()
        {
            RequestedAt = DateTime.UtcNow,
            AnalysisStatus = AnalysisStatus.Pending,
            RequestedTransactions = transactions,
            RequestedAmount = transactions.Sum(t => t.NetAmount)
        };

        public void StartAnalysis() { 
            AnalysisStartedAt = DateTime.UtcNow;
            AnalysisStatus = AnalysisStatus.UnderAnalysis;
        }
    }
}
