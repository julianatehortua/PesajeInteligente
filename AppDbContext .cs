using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica1
{
    public class AppDbContext : DbContext
    {
        public DbSet<Company> Empresas { get; set; }

        public AppDbContext() : base("name=CompanyDbConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .Property(e => e.FechaCreacion)
                .IsOptional();

            modelBuilder.Entity<Company>()
                .Property(e => e.FechaUltimaModificacion)
                .IsOptional();
        }
    }
}
