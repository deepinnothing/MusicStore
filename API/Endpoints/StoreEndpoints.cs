using MusicStore.API.Controllers;

namespace MusicStore.API.Endpoints;

public static class StoreEndpoints
{
    public static void MapStoreRoutes(this WebApplication app)
    {
        RouteGroupBuilder group = app.MapGroup("/store");
        
        // Endpoints for tracks in the store

        group.MapGet("/", () => Results.Redirect("/store/tracks"));

        group.MapPost("/tracks", StoreController.AddNewTrack).RequireAuthorization("Admin");
        
        group.MapGet("/tracks", StoreController.GetAllTracks);

        group.MapPatch("/tracks/{id}", StoreController.ModifyTrack).RequireAuthorization("Admin");

        group.MapDelete("/tracks/{id}", StoreController.DeleteTrack).RequireAuthorization("Admin");

        group.MapGet("/tracks/{id}", StoreController.GetTrackById);
        
        // Endpoints for albums in the store

        group.MapPost("/albums", StoreController.AddNewAlbum).RequireAuthorization("Admin");
        
        group.MapGet("/albums", StoreController.GetAllAlbums);

        group.MapPatch("/albums/{id}", StoreController.ModifyAlbum).RequireAuthorization("Admin");

        group.MapDelete("/albums/{id}", StoreController.DeleteAlbum).RequireAuthorization("Admin");

        group.MapGet("/albums/{id}", StoreController.GetAlbumById);
    }
}