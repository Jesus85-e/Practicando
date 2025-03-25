namespace Practicando.Models
{
    public class DetallePedidoViewModel
    {
        public IEnumerable<VDetallePedido> Datos { get; set; }
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
    }
}
