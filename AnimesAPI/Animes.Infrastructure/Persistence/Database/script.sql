CREATE DATABASE AnimeDatabase;
GO

USE AnimeDatabase;
GO

CREATE TABLE Directors (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL
);
GO

CREATE TABLE Studios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL
);
GO

CREATE TABLE Genres (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL UNIQUE
);
GO


CREATE TABLE Animes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    Summary NVARCHAR(MAX) NOT NULL,
    ReleaseYear INT,
    EpisodeCount INT,
    Score DECIMAL(4, 2),
    DirectorId INT,
    StudioId INT,
    CONSTRAINT FK_Animes_Directors FOREIGN KEY (DirectorId) REFERENCES Directors(Id),
    CONSTRAINT FK_Animes_Studios FOREIGN KEY (StudioId) REFERENCES Studios(Id)
);
GO


CREATE TABLE Anime_Genres (
    AnimeId INT NOT NULL,
    GenreId INT NOT NULL,
    CONSTRAINT PK_Anime_Genres PRIMARY KEY (AnimeId, GenreId),
    CONSTRAINT FK_AnimeGenres_Animes FOREIGN KEY (AnimeId) REFERENCES Animes(Id),
    CONSTRAINT FK_AnimeGenres_Genres FOREIGN KEY (GenreId) REFERENCES Genres(Id)
);
GO

