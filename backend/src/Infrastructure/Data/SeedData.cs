using Core.Entities;

namespace Infrastructure.Data;

public static class SeedData
{
    public static List<Caixa> GetCaixas()
    {
        return new List<Caixa>
        {
            new()
            {
                Numero = 1,
                Descricao = "Eletrônicos - Escritório",
                Itens = new List<Item>
                {
                    new() { Nome = "Carregador Notebook", Descricao = "Carregador Dell 65W" },
                    new() { Nome = "Cabo HDMI", Descricao = "Cabo 2m" },
                    new() { Nome = "Mouse Sem Fio", Descricao = "Mouse Logitech" },
                    new() { Nome = "Teclado USB", Descricao = "Teclado Genius" },
                    new() { Nome = "Webcam", Descricao = "Webcam HD" }
                }
            },
            new()
            {
                Numero = 2,
                Descricao = "Material de Escritório",
                Itens = new List<Item>
                {
                    new() { Nome = "Canetas", Descricao = "Kit canetas coloridas" },
                    new() { Nome = "Cadernos", Descricao = "3 cadernos universitários" },
                    new() { Nome = "Post-it", Descricao = "Blocos coloridos" },
                    new() { Nome = "Grampeador", Descricao = "Grampeador médio" },
                    new() { Nome = "Fita Adesiva", Descricao = "Fita translúcida" }
                }
            },
            new()
            {
                Numero = 3,
                Descricao = "Cozinha",
                Itens = new List<Item>
                {
                    new() { Nome = "Faca de Cozinha", Descricao = "Faca chef" },
                    new() { Nome = "Colher de Pau", Descricao = "Colher grande" },
                    new() { Nome = "Panela", Descricao = "Panela antiaderente" },
                    new() { Nome = "Tampa", Descricao = "Tampa de vidro" },
                    new() { Nome = "Assadeira", Descricao = "Assadeira de alumínio" }
                }
            },
            new()
            {
                Numero = 4,
                Descricao = "Roupas",
                Itens = new List<Item>
                {
                    new() { Nome = "Camisetas", Descricao = "5 camisetas" },
                    new() { Nome = "Calças", Descricao = "3 calças jeans" },
                    new() { Nome = "Meias", Descricao = "Par de meias" },
                    new() { Nome = "Casaco", Descricao = "Casaco de lã" }
                }
            },
            new()
            {
                Numero = 5,
                Descricao = "Ferramentas",
                Itens = new List<Item>
                {
                    new() { Nome = "Chave de Fenda", Descricao = "Kit ferramentas" },
                    new() { Nome = "Martelo", Descricao = "Martelo pequeno" },
                    new() { Nome = "Alicate", Descricao = "Alicate universal" },
                    new() { Nome = "Fita Métrica", Descricao = "Fita 5m" }
                }
            }
        };
    }
}