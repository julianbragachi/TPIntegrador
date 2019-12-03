using AyudandoAlProjimo.Data.ViewModels;
using AyudandoAlProjimo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AyudandoAlProjimo.WebServices.Controllers
{
    [EnableCors(origins: "http://localhost:44341", headers: "*", methods: "*")]
    public class DonacionesController : ApiController
    {
        private DonacionesService DonacionesService = new DonacionesService();
        public IEnumerable<DonacionesViewModel> Get([FromUri] int userId)
        {
            return DonacionesService.BuscarDonaciones(userId);
        }
    }
}
