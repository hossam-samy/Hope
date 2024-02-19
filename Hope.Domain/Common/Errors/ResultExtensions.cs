using Hope.Domain.Common.Errors;
using Microsoft.AspNetCore.Http;

namespace Hope.Core.Common.Errors
{
    public static class ResultExtensions
    {
        public static IResult ToProblemDetails(this Result result)
        {

            if (result.IsSucceded)
            {
                throw new InvalidOperationException();
            }
            return Results.Problem(
                statusCode: GetStatusCode(result.Error.Type),
                title: GetTitle(result.Error.Type),
                 extensions: new Dictionary<string, object?> {
                     {"errors",new[]{result.Error } }

                 });
            static int GetStatusCode(ErrorType errorType) => errorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };
            static string GetTitle(ErrorType errorType) => errorType switch
            {
                ErrorType.Validation => "Bad Request",
                ErrorType.Conflict => "Conflict",
                ErrorType.NotFound => "Not Found",
                _ => "Server Error"
            };

        }

    }
}
