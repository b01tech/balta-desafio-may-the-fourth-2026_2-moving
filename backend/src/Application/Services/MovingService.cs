using Application.Interfaces;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services;

public class MovingService : IMovingService
{
    private readonly ICaixaRepository _caixaRepository;
    private readonly IItemRepository _itemRepository;

    public MovingService(ICaixaRepository caixaRepository, IItemRepository itemRepository)
    {
        _caixaRepository = caixaRepository;
        _itemRepository = itemRepository;
    }

    public async Task<IEnumerable<CaixaDto>> GetAllCaixasAsync()
    {
        var caixas = await _caixaRepository.GetAllAsync();
        return caixas.Select(MapToCaixaDto);
    }

    public async Task<CaixaDto?> GetCaixaByIdAsync(int id)
    {
        var caixa = await _caixaRepository.GetByIdAsync(id);
        return caixa == null ? null : MapToCaixaDto(caixa);
    }

    public async Task<CaixaDto> CriarCaixaAsync(CriarCaixaRequest request)
    {
        var caixaExistente = await _caixaRepository.GetByNumeroAsync(request.Numero);
        if (caixaExistente != null)
            throw new InvalidOperationException($"Caixa com número {request.Numero} já existe");

        var caixa = new Caixa
        {
            Numero = request.Numero,
            Descricao = request.Descricao
        };

        var caixaCriada = await _caixaRepository.AddAsync(caixa);
        return MapToCaixaDto(caixaCriada);
    }

    public async Task<ItemDto> AdicionarItemAsync(AdicionarItemRequest request)
    {
        var caixa = await _caixaRepository.GetByIdAsync(request.CaixaId);
        if (caixa == null)
            throw new InvalidOperationException($"Caixa com ID {request.CaixaId} não encontrada");

        var item = new Item
        {
            Nome = request.Nome,
            Descricao = request.Descricao,
            CaixaId = request.CaixaId
        };

        var itemCriado = await _itemRepository.AddAsync(item);
        return MapToItemDto(itemCriado);
    }

    public async Task<BuscarItemResponse> BuscarItemAsync(string termo)
    {
        var item = await _itemRepository.GetByNomeContainingAsync(termo);

        if (item == null)
        {
            return new BuscarItemResponse(
                false,
                $"Item '{termo}' não encontrado em nenhuma caixa.",
                null,
                null
            );
        }

        var caixa = await _caixaRepository.GetByIdAsync(item.CaixaId);

        return new BuscarItemResponse(
            true,
            $"O item '{item.Nome}' está na caixa número {caixa?.Numero}.",
            item.CaixaId,
            caixa?.Numero
        );
    }

    private static CaixaDto MapToCaixaDto(Caixa caixa)
    {
        return new CaixaDto(
            caixa.Id,
            caixa.Numero,
            caixa.Descricao,
            caixa.Itens.Select(MapToItemDto).ToList()
        );
    }

    private static ItemDto MapToItemDto(Item item)
    {
        return new ItemDto(item.Id, item.Nome, item.Descricao, item.CaixaId);
    }
}