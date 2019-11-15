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
        private UserService UserService = new UserService();

        public int AgregarPropuestaInsumos(AgregarPropuestaInsumosViewModel pm, Usuarios user)
        {
            //Tipo 1 para insumos
            pm.TipoDonacion = 1;

            Propuestas p = MapDTOToEntities(pm, user.IdUsuario);

            pm.Insumos.ForEach(x => {
                PropuestasDonacionesInsumos pdi = new PropuestasDonacionesInsumos();

                pdi.Cantidad = x.Cantidad;
                pdi.Nombre = x.Nombre;

                p.PropuestasDonacionesInsumos.Add(pdi);
            });


            return AgregarPropuesta(p);
        }

        public int AgregarPropuestaMonetaria(AgregarPropuestaMonetariaViewModel pm, Usuarios user)
        {
            //Tipo 3 para monetarias
            pm.TipoDonacion = 3;

            Propuestas p = MapDTOToEntities(pm, user.IdUsuario);

            PropuestasDonacionesMonetarias pdm = new PropuestasDonacionesMonetarias();

            pdm.CBU = pm.CBU;
            pdm.Dinero = pm.Dinero;

            p.PropuestasDonacionesMonetarias.Add(pdm);

            return AgregarPropuesta(p);
        }

        public int AgregarPropuestaHoraTrabajo(AgregarPropuestaHoraTrabajoViewModel pm, Usuarios user)
        {
            //Tipo 2 para horas de trabajo
            pm.TipoDonacion = 2;
            Propuestas p = MapDTOToEntities(pm, user.IdUsuario);

            PropuestasDonacionesHorasTrabajo pht = new PropuestasDonacionesHorasTrabajo();

            pht.CantidadHoras = pm.CantidadHoras;
            pht.Profesion = pm.Profesion;

            p.PropuestasDonacionesHorasTrabajo.Add(pht);

            return AgregarPropuesta(p);
        }

        public List<Propuestas> BusquedaMisPropuestas(int id)
        {
            return context.Propuestas.Where(p => p.IdUsuarioCreador == id).ToList();
        }
        public List<Propuestas> BusquedaMisPropuestasActivas(int id)
        {
            return context.Propuestas.Where(p => p.IdUsuarioCreador == id && p.Estado == 1).ToList();
        }
        public List<Propuestas> BusquedaPropuestasAjenas(int id)
        {
            List<Propuestas> lista = context.Propuestas.Where(p => p.IdUsuarioCreador != id)
                .OrderByDescending(c => c.FechaFin)
                .ThenByDescending(c => c.Valoracion)
                .ToList();
            return lista;
        }
        public List<Propuestas> BusquedaPropuestasAjenas()
        {
            List<Propuestas> lista = context.Propuestas
                .OrderByDescending(c => c.FechaFin)
                .ThenByDescending(c => c.Valoracion)
                .ToList();
            return lista;
        }
        public List<Propuestas> BusquedaPropuestasAjenasPorParametro(string busqueda, int id)
        {
            List<Usuarios> listUsernames = context.Usuarios.Where(u => u.UserName.Contains(busqueda)).ToList();
            List<int> listaIdUsernames = listUsernames.Select(c => c.IdUsuario).ToList();
            List<Propuestas> lista = context.Propuestas.Where(p => p.IdUsuarioCreador != id && 
                (p.Nombre.Contains(busqueda) || listaIdUsernames.Contains(p.IdUsuarioCreador)))
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
                            .Include("Usuarios")
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

        public ErrorCodeAddProposalEnum ValidateBeforeCreate(Usuarios u)
        {
            var hasValidProfile = UserService.isProfileValid(u);

            if (!hasValidProfile)
            {
                return ErrorCodeAddProposalEnum.InvalidProfile;
            }

            var hasLessThan3ProposalActives = BusquedaMisPropuestasActivas(u.IdUsuario).Count < 3;

            if(!hasLessThan3ProposalActives)
            {
                return ErrorCodeAddProposalEnum.InvalidProposalCount;
            }

            return ErrorCodeAddProposalEnum.None;
        }

        private int AgregarPropuesta(Propuestas p)
        {
            context.Propuestas.Add(p);

            return context.SaveChanges();
        }

        private Propuestas MapDTOToEntities(AgregarPropuestaBase pm, int idUsuario)
        {
            Propuestas p = new Propuestas();

            p.Nombre = pm.Nombre;
            p.Descripcion = pm.Descripcion;
            p.FechaCreacion = DateTime.Now;
            p.FechaFin = DateTime.Parse(pm.FechaFin);
            p.TipoDonacion = pm.TipoDonacion;
            p.TelefonoContacto = pm.TelefonoContacto;
            p.Foto = pm.Foto;
            p.IdUsuarioCreador = idUsuario;
            p.Valoracion = 0;

            return p;
        }

        public void Valorar(int id, int idUser, string valor)
        {
            var result =context.PropuestasValoraciones.Where(p => p.IdPropuesta == id)
                                .Where(p => p.IdUsuario == idUser).FirstOrDefault();

            if (result == null)
            {
                PropuestasValoraciones valoracion = new PropuestasValoraciones();
                valoracion.IdPropuesta = id;
                valoracion.IdUsuario = idUser;
                if (valor == "Like")
                {
                    valoracion.Valoracion = true;  
                }
                else if(valor == "Dislike")
                {
                    valoracion.Valoracion = false;
                }
                else
                {
                    return;
                }
                context.PropuestasValoraciones.Add(valoracion);
            }
            else
            {
                if(valor == "Like")
                {
                    result.Valoracion = true;
                }
                else if (valor == "Dislike")
                {
                    result.Valoracion = false;
                }
                else
                {
                    return;
                }
            }
            context.SaveChanges();
            CalcularValoracion(id);
        }

        private void CalcularValoracion(int id)
        {
            Propuestas propuesta = context.Propuestas.Where(p => p.IdPropuesta == id).FirstOrDefault();
            int a = context.PropuestasValoraciones
                        .Where(p1 => p1.Valoracion == true && p1.IdPropuesta == propuesta.IdPropuesta).Count();
            int b = context.PropuestasValoraciones.Where(p1 => p1.IdPropuesta == id).Count();
            
            propuesta.Valoracion = (a/b) * 100;
            context.SaveChanges();
        }

    }
}
