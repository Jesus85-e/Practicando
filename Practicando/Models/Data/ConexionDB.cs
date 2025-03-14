using Microsoft.EntityFrameworkCore;
using Practicando.Models;
using System.Runtime.CompilerServices;
namespace Practicando.Models.Data
{
    public class ConexionDB : DbContext
    {

      public ConexionDB(DbContextOptions<ConexionDB> options) : base(options)
        {
        } 

        public DbSet<Clientes> Clientes { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clientes>().HasKey(c => c.IdCliente);
        }
    }
}