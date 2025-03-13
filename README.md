Movie and Series Social Platform

A full-stack web application designed for a vibrant community of film and series enthusiasts. This platform offers a wide range of features from user management and content browsing to interactive group quizzes.

Features
User Management:

User registration, login, and profile updates using ASP.NET Identity.
Extended user profiles with additional properties (e.g., registration date, bio).

Content Management:

Display films and series with details such as title, description, release year, IMDb rating, trailer, and poster.
CRUD operations for managing content.

Rating and Commenting:

Users can rate films/series on a 1-to-10 scale.
Threaded comments with support for replies and soft deletion (hiding the comment text instead of deleting the record).

Favorites & Watchlist:

Mark content as favorites.
Create and manage a personalized watchlist.

Genre-Based Search:

Filter content by genres using many-to-many relationships.

Group Quiz Functionality:

Create quiz groups and invite participants.
Conduct group quizzes with sessions and votes to recommend films/series.

Technology Stack

Backend: ASP.NET Core with Entity Framework Core (Code-First Migrations)

User Management: ASP.NET Core Identity

Mapping: AutoMapper (for DTO to entity mappings)

Database: SQL Server

Asynchronous Operations: All service methods are implemented asynchronously to ensure scalability and responsiveness.
