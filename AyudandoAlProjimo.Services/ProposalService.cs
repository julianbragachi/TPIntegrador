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
            pm.TipoDonacion = TipoPropuestaEnum.Insumos;

            Propuestas p = MapDTOToEntities(pm, user.IdUsuario);

            pm.Insumos.ForEach(x =>
            {
                PropuestasDonacionesInsumos pdi = new PropuestasDonacionesInsumos();

                pdi.Cantidad = x.Cantidad;
                pdi.Nombre = x.Nombre;

                p.PropuestasDonacionesInsumos.Add(pdi);
            });


            return AgregarPropuesta(p);
        }

        public int AgregarPropuestaMonetaria(AgregarPropuestaMonetariaViewModel pm, Usuarios user)
        {
            pm.TipoDonacion = TipoPropuestaEnum.Monetaria;

            Propuestas p = MapDTOToEntities(pm, user.IdUsuario);

            PropuestasDonacionesMonetarias pdm = new PropuestasDonacionesMonetarias();

            pdm.CBU = pm.CBU;
            pdm.Dinero = pm.Dinero;

            p.PropuestasDonacionesMonetarias.Add(pdm);

            return AgregarPropuesta(p);
        }

        public int AgregarPropuestaHoraTrabajo(AgregarPropuestaHoraTrabajoViewModel pm, Usuarios user)
        {
            pm.TipoDonacion = TipoPropuestaEnum.HorasTrabajo;

            Propuestas p = MapDTOToEntities(pm, user.IdUsuario);

            PropuestasDonacionesHorasTrabajo pht = new PropuestasDonacionesHorasTrabajo();

            pht.CantidadHoras = pm.CantidadHoras;
            pht.Profesion = pm.Profesion;

            p.PropuestasDonacionesHorasTrabajo.Add(pht);

            return AgregarPropuesta(p);
        }

        public int AgregarDonacionMonetaria(RealizarDonacionMonetariaFormulario f, int userId, int idPropuesta)
        {
            Propuestas p = BuscarPorId(idPropuesta);
            var idPropuestaDonacionMonetaria = p.PropuestasDonacionesMonetarias.FirstOrDefault().IdPropuestaDonacionMonetaria;
            DonacionesMonetarias d = new DonacionesMonetarias();

            d.FechaCreacion = DateTime.Now;
            d.ArchivoTransferencia = f.ArchivoTransferencia;
            d.Dinero = f.Dinero;
            d.IdUsuario = userId;
            d.IdPropuestaDonacionMonetaria = idPropuestaDonacionMonetaria;

            context.Propuestas.Where(x => x.IdPropuesta == idPropuesta).Single().PropuestasDonacionesMonetarias.Single().DonacionesMonetarias.Add(d);

            return context.SaveChanges();
        }

        public int AgregarDonacionHoras(RealizarDonacionHorasFormulario f, int userId, int idPropuesta)
        {
            Propuestas p = BuscarPorId(idPropuesta);
            var idPropuestaDonacionHorasTrabajo = p.PropuestasDonacionesHorasTrabajo.FirstOrDefault().IdPropuestaDonacionHorasTrabajo;
            DonacionesHorasTrabajo d = new DonacionesHorasTrabajo();

            d.Cantidad = f.Cantidad;
            d.IdUsuario = userId;
            d.IdPropuestaDonacionHorasTrabajo = idPropuestaDonacionHorasTrabajo;

            context.Propuestas.Where(x => x.IdPropuesta == idPropuesta).Single().PropuestasDonacionesHorasTrabajo.Single().DonacionesHorasTrabajo.Add(d);

            return context.SaveChanges();
        }

        public int AgregarDonacionInsumos(RealizarDonacionInsumosFormulario f, int userId, int idPropuesta)
        {
            Propuestas p = BuscarPorId(idPropuesta);

            foreach (var item in f.Insumos)
            {
                DonacionesInsumos d = new DonacionesInsumos();
                d.IdPropuestaDonacionInsumo = item.Id;
                d.Cantidad = item.Cantidad;
                d.IdUsuario = userId;

                context.Propuestas
                    .Where(x => x.IdPropuesta == idPropuesta)
                    .Single().PropuestasDonacionesInsumos
                    .Where(x => x.IdPropuestaDonacionInsumo == item.Id)
                    .Single().DonacionesInsumos.Add(d);
            }

            return context.SaveChanges();
        }

        public Propuestas BuscarPorId(int id)
        {
            return context.Propuestas.Where(p => p.IdPropuesta == id).FirstOrDefault();
        }

        public List<Propuestas> ObtenerCincoPropuestasMasValoradas()
        {
            return context.Propuestas.Where(x => x.Estado == 1).OrderByDescending(x => x.Valoracion).Take(5).ToList();
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
            List<Usuarios> listUsernames = context.Usuarios.Where(u => u.UserName.Contains(busqueda) || u.Nombre.Contains(busqueda) || u.Apellido.Contains(busqueda)).ToList();
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
                Usuarios usuarioCreador = UserService.TraerPerfilDelUsuario(propuesta.IdUsuarioCreador);
                PropuestaViewModel pvm = new PropuestaViewModel
                {
                    Propuesta = propuesta,
                    UsuarioCreador = usuarioCreador,
                };

                var porcentajeRealizacion = 0;

                switch (propuesta.TipoDonacion)
                {
                    case (int)TipoPropuestaEnum.HorasTrabajo:
                        pvm.DonacionesHorasTrabajo = propuesta.PropuestasDonacionesHorasTrabajo.FirstOrDefault().DonacionesHorasTrabajo.ToList();

                        porcentajeRealizacion = (int)(pvm.DonacionesHorasTrabajo.Sum(x => x.Cantidad) * 100) / propuesta.PropuestasDonacionesHorasTrabajo.FirstOrDefault().CantidadHoras;
                        pvm.PorcentajeRealizacion = porcentajeRealizacion > 100 ? 100 : (int)porcentajeRealizacion;

                        return pvm;
                    case (int)TipoPropuestaEnum.Insumos:
                        List<int> porcentajes = new List<int>();

                        foreach (var p in propuesta.PropuestasDonacionesInsumos)
                        {
                            var realizacion = (p.DonacionesInsumos.Sum(x => x.Cantidad) * 100) / p.Cantidad;
                            porcentajes.Add(realizacion > 100 ? 100 : realizacion);
                        }

                        porcentajeRealizacion = porcentajes.Sum() / porcentajes.Count();
                        pvm.PorcentajeRealizacion = porcentajeRealizacion > 100 ? 100 : (int)porcentajeRealizacion;

                        return pvm;
                    case (int)TipoPropuestaEnum.Monetaria:
                        pvm.DonacionesMonetarias = propuesta.PropuestasDonacionesMonetarias.FirstOrDefault().DonacionesMonetarias.ToList();
                        porcentajeRealizacion = (int)((pvm.DonacionesMonetarias.Sum(x => x.Dinero) * 100) / propuesta.PropuestasDonacionesMonetarias.FirstOrDefault().Dinero);
                        pvm.PorcentajeRealizacion = porcentajeRealizacion > 100 ? 100 : (int)porcentajeRealizacion;

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

            if (!hasLessThan3ProposalActives)
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
            p.TipoDonacion = (int)pm.TipoDonacion;
            p.TelefonoContacto = pm.TelefonoContacto;
            p.Foto = pm.Foto;
            p.IdUsuarioCreador = idUsuario;
            p.Valoracion = 0;
            p.Estado = 1;

            foreach (var r in pm.Referencias)
            {
                PropuestasReferencias referencia = new PropuestasReferencias();
                referencia.Nombre = r.Nombre;
                referencia.Telefono = r.Telefono;

                p.PropuestasReferencias.Add(referencia);
            }

            return p;
        }
        public void ModificarPropuestaBase(PropuestaViewModel pvm)
        {
            Propuestas p = context.Propuestas.Find(pvm.Propuesta.IdPropuesta);
            p.Nombre = pvm.Propuesta.Nombre;
            p.Descripcion = pvm.Propuesta.Descripcion;
            p.FechaFin = pvm.Propuesta.FechaFin;
            p.TelefonoContacto = pvm.Propuesta.TelefonoContacto;
            p.Foto = pvm.Propuesta.Foto;
            context.SaveChanges();
            switch (pvm.Propuesta.TipoDonacion)
            {
                case (int)TipoPropuestaEnum.HorasTrabajo:
                    ModificarPropuestaHorasTrabajo(p.PropuestasDonacionesHorasTrabajo.FirstOrDefault(), pvm);
                    break;
                //case (int)TipoPropuestaEnum.Insumos:
                //    break;
                case (int)TipoPropuestaEnum.Monetaria:
                    ModificarPropuestaMonetaria(p.PropuestasDonacionesMonetarias.FirstOrDefault(), pvm);
                    break;
            }
        }
        private void ModificarPropuestaHorasTrabajo(PropuestasDonacionesHorasTrabajo propuesta, PropuestaViewModel pvm)
        {
            PropuestasDonacionesHorasTrabajo propuestaModificada = context.PropuestasDonacionesHorasTrabajo
                .Find(propuesta.IdPropuestaDonacionHorasTrabajo);
            if (pvm.PropuestaDonacionesHorasTrabajo.CantidadHoras == 0)
            {
                propuestaModificada.CantidadHoras = propuesta.CantidadHoras;
            }
            else
            {
                propuestaModificada.CantidadHoras = pvm.PropuestaDonacionesHorasTrabajo.CantidadHoras;
            }
            propuestaModificada.Profesion = pvm.PropuestaDonacionesHorasTrabajo.Profesion;
            context.SaveChanges();
        }
        private void ModificarPropuestaMonetaria(PropuestasDonacionesMonetarias propuesta, PropuestaViewModel pvm)
        {
            PropuestasDonacionesMonetarias propuestaModificada = context.PropuestasDonacionesMonetarias
                .Find(propuesta.IdPropuestaDonacionMonetaria);
            propuestaModificada.Dinero = pvm.PropuestasDonacionesMonetarias.Dinero;
            context.SaveChanges();
        }
        //private void ModificarPropuestaInsumos(PropuestasDonacionesInsumos propuesta)
        //{
        //    PropuestasDonacionesInsumos propuestaModificada = context.PropuestasDonacionesHorasTrabajo
        //        .Find(propuesta.IdPropuestaDonacionHorasTrabajo);
        //    propuestaModificada.Profesion = propuesta.Profesion;
        //    propuestaModificada.CantidadHoras = propuesta.CantidadHoras;
        //    context.SaveChanges();
        //}
        public void Valorar(int id, int idUser, string valor)
        {
            var result = context.PropuestasValoraciones.Where(p => p.IdPropuesta == id)
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
                else if (valor == "Dislike")
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
                if (valor == "Like")
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
            var a = context.PropuestasValoraciones
                        .Where(p1 => p1.Valoracion == true && p1.IdPropuesta == propuesta.IdPropuesta).Count();
            var b = context.PropuestasValoraciones.Where(p1 => p1.IdPropuesta == id).Count();
            
            propuesta.Valoracion = Math.Round(((decimal)a / (decimal)b) * 100);
            context.SaveChanges();
        }
        public void VerificarPropuestasPorTerminar()
        {
            List<Propuestas> lista = context.Propuestas.Where(p => p.Estado == 1).ToList();
            foreach(var l in lista)
            {
                if (l.FechaFin <= DateTime.Now)
                {
                    l.Estado = 0;
                }
            }
            context.SaveChanges();
        }
    }
}
