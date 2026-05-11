using Core.Entities;

namespace Core.Test.Entities;

public class CaixaTests
{
    [Fact]
    public void Caixa_DeveTerPropriedadesCorretas()
    {
        var caixa = new Caixa
        {
            Id = 1,
            Numero = 5,
            Descricao = "Caixa de materiais de escritório",
            CreatedAt = new DateTime(2026, 5, 11, 12, 0, 0, DateTimeKind.Utc)
        };

        Assert.Equal(1, caixa.Id);
        Assert.Equal(5, caixa.Numero);
        Assert.Equal("Caixa de materiais de escritório", caixa.Descricao);
        Assert.NotNull(caixa.Itens);
        Assert.Empty(caixa.Itens);
    }

    [Fact]
    public void Caixa_DeveInicializarItensComoListaVazia()
    {
        var caixa = new Caixa { Numero = 1 };

        Assert.NotNull(caixa.Itens);
        Assert.Empty(caixa.Itens);
    }

    [Fact]
    public void Caixa_DeveTerCreatedAtPorPadrao()
    {
        var antes = DateTime.UtcNow;
        var caixa = new Caixa { Numero = 1 };
        var depois = DateTime.UtcNow;

        Assert.InRange(caixa.CreatedAt, antes, depois);
    }

    [Fact]
    public void Caixa_PodeTerItens()
    {
        var caixa = new Caixa { Id = 1, Numero = 1 };
        var item1 = new Item { Id = 1, Nome = "Carregador", CaixaId = 1 };
        var item2 = new Item { Id = 2, Nome = "Cabo HDMI", CaixaId = 1 };

        caixa.Itens.Add(item1);
        caixa.Itens.Add(item2);

        Assert.Equal(2, caixa.Itens.Count);
    }
}