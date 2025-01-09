MusicStore is a small backend REST API made with ASP.NET (C#), MongoDB and JWT auth. The Requests.http file includes all available requests.

The project relies on environment variables which can be changed in Properties/launchSettings.json

API has the following structure:
- Models: includes object representations that are stored in the Mongo database.
- Endpoint: maps endpoints that accept REST request from a client (can be tested by executing requests in Requests.http if used with Rider).
- Controllers: classes and methods that handle requests from a client (used by endpoints).

MongoDB contains the following collections:
- users: information about user accounts and libraries attached to them: email (used as login), password (stored as hash), field indicating if user is an admin, libraries for music tracks and albums respectively (stores only ids of the tracks in the store).
- tracks: information about music track: title, artist, year, length.
- albums: information about albums: title, artist, year, number of tracks, array of tracks (track ids).
