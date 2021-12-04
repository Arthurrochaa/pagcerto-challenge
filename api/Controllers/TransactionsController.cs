using api.Extensions;
using api.Models.ResultModel;
using api.Models.ServiceModel.TransactionServices;
using api.Models.ViewModel.TransactionViewModels;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/v1/transactions")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        [HttpPost, Route("pay-with-card")]
        public async Task<IActionResult> PayWithCard([FromBody] TransactionViewModel model, [FromServices] ITransactionService transactionService)
        {
            var transaction = model.Map();

            var paymentResult = await transactionService.Process(transaction, model.CardDigits.FirstFourCharacters());
            if (!paymentResult.Success) return new TransactionErrorResult(paymentResult.Error);

            return new TransactionSuccessResult(paymentResult.Transaction!);
        }
    }
}
