using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyudandoAlProjimo.Data.ViewModels
{
    public class ReferenciasViewModel
    {
        [Required]
        public string Nombre { get; set; }
        
        [Required]
        public string Telefono { get; set; }
    }
}
