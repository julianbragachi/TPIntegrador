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
        public ActionResult Donar(int id)
        {
            var p = ProposalService.BuscarPorId(id);

            switch (p.TipoDonacion)
            {
                case (int)TipoPropuestaEnum.Monetaria:
                    return Redirect("/Propuestas/DonarMonetario/" + id);
                case (int)TipoPropuestaEnum.Insumos:
                    return Redirect("/Propuestas/DonarInsumos/" + id);
                case (int)TipoPropuestaEnum.HorasTrabajo:
                    return Redirect("/Propuestas/DonarHoras/" + id);
            }

            return View();

        }

        [CheckSession]
        public ActionResult DonarMonetario(int id)
        {
            RealizarDonacionMonetariaViewModel m = new RealizarDonacionMonetariaViewModel();
            m.Formulario = new RealizarDonacionMonetariaFormulario();
            m.Propuesta = ProposalService.BuscarPorId(id);

            return View(m);
        }

        [CheckSession]
        public ActionResult DonarHoras(int id)
        {
            RealizarDonacionHorasViewModel m = new RealizarDonacionHorasViewModel();
            m.Formulario = new RealizarDonacionHorasFormulario();
            m.Propuesta = ProposalService.BuscarPorId(id);

            return View(m);
        }

        [CheckSession]
        public ActionResult AgregarPropuesta()
        {
            var isLoggedIn = isValidUserSession();

            if (!isLoggedIn) return Redirect("/Ingresar/Login");

            return View();
        }

        [CheckSession]
        public ActionResult AgregarPropuestaMonetaria()
        {
            var isLoggedIn = isValidUserSession();

            if (!isLoggedIn) return Redirect("/Ingresar/Login");

            AgregarPropuestaMonetariaViewModel p = new AgregarPropuestaMonetariaViewModel();

            p.TipoDonacion = TipoPropuestaEnum.Monetaria;

            return View(p);
        }

        [CheckSession]
        public ActionResult AgregarPropuestaInsumos()
        {
            var isLoggedIn = isValidUserSession();

            if (!isLoggedIn) return Redirect("/Ingresar/Login");

            AgregarPropuestaInsumosViewModel p = new AgregarPropuestaInsumosViewModel();

            p.TipoDonacion = TipoPropuestaEnum.Insumos;

            return View(p);
        }

        [CheckSession]
        public ActionResult AgregarPropuestaHoraTrabajo()
        {
            var isLoggedIn = isValidUserSession();

            if (!isLoggedIn) return Redirect("/Ingresar/Login");

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

            p.Foto = GetPathForPhoto(p.Nombre);

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

            p.Foto = GetPathForPhoto(p.Foto);

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

            p.Foto = GetPathForPhoto(p.Foto);

            ProposalService.AgregarPropuestaInsumos(p, user);

            return Redirect("/Home/Index");
        }

        [HttpPost]
        public ActionResult DonarMonetario(RealizarDonacionMonetariaViewModel m)
        {
            int id = Int32.Parse(RouteData.Values["id"].ToString());

            if (!ModelState.IsValid)
            {
                m.Propuesta = ProposalService.BuscarPorId(id);
                return View(m);
            }

            var name = m.Propuesta.Nombre + "-" + Session["ID"];
            m.Formulario.ArchivoTransferencia = GetPathForPhoto(name);

            ProposalService.AgregarDonacionMonetaria(m.Formulario, (int)Session["ID"], id);

            return Redirect("/Home/Index");
        }

        [HttpPost]
        public ActionResult DonarHoras(RealizarDonacionHorasViewModel m)
        {
            int id = Int32.Parse(RouteData.Values["id"].ToString());

            if (!ModelState.IsValid)
            {
                m.Propuesta = ProposalService.BuscarPorId(id);
                return View(m);
            }

            ProposalService.AgregarDonacionHoras(m.Formulario, (int)Session["ID"], id);

            return Redirect("/Home/Index");
        }

        [HttpGet]
        public ActionResult VerDetalles(int id)
        {
            return View(ProposalService.VerPropuestaYDonaciones(id));
        }

        private bool isValidUserSession()
        {
            return Session["ID"] != null && UserService.TraerPerfilDelUsuario((int)Session["ID"]) != null;
        }

        private string GetPathForPhoto(string name)
        {
            return ImagenesUtility.Guardar(Request.Files[0], name + "-FOTO");
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

        //[CheckSession]
        //[HttpGet]
        //public ActionResult Busqueda()
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult Buscar()
        {
            int id = int.Parse(Request["id"]);
            string busqueda = Request["Busqueda"];
            List<Propuestas> resultado = ProposalService.BusquedaPropuestasAjenasPorParametro(busqueda, id);
            return View(resultado);
        }
    }
}