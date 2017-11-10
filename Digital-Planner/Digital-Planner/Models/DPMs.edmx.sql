
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/10/2017 02:15:26
-- Generated from EDMX file: C:\Users\techs\Source\Repos\540-Digital-Planner\Digital-Planner\Digital-Planner\Models\DPMs.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [calendar];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Category_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Categories] DROP CONSTRAINT [FK_Category_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Day_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Days] DROP CONSTRAINT [FK_Day_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Event_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_Event_Category];
GO
IF OBJECT_ID(N'[dbo].[FK_Event_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_Event_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories];
GO
IF OBJECT_ID(N'[dbo].[Days]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Days];
GO
IF OBJECT_ID(N'[dbo].[Events]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Description] varchar(50)  NOT NULL,
    [UserID] int  NOT NULL
);
GO

-- Creating table 'Days'
CREATE TABLE [dbo].[Days] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [HoursAvailable] time  NOT NULL,
    [WorkStarts] time  NOT NULL,
    [Date] datetime  NOT NULL,
    [UserID] int  NOT NULL
);
GO

-- Creating table 'Events'
CREATE TABLE [dbo].[Events] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] varchar(50)  NOT NULL,
    [OccursAt] datetime  NOT NULL,
    [Duration] time  NOT NULL,
    [CompleteBy] datetime  NOT NULL,
    [Priority] int  NOT NULL,
    [IsComplete] bit  NOT NULL,
    [AutoAssign] bit  NOT NULL,
    [Location] varchar(50)  NULL,
    [UserID] int  NOT NULL,
    [CategoryID] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [FirstName] varchar(50)  NOT NULL,
    [LastName] varchar(50)  NOT NULL,
    [Email] varchar(50)  NOT NULL,
    [Password] varchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Days'
ALTER TABLE [dbo].[Days]
ADD CONSTRAINT [PK_Days]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [PK_Events]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserID] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [FK_Category_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Category_User'
CREATE INDEX [IX_FK_Category_User]
ON [dbo].[Categories]
    ([UserID]);
GO

-- Creating foreign key on [UserID] in table 'Days'
ALTER TABLE [dbo].[Days]
ADD CONSTRAINT [FK_Day_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Day_User'
CREATE INDEX [IX_FK_Day_User]
ON [dbo].[Days]
    ([UserID]);
GO

-- Creating foreign key on [UserID] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [FK_Event_User]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Event_User'
CREATE INDEX [IX_FK_Event_User]
ON [dbo].[Events]
    ([UserID]);
GO

-- Creating foreign key on [CategoryID] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [FK_Event_Category]
    FOREIGN KEY ([CategoryID])
    REFERENCES [dbo].[Categories]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Event_Category'
CREATE INDEX [IX_FK_Event_Category]
ON [dbo].[Events]
    ([CategoryID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------