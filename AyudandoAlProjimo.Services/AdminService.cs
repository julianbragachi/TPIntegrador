using AyudandoAlProjimo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AyudandoAlProjimo.Services
{
    public class AdminService
    {
        Entities ctx = new Entities();

        public List<Denuncias> ListarDenuncias()
        {
            var result = ctx.Denuncias
                            .Include("MotivoDenuncia")
                            .Where(d => d.Estado == 1).ToList();

            return result;
        }
    }
}
