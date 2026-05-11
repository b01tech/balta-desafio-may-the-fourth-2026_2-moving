using AI.Agents;
using Application.Interfaces;
using Core.DTOs;

namespace Api.Extensions;

public static class EndpointExtensions
{
    public static void MapMovingEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api");

        group.MapGet("/caixas", CaixasEndpoint);
        group.MapGet("/caixas/{id}", CaixaByIdEndpoint);
        group.MapPost("/caixas", CriarCaixaEndpoint);
        group.MapPost("/itens", AdicionarItemEndpoint);
        group.MapPost("/buscar", BuscarItemEndpoint);
    }

    private static async Task<IResult> CaixasEndpoint(IMovingService movingService)
    {
        var caixas = await movingService.GetAllCaixasAsync();
        return Results.Ok(caixas);
    }

    private static async Task<IResult> CaixaByIdEndpoint(int id, IMovingService movingService)
    {
        var caixa = await movingService.GetCaixaByIdAsync(id);
        return caixa == null ? Results.NotFound() : Results.Ok(caixa);
    }

    private static async Task<IResult> CriarCaixaEndpoint(
        CriarCaixaRequest request,
        IMovingService movingService)
    {
        try
        {
            var caixa = await movingService.CriarCaixaAsync(request);
            return Results.Created($"/api/caixas/{caixa.Id}", caixa);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { erro = ex.Message });
        }
    }

    private static async Task<IResult> AdicionarItemEndpoint(
        AdicionarItemRequest request,
        IMovingService movingService)
    {
        try
        {
            var item = await movingService.AdicionarItemAsync(request);
            return Results.Created($"/api/itens/{item.Id}", item);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { erro = ex.Message });
        }
    }

    private static async Task<IResult> BuscarItemEndpoint(
        BuscarItemRequest request,
        IMovingService movingService,
        IChefService? chefService)
    {
        if (string.IsNullOrWhiteSpace(request.TermoBusca))
            return Results.BadRequest(new { erro = "Termo de busca é obrigatório" });

        if (chefService != null && !string.IsNullOrEmpty(chefService.ToString()))
        {
            try
            {
                var caixas = await movingService.GetAllCaixasAsync();
                var prompt = MovingAgent.BuildSearchPrompt(request.TermoBusca, caixas);
                var respostaIA = await chefService.GetSuggestionAsync(prompt);
                return Results.Ok(new { respostaIA });
            }
            catch
            {
                var result = await movingService.BuscarItemAsync(request.TermoBusca);
                return Results.Ok(result);
            }
        }
        else
        {
            var result = await movingService.BuscarItemAsync(request.TermoBusca);
            return Results.Ok(result);
        }
    }
}