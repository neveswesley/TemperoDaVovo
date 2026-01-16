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

                case TemperoDaVovoException projectEx:
                    HandleProjectException(context, projectEx);
                    break;

                default:
                    HandleUnknownException(context);
                    break;
            }
        }

        private void HandleValidationException(ExceptionContext context, ErrorOnValidationException ex)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new BadRequestObjectResult(new ResponseErrorJson(ex.ErrorMessages));
        }

        private void HandleProjectException(ExceptionContext context, TemperoDaVovoException ex)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new BadRequestObjectResult(new ResponseErrorJson(ex.Message));
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorJson("Erro desconhecido!"));
        }
    }
}