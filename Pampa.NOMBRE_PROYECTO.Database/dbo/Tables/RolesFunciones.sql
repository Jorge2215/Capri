CREATE TABLE [dbo].[RolesFunciones] (
    [IdRol]  INT NOT NULL,
    [IdFuncion] INT NOT NULL,
    CONSTRAINT [PK_Rol_Funcion] PRIMARY KEY CLUSTERED ([IdRol] ASC, [IdFuncion] ASC),
    CONSTRAINT [FK_RolFuncion_ToFuncion] FOREIGN KEY ([IdFuncion]) REFERENCES [dbo].[Funciones] ([Id]),
    CONSTRAINT [FK_RolFuncion_ToRol] FOREIGN KEY ([IdRol]) REFERENCES [dbo].[Roles] ([Id])
);