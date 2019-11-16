﻿using AyudandoAlProjimo.Data.ViewModels;
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
                Usuarios user = loginService.VerifyUser(loginViewModel);

                if (user != null)
                {
                    Session["ID"] = user.IdUsuario;
                    Session["UserNombre"] = user.Email;
                    Session["User"] = user.TipoUsuario;
                    HttpCookie returnCookie = Request.Cookies["returnUrl"];
                    if ((returnCookie == null) || string.IsNullOrEmpty(returnCookie.Value))
                    {
                        return Redirect("/User/Home");
                    }
                    else
                    {
                        HttpCookie deleteCookie = new HttpCookie("returnUrl");
                        deleteCookie.Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies.Add(deleteCookie);
                        Response.Redirect(returnCookie.Value);
                    }
                }
                else
                {
                    TempData["message"] = "Wrong Email or Password";
                    return View(loginViewModel);
                }
            }
            return View(loginViewModel);
        }
		
        public ActionResult Logout()
        {
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Index", "Home");
        }

        //[HttpGet]
        //public ActionResult Logout()
        //{
        //    /*
        //       Session.Remove("IdRolPermiso");
        //       Session("IdRolPermiso") = null;
        //     */
        //    Session["ID"] = null;
        //    Session["User"] = null;

        //    return Redirect("/Home/Index");
        //}
    }
}