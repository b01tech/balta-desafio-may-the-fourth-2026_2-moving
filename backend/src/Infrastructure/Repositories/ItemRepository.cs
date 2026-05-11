using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly MovingDbContext _context;

    public ItemRepository(MovingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Item>> GetAllAsync()
    {
        return await _context.Itens
            .Include(i => i.Caixa)
            .ToListAsync();
    }

    public async Task<IEnumerable<Item>> GetByCaixaIdAsync(int caixaId)
    {
        return await _context.Itens
            .Where(i => i.CaixaId == caixaId)
            .ToListAsync();
    }

    public async Task<Item?> GetByIdAsync(int id)
    {
        return await _context.Itens
            .Include(i => i.Caixa)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Item?> GetByNomeContainingAsync(string termo)
    {
        return await _context.Itens
            .Include(i => i.Caixa)
            .FirstOrDefaultAsync(i => i.Nome.ToLower().Contains(termo.ToLower()));
    }

    public async Task<Item> AddAsync(Item item)
    {
        _context.Itens.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task UpdateAsync(Item item)
    {
        _context.Itens.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.Itens.FindAsync(id);
        if (item != null)
        {
            _context.Itens.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}