using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TpIntegrador.Filters
{
    public class CheckUser : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["ID"] == null)
            {
                HttpCookie cookie = new HttpCookie("returnUrl", HttpContext.Current.Request.Path);
                HttpContext.Current.Response.Cookies.Add(cookie);
                filterContext.Result = new RedirectResult("~/Ingresar/Login");
                return;
            }
            //sino es user que no me deje entrara a la pag
            if ((int) HttpContext.Current.Session["User"] != 2)
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}