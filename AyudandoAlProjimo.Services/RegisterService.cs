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

        public void Registrar(RegistroViewModel model, string enlace)
        {
            try
            {
                string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                var user = new Usuarios
                {
                    Email = model.Email,
                    Activo = false,
                    Nombre = model.Nombre,
                    Apellido = model.Nombre,
                    Password = model.Password,
                    FechaNacimiento = model.FechaNacimiento,
                    FechaCracion = DateTime.Now,
                    Token = token,
                    TipoUsuario = 1
                };
                string username = model.Nombre + model.Apellido;
                int cantidad_usernames = context.Usuarios.Where(t => t.UserName.StartsWith(username)).Count();
                if (cantidad_usernames > 1)
                {
                    user.UserName = username + (cantidad_usernames+1).ToString();
                }
                else if (cantidad_usernames == 1)
                {
                    user.UserName = username + 1;
                }
                else
                {
                    user.UserName = username;
                }
                context.Usuarios.Add(user);
                context.SaveChanges(); //traer el cuerpo que deberia linkear el mail desde el controlador, a menos que se le tire localhostyadayada.
                System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(
                new System.Net.Mail.MailAddress("EmailAutomaticoTPW3@gmail.com", "Web Registration"),
                new System.Net.Mail.MailAddress(user.Email));
                m.Subject = "Verificación de correo electrónico";
                m.Body = string.Format("Estimado <BR/>Gracias por registrarse en NombreNombre, por favor haga click en el enlace:" +
                enlace.ToString() + "?token=" + token.ToString());
                m.IsBodyHtml = true;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");
                smtp.Credentials = new System.Net.NetworkCredential("EmailAutomaticoTPW3@gmail.com", "TestUnitario8");
                smtp.EnableSsl = true;
                smtp.Send(m);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int ActivarUsuario(string token)
        {
            Usuarios u = context.Usuarios.Where(t => t.Token == token).Single();
            if (u != null)
            {
                if (u.Activo == false)
                {
                    u.Activo = true;
                    context.SaveChanges();
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            else
            {
                return 3;
            }
            
        }
    }
}
