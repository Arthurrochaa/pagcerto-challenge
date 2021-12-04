namespace api.Models.ResultModel
{
    public class BaseResult
    {
        public BaseResult(bool success, string error)
        {
            Success = success;
            Error = error;
        }

        public bool Success { get; set; }
        public string Error { get; set; }
    }
}
