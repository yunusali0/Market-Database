
/****** Object:  Table [dbo].[Categories]    Script Date: 05-Apr-18 9:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItems]    Script Date: 05-Apr-18 9:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NULL,
	[StockId] [int] NULL,
	[Count] [money] NULL,
	[SellPrice] [money] NULL,
 CONSTRAINT [PK_OrderItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 05-Apr-18 9:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[Total] [money] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 05-Apr-18 9:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Price] [money] NULL,
	[CategoryId] [int] NULL,
	[TypeId] [int] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stocks]    Script Date: 05-Apr-18 9:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stocks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[Barcode] [nvarchar](150) NULL,
	[EnterDate] [datetime] NULL,
	[SellDate] [datetime] NULL,
	[ExpireDate] [datetime] NULL,
	[Count] [money] NULL,
 CONSTRAINT [PK_Stocks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Types]    Script Date: 05-Apr-18 9:17:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Types](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Types] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name]) VALUES (1, N'Qida')
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[OrderItems] ON 

INSERT [dbo].[OrderItems] ([Id], [OrderId], [StockId], [Count], [SellPrice]) VALUES (1, 3, 1, 1.0000, 2.3500)
INSERT [dbo].[OrderItems] ([Id], [OrderId], [StockId], [Count], [SellPrice]) VALUES (2, 3, 1, 3.0000, 2.3500)
INSERT [dbo].[OrderItems] ([Id], [OrderId], [StockId], [Count], [SellPrice]) VALUES (3, 3, 5, 40.0000, 2.3500)
INSERT [dbo].[OrderItems] ([Id], [OrderId], [StockId], [Count], [SellPrice]) VALUES (4, 3, 4, 5.4000, 1.1000)
INSERT [dbo].[OrderItems] ([Id], [OrderId], [StockId], [Count], [SellPrice]) VALUES (5, 4, 1, 1.0000, 2.3500)
INSERT [dbo].[OrderItems] ([Id], [OrderId], [StockId], [Count], [SellPrice]) VALUES (6, 4, 4, 1.0000, 1.1000)
INSERT [dbo].[OrderItems] ([Id], [OrderId], [StockId], [Count], [SellPrice]) VALUES (7, 4, 5, 1.0000, 2.3500)
INSERT [dbo].[OrderItems] ([Id], [OrderId], [StockId], [Count], [SellPrice]) VALUES (8, 5, 1, 123.0000, 2.3500)
INSERT [dbo].[OrderItems] ([Id], [OrderId], [StockId], [Count], [SellPrice]) VALUES (9, 6, 1, 1.0000, 2.3500)
INSERT [dbo].[OrderItems] ([Id], [OrderId], [StockId], [Count], [SellPrice]) VALUES (10, 7, 1, 1.0000, 2.3500)
INSERT [dbo].[OrderItems] ([Id], [OrderId], [StockId], [Count], [SellPrice]) VALUES (11, 7, 4, 1.0000, 1.1000)
SET IDENTITY_INSERT [dbo].[OrderItems] OFF
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([Id], [Date], [Total]) VALUES (3, CAST(N'2018-04-05T20:20:34.580' AS DateTime), 109.3400)
INSERT [dbo].[Orders] ([Id], [Date], [Total]) VALUES (4, CAST(N'2018-04-05T20:23:05.323' AS DateTime), 5.8000)
INSERT [dbo].[Orders] ([Id], [Date], [Total]) VALUES (5, CAST(N'2018-04-05T16:25:40.217' AS DateTime), 289.0500)
INSERT [dbo].[Orders] ([Id], [Date], [Total]) VALUES (6, CAST(N'2012-04-05T19:27:15.003' AS DateTime), 2.3500)
INSERT [dbo].[Orders] ([Id], [Date], [Total]) VALUES (7, CAST(N'2018-04-05T21:15:33.240' AS DateTime), 3.4500)
SET IDENTITY_INSERT [dbo].[Orders] OFF
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Name], [Price], [CategoryId], [TypeId]) VALUES (1, N'Winston XS Blue', 2.3500, 1, 1)
INSERT [dbo].[Products] ([Id], [Name], [Price], [CategoryId], [TypeId]) VALUES (2, N'Ankara Makaron', 1.1000, 1, 2)
SET IDENTITY_INSERT [dbo].[Products] OFF
SET IDENTITY_INSERT [dbo].[Stocks] ON 

INSERT [dbo].[Stocks] ([Id], [ProductId], [Barcode], [EnterDate], [SellDate], [ExpireDate], [Count]) VALUES (1, 1, N'123', CAST(N'2018-04-03T19:51:06.290' AS DateTime), NULL, CAST(N'2018-05-23T20:48:57.467' AS DateTime), 79.0000)
INSERT [dbo].[Stocks] ([Id], [ProductId], [Barcode], [EnterDate], [SellDate], [ExpireDate], [Count]) VALUES (4, 2, N'456', CAST(N'2018-04-03T20:34:33.123' AS DateTime), NULL, CAST(N'2018-04-08T21:30:18.927' AS DateTime), 50.0000)
INSERT [dbo].[Stocks] ([Id], [ProductId], [Barcode], [EnterDate], [SellDate], [ExpireDate], [Count]) VALUES (5, 1, N'789', CAST(N'2018-04-02T21:28:25.930' AS DateTime), NULL, CAST(N'2018-04-13T21:28:25.930' AS DateTime), 70.0000)
SET IDENTITY_INSERT [dbo].[Stocks] OFF
SET IDENTITY_INSERT [dbo].[Types] ON 

INSERT [dbo].[Types] ([Id], [Name]) VALUES (1, N'Ədəd')
INSERT [dbo].[Types] ([Id], [Name]) VALUES (2, N'Kilogram')
INSERT [dbo].[Types] ([Id], [Name]) VALUES (3, N'Metr')
SET IDENTITY_INSERT [dbo].[Types] OFF
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItems_Orders] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_Orders]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItems_Stocks] FOREIGN KEY([StockId])
REFERENCES [dbo].[Stocks] ([Id])
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_Stocks]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Types] FOREIGN KEY([TypeId])
REFERENCES [dbo].[Types] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Types]
GO
ALTER TABLE [dbo].[Stocks]  WITH CHECK ADD  CONSTRAINT [FK_Stocks_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[Stocks] CHECK CONSTRAINT [FK_Stocks_Products]
GO
USE [master]
GO
ALTER DATABASE [Shopping] SET  READ_WRITE 
GO
