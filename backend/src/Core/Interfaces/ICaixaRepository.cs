using Core.Entities;

namespace Core.Interfaces;

public interface ICaixaRepository
{
    Task<IEnumerable<Caixa>> GetAllAsync();
    Task<Caixa?> GetByIdAsync(int id);
    Task<Caixa?> GetByNumeroAsync(int numero);
    Task<Caixa> AddAsync(Caixa caixa);
    Task UpdateAsync(Caixa caixa);
    Task DeleteAsync(int id);
}