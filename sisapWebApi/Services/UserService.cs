using System.Collections.Generic;
using System.Threading.Tasks;
using sisapWebApi.Context;
using sisapWebApi.Repository;
using sisapWebApi.Models.Firebird;
using Microsoft.AspNetCore.Mvc;

namespace sisapWebApi.Services
{
    public class UserService
    {

        public async Task<ActionResult<List<User>>> getAllUsers(string filial)
        {

            DataContext dbconnection = new DataContext(filial);
            UserRepository service = new UserRepository();
            if (dbconnection.state)
            {
                var result = await service.getUsers("SELECT * FROM TB_USUARIO ORDER BY COD_USUARIO", dbconnection.connection).ConfigureAwait(true);
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<User> getUser(string login, string senha, string filial)
        {
            DataContext dbconnection = new DataContext(filial);
            UserRepository service = new UserRepository();
            if (dbconnection.state)
            {

                var result = await service.getUserLogin("SELECT * FROM TB_USUARIO WHERE LOGIN = '" + login.ToLower() +
                                                "' AND SENHA = '" + senha.ToLower() + "' AND COD_FILIAL = '" + filial.ToUpper() + "'",
                                                dbconnection.connection).ConfigureAwait(true);
                return result;
            }
            else
                return null;
        }

        public async Task<Employe> getEmployeByRegister(int register, string filial)
        {
            DataContext dbconnection = new DataContext(filial);
            UserRepository service = new UserRepository();
            if (dbconnection.state)
            {

                var result = await service.getEmploye("SELECT * FROM TB_FUNCIONARIO WHERE COD_MATRICULA = '"+ register + "'",
                                                dbconnection.connection).ConfigureAwait(true);
                return result;
            }
            else
                return null;
        }
    }
}
