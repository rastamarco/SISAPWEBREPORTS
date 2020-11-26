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

namespace sisapWebApi.Repositories
{
    public class UserRepository
    {
        public async Task<List<User>> getUsers(string select, FbConnection connection)
        {
            List<User> users = new List<User>();
            FbDataReader reader = null;
            FbCommand query = new FbCommand(select, connection);
            reader = query.ExecuteReader();

            while (reader.Read())
            {
                User user = new User();
                user.codUsuario = (int)reader["COD_USUARIO"];
                user.nomeUsuario = reader.GetString(1);
                user.login = reader.GetString(2);
                user.senha = reader.GetString(3);
                user.tipoUsuario = reader.GetString(4);
                user.codFilial = reader.GetString(5);
                users.Add(user);
            }
            return users;

        }

        public async Task<User> getUserLogin(string select, FbConnection connection)
        {
            User user = new User();
            FbDataReader reader = null;
            FbCommand query = new FbCommand(select, connection);
            reader = query.ExecuteReader();
            while (reader.Read())
            {
                user.codUsuario = (Int32)reader["COD_USUARIO"];
                user.nomeUsuario = reader.GetString(1);
                user.login = reader.GetString(2);
                user.senha = reader.GetString(3);
                user.tipoUsuario = reader.GetString(4);
                user.codFilial = reader.GetString(5);
            }
            return user;
        }
        public async Task<List<User>> getAllUsers()
        {

            DataContext dbconnection = new DataContext();
            if (dbconnection.state)
            {
                var result = await getUsers("SELECT * FROM TB_USUARIO ORDER BY COD_USUARIO", dbconnection.connection).ConfigureAwait(true);
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
            if (dbconnection.state)
            {

                var result = await getUserLogin("SELECT * FROM TB_USUARIO WHERE LOGIN = '" + login.ToLower() +
                                                "' AND SENHA = '" + senha.ToLower() + "' AND COD_FILIAL = '" + filial.ToUpper()+"'", 
                                                dbconnection.connection).ConfigureAwait(true);
                return result;
            }
            else
                return null;
        }
    }
}
