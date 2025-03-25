using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Practicando.Models
{
    public class VDetallePedido
    {
        [Key]
        [JsonPropertyName("id_pedidos")]
        public int IdPedidos { get; set; }
        public int IdCliente { get; set; }
        [JsonPropertyName("id_detalle")]
        public int IDPedidos { get; set; }
        public int IdProducto { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal Cantidad { get; set; }
        public string? NombreProducto { get; set; }
        public string? Descripcion { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal Precio { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }

    }
}
