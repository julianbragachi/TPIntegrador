using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AyudandoAlProjimo.Data;

namespace TpIntegrador.Controllers
{
    public class PropuestasController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AgregarPropuesta()
        {
            Propuestas p = new Propuestas();

            p.PropuestasDonacionesHorasTrabajo.Add(new PropuestasDonacionesHorasTrabajo());
            p.PropuestasDonacionesInsumos.Add(new PropuestasDonacionesInsumos());
            p.PropuestasDonacionesMonetarias.Add(new PropuestasDonacionesMonetarias());

            return View(p);
        }

        [HttpPost]
        public ActionResult AgregarPropuesta(Propuestas p)
        {
            return View(p);
        }
    }
}