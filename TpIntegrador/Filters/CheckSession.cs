using AyudandoAlProjimo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TpIntegrador.Controllers;

namespace TpIntegrador.Filters
{
    public class CheckSession : ActionFilterAttribute
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
            base.OnActionExecuting(filterContext);
        }
    }
}