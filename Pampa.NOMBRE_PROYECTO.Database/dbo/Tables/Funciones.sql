CREATE TABLE [dbo].[Funciones] (
    [Id]                INT           NOT NULL,
    [IdPadre]           INT           NULL,
    [Descripcion]       VARCHAR (255) NULL,
    [CreadoPor]         VARCHAR (255) NULL,
    [FechaCreacion]     DATETIME      CONSTRAINT [DF_Funcion_FechaCreacion] DEFAULT (getdate()) NOT NULL,
    [ModificadoPor]     VARCHAR (255) NULL,
    [FechaModificacion] DATETIME      CONSTRAINT [DF_Funcion_FechaModificacion] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Funcion] PRIMARY KEY CLUSTERED ([Id] ASC)
);

