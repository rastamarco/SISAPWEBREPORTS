using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.AspNetCore.Mvc;

namespace sisapWebApi.Context
{
    public class DataContext : DbContext
    {
        private static string pUser = "SYSDBA";
        private static string pPassword = "uv@doc3";
        //private static string pDataBase = "10.10.11.32:C:\\SISAP\\DB\\SISAP.FDB";
        //private static string pDataSource = "10.10.11.32";
        private static int pPort = 3050;
        private static int pDialect = 3;
        private static string pCharset = "ISO8859_1";
        public FbConnection connection;
        public bool state = false;
        public DataContext (string codFilial)
        {
            var filial = GetIpFilial(codFilial);
            try
            {
                FbConnectionStringBuilder stringconnection = new FbConnectionStringBuilder()
                {
                    Port = pPort,
                    UserID = pUser,
                    Password = pPassword,
                    Database = filial + ":C:\\SISAP\\DB\\SISAP.FDB",
                    DataSource = filial,
                    Dialect = pDialect,
                    Charset = pCharset
                };
                connection = new FbConnection(stringconnection.ToString());
                connection.Open();
                state = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                state = false;
            }
        }


        public string GetIpFilial(string codFilial)
        {
            if (codFilial == "UIA")
                return "10.10.11.32";
            else if (codFilial == "UIAII")
                return "10.10.128.100";
            else if (codFilial == "UIAIII")
                return "192.168.1.246";
            else if (codFilial == "UDM")
                return "172.16.5.66";
            else
                return "10.10.11.32";
        }

    }
}
