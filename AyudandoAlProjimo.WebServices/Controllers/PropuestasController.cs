using AyudandoAlProjimo.Data;
using AyudandoAlProjimo.Services;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace AyudandoAlProjimo.WebServices.Controllers
{
    [EnableCors(origins: "http://localhost:44341", headers: "*", methods: "*")]
    public class PropuestasController : ApiController
    {
        private ProposalService ProposalService = new ProposalService();

        public List<Propuestas> Get([FromUri] int userId, [FromUri] string query)
        {
            var Propuestas = ProposalService.BusquedaPropuestasAjenasPorParametro(query, userId);

            return Propuestas;
        }
    }
}