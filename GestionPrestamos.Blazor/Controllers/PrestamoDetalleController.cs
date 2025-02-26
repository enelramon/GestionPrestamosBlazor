using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionPrestamos.Context;
using GestionPrestamos.Models;

[Route("api/[controller]")]
[ApiController]
public class PrestamosDetallesController : ControllerBase
{
    private readonly Contexto _context;

    public PrestamosDetallesController(Contexto context)
    {
        _context = context;
    }

    // GET: api/PrestamosDetalles/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<PrestamosDetalle>> GetPrestamoDetalle(int id)
    {
        if (_context.PrestamosDetalles == null)
        {
            return NotFound();
        }

        var prestamoDetalle = await _context.PrestamosDetalles
            .Include(pd => pd.Prestamo)
            .Where(pd => pd.Id == id)
            .FirstOrDefaultAsync();

        if (prestamoDetalle == null)
        {
            return NotFound();
        }

        return prestamoDetalle;
    }

    // POST: api/PrestamosDetalles
    [HttpPost]
    public async Task<ActionResult<PrestamosDetalle>> PostPrestamoDetalle(PrestamosDetalle prestamoDetalle)
    {
        if (prestamoDetalle == null)
        {
            return BadRequest("Los datos son inválidos.");
        }

        if (!_context.Prestamos.Any(p => p.PrestamoId == prestamoDetalle.PrestamoId))
        {
            return BadRequest("El préstamo asociado no existe.");
        }

        _context.PrestamosDetalles.Add(prestamoDetalle);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPrestamoDetalle), new { id = prestamoDetalle.Id }, prestamoDetalle);
    }
}
