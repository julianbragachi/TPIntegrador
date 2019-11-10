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
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["ID"] == null)

            {
                filterContext.Result = new RedirectResult("~/Ingresar/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}