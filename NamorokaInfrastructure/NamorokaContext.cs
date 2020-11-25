using System;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace NamorokaInfrastructure
{
    public class NamorokaContext : DbContext
    {
        public DbSet<Server> Servers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseMySql("server=localhost;user=root;database=namoroka;port=3306;Connect Timeout=5;");

        public class Server
        {
            public ulong Id { get; set; }
            public string Prefix { get; set; }
            
        }
    }
}