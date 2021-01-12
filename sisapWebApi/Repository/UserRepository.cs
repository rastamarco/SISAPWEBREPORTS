using FirebirdSql.Data.FirebirdClient;
using sisapWebApi.Models.Firebird;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sisapWebApi.Repository
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

        public async Task<Employe> getEmploye(string select, FbConnection connection)
        {
            Employe employe= new Employe();
            FbDataReader reader = null;
            FbCommand query = new FbCommand(select, connection);
            reader = query.ExecuteReader();
            while (reader.Read())
            {
                employe.COD_MATRICULA = (Int32)reader["COD_MATRICULA"];
                employe.COD_FILIAL = reader.GetString(1);
                employe.COD_TURNO = (Int32)reader["COD_TURNO"];
                employe.NO_FUNCIONARIO = reader.GetString(3);
                employe.DESC_FUNCAO = reader.GetString(4);
                employe.NO_SENHA = reader.GetString(5);
                employe.ATIVO = reader.GetString(6);
                employe.COD_SETOR = (Int32)reader["COD_SETOR"];
                employe.CARTAQ_SEGURANCA = reader.GetString(8);
            }
            return employe;
        }
    }
}
