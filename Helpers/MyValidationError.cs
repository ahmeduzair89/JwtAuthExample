using JwtAuthExample.WrapperModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JwtAuthExample.Helpers
{
    public class MyValidationError:IActionResult
    {
        public async Task ExecuteResultAsync(ActionContext context)
        {

            string errors = "";
         
            
                foreach(var error in context.ModelState.Values)
                {
                    foreach(var item in error.Errors)
                {

                    errors = errors + $"{ item.ErrorMessage}; ";
                }
                }
            var objectResult = new ObjectResult(ApiWrapper.SetResponse(error:errors,success:false,data:null)) { StatusCode = ((int)HttpStatusCode.BadRequest) };
            await objectResult.ExecuteResultAsync(context);
        }
    }

}

