namespace DocumentGenerator.Web.Models
{
    public class Result
    {
        public bool IsSuccess { get; init; }
        public string? ErrorMessage { get; init; }
        public int? StatusCode { get; init; }
        public string? RawErrorDetails { get; init; }

        protected Result(bool isSuccess, string? errorMessage = null, int? statusCode = null, string? rawErrorDetails = null)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
            RawErrorDetails = rawErrorDetails;
        }

        public static Result Success() => new(true);

        public static Result Failure(string message, int? statusCode = null, string? rawErrorDetails = null) =>
            new(false, message, statusCode, rawErrorDetails);
    }

    public class Result<TValue> : Result
    {
        public TValue? Value { get; init; }

        private Result(bool isSuccess, TValue? value, string? errorMessage, int? statusCode, string? rawErrorDetails)
            : base(isSuccess, errorMessage, statusCode, rawErrorDetails)
        {
            Value = value;
        }

        public static Result<TValue> Success(TValue value) => new(true, value, null, null, null);

        public static new Result<TValue> Failure(string message, int? statusCode = null, string? rawErrorDetails = null) =>
            new(false, default(TValue), message, statusCode, rawErrorDetails);
    }
}
