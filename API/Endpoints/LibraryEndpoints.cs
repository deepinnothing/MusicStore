using MusicStore.API.Controllers;

namespace MusicStore.API.Endpoints;

public static class LibraryEndpoints
{
    public static void MapLibraryRoutes(this WebApplication app)
    {
        RouteGroupBuilder group = app.MapGroup("/library");

        group.MapGet("/", () => Results.Redirect("/library/tracks"));
        
        // Endpoints for tracks in the library
        
        group.MapGet("/tracks", LibraryController.GetAllTracks).RequireAuthorization();
        
        group.MapPost("/tracks", LibraryController.AddTrack).RequireAuthorization();
        
        group.MapDelete("/tracks", LibraryController.RemoveTrack).RequireAuthorization();
        
        // Endpoints for albums in the library
        
        group.MapGet("/albums", LibraryController.GetAllAlbums).RequireAuthorization();
        
        group.MapPost("/albums", LibraryController.AddAlbum).RequireAuthorization();
        
        group.MapDelete("/albums", LibraryController.RemoveAlbum).RequireAuthorization();
    }
}