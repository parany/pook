using System.Web.Mvc;
using Pook.Data.Exceptions;

namespace Pook.Web.Filters
{
    public class NotFoundAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext exceptionContext)
        {
            if (exceptionContext.Exception is NotFoundException)
            {
                exceptionContext.ExceptionHandled = true;
                exceptionContext.Result = new ViewResult { ViewName = "Error" };
            }
        }
    }

}