using System.Web.Mvc;

namespace Pook.Web.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.Controller.ViewData.ModelState.IsValid == false)
            {
                var viewName = filterContext.Controller.ControllerContext.RouteData.Values["action"];
                filterContext.Result = new ViewResult
                {
                    ViewName = viewName.ToString(),
                    ViewData = filterContext.Controller.ViewData
                };
            }
        }
    }

}