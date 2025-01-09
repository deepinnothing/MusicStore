MusicStore is a backend REST API developed using ASP.NET (C#), MongoDB, and JWT-based authentication. The API provides functionalities to manage users, music tracks, and albums, supporting operations like data retrieval, user authentication, and resource management.

A Requests.http file is included to facilitate testing of available endpoints. This file can be utilized in IDEs like JetBrains Rider to execute and verify requests during development.

The application uses environment variables for configuration. They can be changed in Properties/launchSettings.json

The API is organized into the following key components:
- Models
  -  Contains object representations of entities stored in the MongoDB database.
  -  These classes define the schema and relationships of data within the system.
- Endpoints
  -  Maps the API routes to specific HTTP methods (GET, POST, PUT, DELETE, etc.).
  -  Acts as the interface layer between the client and the API logic.
  -  Endpoints can be tested using the Requests.http file for immediate feedback.
- Controllers
  -  Includes classes and methods that handle the logic for incoming requests.
  -  Controllers process input, interact with the data layer (via Models), and return appropriate responses.
