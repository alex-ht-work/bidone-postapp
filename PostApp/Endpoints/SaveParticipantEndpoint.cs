using Microsoft.AspNetCore.Mvc;
using PostApp.Models;
using PostApp.Services;

namespace PostApp.Endpoints;

public static class SaveParticipantEndpoint
{
    public static void MapSaveParticipantEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("api/save", SaveParticipant);
    }

    private static async Task SaveParticipant([FromBody] Participant participant, [FromServices] IRepository repo)
    {
        await repo.Save(participant);
    }
}