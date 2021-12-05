using api.Models.EntityModel.Transactions;
using api.Models.Enums;
using api.Models.ResultModel.TransactionResults;
using Microsoft.AspNetCore.Mvc;

namespace api.Models.ResultModel
{
    public class TransactionSuccessResult : IActionResult
    {
        public TransactionSuccessResult(Transaction transaction)
        {
            NSU = transaction.NSU;
            CreatedAt = transaction.CreatedAt;
            ApprovalDate = transaction.ApprovalDate;
            DisapprovalDate = transaction.DisapprovalDate;
            Anticipated = transaction.Anticipated;
            GrossAmount = transaction.GrossAmount;
            NetAmount = transaction.NetAmount;
            FixedRateCharged = transaction.FixedRateCharged;
            InstallmentsNumber = transaction.InstallmentsNumber;
            LastCardDigits = transaction.LastCardDigits;
            AcquirerConfirmation = transaction.AcquirerConfirmation;
            TransactionInstallments = transaction.TransactionInstallments?
                .Select(ti => new TransactionInstallmentResult(ti))
                .ToList();
        }

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
        public ICollection<TransactionInstallmentResult>? TransactionInstallments { get; set; }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return new OkObjectResult(this).ExecuteResultAsync(context);
        }
    }
}
