namespace Core.Entities;

public class Caixa
{
    public int Id { get; set; }
    public int Numero { get; set; }
    public string? Descricao { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Item> Itens { get; set; } = new List<Item>();
}