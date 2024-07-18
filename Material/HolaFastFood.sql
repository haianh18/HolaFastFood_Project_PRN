USE [master]
GO
/****** Object:  Database [HolaFastFood]    Script Date: 12/03/2016 16:10:29 ******/
CREATE DATABASE [HolaFastFood] 
GO
ALTER DATABASE [HolaFastFood] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HolaFastFood].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HolaFastFood] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [HolaFastFood] SET ANSI_NULLS OFF
GO
ALTER DATABASE [HolaFastFood] SET ANSI_PADDING OFF
GO
ALTER DATABASE [HolaFastFood] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [HolaFastFood] SET ARITHABORT OFF
GO
ALTER DATABASE [HolaFastFood] SET AUTO_CLOSE ON
GO
ALTER DATABASE [HolaFastFood] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [HolaFastFood] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [HolaFastFood] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [HolaFastFood] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [HolaFastFood] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [HolaFastFood] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [HolaFastFood] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [HolaFastFood] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [HolaFastFood] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [HolaFastFood] SET  ENABLE_BROKER
GO
ALTER DATABASE [HolaFastFood] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [HolaFastFood] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [HolaFastFood] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [HolaFastFood] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [HolaFastFood] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [HolaFastFood] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [HolaFastFood] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [HolaFastFood] SET  READ_WRITE
GO
ALTER DATABASE [HolaFastFood] SET RECOVERY SIMPLE
GO
ALTER DATABASE [HolaFastFood] SET  MULTI_USER
GO
ALTER DATABASE [HolaFastFood] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [HolaFastFood] SET DB_CHAINING OFF
GO
USE [HolaFastFood]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 12/03/2016 16:10:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID('dbo.Account', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Account;
END
CREATE TABLE [dbo].[Account](
	[UserName] [nvarchar](100) NOT NULL,
	[DisplayName] [nvarchar](100) NOT NULL,
	[PassWord] [nvarchar](1000) NOT NULL,
	[Type] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Account] ([UserName], [DisplayName], [PassWord], [Type]) VALUES (N'admin', N'Admin', N'admin123@', 1)
INSERT [dbo].[Account] ([UserName], [DisplayName], [PassWord], [Type]) VALUES (N'staff_1', N'Staff-1', N'staff123@', 0)
INSERT [dbo].[Account] ([UserName], [DisplayName], [PassWord], [Type]) VALUES (N'staff_2', N'Staff-2', N'staff456@', 0)

/****** Object:  Table [dbo].[TableFood]    Script Date: 12/03/2016 16:10:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID('dbo.TableFood', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.TableFood;
END
CREATE TABLE [dbo].[TableFood](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[status] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[TableFood] ON
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (1, N'Table 1', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (2, N'Table 2', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (3, N'Table 3', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (4, N'Table 4', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (5, N'Table 5', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (6, N'Table 6', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (7, N'Table 7', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (8, N'Table 8', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (9, N'Table 9', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (10, N'Table 10', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (11, N'Table 11', N'Empty')
INSERT [dbo].[TableFood] ([id], [name], [status]) VALUES (12, N'Table 12', N'Empty')
SET IDENTITY_INSERT [dbo].[TableFood] OFF

/****** Object:  Table [dbo].[FoodCategory]    Script Date: 12/03/2016 16:10:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID('dbo.FoodCategory', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.FoodCategory;
END
CREATE TABLE [dbo].[FoodCategory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[FoodCategory] ON
INSERT [dbo].[FoodCategory] ([id], [name]) VALUES (1, N'Pizza')
INSERT [dbo].[FoodCategory] ([id], [name]) VALUES (2, N'Chicken')
INSERT [dbo].[FoodCategory] ([id], [name]) VALUES (3, N'Hamburger')
INSERT [dbo].[FoodCategory] ([id], [name]) VALUES (4, N'Desserts')
INSERT [dbo].[FoodCategory] ([id], [name]) VALUES (5, N'Drinks')
SET IDENTITY_INSERT [dbo].[FoodCategory] OFF

/****** Object:  Table [dbo].[Food]    Script Date: 12/03/2016 16:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID('dbo.Food', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Food;
END
CREATE TABLE [dbo].[Food](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[idCategory] [int] NOT NULL,
	[price] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Food] ON
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (1, N'Canadian Pizza S', 1, 120000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (2, N'Common Pizza S', 1, 50000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (3, N'Grilled Chicken Wings', 2, 60000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (4, N'Grilled Chicken Legs', 2, 75000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (5, N'Beef Hamburger', 3, 75000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (6, N'Panaconta', 4, 10000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (7, N'7-Up', 5, 15000)
INSERT [dbo].[Food] ([id], [name], [idCategory], [price]) VALUES (8, N'Coke', 5, 12000)
SET IDENTITY_INSERT [dbo].[Food] OFF
/****** Object:  Table [dbo].[Bill]    Script Date: 12/03/2016 16:10:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO 
IF OBJECT_ID('dbo.Bill', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Bill;
END
CREATE TABLE [dbo].[Bill](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[DateCheckIn] [datetime] NOT NULL,
	[DateCheckOut] [datetime] NULL,
	[idTable] [int] NOT NULL,
	[status] [int] NOT NULL,
	[userName] [nvarchar](100) null,
	[discount] [int] NULL,
	[totalPrice] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

IF OBJECT_ID('dbo.BillInfo', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.BillInfo;
END
CREATE TABLE [dbo].[BillInfo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idBill] [int] NOT NULL,
	[idFood] [int] NOT NULL,
	[count] [int] NOT NULL,
	CONSTRAINT FK_idBill_BillInfo FOREIGN KEY (idBill)
        REFERENCES Bill(id)
        ON DELETE CASCADE,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Default [DF__Account__Display__07020F21]    Script Date: 12/03/2016 16:10:30 ******/
ALTER TABLE [dbo].[Account] ADD  DEFAULT (N'User') FOR [DisplayName]
GO
/****** Object:  Default [DF__Account__PassWor__07F6335A]    Script Date: 12/03/2016 16:10:30 ******/
ALTER TABLE [dbo].[Account] ADD  DEFAULT ((0)) FOR [PassWord]
GO
/****** Object:  Default [DF__Account__Type__08EA5793]    Script Date: 12/03/2016 16:10:30 ******/
ALTER TABLE [dbo].[Account] ADD  DEFAULT ((0)) FOR [Type]
GO
/****** Object:  Default [DF__TableFood__name__014935CB]    Script Date: 12/03/2016 16:10:30 ******/
ALTER TABLE [dbo].[TableFood] ADD  DEFAULT (N'No Name Table') FOR [name]
GO
/****** Object:  Default [DF__TableFood__statu__023D5A04]    Script Date: 12/03/2016 16:10:30 ******/
ALTER TABLE [dbo].[TableFood] ADD  DEFAULT (N'Empty') FOR [status]
GO
/****** Object:  Default [DF__FoodCatego__name__0DAF0CB0]    Script Date: 12/03/2016 16:10:31 ******/
ALTER TABLE [dbo].[FoodCategory] ADD  DEFAULT (N'No Name Category') FOR [name]
GO
/****** Object:  Default [DF__Food__name__1273C1CD]    Script Date: 12/03/2016 16:10:41 ******/
ALTER TABLE [dbo].[Food] ADD  DEFAULT (N'No Name Food/Drinks') FOR [name]
GO
/****** Object:  Default [DF__Food__price__1367E606]    Script Date: 12/03/2016 16:10:41 ******/
ALTER TABLE [dbo].[Food] ADD  DEFAULT ((0)) FOR [price]
GO
/****** Object:  Default [DF__Bill__DateCheckI__1920BF5C]    Script Date: 12/03/2016 16:10:41 ******/
ALTER TABLE [dbo].[Bill] ADD  DEFAULT (getdate()) FOR [DateCheckIn]
GO
/****** Object:  Default [DF__Bill__status__1A14E395]    Script Date: 12/03/2016 16:10:41 ******/
ALTER TABLE [dbo].[Bill] ADD  DEFAULT ((0)) FOR [status]
GO
/****** Object:  Default [DF__BillInfo__count__1FCDBCEB]    Script Date: 12/03/2016 16:10:41 ******/
ALTER TABLE [dbo].[BillInfo] ADD  DEFAULT ((0)) FOR [count]
GO
/****** Object:  ForeignKey [FK__Food__price__145C0A3F]    Script Date: 12/03/2016 16:10:41 ******/
ALTER TABLE [dbo].[Food]  WITH CHECK ADD FOREIGN KEY([idCategory])
REFERENCES [dbo].[FoodCategory] ([id])
GO
/****** Object:  ForeignKey [FK__Bill__status__1B0907CE]    Script Date: 12/03/2016 16:10:41 ******/
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD FOREIGN KEY([idTable])
REFERENCES [dbo].[TableFood] ([id])
GO
/****** Object:  ForeignKey [FK__Bill__account_id__656C112C]    Script Date: 12/03/2016 16:10:41 ******/
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD FOREIGN KEY([userName])
REFERENCES [dbo].[Account] ([UserName])
GO
/****** Object:  ForeignKey [FK__BillInfo__count__20C1E124]    Script Date: 12/03/2016 16:10:41
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD FOREIGN KEY([idBill])
REFERENCES [dbo].[Bill] ([id])
GO 

ALTER TABLE [dbo].[BillInfo]
ADD CONSTRAINT fk_cascade_on_delete_Bill
FOREIGN KEY ([idBill]) REFERENCES [dbo].[Bill] ([id])
ON DELETE CASCADE;
******/
/****** Object:  ForeignKey [FK__BillInfo__idFood__21B6055D]    Script Date: 12/03/2016 16:10:41 ******/
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD FOREIGN KEY([idFood])
REFERENCES [dbo].[Food] ([id])
GO


