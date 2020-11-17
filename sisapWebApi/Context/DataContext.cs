﻿using Microsoft.EntityFrameworkCore;
using sisapWebApi.Models;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using FastMember;
using System.ComponentModel;

namespace sisapWebApi.Context
{
    public class DataContext : DbContext
    {
        private static string pUser = "SYSDBA";
        private static string pPassword = "uv@doc3";
        private static string pDataBase = "localhost:C:\\SISAP\\DB\\SISAP.FDB";
        private static string pDataSource = "localhost";
        private static int pPort = 3050;
        private static int pDialect = 3;
        private static string pCharset = "None";
        public FbConnection connection;
        public bool state = false;
        public DataContext ()
        {
            try
            {
                FbConnectionStringBuilder stringconnection = new FbConnectionStringBuilder()
                {
                    Port = pPort,
                    UserID = pUser,
                    Password = pPassword,
                    Database = pDataBase,
                    DataSource = pDataSource,
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
            // optionsBuilder.UseFirebird(new FbConnection(stringconnection.ToString())); 
        }

        // public DbSet<User> Users { get; set; }

        public DataSet GetDataSet(string name)
        {
            DataSet set = new DataSet(name);
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(User));
            DataTable dt = new DataTable();
            foreach (PropertyDescriptor p in props)
                dt.Columns.Add(p.Name, p.PropertyType);
            set.Tables.Add(dt);
            return set;
        }

    }
}
