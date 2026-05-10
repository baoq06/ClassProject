USE master;
GO

IF DB_ID('LoginDB') IS NOT NULL
BEGIN
    ALTER DATABASE LoginDB 
    SET SINGLE_USER 
    WITH ROLLBACK IMMEDIATE;

    DROP DATABASE LoginDB;
END
GO

CREATE DATABASE LoginDB;
GO

USE LoginDB;
GO

-- =========================
-- ROLES TABLE
-- =========================
CREATE TABLE Roles
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50) NOT NULL UNIQUE
);
GO

-- =========================
-- USERS TABLE
-- =========================
CREATE TABLE Users
(
    Id INT PRIMARY KEY IDENTITY(1,1),

    Username NVARCHAR(50) NOT NULL UNIQUE,

    Email NVARCHAR(100) NOT NULL UNIQUE,

    Password NVARCHAR(255) NOT NULL,

    RoleId INT NOT NULL,

    Created_At DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (RoleId)
    REFERENCES Roles(Id)
);
GO

-- =========================
-- INSERT ROLES
-- =========================
INSERT INTO Roles (RoleName)
VALUES
('Admin'),
('User');
GO

-- =========================
-- INSERT USERS
-- =========================
INSERT INTO Users
(
    Username,
    Email,
    Password,
    RoleId
)
VALUES
(
    'admin',
    'admin@gmail.com',
    '1234',
    1
),
(
    'user1',
    'user1@gmail.com',
    '123',
    2
),
(
    'user2',
    'user2@gmail.com',
    '123',
    2
);
GO

-- =========================
-- TEST
-- =========================
SELECT * FROM Roles;
SELECT * FROM Users;
GO

SELECT @@SERVERNAME AS ServerName, DB_NAME() AS CurrentDb;
USE LoginDB;
SELECT * FROM dbo.Users;

SELECT
    s.name AS SchemaName,
    o.name AS ObjectName,
    o.type_desc
FROM sys.objects o
JOIN sys.schemas s ON s.schema_id = o.schema_id
WHERE o.name = N'Users';

SELECT base_object_name
FROM sys.synonyms
WHERE name = N'Users';

SELECT COUNT(*) AS TotalRows FROM dbo.Users;
SELECT * FROM dbo.Users;

SELECT name, is_enabled
FROM sys.security_policies;