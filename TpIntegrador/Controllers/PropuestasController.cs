using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AyudandoAlProjimo.Data;
using AyudandoAlProjimo.Data.ViewModels;
using AyudandoAlProjimo.Services;

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

        private bool isValidUserSession()
        {
            return Session["ID"] != null && UserService.TraerPerfilDelUsuario((int)Session["ID"]) != null;
        }
    }
}