using Datos;
using Datos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Loteria.Controllers
{
    public class LoteriaController : Controller
    {
        private AppLoteriaContext _context;

        public LoteriaController(AppLoteriaContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("[controller]/GetCarta")]
        public async Task<IActionResult> GetCarta()
        {
            try
            {
                var cartas = await _context.Cartas.ToListAsync();
                //var data = JsonConvert.DeserializeObject<List<AgentVM>>(result);
                return Ok(cartas);
            }
            catch (Exception e)
            {
                var error = e.InnerException != null ? e.InnerException.ToString() : e.Message;
                return Conflict(error);
            }
        }

        [HttpGet("[controller]/GetTarjetas")]
        public async Task<IActionResult> GetTarjetas()
        {
            try
            {
                //var tablas = await _context.Tablas.Include(c => c.Cartas).ToListAsync();
                var tablas = await _context.Tablas
                        .Include(c => c.Cartas)
                        .Select(x => new Tabla
                        {
                            TablaID = x.TablaID,
                            Nombre = x.Nombre,
                            Cartas = x.Cartas
                        }).ToListAsync(); 
                //var data = JsonConvert.DeserializeObject<List<AgentVM>>(result);
                return Ok(tablas);
            }
            catch (Exception e)
            {
                var error = e.InnerException != null ? e.InnerException.ToString() : e.Message;
                return Conflict(error);
            }
        }

    }
}
