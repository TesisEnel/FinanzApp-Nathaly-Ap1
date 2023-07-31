using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


    [Route("api/[controller]")]
    [ApiController]
    
    public class DeudasController : ControllerBase
    {
        private readonly Context _context;

        public DeudasController(Context context)
        {
            _context = context;
        }
        public bool Exists(int id)
        {
            return _context.Deudas.Any(x => x.DeudaId == id);
        }

        public async Task<bool> Insert(Deudas Deudas)
        {
            await _context.Deudas.AddAsync(Deudas);
            bool salida = await _context.SaveChangesAsync() > 0;
            _context.Entry(Deudas).State = EntityState.Detached;
            return salida;
        }

        public async Task<bool> Update(Deudas Deudas)
        {
            var existe = await _context.Deudas.FindAsync(Deudas.DeudaId);

            if (existe != null)
            {
                _context.Entry(existe).CurrentValues.SetValues(Deudas);
                bool salida = await _context.SaveChangesAsync() > 0;
                _context.Entry(Deudas).State |= EntityState.Detached;
                return salida;
            }

            return false;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Deudas>>> Obtener()
        {
            if(_context.Deudas == null)
            {
                return NotFound();
            }
            else
            {
                return await _context.Deudas.ToListAsync();
            }
        }

        [HttpGet("{DeudaId}")]
        public async Task<ActionResult<Deudas>> ObtenerDeudas(int DeudaId)
        {
            if(_context.Deudas == null)
            {
                return NotFound();
            }

            var encontrado = await _context.Deudas.Where(e => e.DeudaId == DeudaId).FirstOrDefaultAsync();

        if (encontrado == null)
            {
                return NotFound();
            }
            return encontrado;
        }

        [HttpPost]
        public async Task<IActionResult> PostDeudas(Deudas Deudas)
        {
            if (!Exists(Deudas.DeudaId))
                await Insert(Deudas);
            else
                await Update(Deudas);

            return Ok(Deudas);
        }

        [HttpDelete("{DeudaId}")]
        public async Task<IActionResult> Eliminar(int DeudaId)
        {
            if(_context.Deudas == null)
            {
                return NotFound();
            }

            var deudas = await _context.Deudas.FindAsync(DeudaId);

            if(deudas == null)
            {
                return NotFound();
            }

            _context.Deudas.Remove(deudas);
            await _context.SaveChangesAsync();
            _context.Entry(deudas).State = EntityState.Detached;
            return NoContent();
        }

    }