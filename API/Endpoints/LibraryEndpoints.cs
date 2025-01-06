using MusicStore.API.Controllers;

namespace MusicStore.API.Endpoints;

public static class LibraryEndpoints
{
    public static void MapLibraryRoutes(this WebApplication app)
    {
        RouteGroupBuilder group = app.MapGroup("/library");

        group.MapGet("/", () => Results.Redirect("/library/tracks"));
        
        group.MapGet("/tracks", LibraryController.GetAllTracks).RequireAuthorization();
        
        group.MapPost("/tracks", LibraryController.AddTrack).RequireAuthorization();
        
        group.MapDelete("/tracks", LibraryController.RemoveTrack).RequireAuthorization();
        
        
        
        group.MapGet("/albums", LibraryController.GetAllAlbums).RequireAuthorization();
        
        group.MapPost("/albums", LibraryController.AddAlbum).RequireAuthorization();
        
        group.MapDelete("/albums", LibraryController.RemoveAlbum).RequireAuthorization();
    }
}