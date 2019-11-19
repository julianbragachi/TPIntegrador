using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TpIntegrador.Controllers
{
    [Route]
    public class AcercaDeController : Controller
    {

        // eg.: /acerca-de
        [Route("/Acerca-de")]
        public ActionResult Index() 
        {
            return View("Index");
        }
    }
}