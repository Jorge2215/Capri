CREATE TABLE [dbo].[Roles] (
    [Id]                INT           NOT NULL,
    [Descripcion]       VARCHAR (255) NULL,
    [Activo]            BIT       NOT NULL,
    [CreadoPor]         VARCHAR (255) NULL,
    [FechaCreacion]     DATETIME      CONSTRAINT [DF_Rol_FechaCreacion] DEFAULT (getdate()) NOT NULL,
    [ModificadoPor]     VARCHAR (255) NULL,
    [FechaModificacion] DATETIME      CONSTRAINT [DF_Rol_FechaModificacion] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED ([Id] ASC)
);

