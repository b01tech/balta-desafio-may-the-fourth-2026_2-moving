using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CaixaRepository : ICaixaRepository
{
    private readonly MovingDbContext _context;

    public CaixaRepository(MovingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Caixa>> GetAllAsync()
    {
        return await _context.Caixas
            .Include(c => c.Itens)
            .OrderBy(c => c.Numero)
            .ToListAsync();
    }

    public async Task<Caixa?> GetByIdAsync(int id)
    {
        return await _context.Caixas
            .Include(c => c.Itens)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Caixa?> GetByNumeroAsync(int numero)
    {
        return await _context.Caixas
            .Include(c => c.Itens)
            .FirstOrDefaultAsync(c => c.Numero == numero);
    }

    public async Task<Caixa> AddAsync(Caixa caixa)
    {
        _context.Caixas.Add(caixa);
        await _context.SaveChangesAsync();
        return caixa;
    }

    public async Task UpdateAsync(Caixa caixa)
    {
        _context.Caixas.Update(caixa);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var caixa = await _context.Caixas.FindAsync(id);
        if (caixa != null)
        {
            _context.Caixas.Remove(caixa);
            await _context.SaveChangesAsync();
        }
    }
}