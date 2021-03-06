USE [bestrestaurant]
GO
/****** Object:  Table [dbo].[cuisine]    Script Date: 2/25/2016 3:11:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cuisine](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[restaurant]    Script Date: 2/25/2016 3:11:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[restaurant](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[description] [varchar](255) NULL,
	[cuisine_id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[review]    Script Date: 2/25/2016 3:11:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[review](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [varchar](255) NULL,
	[restaurant_id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[cuisine] ON 

INSERT [dbo].[cuisine] ([id], [name]) VALUES (2, N'Merica')
SET IDENTITY_INSERT [dbo].[cuisine] OFF
SET IDENTITY_INSERT [dbo].[restaurant] ON 

INSERT [dbo].[restaurant] ([id], [name], [description], [cuisine_id]) VALUES (5, N'Burgers', N'we sell burgers!', 2)
INSERT [dbo].[restaurant] ([id], [name], [description], [cuisine_id]) VALUES (6, N'Burgers', N'yum', 2)
SET IDENTITY_INSERT [dbo].[restaurant] OFF
SET IDENTITY_INSERT [dbo].[review] ON 

INSERT [dbo].[review] ([id], [description], [restaurant_id]) VALUES (3, N'This place was great!', 2)
INSERT [dbo].[review] ([id], [description], [restaurant_id]) VALUES (2, N'This place was great!', 2)
SET IDENTITY_INSERT [dbo].[review] OFF
