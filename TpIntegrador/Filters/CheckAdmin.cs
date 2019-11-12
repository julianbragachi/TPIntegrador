using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TpIntegrador.Filters
{
    [CheckSession]
    public class CheckAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["ID"] == null)
            {
                HttpContext.Current.Request.Cookies.Add(new HttpCookie("returnUrl",
                                            HttpContext.Current.Request.Path + "?" +
                                            HttpContext.Current.Request.QueryString));
                filterContext.Result = new RedirectResult("~/Ingresar/Login");
                return;
            }
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