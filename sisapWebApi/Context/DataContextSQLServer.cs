using Microsoft.EntityFrameworkCore;
using sisapWebApi.Models.SQLServer;

namespace sisapWebApi.Context
{
    public class DataContextSQLServer : DbContext
    {
        public DataContextSQLServer(DbContextOptions<DataContextSQLServer> opt): base(opt)
        {

        }

        public DbSet<Refeitorio> Refeitorio { get; set; }
    }
}
