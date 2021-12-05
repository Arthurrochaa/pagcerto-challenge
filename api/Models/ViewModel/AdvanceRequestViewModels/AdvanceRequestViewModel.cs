using api.Models.EntityModel.AdvanceTransactionEntities;
using System.ComponentModel.DataAnnotations;

namespace api.Models.ViewModel.AdvanceRequestViewModels
{
    public class AdvanceRequestViewModel
    {
        [Required]
        [MinLength(1)]
        public IEnumerable<long> TransactionNSUs { get; set; }
    }
}
