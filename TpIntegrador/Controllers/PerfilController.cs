using System.Web.Mvc;
using AyudandoAlProjimo.Data.ViewModels;
using AyudandoAlProjimo.Services;
using AyudandoAlProjimo.Data;
using TpIntegrador.Utilities;
using TpIntegrador.Filters;

namespace TpIntegrador.Controllers
{
    public class PerfilController : Controller
    {
        private readonly UserService us = new UserService();

        [CheckUser]
        [HttpGet]
        public ActionResult Index()
        {
            var user = us.TraerPerfilDelUsuario((int)Session["ID"]);

            var ViewModel = new PerfilViewModel();

            ViewModel.Nombre = user.Nombre;
            ViewModel.Apellido = user.Apellido;
            ViewModel.FechaNacimiento = user.FechaNacimiento;
            ViewModel.Foto = user.Foto;
            ViewModel.Email = user.Email;
            ViewModel.UserName = user.UserName;

            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult Index(PerfilViewModel pvm)
        {
            if (!ModelState.IsValid) return View(pvm);

            Usuarios usuarioBD = us.TraerPerfilDelUsuario((int)Session["ID"]);
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                //TODO: Agregar validacion para confirmar que el archivo es una imagen
                if (!string.IsNullOrEmpty(pvm.Foto))
                {
                    //recordar eliminar la foto anterior si tenia
                    if (!string.IsNullOrEmpty(usuarioBD.Foto))
                    {
                        ImagenesUtility.Borrar(usuarioBD.Foto);
                    }

                    //creo un nombre significativo en este caso apellidonombre pero solo un caracter del nombre, ejemplo BatistutaG
                    string nombreSignificativo = pvm.Nombre + pvm.Apellido;
                    //Guardar Imagen
                    string pathRelativoImagen = ImagenesUtility.Guardar(Request.Files[0], nombreSignificativo);
                    usuarioBD.Foto = pathRelativoImagen;
                }
            }

            usuarioBD.Nombre = pvm.Nombre;
            usuarioBD.Apellido = pvm.Apellido;
            usuarioBD.FechaNacimiento = pvm.FechaNacimiento;
            us.ActualizarPerfilDelUsuario(usuarioBD);

            return Redirect("/Home/Index");
        }
    }
}