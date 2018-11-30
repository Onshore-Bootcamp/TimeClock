namespace TimeClock.Custom
{
    using System.Web.Mvc;

    public class AjaxOnly : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                //filterContext.Result = new HttpNotFoundResult();
            }
            base.OnActionExecuting(filterContext);
        }
    }
}