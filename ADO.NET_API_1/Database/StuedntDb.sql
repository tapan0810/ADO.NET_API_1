CREATE DATABASE StuedntDb;
GO

USE StudentDb;
GO

CREATE TABLE Stuednt
(
Id INT IDENTITY(1,1) PRIMARY KEY,
FirstName NVARCHAR(100) NOT NULL,
LastName NVARCHAR(100) NOT NULL,
Email NVARCHAR(255) NOT NULL UNIQUE,
Age INT NOT NULL,
Department NVARCHAR(100) NOT NULL
);
GO

INSERT INTO Stuednt (FirstName, LastName, Email, Age, Department)
VALUES ('John', 'Doe', 'john.doe@example.com', 25, 'Computer Science'),
	   ('Jane', 'Smith', 'jane.smith@example.com', 22, 'Mathematics');
	   GO