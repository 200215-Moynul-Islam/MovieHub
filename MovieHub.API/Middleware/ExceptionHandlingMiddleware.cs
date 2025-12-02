using System.Net.Mime;
using MovieHub.API.Constants;
using MovieHub.API.Exceptions;
using MovieHub.API.Utilities;

namespace MovieHub.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Dictionary<Type, int> _businessExceptionInfo =
            new Dictionary<Type, int>
            {
                {
                    typeof(InvalidCredentialsException),
                    StatusCodes.Status401Unauthorized
                },
                { typeof(NotFoundException), StatusCodes.Status404NotFound },
                { typeof(ConflictException), StatusCodes.Status409Conflict },
            };

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessException ex)
            {
                context.Response.ContentType = MediaTypeNames.Application.Json;

                if (
                    _businessExceptionInfo.TryGetValue(
                        ex.GetType(),
                        out var statusCode
                    )
                )
                {
                    context.Response.StatusCode = statusCode;
                    await context.Response.WriteAsJsonAsync(
                        ResponseHelper.Fail(
                            errors: new List<string> { ex.Message }
                        )
                    );
                }
                else
                {
                    // TODO: Logger is required to save that Exception not found in the _businessExceptionInfo dictionary.
                    context.Response.StatusCode =
                        StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsJsonAsync(
                        ResponseHelper.Fail(
                            errors: new List<string>
                            {
                                BusinessErrorMessages.InternalServerError,
                            }
                        )
                    );
                }
            }
            catch (Exception)
            {
                //TODO: Log the original error message ex.Message to understand the problem.
                context.Response.StatusCode =
                    StatusCodes.Status500InternalServerError;
                context.Response.ContentType = MediaTypeNames.Application.Json;
                await context.Response.WriteAsJsonAsync(
                    ResponseHelper.Fail(
                        message: BusinessErrorMessages.InternalServerError
                    )
                );
            }
        }
    }
}
