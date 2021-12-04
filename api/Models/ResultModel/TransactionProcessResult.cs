using api.Models.EntityModel.Transactions;

namespace api.Models.ResultModel
{
    public class TransactionProcessResult : BaseResult
    {
        public TransactionProcessResult(bool success, string error, Transaction? transaction) : base(success, error)
        {
            Transaction = transaction;
        }

        public Transaction? Transaction { get; set; }
    }
}
