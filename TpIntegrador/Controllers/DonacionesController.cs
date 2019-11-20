using AyudandoAlProjimo.Data;
using AyudandoAlProjimo.Data.ViewModels;
using AyudandoAlProjimo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TpIntegrador.Filters;
using TpIntegrador.Utilities;

namespace TpIntegrador.Controllers
{
    public class DonacionesController : Controller
    {
        private ProposalService ProposalService = new ProposalService();

        [CheckSession]
        public ActionResult Index(int id)
        {
            var p = ProposalService.BuscarPorId(id);

            switch (p.TipoDonacion)
            {
                case (int)TipoPropuestaEnum.Monetaria:
                    return Redirect("/Donaciones/DonarMonetario/" + id);
                case (int)TipoPropuestaEnum.Insumos:
                    return Redirect("/Donaciones/DonarInsumos/" + id);
                case (int)TipoPropuestaEnum.HorasTrabajo:
                    return Redirect("/Donaciones/DonarHoras/" + id);
            }

            return View();
        }

        [CheckSession]
        public ActionResult DonarInsumos(int id)
        {
            RealizarDonacionInsumosViewModel m = new RealizarDonacionInsumosViewModel();
            m.Propuesta = ProposalService.BuscarPorId(id);
            m.Formulario = new RealizarDonacionInsumosFormulario();
            m.Formulario.Insumos = new List<InsumosViewModel>();
            foreach (var item in m.Propuesta.PropuestasDonacionesInsumos)
            {
                m.Formulario.Insumos.Add(new InsumosViewModel() { Id = item.IdPropuestaDonacionInsumo, Cantidad = 0, Nombre = item.Nombre });
            }

            return View(m);
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

        [HttpPost]
        public ActionResult DonarMonetario(RealizarDonacionMonetariaViewModel m)
        {
            int id = Int32.Parse(RouteData.Values["id"].ToString());
            m.Propuesta = ProposalService.BuscarPorId(id);

            if (!ModelState.IsValid) return View(m);

            var name = m.Propuesta.Nombre + "-" + Session["ID"];
            m.Formulario.ArchivoTransferencia = GetPathForPhoto(name);

            ProposalService.AgregarDonacionMonetaria(m.Formulario, (int)Session["ID"], id);

            return Redirect("/Home/Index");
        }

        [HttpPost]
        public ActionResult DonarHoras(RealizarDonacionHorasViewModel m)
        {
            int id = Int32.Parse(RouteData.Values["id"].ToString());
            m.Propuesta = ProposalService.BuscarPorId(id);

            if (!ModelState.IsValid) return View(m);

            ProposalService.AgregarDonacionHoras(m.Formulario, (int)Session["ID"], id);

            return Redirect("/Home/Index");
        }

        [HttpPost]
        public ActionResult DonarInsumos(RealizarDonacionInsumosViewModel m)
        {
            int id = Int32.Parse(RouteData.Values["id"].ToString());
            m.Propuesta = ProposalService.BuscarPorId(id);

            if (!ModelState.IsValid) return View(m);

            ProposalService.AgregarDonacionInsumos(m.Formulario, (int)Session["ID"], id);

            return Redirect("/Home/Index");
        }

        private string GetPathForPhoto(string name)
        {
            return ImagenesUtility.Guardar(Request.Files[0], name + "-FOTO");
        }
    }
}