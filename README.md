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
    	"id": "6779a1736141e8aa89d05aa4",
    	"title": "Ricochet",
    	"artist": "Starset",
    	"year": 2017,
    	"length": 310
    },
    ...
]
```

### GET Track by ID
```js
GET /store/tracks/{id}
```
Expected Response
```
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
```
    {
        "title": "Track Title", // string, optional
        "artist": "Artist who created the track", // string, optional
        "year": 2025, // int, optional
        "length": 228 // length in seconds, int, optional
    }
```
Expected Response
```
"Track with id {id} successfully updated."
```

### POST Track
```js
POST /store/tracks
```
Expected Body
```
    {
        "title": "Track Title", // string, required
        "artist": "Artist who created the track", // string, required
        "year": 2025, // int, required
        "length": 228 // length in seconds, int, required
    }
```
Expected Response
```
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

### DELETE campaign by ID
```js
DELETE /store/tracks/{id}
```
Expected Response: 
```
"Track with id {id} successfully deleted."
```

### GET users
```js
GET /api/users

Expected Response: returns array of all users

Expected Response: 
[
    {
        "id": 1,
        "username": "nicopico",
        "email": "chiku524@icloud.com",
        "age": 25,
        "password": "$2a$15$V.9IqAyj.cOEktdkjBd62OUH9J2ylz80KAerUB9pFcysi7uspAGYy"
    },
    {
        "id": 2,
        "username": "testing",
        "email": "testing@gmail.com",
        "age": 25,
        "password": "$2a$15$olkyTbtgk25E6onHRkAWwe4t8RSNv2kkRhMwj3dNFSAzOKbFxydkW"
    }
]
```

### GET User by user ID
```js
GET /api/users/:id

Expected Response: returns stories created by user specified by :id

Expected Response:
    {
        "id": 2,
        "username": "testing",
        "email": "testing@gmail.com",
        "age": 25,
        "password": "$2a$15$e9k2JvlYn.FF3ivK/qiCMewx/3OhtHO8Dwf755Pu7QlwQvQ7ixnxu"
    }   
```

### Update User info
```js
PUT /api/users/:id/

Expected Body:

    {
        "age": 100
    }

Expected Response:  updates user info specified by id
Expected Response

    {
        "updated": 1
    }
```

### Delete
```js
DELETE /api/users/:id

Expected Response: deletes user specified by :id

Expected Response: 
    {
        "removed": 1
    }
```

## Metrics Endpoints
### Post Metrics to DS API
```js
POST /api/campaigns/:id/metrics

Expected Body:

    {
        "item": "going to make cars fly"
    }

Expected Response:  Prediction of success in a campaign based off of description
Expected Response:

{
    "description": {
        "item": "why is my description now showing up"
    },
    "prediction": {
        "success_failure": "0"
    },
    "campaign_id": "2"
}
```

<!-- ### Get All Stories by ID
```js
GET /api/stories/:id

Expected Response: Lists stories specified by :id

Expected Response:
    {
        "id": 1,
        "storyName": "Chinatown",
        "photoLink": "https://i.ibb.co/DVN5Lnx/20200322-213304.jpg",
        "user_id": 1,
        "stories_id": 1
    }
```

### PUT Story by ID
```js
PUT /api/stories/:id

Expected Body:
    {
        "storyName": "Updated Story Name" //updated field
    }

Expected Response: updates story specified by :id

Expected Response:
"story": [
	        {
		        "id": 5,
		        "storyName": "Updated Story Name",
		        "storyCity": "test23",
		        "storyCountry": "Thailand",
		        "storyDate": "2020-08-28 03:12:34",
		        "storyPhoto": "test photo",
		        "storyDesc": "testDesc",
		        "user_id": 1
			    }
        ]
```

### DELETE Story by ID
```js
DELETE /api/stories/:id

Expected Response: deletes story specified by :id

Expected Response: 
    {
        "removed": 1
    }
```

## Photos Endpoints
### GET ALL Photos
```js
GET /api/photos

Expected Response: List of photos in database

Expected Response:
[
    {
        "id": 1,
        "photoLink": "https://i.ibb.co/DVN5Lnx/20200322-213304.jpg",
        "photoDesc": "Out for dinner",
        "photoDate": "2020-08-28T02:49:30.529Z",
        "stories_id": 1
    },
    {
        "id": 3,
        "photoLink": "https://i.ibb.co/RTZNzfX/20200821-093310.jpg",
        "photoDesc": "Enjoy a moment of relaxation in Pattaya",
        "photoDate": "2020-08-28T02:49:30.529Z",
        "stories_id": 3
    }
]
```

### GET Photos By ID
```js
GET /api/photos/:id

### Expected Response: Photo that matches Users ID

Expected Response
    {
        "id": 1,
        "photoLink": "https://i.ibb.co/DVN5Lnx/20200322-213304.jpg",
        "photoDesc": "Out for dinner",
        "photoDate": "2020-08-28T02:49:30.529Z",
        "stories_id": 1
    }
```

### POST new photo
```js
POST /api/photos

Expected Body:

    {
        "photoLink": "test",
        "photoDesc": "test",
        "stories_id": 1
    }


Expected Response:  creates & returns new photo
Expected Response

[
    {
        "id": 4,
        "photoLink": "test",
        "photoDesc": "test",
        "photoDate": "2020-08-28T05:30:33.313Z",
        "stories_id": 1
    }
]
```

### PUT Photo by ID
```js
PUT /api/photos/:id

Expected Body:
    {
        "photoLink": "updates example" //updated field
    }

Expected Response: updates photo specified by :id

Expected Response:
"story": [
	        {
		        "id": 4,
                "photoLink": "updates example",
                "photoDesc": "test",
                "photoDate": "2020-08-28T05:30:33.313Z",
                "stories_id": 1
            }
        ]
```

 -->
