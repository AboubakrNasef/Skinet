using API.Errors;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("errors/{code}")]
    [ApiExplorerSettings (IgnoreApi =true)]
    public class ErrorController
    {

        public IActionResult Error(int code)
        {

            return new ObjectResult(new ApiResponse(code));
        }


    }
}
