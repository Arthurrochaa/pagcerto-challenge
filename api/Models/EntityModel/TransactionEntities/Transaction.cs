using api.Models.EntityModel.AdvanceTransactionEntities;
using api.Models.EntityModel.TransactionEntities;
using api.Models.EntityModel.TransactionInstallmentModels;
using api.Models.Enums;

namespace api.Models.EntityModel.Transactions
{
    public class Transaction
    {
        public long NSU { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public DateTime? DisapprovalDate { get; set; }
        public bool Anticipated { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal FixedRateCharged { get; set; }
        public int InstallmentsNumber { get; set; }
        public string LastCardDigits { get; set; } = "";
        public AcquirerStatus AcquirerConfirmation { get; set; }

        public long? AdvanceTransactionRequestID { get; set; }
        public AdvanceTransactionRequest? AdvanceTransactionRequest { get; set; }

        public ICollection<TransactionInstallment> TransactionInstallments { get; set; }

        public void Disapprove()
        {
            DisapprovalDate = DateTime.UtcNow;
            InstallmentsNumber = 0;
        }

        public Transaction Approve()
        {
            ApprovalDate = DateTime.UtcNow;
            return this;
        }

        public Transaction DiscountRate()
        {
            NetAmount = GrossAmount - TransactionRules.FixedRate;
            return this;
        }

        public Transaction GenerateParcels()
        {
            var installments = new List<TransactionInstallment>();

            decimal netAmountPerInstallment = NetAmount / InstallmentsNumber;
            decimal grossAmountPerInstallment = GrossAmount / InstallmentsNumber;

            for (var i = 0; i < InstallmentsNumber; i++)
            {
                var parcelNumber = i + 1;
                var daysUntilExpiration = parcelNumber * 30;

                installments.Add(new TransactionInstallment
                {
                    ParcelNumber = parcelNumber,
                    GrossAmount = grossAmountPerInstallment,
                    NetAmount = netAmountPerInstallment,
                    ExpectedReceiptDate = DateTime.UtcNow.AddDays(daysUntilExpiration)
                });

                TransactionInstallments = installments;
            }

            return this;
        }

        public Transaction Anticipate()
        {
            Anticipated = true;
            AcquirerConfirmation = AcquirerStatus.Approved;

            foreach(var installment in TransactionInstallments)
            {
                installment.TransferDate = DateTime.UtcNow;
                installment.AnticipatedAmount = installment.NetAmount - ((decimal)(3.8 / 100) * NetAmount);
            }

            return this;
        }

        public Transaction RefuseAnticipation()
        {
            Anticipated = false;
            AcquirerConfirmation = AcquirerStatus.Refused;

            return this;
        }
    }
}
