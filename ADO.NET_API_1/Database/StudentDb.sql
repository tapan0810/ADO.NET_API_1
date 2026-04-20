CREATE DATABASE StudentDb;
GO

USE StudentDb;
GO
DROP TABLE IF EXISTS Students;

CREATE TABLE Students
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    Email NVARCHAR(255),
    Age INT,
    Department NVARCHAR(100)
);
GO

INSERT INTO Students (FirstName, LastName, Email, Age, Department)
VALUES 
('John', 'Doe', 'john.doe@example.com', 25, 'Computer Science'),
('Jane', 'Smith', 'jane.smith@example.com', 22, 'Mathematics');
GO