using Application.Common.Result;

namespace API.Extensions
{
    public static class ResultExtension
    {
        public static IResult ToHttpResponse(this Application.Common.Result.Result result)
        {
            if (result.IsSuccess)
            {
                return Results.Ok(result);
            } else
            {
                return MapErrorResponse(result.Error, result);
            }
        }

        public static IResult ToHttpResponse<T>(this Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Microsoft.AspNetCore.Http.Results.Ok(result);
            }
            else
            {
                return MapErrorResponse(result.Error, result);
            }
        }

        private static IResult MapErrorResponse(Error error, object result)
        {
            return error.Code switch
            {
                ErrorTypeConstant.ValidationError => Microsoft.AspNetCore.Http.Results.BadRequest(result),
                ErrorTypeConstant.NotFound => Microsoft.AspNetCore.Http.Results.NotFound(result),
                ErrorTypeConstant.Forbidden => Microsoft.AspNetCore.Http.Results.Forbid(),
                ErrorTypeConstant.UnAuthorized => Microsoft.AspNetCore.Http.Results.Unauthorized(),
                _ => Microsoft.AspNetCore.Http.Results.Problem(detail: error.Message, statusCode: 500)
            };
        }
    }                                                       
}
