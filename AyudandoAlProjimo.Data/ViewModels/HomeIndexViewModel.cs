using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyudandoAlProjimo.Data.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<Propuestas> MasValoradas { get; set; }
        
        public List<Propuestas> MisPropuestas { get; set; }

        public bool IsLoggedIn { get; set; }

        public bool OnlyActiveProposals { get; set; }

        public HomeIndexViewModel()
        {
            MasValoradas = new List<Propuestas>();
            MisPropuestas = new List<Propuestas>();
        }
    }
}
