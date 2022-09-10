CREATE DATABASE [playlistdb]
GO

USE [playlistdb]
GO

CREATE TABLE Song (Id int PRIMARY KEY IDENTITY(1,1) NOT NULL, Name varchar(max) NOT NULL, Author varchar(max) NOT NULL)
GO

CREATE TABLE Playlist (Id int PRIMARY KEY IDENTITY(1,1) NOT NULL, Name varchar(max) NOT NULL, Description varchar(max) NOT NULL)
GO

CREATE TABLE SongsPlaylists (SongId int NOT NULL FOREIGN KEY REFERENCES Song(Id), PlaylistId int NOT NULL FOREIGN KEY REFERENCES Playlist(Id), PRIMARY KEY(SongId, PlaylistId))