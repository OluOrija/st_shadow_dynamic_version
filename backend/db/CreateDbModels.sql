-- Script to create database models for NavigationMenu, SectionContent, and MediaAssets

-- Create NavigationMenu table
CREATE TABLE NavigationMenu (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Url NVARCHAR(500) NOT NULL,
    [Order] INT NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);
GO

-- Create SectionContent table
CREATE TABLE SectionContent (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    ImageUrl NVARCHAR(500) NULL,
    [Order] INT NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);
GO

-- Create MediaAssets table
CREATE TABLE MediaAssets (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FileName NVARCHAR(255) NOT NULL,
    FilePath NVARCHAR(500) NOT NULL,
    UploadedDate DATETIME DEFAULT GETDATE(),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);
GO

-- Add triggers to update UpdatedAt column on updates
-- NavigationMenu trigger
CREATE TRIGGER trg_Update_NavigationMenu_UpdatedAt
ON NavigationMenu
AFTER UPDATE
AS
BEGIN
    UPDATE NavigationMenu
    SET UpdatedAt = GETDATE()
    WHERE Id IN (SELECT DISTINCT Id FROM Inserted);
END;
GO

-- SectionContent trigger
CREATE TRIGGER trg_Update_SectionContent_UpdatedAt
ON SectionContent
AFTER UPDATE
AS
BEGIN
    UPDATE SectionContent
    SET UpdatedAt = GETDATE()
    WHERE Id IN (SELECT DISTINCT Id FROM Inserted);
END;
GO

-- MediaAssets trigger
CREATE TRIGGER trg_Update_MediaAssets_UpdatedAt
ON MediaAssets
AFTER UPDATE
AS
BEGIN
    UPDATE MediaAssets
    SET UpdatedAt = GETDATE()
    WHERE Id IN (SELECT DISTINCT Id FROM Inserted);
END;
GO
