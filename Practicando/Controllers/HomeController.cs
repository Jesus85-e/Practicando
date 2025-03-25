
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Practicando.Models;
using Practicando.Models.Data;



namespace Practicando.Controllers;

public class HomeController : Controller
{
    private readonly ConexionDB context;
    //private readonly ILogger<HomeController> _logger;

    public HomeController(ConexionDB context)
    {
        this.context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var lista = await context.Clientes.ToListAsync();
        
        return View(lista);
    }

    [HttpGet]
    public async Task<IActionResult> recargartabla()
    {
        var lista = await context.Clientes.ToListAsync();

        return PartialView("_tablaclientes", lista);
    }


    [HttpPost]
    public async Task<IActionResult> Guardar([FromBody] Clientes datos)
    {

        try
        {
            if (ModelState.IsValid)
            {
                var usuarioExistente = await context.Clientes
                .FirstOrDefaultAsync(c => c.NombreC == datos.NombreC && c.Apellido == datos.Apellido);

                if (usuarioExistente != null)
                {
                    return Json(new { success = false, message = "Este cliente ya existe." });
                }
                var Formd = new Clientes
                {
                    NombreC = datos.NombreC,
                    Apellido = datos.Apellido,
                    CorreoElectronico = datos.CorreoElectronico,
                    Direccion = datos.Direccion,
                    Telefono = datos.Telefono
                };

                _ = context.Clientes.Add(Formd);
                _ = await context.SaveChangesAsync();

                return Json(new { success = true, message = "Los datos se guardaron correctamente" });

            }
            return Json(new { success = false, message = "Los datos no se guardaron " });
        }
        catch (Exception ex)
        {
            // Registrar la excepción en los logs
            Console.WriteLine($"Exception: {ex.Message}");
            var errorRespuesta = new { mensaje = "Ocurrió un error al guardar los datos.", error = ex.Message };
            return StatusCode(500, errorRespuesta);
        }



    }
    [HttpGet]
    public async Task<IActionResult> GetById(int IdCliente)
    {
        var Cliente = await context.Clientes.FirstOrDefaultAsync(c => c.IdCliente == IdCliente);
        if (Cliente == null)
        {
            return Json(new { menssage = "No se encontro al cliente" });
        }

        return Json(Cliente);
    }

    [HttpPost]
    public async Task<IActionResult> EditarCliente([FromBody] Clientes datos)
    {
        var buscarcliente = await context.Clientes.FindAsync(datos.IdCliente);

        if (buscarcliente != null)
        {

            buscarcliente.CorreoElectronico = datos.CorreoElectronico;
            buscarcliente.Direccion = datos.Direccion;
            buscarcliente.Telefono = datos.Telefono;


            _ = await context.SaveChangesAsync();

            return Json(new { success = true, menssage = "Los datos del cliente se actualizaron correctamente" });
        }

        return Json(new { success = false, menssage = "Ocurrio un error al actualizar los datos" });
    }

    [HttpPost]
    public async Task<IActionResult> EliminarCliente(int IdCliente)
    {
        var buscarcliente = await context.Clientes.FindAsync(IdCliente);

        if (buscarcliente != null)
        {
            context.Clientes.Remove(buscarcliente);
            _ = await context.SaveChangesAsync();

            return Json(new { success = true, menssage = "El cliente se elimino correctamente" });
        }

        return Json(new { success = false, menssage = "Ocurrio un problema al eliminar al cliente" });
    }

  

}
