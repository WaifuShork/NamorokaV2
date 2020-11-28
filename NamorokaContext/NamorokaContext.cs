using Microsoft.EntityFrameworkCore;

namespace NamorokaContext
{
    public class NamorokaContext : DbContext
    {
        public DbSet<Server> Servers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("server=localhost;user=root;database=namorokadb;port=3306;Connect Timeout=5");

        public class Server
        {
            public ulong Id { get; set; }
            public string Prefix { get; set; }
        }
    }
}