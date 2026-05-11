using Core.DTOs;

namespace Core.Test.DTOs;

public class CaixaDtoTests
{
    [Fact]
    public void CaixaDto_DeveCriarComPropriedadesCorretas()
    {
        var itens = new List<ItemDto>
        {
            new(1, "Carregador", null, 1),
            new(2, "Mouse", "Mouse sem fio", 1)
        };

        var caixaDto = new CaixaDto(1, 5, "Caixa de eletrônicos", itens);

        Assert.Equal(1, caixaDto.Id);
        Assert.Equal(5, caixaDto.Numero);
        Assert.Equal("Caixa de eletrônicos", caixaDto.Descricao);
        Assert.Equal(2, caixaDto.Itens.Count);
    }

    [Fact]
    public void ItemDto_DeveCriarComPropriedadesCorretas()
    {
        var itemDto = new ItemDto(1, "Carregador", "Carregador USB-C", 3);

        Assert.Equal(1, itemDto.Id);
        Assert.Equal("Carregador", itemDto.Nome);
        Assert.Equal("Carregador USB-C", itemDto.Descricao);
        Assert.Equal(3, itemDto.CaixaId);
    }

    [Fact]
    public void CriarCaixaRequest_DeveCriarComPropriedadesCorretas()
    {
        var request = new CriarCaixaRequest(10, "Caixa de cozinha");

        Assert.Equal(10, request.Numero);
        Assert.Equal("Caixa de cozinha", request.Descricao);
    }

    [Fact]
    public void AdicionarItemRequest_DeveCriarComPropriedadesCorretas()
    {
        var request = new AdicionarItemRequest("Faca", "Faca de cozinha", 2);

        Assert.Equal("Faca", request.Nome);
        Assert.Equal("Faca de cozinha", request.Descricao);
        Assert.Equal(2, request.CaixaId);
    }

    [Fact]
    public void BuscarItemRequest_DeveCriarComTermoBusca()
    {
        var request = new BuscarItemRequest("carregador");

        Assert.Equal("carregador", request.TermoBusca);
    }

    [Fact]
    public void BuscarItemResponse_DeveIndicarItemEncontrado()
    {
        var response = new BuscarItemResponse(true, "Item encontrado na Caixa 5", 5, 5);

        Assert.True(response.Encontrado);
        Assert.Equal("Item encontrado na Caixa 5", response.Mensagem);
        Assert.Equal(5, response.CaixaId);
        Assert.Equal(5, response.NumeroCaixa);
    }

    [Fact]
    public void BuscarItemResponse_DeveIndicarItemNaoEncontrado()
    {
        var response = new BuscarItemResponse(false, "Item não encontrado", null, null);

        Assert.False(response.Encontrado);
        Assert.Equal("Item não encontrado", response.Mensagem);
        Assert.Null(response.CaixaId);
        Assert.Null(response.NumeroCaixa);
    }
}