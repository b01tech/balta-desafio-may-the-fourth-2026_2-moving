using AI.Agents;
using AI.Configuration;
using Application.Interfaces;
using Application.Services;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

namespace Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMovingServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MovingDbContext>(options =>
            options.UseSqlite("Data Source=moving.db"));

        services.AddScoped<ICaixaRepository, CaixaRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();

        services.AddScoped<IMovingService, MovingService>();

        var aiOptions = configuration.GetSection(AIOptions.SectionName).Get<AIOptions>();
        if (aiOptions != null && !string.IsNullOrEmpty(aiOptions.ApiKey))
        {
            services.AddSingleton(aiOptions);
            services.AddSingleton<IChefService>(sp =>
            {
                var httpClient = new HttpClient();
                return new ChefService(httpClient, aiOptions);
            });
        }

        return services;
    }

    public static IServiceCollection AddMovingCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        return services;
    }
}

public interface IChefService
{
    Task<string> GetSuggestionAsync(string userPrompt, CancellationToken cancellationToken = default);
}

public class ChefService : IChefService
{
    private readonly HttpClient _httpClient;
    private readonly AIOptions _options;

    public ChefService(HttpClient httpClient, AIOptions options)
    {
        _httpClient = httpClient;
        _options = options;
    }

    public async Task<string> GetSuggestionAsync(string userPrompt, CancellationToken cancellationToken = default)
    {
        var requestBody = new
        {
            model = _options.Model,
            messages = new[]
            {
                new { role = "system", content = MovingAgent.Instructions },
                new { role = "user", content = userPrompt }
            }
        };

        var request = new HttpRequestMessage(HttpMethod.Post, $"{_options.BaseUrl}/chat/completions");
        request.Headers.Add("Authorization", $"Bearer {_options.ApiKey}");
        request.Headers.Add("HTTP-Referer", "https://localhost");
        request.Headers.Add("X-Title", "MovingAI");
        request.Content = JsonContent.Create(requestBody);

        var response = await _httpClient.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        using var doc = System.Text.Json.JsonDocument.Parse(await response.Content.ReadAsStringAsync(cancellationToken));
        return doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? string.Empty;
    }
}