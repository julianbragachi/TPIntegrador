
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/08/2019 22:51:44
-- Generated from EDMX file: C:\ProyectoWEB3\TPIntegrador\AyudandoAlProjimo.Data\BD.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TP-20192C];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Denuncias_MotivoDenuncia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Denuncias] DROP CONSTRAINT [FK_Denuncias_MotivoDenuncia];
GO
IF OBJECT_ID(N'[dbo].[FK_Denuncias_Propuestas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Denuncias] DROP CONSTRAINT [FK_Denuncias_Propuestas];
GO
IF OBJECT_ID(N'[dbo].[FK_Denuncias_Usuarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Denuncias] DROP CONSTRAINT [FK_Denuncias_Usuarios];
GO
IF OBJECT_ID(N'[dbo].[FK_DonacionesHorasTrabajo_PropuestasDonacionesHorasTrabajo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DonacionesHorasTrabajo] DROP CONSTRAINT [FK_DonacionesHorasTrabajo_PropuestasDonacionesHorasTrabajo];
GO
IF OBJECT_ID(N'[dbo].[FK_DonacionesHorasTrabajo_Usuarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DonacionesHorasTrabajo] DROP CONSTRAINT [FK_DonacionesHorasTrabajo_Usuarios];
GO
IF OBJECT_ID(N'[dbo].[FK_DonacionesInsumos_PropuestasDonacionesInsumos]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DonacionesInsumos] DROP CONSTRAINT [FK_DonacionesInsumos_PropuestasDonacionesInsumos];
GO
IF OBJECT_ID(N'[dbo].[FK_DonacionesInsumos_Usuarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DonacionesInsumos] DROP CONSTRAINT [FK_DonacionesInsumos_Usuarios];
GO
IF OBJECT_ID(N'[dbo].[FK_DonacionesMonetarias_PropuestasDonacionesMonetarias]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DonacionesMonetarias] DROP CONSTRAINT [FK_DonacionesMonetarias_PropuestasDonacionesMonetarias];
GO
IF OBJECT_ID(N'[dbo].[FK_DonacionesMonetarias_Usuarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DonacionesMonetarias] DROP CONSTRAINT [FK_DonacionesMonetarias_Usuarios];
GO
IF OBJECT_ID(N'[dbo].[FK_Propuestas_Usuarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Propuestas] DROP CONSTRAINT [FK_Propuestas_Usuarios];
GO
IF OBJECT_ID(N'[dbo].[FK_PropuestasDonacionesHorasTrabajo_Propuestas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PropuestasDonacionesHorasTrabajo] DROP CONSTRAINT [FK_PropuestasDonacionesHorasTrabajo_Propuestas];
GO
IF OBJECT_ID(N'[dbo].[FK_PropuestasDonacionesInsumos_Propuestas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PropuestasDonacionesInsumos] DROP CONSTRAINT [FK_PropuestasDonacionesInsumos_Propuestas];
GO
IF OBJECT_ID(N'[dbo].[FK_PropuestasDonacionesMonetarias_Propuestas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PropuestasDonacionesMonetarias] DROP CONSTRAINT [FK_PropuestasDonacionesMonetarias_Propuestas];
GO
IF OBJECT_ID(N'[dbo].[FK_PropuestasReferencias_Propuestas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PropuestasReferencias] DROP CONSTRAINT [FK_PropuestasReferencias_Propuestas];
GO
IF OBJECT_ID(N'[dbo].[FK_PropuestasValoraciones_Propuestas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PropuestasValoraciones] DROP CONSTRAINT [FK_PropuestasValoraciones_Propuestas];
GO
IF OBJECT_ID(N'[dbo].[FK_PropuestasValoraciones_Usuarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PropuestasValoraciones] DROP CONSTRAINT [FK_PropuestasValoraciones_Usuarios];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Denuncias]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Denuncias];
GO
IF OBJECT_ID(N'[dbo].[DonacionesHorasTrabajo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DonacionesHorasTrabajo];
GO
IF OBJECT_ID(N'[dbo].[DonacionesInsumos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DonacionesInsumos];
GO
IF OBJECT_ID(N'[dbo].[DonacionesMonetarias]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DonacionesMonetarias];
GO
IF OBJECT_ID(N'[dbo].[MotivoDenuncia]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MotivoDenuncia];
GO
IF OBJECT_ID(N'[dbo].[Propuestas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Propuestas];
GO
IF OBJECT_ID(N'[dbo].[PropuestasDonacionesHorasTrabajo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PropuestasDonacionesHorasTrabajo];
GO
IF OBJECT_ID(N'[dbo].[PropuestasDonacionesInsumos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PropuestasDonacionesInsumos];
GO
IF OBJECT_ID(N'[dbo].[PropuestasDonacionesMonetarias]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PropuestasDonacionesMonetarias];
GO
IF OBJECT_ID(N'[dbo].[PropuestasReferencias]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PropuestasReferencias];
GO
IF OBJECT_ID(N'[dbo].[PropuestasValoraciones]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PropuestasValoraciones];
GO
IF OBJECT_ID(N'[dbo].[Usuarios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Usuarios];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Denuncias'
CREATE TABLE [dbo].[Denuncias] (
    [IdDenuncia] int IDENTITY(1,1) NOT NULL,
    [IdPropuesta] int  NOT NULL,
    [IdMotivo] int  NOT NULL,
    [Comentarios] nvarchar(300)  NOT NULL,
    [IdUsuario] int  NOT NULL,
    [FechaCreacion] datetime  NOT NULL,
    [Estado] int  NOT NULL
);
GO

-- Creating table 'DonacionesHorasTrabajo'
CREATE TABLE [dbo].[DonacionesHorasTrabajo] (
    [IdDonacionHorasTrabajo] int IDENTITY(1,1) NOT NULL,
    [IdPropuestaDonacionHorasTrabajo] int  NOT NULL,
    [IdUsuario] int  NOT NULL,
    [Cantidad] int  NOT NULL
);
GO

-- Creating table 'DonacionesInsumos'
CREATE TABLE [dbo].[DonacionesInsumos] (
    [IdDonacionInsumo] int IDENTITY(1,1) NOT NULL,
    [IdPropuestaDonacionInsumo] int  NOT NULL,
    [IdUsuario] int  NOT NULL,
    [Cantidad] int  NOT NULL
);
GO

-- Creating table 'DonacionesMonetarias'
CREATE TABLE [dbo].[DonacionesMonetarias] (
    [IdDonacionMonetaria] int IDENTITY(1,1) NOT NULL,
    [IdPropuestaDonacionMonetaria] int  NOT NULL,
    [IdUsuario] int  NOT NULL,
    [Dinero] decimal(18,2)  NOT NULL,
    [ArchivoTransferencia] nvarchar(200)  NOT NULL,
    [FechaCreacion] datetime  NOT NULL
);
GO

-- Creating table 'MotivoDenuncia'
CREATE TABLE [dbo].[MotivoDenuncia] (
    [IdMotivoDenuncia] int  NOT NULL,
    [Descripcion] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Propuestas'
CREATE TABLE [dbo].[Propuestas] (
    [IdPropuesta] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(50)  NOT NULL,
    [Descripcion] varchar(max)  NOT NULL,
    [FechaCreacion] datetime  NOT NULL,
    [FechaFin] datetime  NOT NULL,
    [TelefonoContacto] nvarchar(30)  NOT NULL,
    [TipoDonacion] int  NOT NULL,
    [Foto] nvarchar(100)  NOT NULL,
    [IdUsuarioCreador] int  NOT NULL,
    [Estado] int  NOT NULL,
    [Valoracion] decimal(18,0)  NULL
);
GO

-- Creating table 'PropuestasDonacionesHorasTrabajo'
CREATE TABLE [dbo].[PropuestasDonacionesHorasTrabajo] (
    [IdPropuestaDonacionHorasTrabajo] int IDENTITY(1,1) NOT NULL,
    [IdPropuesta] int  NOT NULL,
    [CantidadHoras] int  NOT NULL,
    [Profesion] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'PropuestasDonacionesInsumos'
CREATE TABLE [dbo].[PropuestasDonacionesInsumos] (
    [IdPropuestaDonacionInsumo] int IDENTITY(1,1) NOT NULL,
    [IdPropuesta] int  NOT NULL,
    [Nombre] nvarchar(50)  NOT NULL,
    [Cantidad] int  NOT NULL
);
GO

-- Creating table 'PropuestasDonacionesMonetarias'
CREATE TABLE [dbo].[PropuestasDonacionesMonetarias] (
    [IdPropuestaDonacionMonetaria] int IDENTITY(1,1) NOT NULL,
    [IdPropuesta] int  NOT NULL,
    [Dinero] decimal(18,2)  NOT NULL,
    [CBU] nvarchar(80)  NOT NULL
);
GO

-- Creating table 'PropuestasReferencias'
CREATE TABLE [dbo].[PropuestasReferencias] (
    [IdReferencia] int IDENTITY(1,1) NOT NULL,
    [IdPropuesta] int  NOT NULL,
    [Nombre] nvarchar(50)  NOT NULL,
    [Telefono] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'PropuestasValoraciones'
CREATE TABLE [dbo].[PropuestasValoraciones] (
    [IdValoracion] int IDENTITY(1,1) NOT NULL,
    [IdUsuario] int  NOT NULL,
    [IdPropuesta] int  NOT NULL,
    [Valoracion] bit  NOT NULL
);
GO

-- Creating table 'Usuarios'
CREATE TABLE [dbo].[Usuarios] (
    [IdUsuario] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(50)  NULL,
    [Apellido] nvarchar(50)  NULL,
    [FechaNacimiento] datetime  NOT NULL,
    [UserName] nvarchar(20)  NULL,
    [Email] nvarchar(50)  NOT NULL,
    [Password] nvarchar(20)  NOT NULL,
    [Foto] nvarchar(100)  NULL,
    [TipoUsuario] int  NOT NULL,
    [FechaCracion] datetime  NOT NULL,
    [Activo] bit  NOT NULL,
    [Token] nvarchar(30)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdDenuncia] in table 'Denuncias'
ALTER TABLE [dbo].[Denuncias]
ADD CONSTRAINT [PK_Denuncias]
    PRIMARY KEY CLUSTERED ([IdDenuncia] ASC);
GO

-- Creating primary key on [IdDonacionHorasTrabajo] in table 'DonacionesHorasTrabajo'
ALTER TABLE [dbo].[DonacionesHorasTrabajo]
ADD CONSTRAINT [PK_DonacionesHorasTrabajo]
    PRIMARY KEY CLUSTERED ([IdDonacionHorasTrabajo] ASC);
GO

-- Creating primary key on [IdDonacionInsumo] in table 'DonacionesInsumos'
ALTER TABLE [dbo].[DonacionesInsumos]
ADD CONSTRAINT [PK_DonacionesInsumos]
    PRIMARY KEY CLUSTERED ([IdDonacionInsumo] ASC);
GO

-- Creating primary key on [IdDonacionMonetaria] in table 'DonacionesMonetarias'
ALTER TABLE [dbo].[DonacionesMonetarias]
ADD CONSTRAINT [PK_DonacionesMonetarias]
    PRIMARY KEY CLUSTERED ([IdDonacionMonetaria] ASC);
GO

-- Creating primary key on [IdMotivoDenuncia] in table 'MotivoDenuncia'
ALTER TABLE [dbo].[MotivoDenuncia]
ADD CONSTRAINT [PK_MotivoDenuncia]
    PRIMARY KEY CLUSTERED ([IdMotivoDenuncia] ASC);
GO

-- Creating primary key on [IdPropuesta] in table 'Propuestas'
ALTER TABLE [dbo].[Propuestas]
ADD CONSTRAINT [PK_Propuestas]
    PRIMARY KEY CLUSTERED ([IdPropuesta] ASC);
GO

-- Creating primary key on [IdPropuestaDonacionHorasTrabajo] in table 'PropuestasDonacionesHorasTrabajo'
ALTER TABLE [dbo].[PropuestasDonacionesHorasTrabajo]
ADD CONSTRAINT [PK_PropuestasDonacionesHorasTrabajo]
    PRIMARY KEY CLUSTERED ([IdPropuestaDonacionHorasTrabajo] ASC);
GO

-- Creating primary key on [IdPropuestaDonacionInsumo] in table 'PropuestasDonacionesInsumos'
ALTER TABLE [dbo].[PropuestasDonacionesInsumos]
ADD CONSTRAINT [PK_PropuestasDonacionesInsumos]
    PRIMARY KEY CLUSTERED ([IdPropuestaDonacionInsumo] ASC);
GO

-- Creating primary key on [IdPropuestaDonacionMonetaria] in table 'PropuestasDonacionesMonetarias'
ALTER TABLE [dbo].[PropuestasDonacionesMonetarias]
ADD CONSTRAINT [PK_PropuestasDonacionesMonetarias]
    PRIMARY KEY CLUSTERED ([IdPropuestaDonacionMonetaria] ASC);
GO

-- Creating primary key on [IdReferencia] in table 'PropuestasReferencias'
ALTER TABLE [dbo].[PropuestasReferencias]
ADD CONSTRAINT [PK_PropuestasReferencias]
    PRIMARY KEY CLUSTERED ([IdReferencia] ASC);
GO

-- Creating primary key on [IdValoracion] in table 'PropuestasValoraciones'
ALTER TABLE [dbo].[PropuestasValoraciones]
ADD CONSTRAINT [PK_PropuestasValoraciones]
    PRIMARY KEY CLUSTERED ([IdValoracion] ASC);
GO

-- Creating primary key on [IdUsuario] in table 'Usuarios'
ALTER TABLE [dbo].[Usuarios]
ADD CONSTRAINT [PK_Usuarios]
    PRIMARY KEY CLUSTERED ([IdUsuario] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdMotivo] in table 'Denuncias'
ALTER TABLE [dbo].[Denuncias]
ADD CONSTRAINT [FK_Denuncias_MotivoDenuncia]
    FOREIGN KEY ([IdMotivo])
    REFERENCES [dbo].[MotivoDenuncia]
        ([IdMotivoDenuncia])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Denuncias_MotivoDenuncia'
CREATE INDEX [IX_FK_Denuncias_MotivoDenuncia]
ON [dbo].[Denuncias]
    ([IdMotivo]);
GO

-- Creating foreign key on [IdPropuesta] in table 'Denuncias'
ALTER TABLE [dbo].[Denuncias]
ADD CONSTRAINT [FK_Denuncias_Propuestas]
    FOREIGN KEY ([IdPropuesta])
    REFERENCES [dbo].[Propuestas]
        ([IdPropuesta])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Denuncias_Propuestas'
CREATE INDEX [IX_FK_Denuncias_Propuestas]
ON [dbo].[Denuncias]
    ([IdPropuesta]);
GO

-- Creating foreign key on [IdUsuario] in table 'Denuncias'
ALTER TABLE [dbo].[Denuncias]
ADD CONSTRAINT [FK_Denuncias_Usuarios]
    FOREIGN KEY ([IdUsuario])
    REFERENCES [dbo].[Usuarios]
        ([IdUsuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Denuncias_Usuarios'
CREATE INDEX [IX_FK_Denuncias_Usuarios]
ON [dbo].[Denuncias]
    ([IdUsuario]);
GO

-- Creating foreign key on [IdPropuestaDonacionHorasTrabajo] in table 'DonacionesHorasTrabajo'
ALTER TABLE [dbo].[DonacionesHorasTrabajo]
ADD CONSTRAINT [FK_DonacionesHorasTrabajo_PropuestasDonacionesHorasTrabajo]
    FOREIGN KEY ([IdPropuestaDonacionHorasTrabajo])
    REFERENCES [dbo].[PropuestasDonacionesHorasTrabajo]
        ([IdPropuestaDonacionHorasTrabajo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DonacionesHorasTrabajo_PropuestasDonacionesHorasTrabajo'
CREATE INDEX [IX_FK_DonacionesHorasTrabajo_PropuestasDonacionesHorasTrabajo]
ON [dbo].[DonacionesHorasTrabajo]
    ([IdPropuestaDonacionHorasTrabajo]);
GO

-- Creating foreign key on [IdUsuario] in table 'DonacionesHorasTrabajo'
ALTER TABLE [dbo].[DonacionesHorasTrabajo]
ADD CONSTRAINT [FK_DonacionesHorasTrabajo_Usuarios]
    FOREIGN KEY ([IdUsuario])
    REFERENCES [dbo].[Usuarios]
        ([IdUsuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DonacionesHorasTrabajo_Usuarios'
CREATE INDEX [IX_FK_DonacionesHorasTrabajo_Usuarios]
ON [dbo].[DonacionesHorasTrabajo]
    ([IdUsuario]);
GO

-- Creating foreign key on [IdPropuestaDonacionInsumo] in table 'DonacionesInsumos'
ALTER TABLE [dbo].[DonacionesInsumos]
ADD CONSTRAINT [FK_DonacionesInsumos_PropuestasDonacionesInsumos]
    FOREIGN KEY ([IdPropuestaDonacionInsumo])
    REFERENCES [dbo].[PropuestasDonacionesInsumos]
        ([IdPropuestaDonacionInsumo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DonacionesInsumos_PropuestasDonacionesInsumos'
CREATE INDEX [IX_FK_DonacionesInsumos_PropuestasDonacionesInsumos]
ON [dbo].[DonacionesInsumos]
    ([IdPropuestaDonacionInsumo]);
GO

-- Creating foreign key on [IdUsuario] in table 'DonacionesInsumos'
ALTER TABLE [dbo].[DonacionesInsumos]
ADD CONSTRAINT [FK_DonacionesInsumos_Usuarios]
    FOREIGN KEY ([IdUsuario])
    REFERENCES [dbo].[Usuarios]
        ([IdUsuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DonacionesInsumos_Usuarios'
CREATE INDEX [IX_FK_DonacionesInsumos_Usuarios]
ON [dbo].[DonacionesInsumos]
    ([IdUsuario]);
GO

-- Creating foreign key on [IdPropuestaDonacionMonetaria] in table 'DonacionesMonetarias'
ALTER TABLE [dbo].[DonacionesMonetarias]
ADD CONSTRAINT [FK_DonacionesMonetarias_PropuestasDonacionesMonetarias]
    FOREIGN KEY ([IdPropuestaDonacionMonetaria])
    REFERENCES [dbo].[PropuestasDonacionesMonetarias]
        ([IdPropuestaDonacionMonetaria])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DonacionesMonetarias_PropuestasDonacionesMonetarias'
CREATE INDEX [IX_FK_DonacionesMonetarias_PropuestasDonacionesMonetarias]
ON [dbo].[DonacionesMonetarias]
    ([IdPropuestaDonacionMonetaria]);
GO

-- Creating foreign key on [IdUsuario] in table 'DonacionesMonetarias'
ALTER TABLE [dbo].[DonacionesMonetarias]
ADD CONSTRAINT [FK_DonacionesMonetarias_Usuarios]
    FOREIGN KEY ([IdUsuario])
    REFERENCES [dbo].[Usuarios]
        ([IdUsuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DonacionesMonetarias_Usuarios'
CREATE INDEX [IX_FK_DonacionesMonetarias_Usuarios]
ON [dbo].[DonacionesMonetarias]
    ([IdUsuario]);
GO

-- Creating foreign key on [IdUsuarioCreador] in table 'Propuestas'
ALTER TABLE [dbo].[Propuestas]
ADD CONSTRAINT [FK_Propuestas_Usuarios]
    FOREIGN KEY ([IdUsuarioCreador])
    REFERENCES [dbo].[Usuarios]
        ([IdUsuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Propuestas_Usuarios'
CREATE INDEX [IX_FK_Propuestas_Usuarios]
ON [dbo].[Propuestas]
    ([IdUsuarioCreador]);
GO

-- Creating foreign key on [IdPropuesta] in table 'PropuestasDonacionesHorasTrabajo'
ALTER TABLE [dbo].[PropuestasDonacionesHorasTrabajo]
ADD CONSTRAINT [FK_PropuestasDonacionesHorasTrabajo_Propuestas]
    FOREIGN KEY ([IdPropuesta])
    REFERENCES [dbo].[Propuestas]
        ([IdPropuesta])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PropuestasDonacionesHorasTrabajo_Propuestas'
CREATE INDEX [IX_FK_PropuestasDonacionesHorasTrabajo_Propuestas]
ON [dbo].[PropuestasDonacionesHorasTrabajo]
    ([IdPropuesta]);
GO

-- Creating foreign key on [IdPropuesta] in table 'PropuestasDonacionesInsumos'
ALTER TABLE [dbo].[PropuestasDonacionesInsumos]
ADD CONSTRAINT [FK_PropuestasDonacionesInsumos_Propuestas]
    FOREIGN KEY ([IdPropuesta])
    REFERENCES [dbo].[Propuestas]
        ([IdPropuesta])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PropuestasDonacionesInsumos_Propuestas'
CREATE INDEX [IX_FK_PropuestasDonacionesInsumos_Propuestas]
ON [dbo].[PropuestasDonacionesInsumos]
    ([IdPropuesta]);
GO

-- Creating foreign key on [IdPropuesta] in table 'PropuestasDonacionesMonetarias'
ALTER TABLE [dbo].[PropuestasDonacionesMonetarias]
ADD CONSTRAINT [FK_PropuestasDonacionesMonetarias_Propuestas]
    FOREIGN KEY ([IdPropuesta])
    REFERENCES [dbo].[Propuestas]
        ([IdPropuesta])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PropuestasDonacionesMonetarias_Propuestas'
CREATE INDEX [IX_FK_PropuestasDonacionesMonetarias_Propuestas]
ON [dbo].[PropuestasDonacionesMonetarias]
    ([IdPropuesta]);
GO

-- Creating foreign key on [IdPropuesta] in table 'PropuestasReferencias'
ALTER TABLE [dbo].[PropuestasReferencias]
ADD CONSTRAINT [FK_PropuestasReferencias_Propuestas]
    FOREIGN KEY ([IdPropuesta])
    REFERENCES [dbo].[Propuestas]
        ([IdPropuesta])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PropuestasReferencias_Propuestas'
CREATE INDEX [IX_FK_PropuestasReferencias_Propuestas]
ON [dbo].[PropuestasReferencias]
    ([IdPropuesta]);
GO

-- Creating foreign key on [IdPropuesta] in table 'PropuestasValoraciones'
ALTER TABLE [dbo].[PropuestasValoraciones]
ADD CONSTRAINT [FK_PropuestasValoraciones_Propuestas]
    FOREIGN KEY ([IdPropuesta])
    REFERENCES [dbo].[Propuestas]
        ([IdPropuesta])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PropuestasValoraciones_Propuestas'
CREATE INDEX [IX_FK_PropuestasValoraciones_Propuestas]
ON [dbo].[PropuestasValoraciones]
    ([IdPropuesta]);
GO

-- Creating foreign key on [IdUsuario] in table 'PropuestasValoraciones'
ALTER TABLE [dbo].[PropuestasValoraciones]
ADD CONSTRAINT [FK_PropuestasValoraciones_Usuarios]
    FOREIGN KEY ([IdUsuario])
    REFERENCES [dbo].[Usuarios]
        ([IdUsuario])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PropuestasValoraciones_Usuarios'
CREATE INDEX [IX_FK_PropuestasValoraciones_Usuarios]
ON [dbo].[PropuestasValoraciones]
    ([IdUsuario]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------