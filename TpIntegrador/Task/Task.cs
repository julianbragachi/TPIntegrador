using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AyudandoAlProjimo.Services;

namespace TpIntegrador.Task
{
    public class Task : IJob
    {
        public async System.Threading.Tasks.Task Execute(IJobExecutionContext context)
        {
            ProposalService ps = new ProposalService();
            ps.VerificarPropuestasPorTerminar();
            await Console.Error.WriteLineAsync("Error de la Tarea");
        }
    }
}