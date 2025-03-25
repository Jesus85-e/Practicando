using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Practicando.Models
{
    public class Productos
    {
        [Key]
        public int IdProducto { get; set; }
        public string? NombreProducto { get; set; }
        public string? Descripcion { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal Precio { get; set; }
        public int IdCategoria { get; set;}
        public int Stock { get; set; }
    }
}
