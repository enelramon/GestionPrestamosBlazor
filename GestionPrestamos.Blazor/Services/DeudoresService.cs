using GestionPrestamos.Context;
using GestionPrestamos.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestionPrestamos.Services;

public class DeudoresService(
    IDbContextFactory<Contexto> contextFactory
): Aplicada1.Core.IService<Deudores,int>
{
    public Task<bool> Guardar(Deudores entidad)
    {
        throw new NotImplementedException();
    }

    public async Task<Deudores?> Buscar(int deudorId)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Deudores
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.DeudorId == deudorId);
    }

    public Task<bool> Eliminar(int id)
    {
        throw new NotImplementedException();
    }
     
    public async Task<List<Deudores>> GetList(Expression<Func<Deudores, bool>> criterio)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Deudores
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}
