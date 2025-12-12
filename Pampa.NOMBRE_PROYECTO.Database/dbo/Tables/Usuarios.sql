CREATE TABLE [dbo].[Usuarios] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [UsuarioNT]         VARCHAR (255) NOT NULL,
    [Nombre]            VARCHAR (255) NULL,
    [Apellido]          VARCHAR (255) NULL,
    [Activo]            BIT       NOT NULL,
    [CreadoPor]         VARCHAR (255) NULL,
    [FechaCreacion]     DATETIME      CONSTRAINT [DF_Usuario_FechaCreacion] DEFAULT (getdate()) NOT NULL,
    [ModificadoPor]     VARCHAR (255) NULL,
    [FechaModificacion] DATETIME      CONSTRAINT [DF_Usuario_FechaModificacion] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Usuario_UsuarioNT]
    ON [dbo].[Usuarios]([UsuarioNT] ASC);

