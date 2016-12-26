
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/26/2016 09:25:00
-- Generated from EDMX file: E:\Documents\Master\POSD\Tema1\POSD-Tema1.Data\Database\DbEntities.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [aspnet-POSD-Tema1-20161026095126];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Constrain__RoleI__0E6E26BF]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Constraint] DROP CONSTRAINT [FK__Constrain__RoleI__0E6E26BF];
GO
IF OBJECT_ID(N'[dbo].[FK__Constrain__RoleI__0F624AF8]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Constraint] DROP CONSTRAINT [FK__Constrain__RoleI__0F624AF8];
GO
IF OBJECT_ID(N'[dbo].[FK__Permissio__Permi__04E4BC85]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PermissionToRole] DROP CONSTRAINT [FK__Permissio__Permi__04E4BC85];
GO
IF OBJECT_ID(N'[dbo].[FK__Permissio__Permi__08B54D69]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PermissionForResource] DROP CONSTRAINT [FK__Permissio__Permi__08B54D69];
GO
IF OBJECT_ID(N'[dbo].[FK__Permissio__Resou__09A971A2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PermissionForResource] DROP CONSTRAINT [FK__Permissio__Resou__09A971A2];
GO
IF OBJECT_ID(N'[dbo].[FK__Permissio__RoleI__05D8E0BE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PermissionToRole] DROP CONSTRAINT [FK__Permissio__RoleI__05D8E0BE];
GO
IF OBJECT_ID(N'[dbo].[FK__Resource__Parent__33D4B598]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Resource] DROP CONSTRAINT [FK__Resource__Parent__33D4B598];
GO
IF OBJECT_ID(N'[dbo].[FK__Resource__Resour__31EC6D26]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Resource] DROP CONSTRAINT [FK__Resource__Resour__31EC6D26];
GO
IF OBJECT_ID(N'[dbo].[FK__UserInRol__RoleI__6383C8BA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInRole] DROP CONSTRAINT [FK__UserInRol__RoleI__6383C8BA];
GO
IF OBJECT_ID(N'[dbo].[FK__UserInRol__UserI__628FA481]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInRole] DROP CONSTRAINT [FK__UserInRol__UserI__628FA481];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[__MigrationHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[__MigrationHistory];
GO
IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[Constraint]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Constraint];
GO
IF OBJECT_ID(N'[dbo].[Permission]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Permission];
GO
IF OBJECT_ID(N'[dbo].[PermissionForResource]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PermissionForResource];
GO
IF OBJECT_ID(N'[dbo].[PermissionToRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PermissionToRole];
GO
IF OBJECT_ID(N'[dbo].[Resource]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Resource];
GO
IF OBJECT_ID(N'[dbo].[ResourceType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ResourceType];
GO
IF OBJECT_ID(N'[dbo].[Role]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Role];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
IF OBJECT_ID(N'[dbo].[UserInRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInRole];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'C__MigrationHistory'
CREATE TABLE [dbo].[C__MigrationHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ContextKey] nvarchar(300)  NOT NULL,
    [Model] varbinary(max)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'Constraints'
CREATE TABLE [dbo].[Constraints] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RoleId1] int  NOT NULL,
    [RoleId2] int  NOT NULL
);
GO

-- Creating table 'Permissions'
CREATE TABLE [dbo].[Permissions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(255)  NOT NULL,
    [Read] bit  NULL,
    [Write] bit  NULL
);
GO

-- Creating table 'PermissionForResources'
CREATE TABLE [dbo].[PermissionForResources] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PermissionId] int  NOT NULL,
    [ResourceId] int  NOT NULL
);
GO

-- Creating table 'PermissionToRoles'
CREATE TABLE [dbo].[PermissionToRoles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PermissionId] int  NOT NULL,
    [RoleId] int  NOT NULL
);
GO

-- Creating table 'Resources'
CREATE TABLE [dbo].[Resources] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(255)  NOT NULL,
    [ResourceTypeId] int  NOT NULL,
    [OwnerId] int  NULL,
    [Content] varchar(255)  NULL,
    [FullPath] varchar(255)  NOT NULL,
    [ParentId] int  NULL
);
GO

-- Creating table 'ResourceTypes'
CREATE TABLE [dbo].[ResourceTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(255)  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserInRoles'
CREATE TABLE [dbo].[UserInRoles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [RoleId] int  NOT NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [AspNetRoles_Id] nvarchar(128)  NOT NULL,
    [AspNetUsers_Id] nvarchar(128)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MigrationId], [ContextKey] in table 'C__MigrationHistory'
ALTER TABLE [dbo].[C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY CLUSTERED ([MigrationId], [ContextKey] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Constraints'
ALTER TABLE [dbo].[Constraints]
ADD CONSTRAINT [PK_Constraints]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Permissions'
ALTER TABLE [dbo].[Permissions]
ADD CONSTRAINT [PK_Permissions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PermissionForResources'
ALTER TABLE [dbo].[PermissionForResources]
ADD CONSTRAINT [PK_PermissionForResources]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PermissionToRoles'
ALTER TABLE [dbo].[PermissionToRoles]
ADD CONSTRAINT [PK_PermissionToRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Resources'
ALTER TABLE [dbo].[Resources]
ADD CONSTRAINT [PK_Resources]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ResourceTypes'
ALTER TABLE [dbo].[ResourceTypes]
ADD CONSTRAINT [PK_ResourceTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserInRoles'
ALTER TABLE [dbo].[UserInRoles]
ADD CONSTRAINT [PK_UserInRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [AspNetRoles_Id], [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([AspNetRoles_Id], [AspNetUsers_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [RoleId1] in table 'Constraints'
ALTER TABLE [dbo].[Constraints]
ADD CONSTRAINT [FK__Constrain__RoleI__0E6E26BF]
    FOREIGN KEY ([RoleId1])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Constrain__RoleI__0E6E26BF'
CREATE INDEX [IX_FK__Constrain__RoleI__0E6E26BF]
ON [dbo].[Constraints]
    ([RoleId1]);
GO

-- Creating foreign key on [RoleId2] in table 'Constraints'
ALTER TABLE [dbo].[Constraints]
ADD CONSTRAINT [FK__Constrain__RoleI__0F624AF8]
    FOREIGN KEY ([RoleId2])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Constrain__RoleI__0F624AF8'
CREATE INDEX [IX_FK__Constrain__RoleI__0F624AF8]
ON [dbo].[Constraints]
    ([RoleId2]);
GO

-- Creating foreign key on [PermissionId] in table 'PermissionToRoles'
ALTER TABLE [dbo].[PermissionToRoles]
ADD CONSTRAINT [FK__Permissio__Permi__04E4BC85]
    FOREIGN KEY ([PermissionId])
    REFERENCES [dbo].[Permissions]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Permissio__Permi__04E4BC85'
CREATE INDEX [IX_FK__Permissio__Permi__04E4BC85]
ON [dbo].[PermissionToRoles]
    ([PermissionId]);
GO

-- Creating foreign key on [PermissionId] in table 'PermissionForResources'
ALTER TABLE [dbo].[PermissionForResources]
ADD CONSTRAINT [FK__Permissio__Permi__08B54D69]
    FOREIGN KEY ([PermissionId])
    REFERENCES [dbo].[Permissions]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Permissio__Permi__08B54D69'
CREATE INDEX [IX_FK__Permissio__Permi__08B54D69]
ON [dbo].[PermissionForResources]
    ([PermissionId]);
GO

-- Creating foreign key on [ResourceId] in table 'PermissionForResources'
ALTER TABLE [dbo].[PermissionForResources]
ADD CONSTRAINT [FK__Permissio__Resou__09A971A2]
    FOREIGN KEY ([ResourceId])
    REFERENCES [dbo].[Resources]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Permissio__Resou__09A971A2'
CREATE INDEX [IX_FK__Permissio__Resou__09A971A2]
ON [dbo].[PermissionForResources]
    ([ResourceId]);
GO

-- Creating foreign key on [RoleId] in table 'PermissionToRoles'
ALTER TABLE [dbo].[PermissionToRoles]
ADD CONSTRAINT [FK__Permissio__RoleI__05D8E0BE]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Permissio__RoleI__05D8E0BE'
CREATE INDEX [IX_FK__Permissio__RoleI__05D8E0BE]
ON [dbo].[PermissionToRoles]
    ([RoleId]);
GO

-- Creating foreign key on [OwnerId] in table 'Resources'
ALTER TABLE [dbo].[Resources]
ADD CONSTRAINT [FK__Resource__Parent__33D4B598]
    FOREIGN KEY ([OwnerId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Resource__Parent__33D4B598'
CREATE INDEX [IX_FK__Resource__Parent__33D4B598]
ON [dbo].[Resources]
    ([OwnerId]);
GO

-- Creating foreign key on [ResourceTypeId] in table 'Resources'
ALTER TABLE [dbo].[Resources]
ADD CONSTRAINT [FK__Resource__Resour__31EC6D26]
    FOREIGN KEY ([ResourceTypeId])
    REFERENCES [dbo].[ResourceTypes]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Resource__Resour__31EC6D26'
CREATE INDEX [IX_FK__Resource__Resour__31EC6D26]
ON [dbo].[Resources]
    ([ResourceTypeId]);
GO

-- Creating foreign key on [RoleId] in table 'UserInRoles'
ALTER TABLE [dbo].[UserInRoles]
ADD CONSTRAINT [FK__UserInRol__RoleI__6383C8BA]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__UserInRol__RoleI__6383C8BA'
CREATE INDEX [IX_FK__UserInRol__RoleI__6383C8BA]
ON [dbo].[UserInRoles]
    ([RoleId]);
GO

-- Creating foreign key on [UserId] in table 'UserInRoles'
ALTER TABLE [dbo].[UserInRoles]
ADD CONSTRAINT [FK__UserInRol__UserI__628FA481]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__UserInRol__UserI__628FA481'
CREATE INDEX [IX_FK__UserInRol__UserI__628FA481]
ON [dbo].[UserInRoles]
    ([UserId]);
GO

-- Creating foreign key on [AspNetRoles_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRole]
    FOREIGN KEY ([AspNetRoles_Id])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUser]
    FOREIGN KEY ([AspNetUsers_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserRoles_AspNetUser'
CREATE INDEX [IX_FK_AspNetUserRoles_AspNetUser]
ON [dbo].[AspNetUserRoles]
    ([AspNetUsers_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------