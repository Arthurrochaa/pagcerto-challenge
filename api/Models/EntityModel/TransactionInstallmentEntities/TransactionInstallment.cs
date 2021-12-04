using api.Models.EntityModel.Transactions;

namespace api.Models.EntityModel.TransactionInstallmentModels
{
    public class TransactionInstallment
    {
        public long ID { get; set; }
        public long ParcelNumber { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal? AnticipatedAmount { get; set; }
        public DateTime ExpectedReceiptDate { get; set; }
        public DateTime? TransferDate { get; set; }

        public long TransactionNSU { get; set; }
        public Transaction? Transaction { get; set; }
    }
}
