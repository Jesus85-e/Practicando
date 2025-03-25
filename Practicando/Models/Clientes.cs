using System.ComponentModel.DataAnnotations;

namespace Practicando.Models
{
    public class Clientes
    {
        [Key]
        public int IdCliente { get; set; }
        public  string? NombreC { get; set; }
        public string? Apellido { get; set; }
        public string? CorreoElectronico { get; set; }
        public  string? Direccion { get; set; }
        public  string? Telefono { get; set; }

    }
}
