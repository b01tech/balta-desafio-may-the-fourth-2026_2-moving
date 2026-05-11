namespace Core.DTOs;

public record CaixaDto(int Id, int Numero, string? Descricao, List<ItemDto> Itens);

public record ItemDto(int Id, string Nome, string? Descricao, int CaixaId);

public record CriarCaixaRequest(int Numero, string? Descricao);

public record AdicionarItemRequest(string Nome, string? Descricao, int CaixaId);

public record BuscarItemRequest(string TermoBusca);

public record BuscarItemResponse(bool Encontrado, string? Mensagem, int? CaixaId, int? NumeroCaixa);