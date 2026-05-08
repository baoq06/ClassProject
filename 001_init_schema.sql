USE master;
GO

IF DB_ID('LoginDB') IS NOT NULL
BEGIN
    ALTER DATABASE LoginDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE LoginDB;
END
GO

CREATE DATABASE LoginDB;
GO

USE LoginDB;
GO

CREATE TABLE Roles (
    Id INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    RoleId INT NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RoleId) REFERENCES Roles(Id)
);

CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(100) NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    Stock INT DEFAULT 0
);

CREATE TABLE Orders (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    OrderDate DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(18,2) DEFAULT 0,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE OrderDetails (
    Id INT PRIMARY KEY IDENTITY(1,1),
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    UnitPrice DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(Id) ON DELETE CASCADE,
    FOREIGN KEY (ProductId) REFERENCES Products(Id)
);

INSERT INTO Roles (RoleName) VALUES ('Admin'), ('User');

INSERT INTO Users (Username, Password, RoleId) VALUES 
('admin', '123', 1),
('user1', '123', 2),
('user2', '123', 2);

INSERT INTO Products (ProductName, Price, Stock) VALUES
('Laptop', 1500, 10),
('Mouse', 20, 100),
('Keyboard', 50, 50),
('Monitor', 300, 20);

INSERT INTO Orders (UserId) VALUES (1), (2), (3);

INSERT INTO OrderDetails (OrderId, ProductId, Quantity, UnitPrice) VALUES
(1, 1, 1, 1500),
(1, 2, 2, 20),
(2, 3, 1, 50),
(3, 4, 1, 300);

SELECT * FROM Users;