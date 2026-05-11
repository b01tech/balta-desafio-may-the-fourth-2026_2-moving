using Core.Entities;

namespace Core.Test.Entities;

public class ItemTests
{
    [Fact]
    public void Item_DeveTerPropriedadesCorretas()
    {
        var item = new Item
        {
            Id = 1,
            Nome = "Carregador",
            Descricao = "Carregador do notebook",
            CaixaId = 5,
            CreatedAt = new DateTime(2026, 5, 11, 12, 0, 0, DateTimeKind.Utc)
        };

        Assert.Equal(1, item.Id);
        Assert.Equal("Carregador", item.Nome);
        Assert.Equal("Carregador do notebook", item.Descricao);
        Assert.Equal(5, item.CaixaId);
    }

    [Fact]
    public void Item_DeveTerNomeObrigatorio()
    {
        var item = new Item();

        Assert.Equal(string.Empty, item.Nome);
    }

    [Fact]
    public void Item_DeveTerCreatedAtPorPadrao()
    {
        var antes = DateTime.UtcNow;
        var item = new Item { Nome = "Teste", CaixaId = 1 };
        var depois = DateTime.UtcNow;

        Assert.InRange(item.CreatedAt, antes, depois);
    }

    [Fact]
    public void Item_PertenceACaixa()
    {
        var caixa = new Caixa { Id = 1, Numero = 3 };
        var item = new Item { Id = 1, Nome = "Mouse", CaixaId = 1, Caixa = caixa };

        Assert.Equal(caixa, item.Caixa);
        Assert.Equal(1, item.CaixaId);
    }
}