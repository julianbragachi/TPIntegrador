
INSERT [dbo].[MotivoDenuncia] ([IdMotivoDenuncia], [Descripcion]) VALUES (1, N'Fraude')
GO
INSERT [dbo].[MotivoDenuncia] ([IdMotivoDenuncia], [Descripcion]) VALUES (2, N'Indebida')
GO
INSERT [dbo].[MotivoDenuncia] ([IdMotivoDenuncia], [Descripcion]) VALUES (3, N'Violacion de derechos')
GO
INSERT [dbo].[MotivoDenuncia] ([IdMotivoDenuncia], [Descripcion]) VALUES (4, N'Contiene informacion politica')
GO
INSERT INTO [dbo].[Usuarios]
           ([Nombre]
           ,[Apellido]
           ,[FechaNacimiento]
           ,[UserName]
           ,[Email]
           ,[Password]
           ,[Foto]
           ,[TipoUsuario]
           ,[FechaCracion]
           ,[Activo]
           ,[Token])
     VALUES
           ('MARCOS'
           ,'PEREZ'
           ,'01/02/1978'
           ,'admin'
           ,'admin@test.com'
           ,'12121212'
           ,''
           ,1
           ,getdate()
           ,1
           ,'19E41C31E74A4526')
GO
