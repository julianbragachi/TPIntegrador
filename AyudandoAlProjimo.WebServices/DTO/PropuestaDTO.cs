using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AyudandoAlProjimo.WebServices
{
    public class PropuestaDTO
    {
        public string ID { get; set; }
        public string Foto { get; set; }
        public string Nombre { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public string FechaCreacion { get; set; }
        public string Descripcion { get; set; }
        public string Valoracion { get; set; }
        public List<ReferenciaDTO> Referencias { get; set; }
    }

    public class ReferenciaDTO
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }

    }
}