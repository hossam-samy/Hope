namespace Hope.Domain.Common.Errors
{
    public record Error
    {
        private Error(string Code, string Describtion, ErrorType errorType)
        {
            this.Code = Code;
            this.Describtion = Describtion;
            Type = errorType;
        }
        public static Error None => new(string.Empty, string.Empty, ErrorType.Failure);
        public static Error NullValue => new("Error.NullValue", "Null Value Was Provided ", ErrorType.Failure);

        public string Code { get; }
        public string Describtion { get; }
        public ErrorType Type { get; }

        public static Error NotFound(string Code, string Describtion) =>
            new(Code, Describtion, ErrorType.NotFound);
        public static Error Conflict(string Code, string Describtion) =>
            new(Code, Describtion, ErrorType.Conflict);
        public static Error Validation(string Code, string Describtion) =>
            new(Code, Describtion, ErrorType.Validation);
        public static Error Failure(string Code, string Describtion) =>
            new(Code, Describtion, ErrorType.Failure);

    }
    public class Result
    {
        public bool IsSucceded { get; }
        public object? Data { get; }
        private Result(bool isSucceded, Error error, object? data = null)
        {
            IsSucceded = isSucceded;
            Error = error;
            Data = data;
        }
        public Error Error { get; }

        public static Result Success(object? data) => new Result(true, Error.None, data);

        public static Result Failure(Error error) => new Result(false, error);

    }
    public enum ErrorType
    {
        Failure = 0,
        Validation = 1,
        NotFound = 2,
        Conflict = 3
    }

}
