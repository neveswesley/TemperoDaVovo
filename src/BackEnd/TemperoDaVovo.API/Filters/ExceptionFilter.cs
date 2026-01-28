using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case ErrorOnValidationException validationEx:
                    HandleValidationException(context, validationEx);
                    break;
                
                case UnauthorizedException unauthorizedEx:
                    HandleUnauthorizedException(context, unauthorizedEx);
                    break;
                
                case BusinessException businessEx:
                    HandleBusinessException(context, businessEx);
                    break;
                    
                case TemperoDaVovoException projectEx:
                    HandleProjectException(context, projectEx);
                    break;

                default:
                    HandleUnknownException(context);
                    break;
            }
        }

        private void HandleBusinessException(ExceptionContext context, BusinessException ex)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new BadRequestObjectResult(new ResponseErrorJson(ex.ErrorMessages));
        }

        private void HandleValidationException(ExceptionContext context, ErrorOnValidationException ex)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new BadRequestObjectResult(new ResponseErrorJson(ex.ErrorMessages));
        }

        private void HandleUnauthorizedException(ExceptionContext context, UnauthorizedException ex)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ex.ErrorMessages));
        }

        private void HandleProjectException(ExceptionContext context, TemperoDaVovoException ex)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new BadRequestObjectResult(new ResponseErrorJson(ex.Message));
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            var exception = context.Exception;

            context.HttpContext.Response.StatusCode =
                (int)HttpStatusCode.InternalServerError;

            context.Result = new ObjectResult(new
            {
                message = exception.Message,
                exception = exception.GetType().Name,
                stackTrace = exception.StackTrace
            });

            context.ExceptionHandled = true;
        }

    }
}