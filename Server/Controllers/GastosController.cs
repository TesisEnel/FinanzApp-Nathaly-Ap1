using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanzApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GastosController : ControllerBase
    {
        private readonly Context _context;

        public GastosController(Context context)
        {
            _context = context;
        }
        public bool Exists(int id)
        {
            return (_context.Gastos?.Any(c => c.GastoId == id)).GetValueOrDefault();

        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gastos>>> Obtener()
        {
            if (_context.Gastos == null)
            {
                return NotFound();
            }
            else
            {
                return await _context.Gastos.ToListAsync();
            }
        }

        [HttpGet("{GastoId}")]
        public async Task<ActionResult<Gastos>> ObtenerGastos(int gastoId)
        {
            if (_context.Gastos == null)
            {
                return NotFound();
            }

            var encontrado = await _context.Gastos.Include(e => e.DetalleGastos).Where(e => e.GastoId == gastoId).FirstOrDefaultAsync();

            if (encontrado == null)
            {
                return NotFound();
            }
            return encontrado;
        }

        [HttpPost]
        public async Task<ActionResult<Gastos>> PostIngresos(Gastos gasto)
        {
            if (!Exists(gasto.GastoId))
            {

                foreach (var item in gasto.DetalleGastos)
                {
                    var cuenta = _context.Cuenta.Find(1);

                    if (cuenta != null)
                    {
                        cuenta.Monto -= item.Monto;
                        _context.Cuenta.Update(cuenta);
                        await _context.SaveChangesAsync();
                        _context.Entry(cuenta).State = EntityState.Detached;
                    }
                }
                await _context.Gastos.AddAsync(gasto);
            }
            else
            {
                var gastoAnterior = _context.Gastos.Include(c => c.DetalleGastos).AsNoTracking()
                .FirstOrDefault(c => c.GastoId == gasto.GastoId);

                if (gastoAnterior != null && gastoAnterior.DetalleGastos != null)
                {
                    foreach (var item in gastoAnterior.DetalleGastos)
                    {
                        if (item != null)
                        {
                            var cuenta = _context.Cuenta.Find(1);

                            if (cuenta != null)
                            {
                                cuenta.Monto += item.Monto;
                                _context.Cuenta.Update(cuenta);
                                await _context.SaveChangesAsync();
                                _context.Entry(cuenta).State = EntityState.Detached;
                            }
                        }
                    }
                }

                _context.Database.ExecuteSql($"Delete from DetalleGastos where GastoId = {gasto.GastoId}");

                foreach (var item in gasto.DetalleGastos)
                {
                    var cuenta = _context.Cuenta.Find(1);

                    if (cuenta != null)
                    {
                        cuenta.Monto += item.Monto;
                        _context.Cuenta.Update(cuenta);
                        await _context.SaveChangesAsync();
                        _context.Entry(cuenta).State = EntityState.Detached;
                        _context.Entry(item).State = EntityState.Added;
                    }
                }
            }

            await _context.SaveChangesAsync();
            _context.Entry(gasto).State = EntityState.Detached;
            return Ok(gasto);
        }

        [HttpDelete("{GastoId}")]
        public async Task<IActionResult> Eliminar(int GastoId)
        {
            if (_context.Gastos == null)
            {
                return NotFound();
            }

            var eliminado = await _context.Gastos.Include(o => o.DetalleGastos).SingleOrDefaultAsync(o => o.GastoId == GastoId);

            if (eliminado == null)
            {
                return NotFound();
            }

            foreach(var item in eliminado.DetalleGastos)
            {
                var cuenta = _context.Cuenta.Find(1);

                if(cuenta != null)
                {
                    cuenta.Monto += item.Monto;
                    _context.Cuenta.Update(cuenta);
                }
            }

            _context.Gastos.Remove(eliminado);
            await _context.SaveChangesAsync();
            _context.Entry(eliminado).State = EntityState.Detached;
            return NoContent();
        }
    }
}