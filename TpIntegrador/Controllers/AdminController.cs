using AyudandoAlProjimo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TpIntegrador.Filters;
using AyudandoAlProjimo.Data;

namespace TpIntegrador.Controllers
{
    [CheckAdmin]
    public class AdminController : Controller
    {
        Entities ctx = new Entities();

        AdminService adminService = new AdminService();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Complaints()
        {
            //return View(adminService.ListarDenuncias());
            List<Denuncias> denuncias = ctx.Denuncias.Where(o => o.Estado == 1).ToList();
            return View(denuncias);
        }
    }
}