using System;
using System.Text;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.AspNetCore.Mvc;
using sisapWebApi.Models;
using sisapWebApi.Context;
using FastReport.Utils;
using sisapWebApi.Repository;

namespace sisapWebApi.Services
{
    public class UserService
    {

        public async Task<List<User>> getAllUsers()
        {

            DataContext dbconnection = new DataContext();
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
            DataContext dbconnection = new DataContext();
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
    }
}
