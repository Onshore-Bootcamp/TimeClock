namespace TimeClock.Custom
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class SecurityFilter : ActionFilterAttribute
    {
        private readonly string _Redirect;
        private readonly string _Key;
        private readonly string[] _Args;
        public SecurityFilter(string redirect, string key, params string[] args)
        {
            _Redirect = redirect;
            _Key = key;
            _Args = args;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;

            if(session[_Key] == null || !_Args.Contains(session[_Key]))
            {
                filterContext.Result = new RedirectResult(_Redirect, false);
            }
            base.OnActionExecuting(filterContext);
        }


    }
}