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
            //var result = (from d in ctx.Denuncias
            //                 join art in db.Articulos on kar.ArticuloID equals art.ArticuloID
            //                 join bod in db.Bodegas on art.BodegaID equals bod.BodegaID

            //                 where bod.BodegaID == busqueda
            //                 select kar
            //   );

        }
    }
}
