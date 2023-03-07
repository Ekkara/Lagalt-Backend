using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Lagalt_Backend.Models
{
    public class LagaltDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }

        private static string GetConnectionString()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = "localhost\\SQLEXPRESS",
                InitialCatalog = "LagaltDatabase",
                IntegratedSecurity = true,
                TrustServerCertificate = true
            };

            return builder.ConnectionString;
        }
    }
}
