using api.Extensions;
using api.Models.EntityModel.TransactionEntities;
using api.Models.EntityModel.Transactions;
using api.Models.Enums;
using api.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace api.Models.ViewModel.TransactionViewModels
{
    public class TransactionViewModel
    {
        [Required]
        [Range((double)TransactionRules.FixedRate, (double)Decimal.MaxValue)]
        public decimal GrossAmount { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int InstallmentsNumber { get; set; }

        [Required]
        [CustomCreditCard(ErrorMessage = "Invalid card number.")]
        public string CardDigits { get; set; }

        public Transaction Map() => new()
        {
            GrossAmount = GrossAmount,
            InstallmentsNumber = InstallmentsNumber,
            LastCardDigits = CardDigits.LastFourCharacters(),
            CreatedAt = DateTime.UtcNow,
            FixedRateCharged = TransactionRules.FixedRate,
            NetAmount = GrossAmount,
            AcquirerConfirmation = AcquirerStatus.None
        };
    }
}
