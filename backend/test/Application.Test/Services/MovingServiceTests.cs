using Application.Interfaces;
using Application.Services;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Moq;

namespace Application.Test.Services;

public class MovingServiceTests
{
    private readonly Mock<ICaixaRepository> _caixaRepoMock;
    private readonly Mock<IItemRepository> _itemRepoMock;
    private readonly MovingService _service;

    public MovingServiceTests()
    {
        _caixaRepoMock = new Mock<ICaixaRepository>();
        _itemRepoMock = new Mock<IItemRepository>();
        _service = new MovingService(_caixaRepoMock.Object, _itemRepoMock.Object);
    }

    [Fact]
    public async Task GetAllCaixasAsync_DeveRetornarListaDeCaixas()
    {
        var caixas = new List<Caixa>
        {
            new() { Id = 1, Numero = 1, Descricao = "Caixa 1", Itens = new List<Item>() },
            new() { Id = 2, Numero = 2, Descricao = "Caixa 2", Itens = new List<Item>() }
        };

        _caixaRepoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(caixas);

        var result = await _service.GetAllCaixasAsync();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetCaixaByIdAsync_DeveRetornarCaixaQuandoExistir()
    {
        var caixa = new Caixa { Id = 1, Numero = 5, Descricao = "Teste", Itens = new List<Item>() };
        _caixaRepoMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(caixa);

        var result = await _service.GetCaixaByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(5, result.Numero);
    }

    [Fact]
    public async Task GetCaixaByIdAsync_DeveRetornarNullQuandoNaoExistir()
    {
        _caixaRepoMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((Caixa?)null);

        var result = await _service.GetCaixaByIdAsync(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task CriarCaixaAsync_DeveLancarExcecaoQuandoCaixaExistir()
    {
        var caixaExistente = new Caixa { Id = 1, Numero = 1 };
        _caixaRepoMock.Setup(x => x.GetByNumeroAsync(1)).ReturnsAsync(caixaExistente);

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _service.CriarCaixaAsync(new CriarCaixaRequest(1, "Teste")));
    }

    [Fact]
    public async Task CriarCaixaAsync_DeveCriarCaixaQuandoNaoExistir()
    {
        _caixaRepoMock.Setup(x => x.GetByNumeroAsync(1)).ReturnsAsync((Caixa?)null);
        _caixaRepoMock.Setup(x => x.AddAsync(It.IsAny<Caixa>())).ReturnsAsync((Caixa c) => c);

        var result = await _service.CriarCaixaAsync(new CriarCaixaRequest(1, "Nova caixa"));

        Assert.Equal(1, result.Numero);
    }

    [Fact]
    public async Task AdicionarItemAsync_DeveLancarExcecaoQuandoCaixaNaoExistir()
    {
        _caixaRepoMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((Caixa?)null);

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _service.AdicionarItemAsync(new AdicionarItemRequest("Item", "Desc", 1)));
    }

    [Fact]
    public async Task AdicionarItemAsync_DeveAdicionarItemQuandoCaixaExistir()
    {
        var caixa = new Caixa { Id = 1, Numero = 1 };
        _caixaRepoMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(caixa);
        _itemRepoMock.Setup(x => x.AddAsync(It.IsAny<Item>())).ReturnsAsync((Item i) => i);

        var result = await _service.AdicionarItemAsync(new AdicionarItemRequest("Novo item", "Desc", 1));

        Assert.Equal("Novo item", result.Nome);
    }

    [Fact]
    public async Task BuscarItemAsync_DeveRetornarNaoEncontradoQuandoItemNaoExistir()
    {
        _itemRepoMock.Setup(x => x.GetByNomeContainingAsync("teste")).ReturnsAsync((Item?)null);

        var result = await _service.BuscarItemAsync("teste");

        Assert.False(result.Encontrado);
        Assert.Contains("não encontrado", result.Mensagem);
    }

    [Fact]
    public async Task BuscarItemAsync_DeveRetornarEncontradoQuandoItemExistir()
    {
        var item = new Item { Id = 1, Nome = "Carregador", CaixaId = 1 };
        var caixa = new Caixa { Id = 1, Numero = 5 };

        _itemRepoMock.Setup(x => x.GetByNomeContainingAsync("carregador")).ReturnsAsync(item);
        _caixaRepoMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(caixa);

        var result = await _service.BuscarItemAsync("carregador");

        Assert.True(result.Encontrado);
        Assert.Equal(5, result.NumeroCaixa);
    }
}