__MusicStore__ is a backend RESTful API developed using ASP.NET (C#), MongoDB, and JWT-based authentication. The API provides functionalities to manage users, music tracks, and albums, supporting operations like data retrieval, user authentication, and resource management.

A `Requests.http` file is included to facilitate testing of available endpoints. This file can be utilized in IDEs like JetBrains Rider to execute and verify requests during development.

The application uses environment variables for configuration. They can be changed in `Properties/launchSettings.json`

The API is organized into the following key components:
- Models
  -  Contains object representations of entities stored in the MongoDB database.
  -  These classes define the schema and relationships of data within the system.
- Endpoints
  -  Maps the API routes to specific HTTP methods (GET, POST, PUT, PATCH, DELETE, etc.).
  -  Acts as the interface layer between the client and the API logic.
  -  Endpoints can be tested using the Requests.http file for immediate feedback.
- Controllers
  -  Includes classes and methods that handle the logic for incoming requests.
  -  Controllers process input, interact with the data layer (via Models), and return appropriate responses.

## API Endpoints
On local host use: `http://localhost:5133/` or `https://localhost:7073` (port can be changed in `Properties/launchSettings.json`)

Register & Login 
| Method | Route                  | Description                                      |
|--------|------------------------|--------------------------------------------------|
| POST   | /signup                | registers new users                              |
| POST   | /login                 | logins into user account                         |

Store
| Method | Route                  | Description                                      |
|--------|------------------------|--------------------------------------------------|
| GET    | /store/tracks          | returns array of tracks in the store             |
| GET    | /store/tracks/{id}     | returns a track in the store by id               |
| PATCH  | /store/tracks/{id}     | updates track specified by id                    |
| POST   | /store/tracks/         | creates & returns new track                      |
| DELETE | /store/tracks/{id}     | deletes track specified by id                    |
| GET    | /store/albums          | returns array of albums in the store             |
| GET    | /store/albums/{id}     | returns an album in the store by id              |
| PATCH  | /store/albums/{id}     | updates album specified by id                    |
| POST   | /store/albums/         | creates & returns new album                      |
| DELETE | /store/albums/{id}     | deletes album specified by id                    |


Library
| Method | Route                  | Description                                      |
|--------|------------------------|--------------------------------------------------|
| GET    | /library/tracks        | returns array of tracks in the user's library    |
| POST   | /library/tracks        | adds new track to the user's library             |
| DELETE | /library/tracks        | removes a track from the user's library          |
| GET    | /library/albums        | returns array of albums in the user's library    |
| POST   | /library/albums        | adds new album to the user's library             |
| DELETE | /library/albums        | removes an album from the user's library         |

## Register & Login Endpoints
### Register Endpoint
```js
POST /signup
```
Expected Body 
```js
{
    "email": "someone@gmail.com", // string, required
    "password": "pass123", // string, required
}
```

Expected Response
```js
"Added new user with id 677ab7f48618531662841e0e."
```

### Login Endpoint
```js
POST /login
```
Expected Body
```js
{
    "email": "someone@gmail.com", //string, required
    "password": "pass123", //string, required
}
```
Expected Response
```js
"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWJqZWN0IjoxMiwidXNlcm5hbWUiOiJuZXdfdXNlcjEyMTIxMiIsImlhdCI6MTU5ODQyMDg0NywiZXhwIjoxNTk4NDI4MDQ3fQ.YyR_rrRxYaDVTt3FPM155hPwbUAEFhyaDSOWqVOD8kM"
```

## Store Endpoints
### GET All tracks
```js
GET /store/tracks
```
Expected Response
```js
[
    {
    	"id": "6779d77866272f6123d04e35",
    	"title": "Hell Is Where The Heart Is",
    	"artist": "Get Scared",
    	"year": 2019,
    	"length": 213
    },
    ...
]
```

### GET Track by ID
```js
GET /store/tracks/{id}
```
Expected Response
```js
  {
    "id": "6779a1736141e8aa89d05aa4",
    "title": "Ricochet",
    "artist": "Starset",
    "year": 2017,
    "length": 310
  }
```

### PATCH Track By ID
```js
PATCH /store/tracks/{id}
```
Expected Body
```js
{
   "title": "Track Title", // string, optional
   "artist": "Artist who created the track", // string, optional
   "year": 2025, // int, optional
   "length": 228 // length in seconds, int, optional
}
```
Expected Response
```js
"Track with id {id} successfully updated."
```

### POST Track
```js
POST /store/tracks
```
Expected Body
```js
{
   "title": "Track Title", // string, required
   "artist": "Artist who created the track", // string, required
   "year": 2025, // int, required
   "length": 228 // length in seconds, int, required
}
```
Expected Response
```js
{
    "message": "Added new track.",
    "data": {
      "id": "6779d77866272f6123d04e35",
      "title": "Track Title",
      "artist": "Artist who created the track",
      "year": 2025,
      "length": 228
    }
}
```

### DELETE track by ID
```js
DELETE /store/tracks/{id}
```
Expected Response: 
```js
"Track with id {id} successfully deleted."
```
