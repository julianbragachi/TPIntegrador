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
        readonly Entities context = new Entities();

        public void Registrar(RegistroViewModel model, string enlace)
        {
            try
            {
                string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                var user = new Usuarios
                {
                    Email = model.Email,
                    Activo = false,
                    Password = model.Password,
                    FechaNacimiento = model.FechaNacimiento,
                    FechaCracion = DateTime.Now,
                    Token = token,
                    TipoUsuario = 2
                };
                //string username = model.Nombre + model.Apellido;
                //int cantidad_usernames = context.Usuarios.Where(t => t.UserName.StartsWith(username)).Count();
                //if (cantidad_usernames > 1)
                //{
                //    user.UserName = username + (cantidad_usernames+1).ToString();
                //}
                //else if (cantidad_usernames == 1)
                //{
                //    user.UserName = username + 1;
                //}
                //else
                //{
                //    user.UserName = username;
                //}
                //Como es necesario que se le agregue un username, se le pondrá uno por defecto.
                user.UserName = "User" + context.Usuarios.Count()+1.ToString();
                context.Usuarios.Add(user);
                context.SaveChanges(); //traer el cuerpo que deberia linkear el mail desde el controlador, a menos que se le tire localhostyadayada.
                System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(
                new System.Net.Mail.MailAddress("EmailAutomaticoTPW3@gmail.com", "Web Registration"),
                new System.Net.Mail.MailAddress(user.Email))
                {
                    Subject = "Verificación de correo electrónico",
                    Body = string.Format("Estimado <BR/>Gracias por registrarse en NombreNombre, por favor haga click en el enlace:" +
                enlace.ToString() + "?token=" + token.ToString()),
                    IsBodyHtml = true
                };
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com")
                {
                    Credentials = new System.Net.NetworkCredential("EmailAutomaticoTPW3@gmail.com", "TestUnitario8"),
                    EnableSsl = true
                };
                smtp.Send(m);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string ActivarUsuario(string token)
        {
            Usuarios u = context.Usuarios.Where(t => t.Token == token).Single();
            if (u != null)
            {
                if (u.Activo == false)
                {
                    u.Activo = true;
                    context.SaveChanges();
                    return "Se ha verificado exitosamente.";
                }
                else
                {
                    return "Este mail ya ha sido verificado.";
                }
            }
            else
            {
                return "Token invalido.";
            }
            
        }
    }
}
