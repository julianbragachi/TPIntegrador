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
            //denuncias estado=1
            //estado = 2 desestimar
            //estado = 3 aceptada
            var result = ctx.Denuncias
                            .Include("MotivoDenuncia")
                            .Where(d => d.Estado == 1)
                            .OrderByDescending(d => d.FechaCreacion)
                            .ToList();
            return result;
        }

        public void DesestimarDenuncia(int id)
        {
            var denuncia = ctx.Denuncias.Find(id);
            denuncia.Estado = 2;
            // Propuesta activa 1 (es visible)
            // Propuesta inactiva= 0 (NO es visble)
            var propuesta = ctx.Propuestas.Find(denuncia.IdPropuesta);
            propuesta.Estado = 1;
            ctx.SaveChanges();
            VerificarLasCincoDenunciasDIferentes(id);
        }

        public void AceptarDenuncia(int id)
        {
            var denuncia = ctx.Denuncias.Find(id);
            denuncia.Estado = 3;
            var propuesta = ctx.Propuestas.Find(denuncia.IdPropuesta);
            propuesta.Estado = 0;
            ctx.SaveChanges();
        }
        public void VerificarLasCincoDenunciasDIferentes(int id)
        {
            int cantidad = ctx.Denuncias.Where(d => d.IdPropuesta == id && d.Estado == 1).Count();
            if (cantidad>=5)
            {
                Propuestas propuesta = ctx.Propuestas.Where(p => p.IdPropuesta == id).Single();
                propuesta.Estado = 0;
                ctx.SaveChanges();
            }
            else
            {
                return;
            }
        }
    }
}
