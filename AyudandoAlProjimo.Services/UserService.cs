using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AyudandoAlProjimo.Data;
using AyudandoAlProjimo.Data.ViewModels;

namespace AyudandoAlProjimo.Services
{
    public class UserService
    {
        readonly Entities context = new Entities();

        public Usuarios TraerPerfilDelUsuario(int id)
        {
            return context.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
        }
        public void ActualizarPerfilDelUsuario(Usuarios user)
        {
            Usuarios UserModificado = context.Usuarios.FirstOrDefault(u => u.IdUsuario == user.IdUsuario);
            UserModificado.Nombre = user.Nombre;
            UserModificado.Apellido = user.Apellido;
            UserModificado.Foto = user.Foto;
            string username = user.Nombre + user.Apellido;
            int cantidad_usernames = context.Usuarios.Where(t => t.UserName.StartsWith(username)).Count();
            if (cantidad_usernames > 1)
            {
                UserModificado.UserName = username + (cantidad_usernames + 1).ToString();
            }
            else if (cantidad_usernames == 1)
            {
                UserModificado.UserName = username + 1;
            }
            else
            {
                UserModificado.UserName = username;
            }
            context.SaveChanges();
        }
    }
}
