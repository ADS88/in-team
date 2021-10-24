
using System;
using Server.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;


namespace Server.Api.Controllers
{
    /// <summary>
    /// Global level error controller to unify error handling logic
    /// </summary>
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {

        /// <summary>
        /// Endpoint to send the error response
        /// </summary>
        /// <returns>An error response with the appropriate error code</returns>
        [Route("error")]
        public ErrorResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error; // Your exception
            var code = 500; // Internal Server Error by default

            if (exception is NullReferenceException) code = 400;
            else code = 500;

            Response.StatusCode = code; // You can use HttpStatusCode enum instead

            return new ErrorResponse(exception); // Your error model
        }
    }
}