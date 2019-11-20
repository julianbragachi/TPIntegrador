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

        public bool isProfileValid(Usuarios u)
        {
            return !String.IsNullOrEmpty(u.Nombre) && !String.IsNullOrEmpty(u.Apellido) && !String.IsNullOrEmpty(u.Foto) && u.FechaNacimiento != null;
        }

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
            if (string.IsNullOrEmpty(UserModificado.UserName))
            {
                string username = user.Nombre + user.Apellido;
                int cantidad_usernames = context.Usuarios
                    .Where(t => t.UserName.StartsWith(username) &&
                    t.IdUsuario != UserModificado.IdUsuario)
                    .Count();
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
            }
            context.SaveChanges();
        }
        public Boolean VerificarExistenciaDeDenunciaDelUsuario(int usuario, int propuesta)
        {
            var denunciaExistente = context.Denuncias.Any(d => d.IdUsuario == usuario && d.IdPropuesta == propuesta);
            if (denunciaExistente)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void DenunciarPropuesta(DenunciaViewModel dvm, int idUser)
        {
            MotivoDenuncia motivo = context.MotivoDenuncia.Single(m => m.Descripcion == dvm.Motivo);
            Denuncias denuncia = new Denuncias
            {
                IdMotivo = motivo.IdMotivoDenuncia,
                IdUsuario = idUser,
                IdPropuesta = dvm.Id,
                Comentarios = dvm.Comentarios,
                FechaCreacion = DateTime.Now,
                Estado = 1
            };
            context.Denuncias.Add(denuncia);
            context.SaveChanges();
            AdminService AS = new AdminService();
            AS.VerificarLasCincoDenunciasDIferentes(dvm.Id);
		}
    }
}
