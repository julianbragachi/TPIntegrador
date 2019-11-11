using AyudandoAlProjimo.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AyudandoAlProjimo.Services;
using AyudandoAlProjimo.Data;

namespace TpIntegrador.Controllers
{
    public class IngresarController : Controller
    {

        LoginService loginService = new LoginService();

        [HttpGet]
        public ActionResult Login()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            return View(loginViewModel);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                Usuarios user= loginService.VerifyUser(loginViewModel);
             
                if (user != null)
                {
                    Session["ID"] = user.IdUsuario;
                    Session["User"] = user.TipoUsuario;
                    return Redirect("/Home/Index");
                }
                else
                {
                    TempData["message"] = "Wrong Email or Password";
                    return View(loginViewModel);
                }
            }
            return View(loginViewModel);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            /*
               Session.Remove("IdRolPermiso");
               Session("IdRolPermiso") = null;
             */
            Session["ID"] = null;
            Session["User"] = null;

            return Redirect("/Home/Index");
        }

        
    }
}