using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using WebApiCRUD.Loggers;
using WebApiCRUD.Models;

namespace WebApiCRUD.Helpers
{
    public class MyErrorAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            MyErrorResponse errorResponse = new MyErrorResponse
            {
                Controller = actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName,
                Action = actionExecutedContext.ActionContext.ActionDescriptor.ActionName,
                Message = actionExecutedContext.Exception.Message,
                StackTrace = actionExecutedContext.Exception.StackTrace
            };

            MyLog.Read(errorResponse);

            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.BadGateway, errorResponse);
        }
    }
}