# Playlist Application

Playlist Application is a REST Api with CRUD operations of songs and playlists, and also the possiblity to create relationships between them. 

## Features

- .NET 6 WebApi
- FluentValidation
- Dapper
- XUnit
- Swagger Api Documentation

## Configuration

The project uses SQL Server as its database. Please, execute the script [db_creation.sql](https://github.com/kamila-almeida/PlaylistProject/blob/develop/PlaylistApplication.API/Scripts/db_creation.sql) that is inside the folder ```.\PlaylistProject\PlaylistApplication.API\Scripts``` to create the database and tables. You can use the built-in SQL Server Object Explorer to connect to the localdb, and execute the script.

## Project Execution (cmd)

> Clone this repository

> Access the folder ```.\PlaylistProject\PlaylistApplication.API\``` and run 
```dotnet run```

> Access the API on ```localhost:5001/swagger```

## Unit Tests Execution (cmd)
> Clone this repository

> Access the folder ```.\PlaylistProject\PlaylistApplication.UnitTests\``` and run ```dotnet test```

## Documentation
You can import the [swagger.json](https://github.com/kamila-almeida/PlaylistProject/blob/develop/swagger.json) file on [editor-next.swagger.io](https://editor-next.swagger.io/) to visualize the API documentation without the need of running the project.

![image](https://user-images.githubusercontent.com/49010603/189496711-e53f0c23-13b6-4003-8b23-f4b711ca51a9.png)

## Postman Collection
You can import the [Playlist API - V1.postman_collection.json](https://github.com/kamila-almeida/PlaylistProject/blob/develop/Playlist%20API%20-%20V1.postman_collection.json) and [Playlist API Environment.postman_environment.json](https://github.com/kamila-almeida/PlaylistProject/blob/develop/Playlist%20API%20Environment.postman_environment.json) files inside Postman and test the endpoints with payload examples.

## Observations

As this application is very simple I chose to put all the layers under the same project but in a real-life scenario I would separate the layers in different class libraries as suggested by the Domain Driven Design and Clean Architecture approaches. The main layers would be:

- **API:** contains the controllers and project configurations
- **Application:** contains the business logic
- **Domain:** contains the models and entities
- **Infrastructure:** contains the repositories and the database access context

The validations using FluentValidator were implemented in the controllers in order to avoid more complexity by adding a new layer. In a real-life scenario I would add these validations in the Application layer and I would create a middleware to manage the responses and errors.
