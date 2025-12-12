  
 --DATOS DE LA TABLA FUNCTION
MERGE INTO Funcion AS Target
USING  (VALUES
           (11, NULL, 'Usuarios_ABM',		'Script Post Deploy', '2016-05-17', NULL, '2016-05-17'),
		   (15, NULL, 'Usuarios_Listado',	'Script Post Deploy', '2016-05-17', NULL, '2016-05-17'),
		   (16, NULL, 'Usuario_Alta',		'Script Post Deploy', '2016-05-17', NULL, '2016-05-17'),
		   (17, NULL, 'Usuario_Editar',		'Script Post Deploy', '2016-05-17', NULL, '2016-05-17'),
		   (18, NULL, 'Usuario_Borrar',		'Script Post Deploy', '2016-05-17', NULL, '2016-05-17'),
		   (20, NULL, 'Usuario_Detalle',	'Script Post Deploy', '2016-05-17', NULL, '2016-05-17'),
		   (1,	NULL, 'GenerarBalance',		'Script Post Deploy', '2016-05-17', NULL, '2016-05-17'),
		   (2,	NULL, 'IngresarMedicion',	'Script Post Deploy', '2016-06-06', NULL, '2016-06-06'),
		   (3,	NULL, 'IngresarOperacion',	'Script Post Deploy', '2016-06-09', NULL, '2016-06-09'),
		   (4,	NULL, 'CerrarAbrirOperacion','Script Post Deploy', '2016-06-13', NULL, '2016-06-13'),
		   (5,	NULL, 'ConsultarOperacion','Script Post Deploy', '2016-06-13', NULL, '2016-06-13'),
		   (6,	NULL, 'ConsultarMedicion','Script Post Deploy', '2016-06-13', NULL, '2016-06-13'),
		   (7,	NULL, 'EditarMedicion','Script Post Deploy', '2016-06-13', NULL, '2016-06-21'),
		   (8,	NULL, 'EliminarMedicion','Script Post Deploy', '2016-06-13', NULL, '2016-06-21'),
		   (9,	NULL, 'AbrirCerrarMedicion','Script Post Deploy', '2016-06-13', NULL, '2016-06-13'),
		   (10,	NULL, 'RegistrarCantidadDeclarada','Script Post Deploy', '2016-06-13', NULL, '2016-06-13'),
		   (12,	NULL, 'EditarOperacion','Script Post Deploy', '2016-06-27', NULL, '2016-06-27'),
		   (13,	NULL, 'EliminarOperacion','Script Post Deploy', '2016-07-01', NULL, '2016-07-01'),
		   (14,	NULL, 'CorregirOperacion','Script Post Deploy', '2016-07-13', NULL, '2016-07-13')
		)
	AS Source (Id, IdPadre, Descripcion, CreadoPor, FechaCreacion, ModificadoPor, FechaModificacion)
ON Target.Id = Source.Id
WHEN MATCHED THEN
	UPDATE SET Id=Source.Id, IdPadre=Source.IdPadre, Descripcion=Source.Descripcion, CreadoPor=Source.CreadoPor, FechaCreacion=Source.FechaCreacion, ModificadoPor=Source.ModificadoPor, FechaModificacion=Source.FechaModificacion
WHEN NOT MATCHED BY TARGET THEN
	INSERT (Id, IdPadre, Descripcion, CreadoPor, FechaCreacion, ModificadoPor, FechaModificacion)
	VALUES (Source.Id, Source.IdPadre, Source.Descripcion, Source.CreadoPor, Source.FechaCreacion, Source.ModificadoPor, GETDATE())
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;
GO

 --DATOS DE LA TABLA ROL
MERGE INTO [Rol] AS Target
USING  (VALUES
           (1, 'Gestor', 1, 'Script Post Deploy', '2016-05-17', NULL, '2016-05-17'),
		   (2, 'Supervisor', 1, 'Script Post Deploy', '2016-05-17', NULL, '2016-05-17'),
		   (3, 'Apropiador', 1, 'Script Post Deploy', '2016-05-17', NULL, '2016-05-17'),
		   (4, 'Operador', 1, 'Script Post Deploy', '2016-05-17', NULL, '2016-05-17'))
	AS Source (Id, Descripcion, Activo, CreadoPor, FechaCreacion, ModificadoPor, FechaModificacion)
ON Target.Id = Source.Id
WHEN MATCHED THEN
	UPDATE SET Id=Source.Id, Descripcion=Source.Descripcion, Activo=Source.Activo, CreadoPor=Source.CreadoPor, FechaCreacion=Source.FechaCreacion, ModificadoPor=Source.ModificadoPor, FechaModificacion=Source.FechaModificacion
WHEN NOT MATCHED BY TARGET THEN
	INSERT (Id, Descripcion, Activo, CreadoPor, FechaCreacion, ModificadoPor, FechaModificacion)
	VALUES (Source.Id, Source.Descripcion, Source.Activo, Source.CreadoPor, Source.FechaCreacion, Source.ModificadoPor, GETDATE())
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;
GO
 

DECLARE @GestorId		int = 1
DECLARE @SupervisorId	int = 2
DECLARE @ApropiadorId	int = 3
DECLARE @OperadorId		int = 4

  --DATOS DE LA TABLA ROL_FUNCION
MERGE INTO [Rol_Funcion] AS Target
USING  (VALUES
		   (@GestorId, 11),
		   (@GestorId, 15),
		   (@GestorId, 16),
		   (@GestorId, 17),
		   (@GestorId, 18),
		   (@GestorId, 20),
		   (@GestorId,		1),
		   (@SupervisorId,	1),
		   (@ApropiadorId,	1),
		   (@OperadorId,	1),
		   (@GestorId,		2),
		   (@SupervisorId,	2),
		   (@OperadorId,	2),
		   (@GestorId,		3),
		   (@SupervisorId,	3),
		   (@ApropiadorId,	3),
		   (@OperadorId,	3),
		   (@GestorId,		4),
		   (@SupervisorId,	4),
		   (@ApropiadorId,	4),
		   (@OperadorId,	4),
		   (@GestorId,		5),
		   (@SupervisorId,	5),
		   (@ApropiadorId,	5),
		   (@OperadorId,	5),
		   (@GestorId,		6),
		   (@SupervisorId,	6),
		   (@ApropiadorId,	6),
		   (@OperadorId,	6),
		    (@GestorId,		7),
		   (@SupervisorId,	7),
		   (@OperadorId,	7),
		    (@GestorId,		8),
		   (@SupervisorId,	8),
		   (@OperadorId,	8),
		   (@GestorId,		9),
		   (@SupervisorId,	9),
		   (@ApropiadorId,	9),
		   (@OperadorId,	9),
		   (@GestorId,		10),
		   (@SupervisorId,	10),
		   (@ApropiadorId,	10),
		   (@OperadorId,	10),
		   (@GestorId,		12),
		   (@SupervisorId,	12),
		   (@ApropiadorId,	12),
		   (@OperadorId,	12),  
		   (@GestorId,		13),
		   (@SupervisorId,	13),
		   (@ApropiadorId,	13),
		   (@OperadorId,	13),
		   (@GestorId,		14),
		   (@SupervisorId,	14),
		   (@ApropiadorId,	14),
		   (@OperadorId,	14)
	)
	AS Source (Id, Id2)
ON Target.Id = Source.Id AND Target.Id2 = Source.Id2
WHEN MATCHED THEN
	UPDATE SET Id=Source.Id, Id2=Source.Id2
WHEN NOT MATCHED BY TARGET THEN
	INSERT (Id, Id2)
	VALUES (Source.Id, Source.Id2)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;
GO

 -- ********* SOLO ENTORNO DEV ************
 --DATOS DE LA TABLA USUARIOS
MERGE INTO Usuario AS Target
USING  (VALUES
           ('HEXACTA\EDELAHAYE',	'Eric',		'Delahaye',		1,	'Script Post Deploy', GETDATE(), 'Script Post Deploy', GETDATE()),
		   ('HEXACTA\NRIDELLA',		'Natalia',	'Ridella',		1,	'Script Post Deploy', GETDATE(), 'Script Post Deploy', GETDATE()),
		   ('HEXACTA\EALVEA',		'Emanuel',	'Alvea',		1,	'Script Post Deploy', GETDATE(), 'Script Post Deploy', GETDATE()),
		   ('HEXACTA\SANTONINI',	'Santiago',	'Antonini',		1,	'Script Post Deploy', GETDATE(), 'Script Post Deploy', GETDATE()),
		   ('HEXACTA\DZAIDENVOREN', 'Denise',	'Zaidenvoren',	1,	'Script Post Deploy', GETDATE(), 'Script Post Deploy', GETDATE()),
		   ('HEXACTA\DSHLUFMAN',	'Daniel',	'Shlufman',		1,	'Script Post Deploy', GETDATE(), 'Script Post Deploy', GETDATE()),
		   ('Pampa\D7QO',		'Eric',		'Delahaye',		1,	'Script Post Deploy', GETDATE(), 'Script Post Deploy', GETDATE()),
		   ('Pampa\PENP',		'Julio',	'Sapia',		1,	'Script Post Deploy', GETDATE(), 'Script Post Deploy', GETDATE()),
		   ('Pampa\D6F5',		'Fernando',	'Abelaira',		1,	'Script Post Deploy', GETDATE(), 'Script Post Deploy', GETDATE())
		)
	AS Source (UsuarioNT, Nombre, Apellido, Activo, CreadoPor, FechaCreacion, ModificadoPor, FechaModificacion)
ON Target.UsuarioNT = Source.UsuarioNT
WHEN MATCHED THEN
	UPDATE SET UsuarioNT=Source.UsuarioNT, Nombre=Source.Nombre, Apellido=Source.Apellido, FechaModificacion=Source.FechaModificacion
WHEN NOT MATCHED BY TARGET THEN
	INSERT (UsuarioNT, Nombre, Apellido, Activo, CreadoPor, FechaCreacion, ModificadoPor, FechaModificacion)
	VALUES (Source.UsuarioNT, Source.Nombre, Source.Apellido, Source.Activo, Source.CreadoPor, Source.FechaCreacion, Source.ModificadoPor, GETDATE())
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;
GO




-- USUARIOS DE DESARROLLO LOS PONE COMO GESTORES 
DECLARE @IdEric		int
DECLARE @IdNatalia	int
DECLARE @IdDenise	int
DECLARE @IdEmanuel	int
DECLARE @IdSantiago int
DECLARE @IdDaniel	int
DECLARE @IdEric2	int
DECLARE @IdFer		int
DECLARE @IdJulio	int

select @IdEric = Id from Usuario where UsuarioNT='HEXACTA\EDELAHAYE'
select @IdNatalia = Id from Usuario where UsuarioNT='HEXACTA\NRIDELLA'
select @IdEmanuel = Id from Usuario where UsuarioNT='HEXACTA\EALVEA'
select @IdSantiago = Id from Usuario where UsuarioNT='HEXACTA\SANTONINI'
select @IdDenise = Id from Usuario where UsuarioNT='HEXACTA\DZAIDENVOREN'
select @IdDaniel = Id from Usuario where UsuarioNT='HEXACTA\DSHLUFMAN'
select @IdEric2		= Id from Usuario where UsuarioNT=	'Pampa\D7QO'
select @IdFer		= Id from Usuario where UsuarioNT=	'Pampa\D6F5'
select @IdJulio		= Id from Usuario where UsuarioNT=	'Pampa\PENP'
	
MERGE INTO Usuario_Rol AS Target
USING  (VALUES
           (@IdEric,		1),
		   (@IdNatalia,		1),
		   (@IdEmanuel,		1),
		   (@IdSantiago,	1),
		   (@IdDenise,		1),
		   (@IdDaniel,		1),
		   (@IdEric2,		1),
		   (@IdFer,			1),
		   (@IdJulio,		1)
		)
	AS Source (Id, Id2)
ON Target.Id = Source.Id AND Target.Id2 = Source.Id2 
WHEN NOT MATCHED BY TARGET THEN
	INSERT (Id, Id2)
	VALUES (Source.Id, Source.Id2)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;
	
