using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AyudandoAlProjimo.Data;
using AyudandoAlProjimo.Data.ViewModels;
using AyudandoAlProjimo.Services;
using TpIntegrador.Filters;
using TpIntegrador.Utilities;

namespace TpIntegrador.Controllers
{
    public class PropuestasController : Controller
    {
        private ProposalService ProposalService = new ProposalService();
        private UserService UserService = new UserService();

        [CheckSession]
        public ActionResult AgregarPropuesta()
        {
            return View();
        }

        [CheckSession]
        public ActionResult AgregarPropuestaMonetaria()
        {
            AgregarPropuestaMonetariaViewModel p = new AgregarPropuestaMonetariaViewModel();

            p.TipoDonacion = TipoPropuestaEnum.Monetaria;

            return View(p);
        }

        [CheckSession]
        public ActionResult AgregarPropuestaInsumos()
        {
            AgregarPropuestaInsumosViewModel p = new AgregarPropuestaInsumosViewModel();

            p.TipoDonacion = TipoPropuestaEnum.Insumos;

            return View(p);
        }

        [CheckSession]
        public ActionResult AgregarPropuestaHoraTrabajo()
        {
            AgregarPropuestaHoraTrabajoViewModel p = new AgregarPropuestaHoraTrabajoViewModel();

            p.TipoDonacion = TipoPropuestaEnum.HorasTrabajo;

            return View(p);
        }

        [HttpPost]
        public ActionResult AgregarPropuestaMonetaria(AgregarPropuestaMonetariaViewModel p)
        {
            if (!ModelState.IsValid) return View(p);

            var user = UserService.TraerPerfilDelUsuario((int)Session["ID"]);
            var error = ProposalService.ValidateBeforeCreate(user);

            if (error != ErrorCodeAddProposalEnum.None)
            {
                ViewBag.Error = error;

                return View(p);
            }

            p.Foto = GetPathForPhoto(user.UserName + p.Nombre);

            ProposalService.AgregarPropuestaMonetaria(p, user);

            return Redirect("/Home/Index");
        }

        [HttpPost]
        public ActionResult AgregarPropuestaHoraTrabajo(AgregarPropuestaHoraTrabajoViewModel p)
        {
            if (!ModelState.IsValid) return View(p);

            var user = UserService.TraerPerfilDelUsuario((int)Session["ID"]);
            var error = ProposalService.ValidateBeforeCreate(user);

            if (error != ErrorCodeAddProposalEnum.None)
            {
                ViewBag.Error = error;

                return View(p);
            }

            p.Foto = GetPathForPhoto(user.UserName + p.Nombre);

            ProposalService.AgregarPropuestaHoraTrabajo(p, user);

            return Redirect("/Home/Index");
        }

        [HttpPost]
        public ActionResult AgregarPropuestaInsumos(AgregarPropuestaInsumosViewModel p)
        {
            if (!ModelState.IsValid) return View(p);

            var user = UserService.TraerPerfilDelUsuario((int)Session["ID"]);
            var error = ProposalService.ValidateBeforeCreate(user);

            if (error != ErrorCodeAddProposalEnum.None)
            {
                ViewBag.Error = error;

                return View(p);
            }

            p.Foto = GetPathForPhoto(user.UserName + p.Nombre);

            ProposalService.AgregarPropuestaInsumos(p, user);

            return Redirect("/Home/Index");
        }

        [HttpGet]
        public ActionResult VerDetalles(int id)
        {
            return View(ProposalService.VerPropuestaYDonaciones(id));
        }
        [HttpGet]
        public ActionResult ModificarPropuesta(int id)
        {
            return View(ProposalService.VerPropuestaYDonaciones(id));
        }
        [HttpPost]
        public ActionResult ModificarPropuesta(PropuestaViewModel pvm)
        {
            if (ModelState.IsValid)
            {
                pvm.Propuesta.Foto = GetPathForPhoto(pvm.Propuesta.Usuarios.UserName + pvm.Propuesta.Nombre);
                ProposalService.ModificarPropuestaBase(pvm);
                return Redirect("/Propuestas/VerDetalles/" + pvm.Propuesta.IdPropuesta);
            }
            return View(pvm);
        }
        private bool isValidUserSession()
        {
            return Session["ID"] != null && UserService.TraerPerfilDelUsuario((int)Session["ID"]) != null;
        }

        private string GetPathForPhoto(AgregarPropuestaBase p)
        {
            return ImagenesUtility.Guardar(Request.Files[0], p.Nombre + "-FOTO");
        }

        [CheckSession]
        [HttpGet]
        public ActionResult Valoracion(int id, string valor)
        {
            ProposalService.Valorar(id, (int)Session["ID"], valor);
            return Redirect("/Home/Index");
        }

        private string GetPathForPhoto(string name)
        {
            return ImagenesUtility.Guardar(Request.Files[0], name + "-FOTO");
        }
    }
}