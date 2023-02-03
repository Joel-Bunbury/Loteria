using Datos;
using Datos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System;

namespace Loteria.Controllers
{
    public class GanadoresController : Controller
    {
        private AppLoteriaContext _context;

        public GanadoresController(AppLoteriaContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            var ganadores = await _context.Ganadores
                            .Include(t => t.Tabla)
                            .ThenInclude(c => c.Cartas)
                            .ToListAsync();
            return View(ganadores);
        }

        [HttpPost("[controller]/RegistrarGanador/{TablaId}")]
        public async Task<ActionResult> RegistrarGanador([FromRoute] int TablaId)
        {
            try
            {
                var ganador = new Ganador();
                ganador.FechaHora = DateTime.Now;
                ganador.TablaID = TablaId;
                _context.Ganadores.Add(ganador);

                var result = await _context.SaveChangesAsync();


                if (result > 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(result);
                }

            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
