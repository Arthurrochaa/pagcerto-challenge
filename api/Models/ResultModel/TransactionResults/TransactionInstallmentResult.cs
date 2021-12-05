using api.Models.EntityModel.TransactionInstallmentModels;

namespace api.Models.ResultModel.TransactionResults
{
    public class TransactionInstallmentResult
    {
        public TransactionInstallmentResult(TransactionInstallment installment)
        {
            ID = installment.ID;
            ParcelNumber = installment.ParcelNumber;
            GrossAmount = installment.GrossAmount;
            NetAmount = installment.NetAmount;
            AnticipatedAmount = installment.AnticipatedAmount;
            ExpectedReceiptDate = installment.ExpectedReceiptDate;
            TransferDate = installment.TransferDate;
        }

        public long ID { get; set; }
        public long ParcelNumber { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal? AnticipatedAmount { get; set; }
        public DateTime ExpectedReceiptDate { get; set; }
        public DateTime? TransferDate { get; set; }
    }
}
