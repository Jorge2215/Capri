CREATE TABLE [dbo].[UsuariosRoles] (
    [IdUsuario]  INT NOT NULL,
    [IdRol] INT NOT NULL,
    CONSTRAINT [PK_UsuariosRoles] PRIMARY KEY CLUSTERED ([IdUsuario] ASC, [IdRol] ASC),
    CONSTRAINT [FK_UsuariosRoles_ToRol] FOREIGN KEY ([IdRol]) REFERENCES [dbo].[Roles] ([Id]),
    CONSTRAINT [FK_UsuarioRol_ToUsuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios] ([Id])
);