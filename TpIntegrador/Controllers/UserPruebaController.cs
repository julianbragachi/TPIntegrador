using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TpIntegrador.Filters;

namespace TpIntegrador.Controllers
{
    [CheckUser]
    public class UserPruebaController : Controller
    {
        // GET: UserPrueba
        public ActionResult Index()
        {
            return View();
        }
    }
}