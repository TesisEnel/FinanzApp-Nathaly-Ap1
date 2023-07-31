using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanzApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly Context _context;

        public CuentaController(Context context)
        {
            _context = context;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Cuenta>> ObtenerGastos(int Id)
        {
            if (_context.Cuenta == null)
            {
                return NotFound();
            }

            var encontrado = await _context.Cuenta.FindAsync(Id);

            if (encontrado == null)
            {
                return NotFound();
            }
            return encontrado;
        }
            [HttpGet]
        public async Task<ActionResult<IEnumerable<Cuenta>>> Obtener()
        {
            if (_context.Cuenta == null)
            {
                return NotFound();
            }
            else
            {
                return await _context.Cuenta.ToListAsync();
            }
        }
    }
}
