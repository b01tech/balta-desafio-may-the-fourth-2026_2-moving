namespace AI.Configuration;

public class AIOptions
{
    public const string SectionName = "AI";

    public string ApiKey { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = "https://openrouter.ai/api/v1";
}