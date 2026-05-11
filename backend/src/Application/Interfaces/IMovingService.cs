using Core.DTOs;

namespace Application.Interfaces;

public interface IMovingService
{
    Task<IEnumerable<CaixaDto>> GetAllCaixasAsync();
    Task<CaixaDto?> GetCaixaByIdAsync(int id);
    Task<CaixaDto> CriarCaixaAsync(CriarCaixaRequest request);
    Task<ItemDto> AdicionarItemAsync(AdicionarItemRequest request);
    Task<BuscarItemResponse> BuscarItemAsync(string termo);
}