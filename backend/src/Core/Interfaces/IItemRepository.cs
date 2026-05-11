using Core.Entities;

namespace Core.Interfaces;

public interface IItemRepository
{
    Task<IEnumerable<Item>> GetAllAsync();
    Task<IEnumerable<Item>> GetByCaixaIdAsync(int caixaId);
    Task<Item?> GetByIdAsync(int id);
    Task<Item?> GetByNomeContainingAsync(string termo);
    Task<Item> AddAsync(Item item);
    Task UpdateAsync(Item item);
    Task DeleteAsync(int id);
}