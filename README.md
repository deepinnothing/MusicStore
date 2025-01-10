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

[Register & Login](#register--login-endpoints) 
| Method | Route                  | Description                                      | Access Level    |
|--------|------------------------|--------------------------------------------------|-----------------|
| POST   | /signup                | registers new users                              | Everyone        |
| POST   | /login                 | logins into user account                         | Everyone        |

[Store](#store-endpoints)
| Method | Route                  | Description                                      | Access Level    |
|--------|------------------------|--------------------------------------------------|-----------------|
| GET    | /store/tracks          | returns array of tracks in the store             | Everyone        |
| GET    | /store/tracks/{id}     | returns a track in the store by id               | Everyone        |
| PATCH  | /store/tracks/{id}     | updates track specified by id                    | Admin           |
| POST   | /store/tracks/         | creates & returns new track                      | Admin           |
| DELETE | /store/tracks/{id}     | deletes track specified by id                    | Admin           |
| GET    | /store/albums          | returns array of albums in the store             | Everyone        |
| GET    | /store/albums/{id}     | returns an album in the store by id              | Everyone        |
| PATCH  | /store/albums/{id}     | updates album specified by id                    | Admin           |
| POST   | /store/albums/         | creates & returns new album                      | Admin           |
| DELETE | /store/albums/{id}     | deletes album specified by id                    | Admin           |


[Library](#library-endpoints)
| Method | Route                  | Description                                      | Access Level       |
|--------|------------------------|--------------------------------------------------|--------------------|
| GET    | /library/tracks        | returns array of tracks in the user's library    | Authenticated User |
| POST   | /library/tracks        | adds new track to the user's library             | Authenticated User |
| DELETE | /library/tracks        | removes a track from the user's library          | Authenticated User |
| GET    | /library/albums        | returns array of albums in the user's library    | Authenticated User |
| POST   | /library/albums        | adds new album to the user's library             | Authenticated User |
| DELETE | /library/albums        | removes an album from the user's library         | Authenticated User |

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
### GET all tracks
```js
GET /store/tracks
```
Expected Response
```js
[
    {
      "id": "6779a1736141e8aa89d05aa4",
      "title": "Ricochet",
      "artist": "Starset",
      "year": 2017,
      "length": 310
    },
    ...
]
```

### GET track by ID
```js
GET /store/tracks/{id}
```
Expected Response
```js
{
  "id": "6779d77866272f6123d04e35",
  "title": "Hell Is Where The Heart Is",
  "artist": "Get Scared",
  "year": 2019,
  "length": 213
}
```

### PATCH track By ID
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

### GET all albums
```js
GET /store/albums
```
Expected Response
```js
[
    {
      "_id": "677c4e1e5e793a5b3903b935",
      "title": "Graveyard Shift",
      "artist": "Motionless In White",
      "year": 2017,
      "tracks_number": 12
    },
    ...
]
```

### GET album by ID
```js
GET /store/albums/{id}
```
Expected Response
```js
{
  "_id": "677c4e1e5e793a5b3903b935",
  "title": "Graveyard Shift",
  "artist": "Motionless In White",
  "year": 2017,
  "tracks": [
    {
      "_id": "677c0b91dc41299ee32f704d",
      "title": "Rats",
      "artist": "Motionless In White",
      "year": 2017,
      "length": 236
    },
    .........
    {
      "_id": "677c4d355e793a5b3903b934",
      "title": "Eternally Yours",
      "artist": "Motionless In White",
      "year": 2017,
      "length": 313
    }
  ],
  "tracks_number": 12
}
```

### PATCH album By ID
```js
PATCH /store/albums/{id}
```
Expected Body
```js
{
   "title": "Album Title", // string, optional
   "artist": "Artist who created the album", // string, optional
   "year": 2025, // int, optional
   "tracks": [
      "677c4d355e793a5b3903b934",
      .....
    ]  // array of track ids, optional
}
```
Expected Response
```js
"Album with id {id} successfully updated."
```

### POST album
```js
POST /store/albums
```
Expected Body
```js
{
   "title": "Album Title", // string, required
   "artist": "Artist who created the album", // string, required
   "year": 2025, // int, required
   "tracks": [
      "677c4d355e793a5b3903b934",
      .....
    ]  // array of track ids, required
}
```
Expected Response
```js
{
    "message": "Added new album.",
    "data": {
      "id": "6779d77866272f6123d04e35",
      "title": "Album Title",
      "artist": "Artist who created the album",
      "year": 2025,
      "tracks": [
        "677c4d355e793a5b3903b934",
        .....
      ],
      "tracks_number": 12
    }
}
```

### DELETE album by ID
```js
DELETE /store/albums/{id}
```
Expected Response: 
```js
"Album with id {id} successfully deleted."
```

## Library Endpoints
### GET tracks from user's library
```js
GET /library/tracks
```
Expected Response
```js
[
    {
      "id": "6779a1736141e8aa89d05aa4",
      "title": "Ricochet",
      "artist": "Starset",
      "year": 2017,
      "length": 310
    },
    ...
]
```

### POST track to user's library
```js
POST /library/tracks
```
Expected Body
```js
"6779a1736141e8aa89d05aa4"
```
Expected Response
```js
"Track with id 6779a1736141e8aa89d05aa4 successfully added to the library."
```

### DELETE track by ID
```js
DELETE /library/tracks
```
Expected Body
```js
"6779a1736141e8aa89d05aa4"
```
Expected Response: 
```js
"Track with id 6779a1736141e8aa89d05aa4 successfully removed from the library."
```

### GET albums from user's library
```js
GET /store/albums
```
Expected Response
```js
[
    {
      "_id": "677c4e1e5e793a5b3903b935",
      "title": "Graveyard Shift",
      "artist": "Motionless In White",
      "year": 2017,
      "tracks_number": 12
    },
    ...
]
```

### POST album to user's library
```js
POST /library/albums
```
Expected Body
```js
"677c4e1e5e793a5b3903b935"
```
Expected Response
```js
"Album with id 677c4e1e5e793a5b3903b935 successfully added to the library."
```

### DELETE album from user's library
```js
DELETE /library/albums
```
Expected Body
```js
"677c4e1e5e793a5b3903b935"
```
Expected Response: 
```js
"Album with id 677c4e1e5e793a5b3903b935 successfully removed from the library."
```
