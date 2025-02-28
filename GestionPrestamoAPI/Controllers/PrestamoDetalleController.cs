using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionPrestamos.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelosPrestamo;


namespace GestionPrestamos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamosDetallesController : ControllerBase
    {
        private readonly Contexto _context;

        public PrestamosDetallesController(Contexto context)
        {
            _context = context;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrestamosDetalle>>> GetAllPrestamosDetalles()
        {
            return await _context.PrestamosDetalles.Include(pd => pd.Prestamo).ToListAsync();
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<PrestamosDetalle>> GetPrestamoDetalle(int id)
        {
            var prestamoDetalle = await _context.PrestamosDetalles
                .Include(pd => pd.Prestamo)
                .FirstOrDefaultAsync(pd => pd.Id == id);

            if (prestamoDetalle == null)
            {
                return NotFound();
            }

            return prestamoDetalle;
        }

        [HttpPost]
        public async Task<ActionResult<PrestamosDetalle>> PostPrestamoDetalle(PrestamosDetalle prestamoDetalle)
        {
            if (prestamoDetalle == null)
            {
                return BadRequest("Los datos son inválidos.");
            }

            var prestamo = await _context.Prestamos.FindAsync(prestamoDetalle.PrestamoId);
            if (prestamo == null)
            {
                return BadRequest("El préstamo asociado no existe.");
            }

           
            _context.PrestamosDetalles.Add(prestamoDetalle);

            
            prestamo.Balance -= prestamoDetalle.Valor;
            _context.Prestamos.Update(prestamo);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPrestamoDetalle), new { id = prestamoDetalle.Id }, prestamoDetalle);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrestamoDetalle(int id, PrestamosDetalle prestamoDetalle)
        {
            if (id != prestamoDetalle.Id)
            {
                return BadRequest("El ID no coincide.");
            }

            _context.Entry(prestamoDetalle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.PrestamosDetalles.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrestamoDetalle(int id)
        {
            var prestamoDetalle = await _context.PrestamosDetalles.FindAsync(id);
            if (prestamoDetalle == null)
            {
                return NotFound();
            }

            _context.PrestamosDetalles.Remove(prestamoDetalle);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}