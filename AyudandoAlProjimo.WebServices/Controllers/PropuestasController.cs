using AyudandoAlProjimo.Data;
using AyudandoAlProjimo.Services;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace AyudandoAlProjimo.WebServices.Controllers
{
    [EnableCors(origins: "http://localhost:44341", headers: "*", methods: "*")]
    public class PropuestasController : ApiController
    {
        private ProposalService ProposalService = new ProposalService();

        public List<PropuestaDTO> Get([FromUri] int userId, [FromUri] string query)
        {
            var Propuestas = ProposalService.BusquedaPropuestasAjenasPorParametro(query, userId, true);

            List<PropuestaDTO> dto = new List<PropuestaDTO>();

            foreach (var item in Propuestas)
            {
                PropuestaDTO p = new PropuestaDTO();

                p.ID = item.IdPropuesta.ToString();
                p.Foto = item.Foto;
                p.Nombre = item.Nombre;
                p.NombreUsuario = item.Usuarios.Nombre;
                p.ApellidoUsuario = item.Usuarios.Apellido;
                p.EmailUsuario = item.Usuarios.Email;
                p.FechaCreacion = item.FechaCreacion.ToString();
                p.Descripcion = item.Descripcion;
                p.Valoracion = item.Valoracion.ToString();
                p.Referencias = new List<ReferenciaDTO>();

                foreach (var referencia in item.PropuestasReferencias)
                {
                    ReferenciaDTO r = new ReferenciaDTO();
                    r.Nombre = referencia.Nombre;
                    r.Telefono = referencia.Telefono;

                    p.Referencias.Add(r);
                }

                dto.Add(p);
            }

            return dto;
        }
    }
}