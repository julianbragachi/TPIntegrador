using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AyudandoAlProjimo.Data;
using AyudandoAlProjimo.Data.ViewModels;

namespace AyudandoAlProjimo.WebServices.Controllers
{
    public class ValuesController : ApiController
    {
        Entities context = new Entities();

        // GET api/values
        public IEnumerable<DonacionesViewModel> Get()
        {
            List<DonacionesViewModel> listaDonaciones = new List<DonacionesViewModel>();

            List<DonacionesInsumos> lista1 = context.DonacionesInsumos.ToList();


            List<DonacionesMonetarias> lista2 = context.DonacionesMonetarias.ToList();

            List<DonacionesHorasTrabajo> lista3 = context.DonacionesHorasTrabajo.ToList();


            foreach (var insumo in lista1)
            {
                DonacionesViewModel dvm = new DonacionesViewModel();
                dvm.donacionesInsumos = insumo;
                dvm.tipo = "insumo";
                dvm.total = context.DonacionesInsumos
                        .Where(p => p.IdPropuestaDonacionInsumo == insumo.IdPropuestaDonacionInsumo)
                        .Sum(p => p.Cantidad);
                listaDonaciones.Add(dvm);
            }

            foreach (var monetaria in lista2)
            {
                DonacionesViewModel dvm = new DonacionesViewModel();
                dvm.donacionesMonetarias = monetaria;
                dvm.tipo = "monetario";
                dvm.total = Decimal.ToInt32(context.DonacionesMonetarias
                    .Where(p => p.IdPropuestaDonacionMonetaria == monetaria.IdPropuestaDonacionMonetaria)
                    .Sum(p => p.Dinero));
                listaDonaciones.Add(dvm);
            }

            foreach (var horastrabajo in lista3)
            {
                DonacionesViewModel dvm = new DonacionesViewModel();
                dvm.donacionesHorasTrabajo = horastrabajo;
                dvm.tipo = "horastrabajo";
                dvm.total = context.DonacionesHorasTrabajo
                    .Where(p => p.IdPropuestaDonacionHorasTrabajo == horastrabajo.IdPropuestaDonacionHorasTrabajo)
                    .Sum(p => p.Cantidad);
                listaDonaciones.Add(dvm);
            }

            return listaDonaciones;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
