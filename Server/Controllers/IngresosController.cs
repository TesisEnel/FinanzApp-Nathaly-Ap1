using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


    [Route("api/[controller]")]
    [ApiController]

    public class IngresosController : ControllerBase
    {
        private readonly Context _context;

        public IngresosController(Context context)
        {
            _context = context;
        }
        public bool Existe(int IngresoId)
        {   
            return (_context.Ingresos?.Any(e => e.IngresoId == IngresoId)).GetValueOrDefault();
        }
         [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingresos>>> Obtener()
        {
            if(_context.Ingresos == null)
            {
                return NotFound();
            }
            else
            {
                return await _context.Ingresos.ToListAsync();
            }
        }

        [HttpGet("{IngresoId}")]
        public async Task<ActionResult<Ingresos>> ObtenerIngresos(int IngresoId)
        {
            if(_context.Ingresos == null)
            {
                return NotFound();
            }

            var ingresoId = await _context.Ingresos.FindAsync(IngresoId);

            if(ingresoId == null)
            {
                return NotFound();
            }
            return ingresoId;
        }

        [HttpPost]
        public async Task<ActionResult<Ingresos>> PostIngresos(Ingresos ingresos)
        {
            if(!Existe(ingresos.IngresoId))
            {
               await _context.Ingresos.AddAsync(ingresos);
               await _context.SaveChangesAsync();
                _context.Entry(ingresos).State = EntityState.Detached;
            }
            else
            {
                _context.Ingresos.Update(ingresos);
                await _context.SaveChangesAsync();
                _context.Entry(ingresos).State = EntityState.Detached;
            }

            await _context.SaveChangesAsync();
            return Ok(ingresos);
        }

        [HttpDelete("{IngresoId}")]
        public async Task<IActionResult> Eliminar(int IngresoId)
        {
            if(_context.Ingresos == null)
            {
                return NotFound();
            }

            var ingreso = await _context.Ingresos.FindAsync(IngresoId);

            if(ingreso == null)
            {
                return NotFound();
            }

            _context.Ingresos.Remove(ingreso);
            await _context.SaveChangesAsync();
            _context.Entry(ingreso).State = EntityState.Detached;
            return NoContent();
        }

    }
    