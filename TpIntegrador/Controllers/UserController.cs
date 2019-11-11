using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AyudandoAlProjimo.Data.ViewModels;
using AyudandoAlProjimo.Services;
using AyudandoAlProjimo.Data;
using TpIntegrador.Utilities;

namespace TpIntegrador.Controllers
{
    public class UserController : Controller
    {
        private readonly RegisterService rs = new RegisterService();
        private readonly UserService us = new UserService();

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
        [HttpGet]
        public ActionResult ModificarPerfil()
        {
            return View(us.TraerPerfilDelUsuario((int)Session["ID"]));
        }
        [HttpPost]
        public ActionResult ModificarPerfil(PerfilViewModel pvm)
        {
            Usuarios usuarioBD = us.TraerPerfilDelUsuario((int)Session["ID"]);
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                //TODO: Agregar validacion para confirmar que el archivo es una imagen
                if (!string.IsNullOrEmpty(pvm.Foto))
                {
                    //recordar eliminar la foto anterior si tenia
                    if (!string.IsNullOrEmpty(usuarioBD.Foto))
                    {
                        ImagenesUtility.Borrar(usuarioBD.Foto);
                    }

                    //creo un nombre significativo en este caso apellidonombre pero solo un caracter del nombre, ejemplo BatistutaG
                    string nombreSignificativo = pvm.Nombre+pvm.Apellido;
                    //Guardar Imagen
                    string pathRelativoImagen = ImagenesUtility.Guardar(Request.Files[0], nombreSignificativo);
                    usuarioBD.Foto = pathRelativoImagen;
                }
            }
            usuarioBD.Nombre = pvm.Nombre;
            usuarioBD.Apellido = pvm.Apellido;
            us.ActualizarPerfilDelUsuario(usuarioBD);
            return Redirect("/Home/Index");
        }
    }
}
