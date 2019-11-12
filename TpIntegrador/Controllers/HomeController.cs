using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TpIntegrador.Filters;
using AyudandoAlProjimo.Services;
using AyudandoAlProjimo.Data;

namespace TpIntegrador.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ProposalService ps = new ProposalService();
        public ActionResult Index()
        {
            if (HttpContext.Session["ID"] != null)
            {
                List<Propuestas> lista = ps.BusquedaPropuestasAjenas((int)HttpContext.Session["ID"]);
                return View(lista);
            }
            else
            {
                List<Propuestas> lista = ps.BusquedaPropuestasAjenas();
                return View(lista);
            }
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Error(int error = 0)
        {
            switch (error)
            {
                case 505:
                    ViewBag.Title = "Ocurrio un error inesperado";
                    ViewBag.Description = "Esto es muy vergonzoso, esperemos que no vuelva a pasar ..";
                    break;

                case 404:
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "La URL que está intentando ingresar no existe";
                    break;

                default:
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "Algo salio muy mal :^( ..";
                    break;
            }

            return View("~/views/Home/Error.cshtml");
        }
    }
}