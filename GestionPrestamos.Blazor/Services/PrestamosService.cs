using Aplicada1.Core;
using GestionPrestamos.Context;
using GestionPrestamos.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestionPrestamos.Services;

public class PrestamosService(
    IDbContextFactory<Contexto> contextFactory
) : Aplicada1.Core.IServiceResult<Prestamos, int>
{

    public async Task<Result> Guardar(Prestamos prestamo)
    {
        prestamo.Balance = prestamo.Monto;
        if (!await Existe(prestamo.PrestamoId))
        {
            var inserto = await Insertar(prestamo);
            return Result.Success(inserto);
        }
        else
        {
            var modifico = await Modificar(prestamo);
            return Result.Success(modifico);
        }
    }
    private async Task<bool> Existe(int prestamoId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Prestamos
            .AnyAsync(p => p.PrestamoId == prestamoId);
    }

    private async Task<bool> Insertar(Prestamos prestamo)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Prestamos.Add(prestamo);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Prestamos prestamo)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Update(prestamo);
        return await contexto
            .SaveChangesAsync() > 0;
    }


    public async Task<Result<Prestamos?>> Buscar(int prestamoId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Prestamos
            .Include(d => d.Deudor)
            .FirstOrDefaultAsync(p => p.PrestamoId == prestamoId);
    }

    public async Task<Result<List<Prestamos>>> GetList(Expression<Func<Prestamos, bool>> criterio)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Prestamos
            .Include(d => d.Deudor)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Result> Eliminar(int prestamoId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        var eliminado = await contexto.Prestamos
            .Where(p => p.PrestamoId == prestamoId)
            .ExecuteDeleteAsync() > 0;
        return Result.Success(eliminado);
    }

    public async Task<List<Prestamos>> GetPrestamosPendientes(int deudorId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Prestamos
            .Where(p => p.DeudorId == deudorId && p.Balance > 0)
            .OrderBy(p => p.PrestamoId)
            .AsNoTracking()
            .ToListAsync();
    }
}
