using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Practicando.Models;
using Practicando.Models.Data;
using Rotativa.AspNetCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Practicando.Controllers
{
    public class DetallePedidoController : Controller
    {
        private readonly ConexionDB _context;

        public DetallePedidoController(ConexionDB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> DetalleP(int id)
        {


            var datos = await _context.VDetallePedidos
            .Where(v=> v.IdCliente == id)
            .ToListAsync();

            ViewBag.IdCliente = id;
            return View(datos);
       
              
        }
        [HttpGet]
        public async Task<IActionResult> DatosCliente(int IdCliente)
        {
            var cliente = await _context.Clientes.FindAsync(IdCliente);
            return Json(cliente);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProductos()
        {
            var productos = await _context.Productos.Select(p => new { p.IdProducto, p.NombreProducto, p.Precio, p.Stock}).ToListAsync();
            return Json(productos);
        }

        [HttpGet]
        public async Task<IActionResult> Recargardatosform(int IdProducto)
        {
            var datosp = await _context.Productos.FindAsync(IdProducto);
            return Json(datosp);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarPedido([FromBody] VDetallePedido datos)
        {


            try
            {
                var Resultado = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC GuardarDatosPedidos @IdCliente, @Fecha, @Total, @IdProducto, @Precio, @Cantidad",
                    new SqlParameter("@IdCliente", datos.IdCliente),
                    new SqlParameter("@Fecha", datos.Fecha),
                    new SqlParameter("@Total", datos.Total),
                    new SqlParameter("@IdProducto", datos.IdProducto),
                    new SqlParameter("@Precio", datos.Precio),
                    new SqlParameter("@Cantidad", datos.Cantidad)
                );

                
                return Json(new {success=true , menssage="Se hizo correctamente el pedido"});
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return Json(ex);
            }


            
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerDatosTabla( int Id)
        {


            var datos = await _context.VDetallePedidos
            .Where(v => v.IdCliente == Id)
            .ToListAsync();

       
            return Json(datos);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarPedidos(int idPedido)
        {
            try
            {
                var resultado = await _context.Database.ExecuteSqlRawAsync(
                    "EXEC EliminarPedidos @IdPedido",
                     new SqlParameter("@IdPedido", idPedido)
                );

                return Json(new { resultado, menssage = "Se elimino correctamente el pedid0" });
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return Json(ex);
            }

        }

        public IActionResult crearPDF()
        {
            var clientes = _context.Clientes.ToList();

            return View(clientes);


        }

        public IActionResult Print()
        {
            var clientes = _context.Clientes.ToList();
            return new ViewAsPdf("crearPDF", clientes)
            { FileName = "test.pdf" };
        }
    }
}

