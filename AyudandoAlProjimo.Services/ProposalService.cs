using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AyudandoAlProjimo.Data;
using AyudandoAlProjimo.Data.ViewModels;

namespace AyudandoAlProjimo.Services
{
    public class ProposalService
    {
        readonly Entities context = new Entities();

        public List<Propuestas> BusquedaMisPropuestas()
        {
            return context.Propuestas.Where(p => p.IdUsuarioCreador == 1).ToList();
        }
        public List<Propuestas> BusquedaMisPropuestasActivas()
        {
            return context.Propuestas.Where(p => p.IdUsuarioCreador == 1 && p.Estado == 1).ToList();
        }
        public List<Propuestas> BusquedaPropuestasAjenas()
        {
            List<Propuestas> lista = context.Propuestas.Where(p => p.IdUsuarioCreador != 1)
                .OrderByDescending(c => c.FechaFin)
                .ThenByDescending(c => c.Valoracion)
                .ToList();
            return lista;
        }
        public List<Propuestas> BusquedaPropuestasAjenasPorParametro(string texto)
        {
            List<Usuarios> listUsernames = context.Usuarios.Where(u => u.UserName.Contains(texto)).ToList();
            List<int> listaIdUsernames = listUsernames.Select(c => c.IdUsuario).ToList();
            List<Propuestas> lista = context.Propuestas.Where(p => p.IdUsuarioCreador != 1 && 
                (p.Nombre.Contains(texto) || listaIdUsernames.Contains(p.IdUsuarioCreador)))
                .OrderByDescending(c => c.FechaFin)
                .ThenByDescending(c => c.Valoracion)
                .ToList();
            return lista;
        }
        public PropuestaViewModel VerPropuestaYDonaciones(int id)
        {
            try
            {
                Propuestas propuesta = context.Propuestas.Where(p => p.IdPropuesta == id).Single();
                PropuestaViewModel pvm = new PropuestaViewModel
                {
                    Propuesta = propuesta,
                };
                switch (propuesta.TipoDonacion)
                {
                    case 1:
                        PropuestasDonacionesHorasTrabajo propuestadht = context.PropuestasDonacionesHorasTrabajo.
                            Where(pdht => pdht.IdPropuesta == propuesta.IdPropuesta).Single();
                        pvm.DonacionesHorasTrabajo = context.DonacionesHorasTrabajo
                            .Where(p => p.IdPropuestaDonacionHorasTrabajo == propuestadht.IdPropuesta).ToList();
                        return pvm;
                    case 2:
                        PropuestasDonacionesInsumos propuestai = context.PropuestasDonacionesInsumos.
                            Where(pdi => pdi.IdPropuesta == propuesta.IdPropuesta).Single();
                        pvm.DonacionesInsumos = context.DonacionesInsumos
                            .Where(p => p.IdPropuestaDonacionInsumo == propuestai.IdPropuesta).ToList();
                        return pvm;
                    case 3:
                        PropuestasDonacionesMonetarias propuestam = context.PropuestasDonacionesMonetarias.
                            Where(pdm => pdm.IdPropuesta == propuesta.IdPropuesta).Single();
                        pvm.DonacionesMonetarias = context.DonacionesMonetarias
                            .Where(p => p.IdPropuestaDonacionMonetaria == propuestam.IdPropuesta).ToList();
                        return pvm;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
