
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/09/2015 21:10:38
-- Generated from EDMX file: C:\Users\David\Source\RestFramework\DataModel\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [scw];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[OUmember]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OUmember];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'OUmembers'
CREATE TABLE [dbo].[OUmembers] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Firstname] nvarchar(50)  NULL,
    [Lastname] nvarchar(50)  NULL,
    [Age] int  NULL,
    [OUmemberUser_OUmember_Id] bigint  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL,
    [OUmemberId] bigint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'OUmembers'
ALTER TABLE [dbo].[OUmembers]
ADD CONSTRAINT [PK_OUmembers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [OUmemberUser_OUmember_Id] in table 'OUmembers'
ALTER TABLE [dbo].[OUmembers]
ADD CONSTRAINT [FK_OUmemberUser]
    FOREIGN KEY ([OUmemberUser_OUmember_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OUmemberUser'
CREATE INDEX [IX_FK_OUmemberUser]
ON [dbo].[OUmembers]
    ([OUmemberUser_OUmember_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------