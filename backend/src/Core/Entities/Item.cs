namespace Core.Entities;

public class Item
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public int CaixaId { get; set; }
    public Caixa Caixa { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}