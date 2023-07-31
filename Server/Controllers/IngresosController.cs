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
    public bool Exists(int id)
    {
        return _context.Ingresos.Any(x => x.IngresoId == id);
    }

    public async Task<bool> Insert(Ingresos ingreso)
    {
        var cuenta = _context.Cuenta.Find(1);
        if (cuenta != null)
        {
            cuenta.Monto += ingreso.Monto;
            _context.Cuenta.Update(cuenta);
        }
        await _context.Ingresos.AddAsync(ingreso);
        bool salida = await _context.SaveChangesAsync() > 0;
        _context.Entry(ingreso).State = EntityState.Detached;
        return salida;
    }

    public async Task<bool> Update(Ingresos ingreso)
    {
        var existe = await _context.Ingresos.FindAsync(ingreso.IngresoId);

        if (existe != null)
        {
            var cuenta = _context.Cuenta.Find(1);
            if (cuenta != null)
            {
                cuenta.Monto -= existe.Monto;
                cuenta.Monto += ingreso.Monto;
                _context.Cuenta.Update(cuenta);
            }
            _context.Entry(existe).CurrentValues.SetValues(ingreso);
            bool salida = await _context.SaveChangesAsync() > 0;
            _context.Entry(ingreso).State |= EntityState.Detached;
            return salida;
        }

        return false;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ingresos>>> Obtener()
    {
        if (_context.Ingresos == null)
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
        if (_context.Ingresos == null)
        {
            return NotFound();
        }

        var encontrado = await _context.Ingresos.Where(e => e.IngresoId == IngresoId).FirstOrDefaultAsync();

        if (encontrado == null)
        {
            return NotFound();
        }
        return encontrado;
    }

    [HttpPost]
    public async Task<IActionResult> PostIngresos(Ingresos ingresos)
    {
        if (!Exists(ingresos.IngresoId))
            await Insert(ingresos);
        else
            await Update(ingresos);

        return Ok(ingresos);
    }

    [HttpDelete("{IngresoId}")]
    public async Task<IActionResult> Eliminar(int IngresoId)
    {
        if (_context.Ingresos == null)
        {
            return NotFound();
        }

        var ingreso = await _context.Ingresos.FindAsync(IngresoId);

        if (ingreso == null)
        {
            return NotFound();
        }

        _context.Ingresos.Remove(ingreso);
        await _context.SaveChangesAsync();
        _context.Entry(ingreso).State = EntityState.Detached;
        return NoContent();
    }

}