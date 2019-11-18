using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AyudandoAlProjimo.Data.ViewModels;
using AyudandoAlProjimo.Data;
using TpIntegrador.Utilities;
using TpIntegrador.Filters;
using AyudandoAlProjimo.Services;

namespace TpIntegrador.Controllers
{
    public class UserController : Controller
    {
        private readonly RegisterService rs = new RegisterService();
        private readonly UserService us = new UserService();
        private readonly ProposalService ps = new ProposalService();
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
                    //creo un nombre significativo en este caso apellidonombre pero solo un caracter del nombre, ejemplo BatistutaG
                    string nombreSignificativo = pvm.Nombre + pvm.Apellido;
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
        [HttpGet]
        public ActionResult Denunciar(int id)
        {
            Boolean b = us.VerificarExistenciaDeDenunciaDelUsuario((int)Session["ID"],id);
            if (b)
            {
                TempData["Mensaje"] = "Ya ha emitido una denuncia para esta propuesta.";
                return Redirect("/Propuestas/VerDetalles/" + id);
            }
            else
            {
                DenunciaViewModel d = new DenunciaViewModel
                {
                    Id = id
                };
                return View(d);
            }
        }
        [HttpPost]
        public ActionResult Denunciar (DenunciaViewModel d)
        {
            if (ModelState.IsValid)
            {
                us.DenunciarPropuesta(d, (int)Session["ID"]);
                return Redirect("/Propuestas/VerDetalles/" + d.Id);
            }
            return View(d);
        }
    }
}
