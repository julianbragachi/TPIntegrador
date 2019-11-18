using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AyudandoAlProjimo.Data.ViewModels;
using AyudandoAlProjimo.Services;
using AyudandoAlProjimo.Data;
using TpIntegrador.Utilities;
using TpIntegrador.Filters;

namespace TpIntegrador.Controllers
{
    public class UserController : Controller
    {
        private readonly RegisterService rs = new RegisterService();
        private ProposalService ProposalService = new ProposalService();
        private UserService userService = new UserService();

        [HttpGet]
        public ActionResult Register()
        {
            RegistroViewModel rvm = new RegistroViewModel();
            return View(rvm);
        }

        [HttpPost]
        public ActionResult Register(RegistroViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                string link = Url.Action("ConfirmEmail", "User", null, Request.Url.Scheme);
                rs.Registrar(rvm, link);

                return Redirect("/Home/Index");
            }

            return View(rvm);
        }

        [HttpGet]
        public ActionResult ConfirmEmail(string token)
        {
            ViewBag.Mensaje = rs.ActivarUsuario(token);
            return View();
        }

        [CheckSession]
        [HttpGet]
        public ActionResult Home()
        {
            int idUsar= Convert.ToInt32(Session["ID"]);
            return View(ProposalService.BusquedaMisPropuestasActivas(idUsar));
        }

        [CheckSession]
        public ActionResult Donaciones()
        {
            int idUser = Convert.ToInt32(Session["ID"]); 
            return View(userService.BuscarDonaciones(idUser));
        }

    }
}
