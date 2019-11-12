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
            var result = (from d in ctx.Denuncias
                          select d
               ).ToList();

            return result;
        }
    }
}
