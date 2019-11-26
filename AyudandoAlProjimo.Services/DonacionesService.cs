using AyudandoAlProjimo.Data;
using AyudandoAlProjimo.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyudandoAlProjimo.Services
{
    public class DonacionesService
    {
        readonly Entities context = new Entities();

        public List<DonacionesViewModel> BuscarDonaciones(int idUser)
        {
            List<DonacionesViewModel> listaDonaciones = new List<DonacionesViewModel>();

            List<DonacionesInsumos> lista1 = context.DonacionesInsumos.Include("PropuestasDonacionesInsumos")
                .Where(d => d.IdUsuario == idUser).ToList();


            List<DonacionesMonetarias> lista2 = context.DonacionesMonetarias.Include("PropuestasDonacionesMonetarias")
                .Where(d => d.IdUsuario == idUser).ToList();

            List<DonacionesHorasTrabajo> lista3 = context.DonacionesHorasTrabajo.Include("PropuestasDonacionesHorasTrabajo")
                .Where(d => d.IdUsuario == idUser).ToList();


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
    }
}
