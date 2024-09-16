using Microsoft.AspNetCore.Mvc;
using POC.ChatSignal.Domain.ViewModel.Api.Base;
using System.Net;

namespace POC.ChatSignal.API.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        protected ActionResult<CustomResponseViewModel<T>> CustomResponse<T>(T result)
            => Ok(new CustomResponseViewModel<T>(result));

        protected ActionResult CustomResponseError(HttpStatusCode? statusCode = null, Exception? exception = null)
        {
            var responseError = new CustomResponseViewModel<string>(null)
            {
                Error = true,
                Mensagem = exception.Message,
                StatusCode = statusCode.Value,
            };

            return statusCode switch
            {
                HttpStatusCode.BadRequest => BadRequest(responseError),
                _ => StatusCode((int)statusCode, responseError)
            };
        }
    }
}
