using api.Extensions;
using api.Models.ResultModel;
using api.Models.ResultModel.TransactionResults;
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
            if (!paymentResult.successful) return new BaseErrorResult(paymentResult.error);

            return new TransactionSuccessResult(paymentResult.transaction!);
        }

        [HttpGet, Route("{transactionNSU:long}")]
        public async Task<IActionResult> Find([FromRoute] long transactionNSU, [FromServices] ITransactionService transactionService)
        {
            var transaction = await transactionService.FindByNSU(transactionNSU);
            if (transaction == null) return new BaseNotFoundResult("TRANSACTION_NOT_FOUND");

            return new TransactionSuccessResult(transaction!);
        }

        [HttpGet, Route("approved")]
        public async Task<IActionResult> ListApproved([FromServices] ITransactionService transactionService)
        {
            var transactions = await transactionService.ListApprovedTransactions();
            return new ListTransactionResult(transactions);
        }
    }
}
