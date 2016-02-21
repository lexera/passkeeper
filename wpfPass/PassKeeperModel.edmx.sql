
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/29/2016 08:05:34
-- Generated from EDMX file: C:\Users\Alex\Desktop\IT\ШАГ study\ADO.net\wpfPass\wpfPass\PassKeeperModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Passkeeper.Main];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserPasswordSet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PasswordSets] DROP CONSTRAINT [FK_UserPasswordSet];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[PasswordSets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PasswordSets];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PasswordSets'
CREATE TABLE [dbo].[PasswordSets] (
    [SetId] int IDENTITY(1,1) NOT NULL,
    [Location] nvarchar(50)  NOT NULL,
    [Login] nvarchar(50)  NOT NULL,
    [Password] nvarchar(100)  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [DateChanged] datetime  NOT NULL,
    [Status] nvarchar(50)  NOT NULL,
    [UserUserId] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(50)  NOT NULL,
    [MasterPassword] nvarchar(100)  NOT NULL,
    [Email] nvarchar(50)  NOT NULL,
    [Status] nvarchar(50)  NOT NULL,
    [DateCreated] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [SetId] in table 'PasswordSets'
ALTER TABLE [dbo].[PasswordSets]
ADD CONSTRAINT [PK_PasswordSets]
    PRIMARY KEY CLUSTERED ([SetId] ASC);
GO

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserUserId] in table 'PasswordSets'
ALTER TABLE [dbo].[PasswordSets]
ADD CONSTRAINT [FK_UserPasswordSet]
    FOREIGN KEY ([UserUserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserPasswordSet'
CREATE INDEX [IX_FK_UserPasswordSet]
ON [dbo].[PasswordSets]
    ([UserUserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------