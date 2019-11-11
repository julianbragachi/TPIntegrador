using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TpIntegrador.Filters
{
    public class CheckAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            //sino es admin que no me deje entrar a la pag
            if ((int) HttpContext.Current.Session["User"] != 1)
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
                return;
            }
            
            base.OnActionExecuting(filterContext);
        }
    }
}