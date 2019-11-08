using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AyudandoAlProjimo.Data;
using AyudandoAlProjimo.Data.ViewModels;
namespace AyudandoAlProjimo.Services
{
    public class RegisterService
    {
        Entities context = new Entities();

        public void Registrar(RegistroViewModel model)
        {
            try
            {
                var user = new Usuarios
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Activo = false,
                    Nombre = model.Nombre,
                    Apellido = model.Nombre,
                    FechaNacimiento = model.FechaNacimiento,
                    FechaCracion = DateTime.Now,
                    Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                };
                context.Usuarios.Add(user);
                if (context.SaveChanges() > 0)
                    {   //traer el cuerpo que deberia linkear el mail desde el controlador, a menos que se le tire localhostyadayada.
                        System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(
                        new System.Net.Mail.MailAddress("sender@mydomain.com", "Web Registration"),
                        new System.Net.Mail.MailAddress(user.Email));
                        m.Subject = "Verificación de correo electrónico";
                        m.Body = string.Format("Estimado {0}<BR/>Gracias por registrarse en NombreNombre, por favor haga click en el enlace:" +
                        " <a href=\"{1}\" title=\"User Email Confirm\">{1}</a>", user.UserName, Url.Action("ConfirmEmail", "Account",
                        new { Token = user.Id, Email = user.Email }, Request.Url.Scheme));
                        m.IsBodyHtml = true;
                        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.mydomain.com");
                        smtp.Credentials = new System.Net.NetworkCredential("sender@mydomain.com", "password");
                        smtp.EnableSsl = true;
                        smtp.Send(m);
                    }
            }
            catch (Exception)
            {
                throw;
            }
        }

            // If we got this far, something failed, redisplay formreturn View(model); 
        }
    }
}
