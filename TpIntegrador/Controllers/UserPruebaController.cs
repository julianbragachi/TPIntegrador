using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TpIntegrador.Filters;

namespace TpIntegrador.Controllers
{
    //Si da exepcion algun controlador o metodo con filtro
    //agregar [CheckUser] [CheckAdmin] como decoracion
    [CheckUser]
    [CheckAdmin]
    public class UserPruebaController : Controller
    {
        // GET: UserPrueba
        public ActionResult Index()
        {
            return View();
        }

    }
}