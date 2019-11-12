using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AyudandoAlProjimo.Data;
using AyudandoAlProjimo.Data.ViewModels;

namespace AyudandoAlProjimo.Services
{
    public class LoginService
    {
        Entities ctx = new Entities();

        public Usuarios VerifyUser(LoginViewModel loginViewModel)
        {
            var user = (from u in ctx.Usuarios
                        where u.Email == loginViewModel.Email && u.Password == loginViewModel.Password
                        select u)
                        .FirstOrDefault();

            if(user.Activo == true)
            {
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
