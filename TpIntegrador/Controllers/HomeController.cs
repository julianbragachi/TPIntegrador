using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TpIntegrador.Filters;
using AyudandoAlProjimo.Services;
using AyudandoAlProjimo.Data;
using AyudandoAlProjimo.Data.ViewModels;

namespace TpIntegrador.Controllers
{

    public class HomeController : Controller
    {
        private readonly ProposalService ps = new ProposalService();
        public ActionResult Index()
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            model.IsLoggedIn = Session["ID"] != null;
            model.MasValoradas = ps.ObtenerCincoPropuestasMasValoradas();
            model.OnlyActiveProposals = Convert.ToBoolean(Request.QueryString.Get("onlyActive"));

            if (model.IsLoggedIn)
            {
                model.MisPropuestas = model.OnlyActiveProposals ?
                    ps.BusquedaMisPropuestasActivas((int)Session["ID"]) :
                    ps.BusquedaMisPropuestas((int)Session["ID"]);
            }


            return View(model);
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