using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApiCRUD.Helpers
{
    public class MyModelValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid)
            {
                base.OnActionExecuting(actionContext);
            }
            else
            {
                var errorList = actionContext.ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage).ToList();

                var errorMessage = string.Join(Environment.NewLine, errorList);

                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadGateway, errorMessage);
            }
        }
    }
}