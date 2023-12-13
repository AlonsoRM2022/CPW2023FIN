using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParkerWeb.Filter
{
    public class ValidarSessionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["User"] == null)
            {
                filterContext.Result = new RedirectResult("~/Access/Index");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}