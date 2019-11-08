using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AyudandoAlProjimo.Data.ViewModels;
using AyudandoAlProjimo.Services;

namespace TpIntegrador.Controllers
{
    public class UserController : Controller
    {
        private RegisterService rs = new RegisterService();

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
            var m = rs.ActivarUsuario(token);
            switch (m)
            {
                case 1:
                    ViewBag.Mensaje= "Se ha verificado exitosamente.";
                    break;
                case 2:
                    ViewBag.Mensaje = "Este mail ya ha sido verificado.";
                    break;
                case 3:
                    ViewBag.Mensaje = "Direccion invalida.";
                    break;
            }
            return View();
        }
    }
}
