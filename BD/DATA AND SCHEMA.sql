USE [master]
GO
/****** Object:  Database [DBJoelRojasEncuesta]    Script Date: 6/04/2022 17:39:44 ******/
CREATE DATABASE [DBJoelRojasEncuesta2]

USE [DBJoelRojasEncuesta2]
GO

CREATE TABLE [dbo].[EncuestaCab](
	[IdEncuesta] [int] IDENTITY(1,1) NOT NULL,
	[NombreEncuesta] [varchar](50) NULL,
	[DescripcionEncuesta] [varchar](50) NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_EncuestaCab] PRIMARY KEY CLUSTERED 
(
	[IdEncuesta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EncuestaDet]    Script Date: 6/04/2022 17:39:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EncuestaDet](
	[IdEncuestaDetalle] [int] IDENTITY(1,1) NOT NULL,
	[IdEncuesta] [int] NULL,
	[NombreCampo] [varchar](50) NULL,
	[TituloCampo] [varchar](50) NULL,
	[EsRequerido] [char](1) NULL,
	[TipoCampo] [varchar](50) NULL,
 CONSTRAINT [PK_EncuestaDet] PRIMARY KEY CLUSTERED 
(
	[IdEncuestaDetalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 6/04/2022 17:39:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Username] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[EncuestaCab] ON 
GO
INSERT [dbo].[EncuestaCab] ([IdEncuesta], [NombreEncuesta], [DescripcionEncuesta], [Estado]) VALUES (1, N'Prueba Nombre encuesta', N'Prueba Desc.', 0)
GO
SET IDENTITY_INSERT [dbo].[EncuestaCab] OFF
GO
SET IDENTITY_INSERT [dbo].[EncuestaDet] ON 
GO
INSERT [dbo].[EncuestaDet] ([IdEncuestaDetalle], [IdEncuesta], [NombreCampo], [TituloCampo], [EsRequerido], [TipoCampo]) VALUES (1, 1, N'Prueba campo Editar', N'Prueba Titulo Editar', N'N', N'Prueba Tipo Campo')
GO
SET IDENTITY_INSERT [dbo].[EncuestaDet] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [Username], [Password]) VALUES (1, N'Joel', N'Rojas', N'test', N'test')
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
USE [master]
GO
ALTER DATABASE [DBJoelRojasEncuesta] SET  READ_WRITE 
GO
