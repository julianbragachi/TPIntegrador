//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class Denuncias
    {
        public int IdDenuncia { get; set; }
        public int IdPropuesta { get; set; }
        public int IdMotivo { get; set; }
        public string Comentarios { get; set; }
        public int IdUsuario { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public int Estado { get; set; }
    
        public virtual MotivoDenuncia MotivoDenuncia { get; set; }
        public virtual Propuestas Propuestas { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}
