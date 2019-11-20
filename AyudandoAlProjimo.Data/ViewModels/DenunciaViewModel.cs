using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyudandoAlProjimo.Data.ViewModels
{
    public class DenunciaViewModel
    {
        [StringLength(300)]
        public string Comentarios { get; set; }
        [Required]
        public string Motivo { get; set; }
        [Required]
        public int Id { get; set; }
    }
}
