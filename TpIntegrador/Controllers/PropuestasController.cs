using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AyudandoAlProjimo.Data;
using AyudandoAlProjimo.Data.ViewModels;
using AyudandoAlProjimo.Services;
using TpIntegrador.Filters;

namespace TpIntegrador.Controllers
{
    public class PropuestasController : Controller
    {
        private ProposalService ProposalService = new ProposalService();
        private UserService UserService = new UserService();

        public ActionResult AgregarPropuesta()
        {
            var isLoggedIn = isValidUserSession();

            if (!isLoggedIn) return Redirect("/Ingresar/Login");

            return View();
        }

        public ActionResult AgregarPropuestaMonetaria()
        {
            var isLoggedIn = isValidUserSession();

            if (!isLoggedIn) return Redirect("/Ingresar/Login");

            AgregarPropuestaMonetariaViewModel p = new AgregarPropuestaMonetariaViewModel();

            p.TipoDonacion = 0;

            return View(p);
        }

        [HttpPost]
        public ActionResult AgregarPropuestaMonetaria(AgregarPropuestaMonetariaViewModel p)
        {
            if (!ModelState.IsValid)
            {
                return View(p);
            }

            var user = UserService.TraerPerfilDelUsuario((int)Session["ID"]);
            var error = ProposalService.ValidateBeforeCreate(user);

            if(error != ErrorCodeAddProposalEnum.None)
            {
                ViewBag.Error = error;

                return View(p);
            }

            ProposalService.AgregarPropuestaMonetaria(p, user);

            return View(p);
        }

        public ActionResult AgregarPropuestaHoraTrabajo()
        {
            var isLoggedIn = isValidUserSession();

            if (!isLoggedIn) return Redirect("/Ingresar/Login");

            AgregarPropuestaHoraTrabajoViewModel p = new AgregarPropuestaHoraTrabajoViewModel();

            p.TipoDonacion = 1;

            return View(p);
        }

        [HttpPost]
        public ActionResult AgregarPropuestaHoraTrabajo(AgregarPropuestaHoraTrabajoViewModel p)
        {
            if (!ModelState.IsValid)
            {
                return View(p);
            }

            var user = UserService.TraerPerfilDelUsuario((int)Session["ID"]);
            var error = ProposalService.ValidateBeforeCreate(user);

            if (error != ErrorCodeAddProposalEnum.None)
            {
                ViewBag.Error = error;

                return View(p);
            }

            ProposalService.AgregarPropuestaHoraTrabajo(p, user);

            return View(p);
        }
        [HttpGet]
        public ActionResult VerDetalles(int id)
        {
            return View(ProposalService.VerPropuestaYDonaciones(id));
        }

        public ActionResult AgregarPropuestaInsumos()
        {
            var isLoggedIn = isValidUserSession();

            if (!isLoggedIn) return Redirect("/Ingresar/Login");

            AgregarPropuestaInsumosViewModel p = new AgregarPropuestaInsumosViewModel();

            p.TipoDonacion = 2;

            return View(p);
        }

        [HttpPost]
        public ActionResult AgregarPropuestaInsumos(AgregarPropuestaInsumosViewModel p)
        {
            if (!ModelState.IsValid)
            {
                return View(p);
            }

            var user = UserService.TraerPerfilDelUsuario((int)Session["ID"]);
            var error = ProposalService.ValidateBeforeCreate(user);

            if (error != ErrorCodeAddProposalEnum.None)
            {
                ViewBag.Error = error;

                return View(p);
            }

            ProposalService.AgregarPropuestaInsumos(p, user);

            return View(p);
        }

        private bool isValidUserSession()
        {
            return Session["ID"] != null && UserService.TraerPerfilDelUsuario((int)Session["ID"]) != null;
        }

        [CheckSession]
        //Falta modificar para que rediriga al boton Megusta 
        //si no esta logeado al apretar el boton
        [HttpGet]
        public ActionResult Valoracion(int id, string valor)
        {
            ProposalService.Valorar(id, (int)Session["ID"], valor);
            return Redirect("/Home/Index");
        }

        [CheckSession]
        [HttpGet]
        public ActionResult Busqueda()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Buscar()
        {
            int id = int.Parse(Request["id"]);
            string busqueda = Request["Busqueda"];
            List<Propuestas> resultado= ProposalService.BusquedaPropuestasAjenasPorParametro(busqueda, id);
            return View(resultado);
        }
    }
}