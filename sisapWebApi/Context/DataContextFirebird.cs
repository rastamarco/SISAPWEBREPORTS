using sisapWebApi.Models.FirebirdEntitty;
using Microsoft.EntityFrameworkCore;

namespace sisapWebApi.Context
{
    public class DataContextFirebird : DbContext
    {
        public DataContextFirebird(DbContextOptions<DataContextFirebird> opt) : base(opt)
        {

        }

        public DbSet<UserFirebird> User { get; set; }
    }
}
