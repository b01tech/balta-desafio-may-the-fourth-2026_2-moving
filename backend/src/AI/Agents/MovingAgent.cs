using Core.DTOs;

namespace AI.Agents;

public class MovingAgent
{
    public const string Instructions = """
        Você é um assistente de mudança especializado em ajudar pessoas a encontrar itens em caixas numeradas.
        
        Seu papel é analisar a pergunta do usuário e o histórico de itens cadastrados para informar em qual caixa o item está.
        
        REGRAS IMPORTANTES:
        1. Sempre responda de forma amigável e útil
        2. Quando souber a caixa, informe o número da caixa claramente
        3. Quando não souber, seja honesto e sugira que o usuário cadastre o item primeiro
        4. Considere sinônimos e variações do nome do item (ex: "carregador" = "carregador de notebook")
        5. Retorne respostas em formato JSON válido
        
        Contexto:
        - Você tem acesso a um banco de dados de caixas numeradas
        - Cada caixa contém vários itens
        - Os itens foram previamente cadastrados pelo usuário
        
        Exemplos de resposta:
        {"encontrado": true, "caixa": 5, "mensagem": "O item está na caixa número 5"}
        {"encontrado": false, "mensagem": "Não encontrei o item. Deseja que eu procure em todas as caixas?"}
        """;

    public static string BuildSearchPrompt(string searchTerm, IEnumerable<CaixaDto> caixas)
    {
        var caixasTexto = string.Join("\n", caixas.Select(c => $"""
            Caixa {c.Numero} {(!string.IsNullOrEmpty(c.Descricao) ? $"({c.Descricao})" : "")}:
            {string.Join(", ", c.Itens.Select(i => i.Nome))}
            """));

        var jsonTemplate = @"{
    ""encontrado"": true/false,
    ""caixa"": numero_da_caixa,
    ""mensagem"": ""sua resposta""
}";

        return $"""
            Usuário está procurando por: "{searchTerm}"
            
            Caixas disponíveis:
            {caixasTexto}
            
            Com base nas caixas acima, determine se o item está em alguma caixa.
            Retorne no formato JSON:
            {jsonTemplate}
            """;
    }
}