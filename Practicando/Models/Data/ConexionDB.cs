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
        public DbSet<VDetallePedido> VDetallePedidos { get; set; }
        public DbSet<Productos> Productos { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var decimalColumns = new[] { "Total", "Precio", "Cantidad" };
            modelBuilder.Entity<Clientes>().HasKey(c => c.IdCliente);
            // Las vistas no tienen clave primaria
            modelBuilder.Entity<VDetallePedido>().HasNoKey();
            modelBuilder.Entity<Productos>().HasKey(p => p.IdProducto);
           
        }
    }
}