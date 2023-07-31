using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


    [Route("api/[controller]")]
    [ApiController]

    public class AhorrosController : ControllerBase
    {
        private readonly Context _context;

        public AhorrosController(Context context)
        {
            _context = context;
        }
        public bool Exists(int id)
        {
            return _context.Ahorros.Any(x => x.AhorroId == id);
        }

        public async Task<bool> Insert(Ahorros ahorros)
        {
            await _context.Ahorros.AddAsync(ahorros);
            bool salida = await _context.SaveChangesAsync() > 0;
            _context.Entry(ahorros).State = EntityState.Detached;
            return salida;
        }

        public async Task<bool> Update(Ahorros ahorro)
        {
            var existe = await _context.Ahorros.FindAsync(ahorro.AhorroId);

            if (existe != null)
            {
                _context.Entry(existe).CurrentValues.SetValues(ahorro);
                bool salida = await _context.SaveChangesAsync() > 0;
                _context.Entry(ahorro).State |= EntityState.Detached;
                return salida;
            }

            return false;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ahorros>>> Obtener()
        {
            if(_context.Ahorros == null)
            {
                return NotFound();
            }
            else
            {
                return await _context.Ahorros.ToListAsync();
            }
        }

        [HttpGet("{AhorroId}")]
        public async Task<ActionResult<Ahorros>> ObtenerAhorros(int AhorroId)
        {
            if(_context.Ahorros == null)
            {
                return NotFound();
            }

            var encontrado = await _context.Ahorros.Where(e => e.AhorroId == AhorroId).FirstOrDefaultAsync();

        if (encontrado == null)
            {
                return NotFound();
            }
            return encontrado;
        }

        [HttpPost]
        public async Task<IActionResult> PostAhorros(Ahorros ahorros)
        {
            if (!Exists(ahorros.AhorroId))
                await Insert(ahorros);
            else
                await Update(ahorros);

            return Ok(ahorros);
        }

        [HttpDelete("{AhorroId}")]
        public async Task<IActionResult> Eliminar(int AhorroId)
        {
            if(_context.Ahorros == null)
            {
                return NotFound();
            }

            var ahorro = await _context.Ahorros.FindAsync(AhorroId);

            if(ahorro == null)
            {
                return NotFound();
            }

            _context.Ahorros.Remove(ahorro);
            await _context.SaveChangesAsync();
            _context.Entry(ahorro).State = EntityState.Detached;
            return NoContent();
        }

    }