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
        public AcquirerStatus AcquirerConfirmation { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal FixedRateCharged { get; set; }
        public int InstallmentsNumber { get; set; }
        public string LastCardDigits { get; set; } = "";

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

            decimal valuePerInstallment = NetAmount / InstallmentsNumber;
            for(var i = 0; i < InstallmentsNumber; i++)
            {
                var parcelNumber = i + 1;
                var daysUntilExpiration = parcelNumber * 30;

                installments.Add(new TransactionInstallment
                {
                    ParcelNumber = parcelNumber,
                    GrossAmount = valuePerInstallment,
                    NetAmount = valuePerInstallment,
                    ExpectedReceiptDate = DateTime.UtcNow.AddDays(daysUntilExpiration)
                });

                TransactionInstallments = installments;
            }

            return this;
        }
    }
}
