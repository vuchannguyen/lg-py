USE [master]
GO
/****** Object:  Database [Weedon]    Script Date: 10/17/2013 10:43:35 ******/
CREATE DATABASE [Weedon] ON  PRIMARY 
( NAME = N'Weedon', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\Weedon.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Weedon_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\Weedon_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Weedon] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Weedon].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Weedon] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [Weedon] SET ANSI_NULLS OFF
GO
ALTER DATABASE [Weedon] SET ANSI_PADDING OFF
GO
ALTER DATABASE [Weedon] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [Weedon] SET ARITHABORT OFF
GO
ALTER DATABASE [Weedon] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [Weedon] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [Weedon] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [Weedon] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [Weedon] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [Weedon] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [Weedon] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [Weedon] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [Weedon] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [Weedon] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [Weedon] SET  DISABLE_BROKER
GO
ALTER DATABASE [Weedon] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [Weedon] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [Weedon] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [Weedon] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [Weedon] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [Weedon] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [Weedon] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [Weedon] SET  READ_WRITE
GO
ALTER DATABASE [Weedon] SET RECOVERY SIMPLE
GO
ALTER DATABASE [Weedon] SET  MULTI_USER
GO
ALTER DATABASE [Weedon] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [Weedon] SET DB_CHAINING OFF
GO
USE [Weedon]
GO
/****** Object:  Table [dbo].[UserGroup]    Script Date: 10/17/2013 10:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](20) NOT NULL,
	[MoTa] [nvarchar](200) NULL,
 CONSTRAINT [PK_NhomNguoiDung] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[UserGroup] ON
INSERT [dbo].[UserGroup] ([Id], [Ten], [MoTa]) VALUES (1, N'Admin', N'Role Admin')
INSERT [dbo].[UserGroup] ([Id], [Ten], [MoTa]) VALUES (2, N'Manager', N'Role Manager')
INSERT [dbo].[UserGroup] ([Id], [Ten], [MoTa]) VALUES (3, N'Staff', N'Role Staff')
SET IDENTITY_INSERT [dbo].[UserGroup] OFF
/****** Object:  Table [dbo].[NguyenLieu]    Script Date: 10/17/2013 10:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NguyenLieu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MaNguyenLieu] [varchar](10) NOT NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[MoTa] [nvarchar](200) NULL,
	[DonViTinh] [nvarchar](10) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[DeleteFlag] [bit] NOT NULL,
 CONSTRAINT [PK_NguyenLieu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[NguyenLieu] ON
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (1, N'0001', N'Sua Tuoi', N'1 hop 1000ml', N'ml', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (2, N'0002', N'Syrup Blue Curacao', N'1 chai 750ml', N'ml', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (3, N'0003', N'Syrup Raspberry', N'1 chai 750 ml', N'ml', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (4, N'0004', N'Syrup Green Apple', N'1 chai 750 ml', N'ml', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (5, N'0005', N'Syrup Green Mint', N'1 chai 750 ml', N'ml', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (6, N'0006', N'Syrup Kiwi', N'1 chai 750 ml', N'ml', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (7, N'0007', N'Syrup Blood Orange', N'1 chai 750 ml', N'ml', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (8, N'0008', N'Ca phe tuoi', N'1 chai 500ml', N'ml', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (9, N'0009', N'Sauce Chocolate', N'', N'ml', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (10, N'0010', N'Sauce Caramel', N'', N'ml', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (11, N'0011', N'Sua chua', N'1 chai 880ml', N'ml', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (12, N'0012', N'Nước đường', N'', N'ml', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (13, N'0013', N'Chocochip nhỏ', N'', N'gram', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (14, N'0014', N'Whipping Cream', N'1 hop 1000ml', N'ml', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (15, N'0015', N'Sua beo', N'1 hop 454ml', N'ml', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (16, N'0016', N'Kem Topping Base', N'1 hop 900ml', N'ml', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (17, N'0017', N'Banh Oreo', N'1 hop 22 cai', N'cai', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (18, N'0018', N'Banh Waff', N'1 bich 15 cai', N'cai', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (19, N'0019', N'Banh Chocopie', N'', N'cai', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (20, N'0020', N'Banh Cupcake', N'', N'cai', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (21, N'0021', N'Mut dau', N'1 chai 1000ml', N'ml', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (22, N'0022', N'Mut dua xoai', N'1 chai 1000ml', N'ml', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (23, N'0023', N'Mut Blueberry', N'1 chai 1000ml', N'ml', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (24, N'0024', N'Mut Raspberry', N'1 chai 1000ml', N'ml', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (25, N'0025', N'Binh gas', N'1 hop 10 cai', N'cai', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (26, N'0026', N'Hat ca phe', N'', N'gram', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (27, N'0027', N'Keo golia', N'1 hop 100 vien', N'vien', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (28, N'0028', N'Ly', N'', N'cai', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (29, N'0029', N'Nap', N'', N'cai', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (30, N'0030', N'Ong hut', N'Gia su 1kg duoc 100 cai', N'gram', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (31, N'0031', N'Bao', N'Gia su 1kg duoc 250 cai', N'gram', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (32, N'0032', N'Da', N'Gia su 1 thung duoc 50 ly', N'thung', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (33, N'0033', N'Bot Frap', N'', N'gram', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (34, N'0034', N'Bot Chocolate den', N'', N'gram', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (35, N'0035', N'Bot Chocolate trang', N'', N'gram', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (36, N'0036', N'Bot Tra Xanh', N'', N'gram', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (37, N'0037', N'Duong', N'', N'gram', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (38, N'0038', N'Soda', N'', N'lon', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (39, N'0039', N'Bot cacao thuong', N'', N'gram', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (40, N'0040', N'Chanh', N'', N'trai', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (41, N'0041', N'Jelly Coffee', N'', N'gram', 1, 0)
INSERT [dbo].[NguyenLieu] ([Id], [MaNguyenLieu], [Ten], [MoTa], [DonViTinh], [IsActive], [DeleteFlag]) VALUES (42, N'0042', N'Bot Jelly', N'', N'goi', 1, 0)
SET IDENTITY_INSERT [dbo].[NguyenLieu] OFF
/****** Object:  Table [dbo].[KhachHangGroup]    Script Date: 10/17/2013 10:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[KhachHangGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ma] [varchar](6) NOT NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[MoTa] [nvarchar](200) NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[KhachHangGroup] ON
INSERT [dbo].[KhachHangGroup] ([Id], [Ma], [Ten], [MoTa]) VALUES (1, N'KT', N'Khách thường', NULL)
INSERT [dbo].[KhachHangGroup] ([Id], [Ma], [Ten], [MoTa]) VALUES (2, N'KHTT', N'Khách hàng thân thiết', NULL)
INSERT [dbo].[KhachHangGroup] ([Id], [Ma], [Ten], [MoTa]) VALUES (3, N'VIP', N'VIP', NULL)
SET IDENTITY_INSERT [dbo].[KhachHangGroup] OFF
/****** Object:  Table [dbo].[Setting]    Script Date: 10/17/2013 10:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Setting](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](20) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
	[MoTa] [nvarchar](200) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Setting] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Setting] ON
INSERT [dbo].[Setting] ([Id], [Ten], [Value], [MoTa], [IsActive]) VALUES (1, N'%Chenh Lech Cho Phep', N'10', N'% chenh lech cho phep giua luong nguyen lieu su dung thuc te so voi luong nguyen lieu su dung ly thuyet', 1)
INSERT [dbo].[Setting] ([Id], [Ten], [Value], [MoTa], [IsActive]) VALUES (2, N'Thứ tự NL', N'1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 40, 41, 15, 16, 17, 18, 19, 20, 42, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, ', N'Sắp xếp thứ tự NL theo bảng Nhật Ký NL', 1)
SET IDENTITY_INSERT [dbo].[Setting] OFF
/****** Object:  Table [dbo].[SanPhamGroup]    Script Date: 10/17/2013 10:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SanPhamGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ma] [varchar](6) NOT NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[MoTa] [nvarchar](200) NULL,
 CONSTRAINT [PK_SanPhamGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[SanPhamGroup] ON
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [MoTa]) VALUES (1, N'1', N'Frappu', N'Nhom san pham frappu')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [MoTa]) VALUES (2, N'2', N'Chocolate', N'Cac san pham chocolate')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [MoTa]) VALUES (3, N'3', N'Cake Blended', N'Cac san pham banh xay')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [MoTa]) VALUES (4, N'4', N'Milkshake', N'Cac san pham sua lac')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [MoTa]) VALUES (5, N'5', N'Yogurt Blended', N'Cac san pham sua chua da xay')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [MoTa]) VALUES (6, N'6', N'Soda', N'Cac san pham soda')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [MoTa]) VALUES (7, N'0', N'Kem Topping', N'')
SET IDENTITY_INSERT [dbo].[SanPhamGroup] OFF
/****** Object:  Table [dbo].[SanPham]    Script Date: 10/17/2013 10:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SanPham](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MaSanPham] [varchar](10) NOT NULL,
	[IdGroup] [int] NOT NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[MoTa] [nvarchar](200) NULL,
	[IsActive] [bit] NOT NULL,
	[DeleteFlag] [bit] NOT NULL,
 CONSTRAINT [PK_SanPham] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[SanPham] ON
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (1, N'10001', 1, N'Frappuccino', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (2, N'10002', 1, N'Caramel Freezer', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (3, N'10003', 1, N'Green Tea Freezer', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (4, N'20001', 2, N'Chocolate Freezer', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (5, N'20002', 2, N'White Chocolate Freezer', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (6, N'20003', 2, N'Black&White Chocolate Freezer', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (7, N'20004', 2, N'Chocomint', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (8, N'30001', 3, N'Cookie Blended', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (9, N'30002', 3, N'Wafer Blended', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (10, N'30003', 3, N'Chocopie Blended', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (11, N'30004', 3, N'CupCake Blended', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (12, N'30005', 3, N'CupCake Blended', N'', 1, 1)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (13, N'40001', 4, N'Coffee Shake', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (14, N'40002', 4, N'Green Tea Shake', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (15, N'40003', 4, N'Blueberry Shake', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (16, N'40004', 4, N'Raspberry Shake', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (17, N'40005', 4, N'Straw Shake', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (18, N'40006', 4, N'Mango-Pine Shake', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (19, N'50001', 5, N'Blueberry Yogurt', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (20, N'50002', 5, N'Raspberry Yogurt', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (21, N'50003', 5, N'Straw Yogurt', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (22, N'50004', 5, N'Mango-Pine Yogurt', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (23, N'50005', 5, N'Mango-Pine Yogurt', N'', 1, 1)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (24, N'60001', 6, N'Blue Curacao Soda', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (25, N'60002', 6, N'Raspberry Soda', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (26, N'60003', 6, N'Green Apple Soda', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (27, N'60004', 6, N'Mint Soda', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (28, N'60005', 6, N'Blood Orange Soda', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (29, N'60006', 6, N'Kiwi Soda', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (30, N'00001', 7, N'Kem topping trang', N'1 binh kem topping base 0.5l
(tuong duong 450ml kem)', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (31, N'00002', 7, N'Kem topping tra xanh', N'Kem topping tra xanh', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (32, N'10004', 1, N'Frappu Jelly Coffee', N'', 1, 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [Ten], [MoTa], [IsActive], [DeleteFlag]) VALUES (33, N'40007', 4, N'Jelly Coffee Shake', N'', 1, 0)
SET IDENTITY_INSERT [dbo].[SanPham] OFF
/****** Object:  Table [dbo].[NhatKyNguyenLieu]    Script Date: 10/17/2013 10:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhatKyNguyenLieu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdNguyenLieu] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[TonDau] [float] NOT NULL,
	[Nhap] [float] NULL,
	[Huy] [float] NULL,
	[TonCuoi] [float] NOT NULL,
	[SuDung] [float] NOT NULL,
	[GhiChu] [nvarchar](200) NULL,
	[DeleteFlag] [bit] NOT NULL,
 CONSTRAINT [PK_NhatKyNguyenLieu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[NhatKyNguyenLieu] ON
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (1, 19, CAST(0x0000A17400C14D5F AS DateTime), 18, 0, 0, 15, 3, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (2, 20, CAST(0x0000A17400C14D5F AS DateTime), 12, 0, 0, 12, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (3, 17, CAST(0x0000A17400C14D5F AS DateTime), 55, 0, 0, 44, 11, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (4, 18, CAST(0x0000A17400C14D5F AS DateTime), 105, 0, 0, 105, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (5, 31, CAST(0x0000A17400C14D5F AS DateTime), 200, 0, 0, 100, 100, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (6, 25, CAST(0x0000A17400C14D5F AS DateTime), 27, 0, 0, 24, 3, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (7, 39, CAST(0x0000A17400C14D5F AS DateTime), 450, 0, 0, 450, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (8, 34, CAST(0x0000A17400C14D5F AS DateTime), 1400, 0, 0, 1200, 200, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (9, 35, CAST(0x0000A17400C14D5F AS DateTime), 100, 0, 0, 0, 100, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (10, 33, CAST(0x0000A17400C14D5F AS DateTime), 1000, 0, 0, 400, 600, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (11, 36, CAST(0x0000A17400C14D5F AS DateTime), 600, 0, 0, 550, 50, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (12, 8, CAST(0x0000A17400C14D5F AS DateTime), 500, 0, 0, 400, 100, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (13, 13, CAST(0x0000A17400C14D5F AS DateTime), 180, 0, 0, 80, 100, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (14, 32, CAST(0x0000A17400C14D5F AS DateTime), 1, 0, 0, 0, 1, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (15, 37, CAST(0x0000A17400C14D5F AS DateTime), 3000, 0, 0, 3000, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (16, 26, CAST(0x0000A17400C14D5F AS DateTime), 1200, 0, 0, 1100, 100, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (17, 16, CAST(0x0000A17400C14D5F AS DateTime), 10080, 0, 0, 9270, 810, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (18, 27, CAST(0x0000A17400C14D5F AS DateTime), 100, 0, 0, 100, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (19, 28, CAST(0x0000A17400C14D5F AS DateTime), 235, 0, 0, 200, 35, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (20, 23, CAST(0x0000A17400C14D5F AS DateTime), 4500, 0, 0, 4200, 300, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (21, 21, CAST(0x0000A17400C14D5F AS DateTime), 3500, 0, 0, 3300, 200, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (22, 22, CAST(0x0000A17400C14D5F AS DateTime), 1300, 0, 0, 1100, 200, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (23, 24, CAST(0x0000A17400C14D5F AS DateTime), 4300, 0, 0, 4300, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (24, 29, CAST(0x0000A17400C14D5F AS DateTime), 317, 0, 0, 282, 35, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (25, 12, CAST(0x0000A17400C14D5F AS DateTime), 1500, 0, 0, 800, 700, N'Cac ban ghi theo don vi chai syrup hay lit?', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (26, 30, CAST(0x0000A17400C14D5F AS DateTime), 0, 0, 0, 0, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (27, 10, CAST(0x0000A17400C14D5F AS DateTime), 1200, 0, 0, 1200, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (28, 9, CAST(0x0000A17400C14D5F AS DateTime), 1300, 0, 0, 1250, 50, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (29, 38, CAST(0x0000A17400C14D5F AS DateTime), 15, 0, 0, 15, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (30, 15, CAST(0x0000A17400C14D5F AS DateTime), 4100, 0, 0, 2750, 1350, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (31, 11, CAST(0x0000A17400C14D5F AS DateTime), 704, 0, 0, 528, 176, N'Cac ban ghi theo don vi chai hay lit?', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (32, 1, CAST(0x0000A17400C14D5F AS DateTime), 8500, 0, 0, 6500, 2000, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (33, 7, CAST(0x0000A17400C14D5F AS DateTime), 870, 0, 0, 870, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (34, 2, CAST(0x0000A17400C14D5F AS DateTime), 950, 0, 0, 950, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (35, 4, CAST(0x0000A17400C14D5F AS DateTime), 930, 0, 0, 930, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (36, 5, CAST(0x0000A17400C14D5F AS DateTime), 1100, 0, 0, 1100, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (37, 6, CAST(0x0000A17400C14D5F AS DateTime), 1580, 0, 0, 1580, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (38, 3, CAST(0x0000A17400C14D5F AS DateTime), 1800, 0, 0, 1800, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (39, 14, CAST(0x0000A17400C14D5F AS DateTime), 14200, 0, 0, 13700, 500, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (40, 1, CAST(0x0000A18901142B38 AS DateTime), 23700, 0, 0, 23000, 700, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (41, 2, CAST(0x0000A18901142B38 AS DateTime), 800, 0, 0, 730, 70, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (42, 3, CAST(0x0000A18901142B38 AS DateTime), 1600, 0, 0, 1560, 40, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (43, 4, CAST(0x0000A18901142B38 AS DateTime), 700, 0, 0, 690, 10, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (44, 5, CAST(0x0000A18901142B38 AS DateTime), 1050, 0, 0, 1020, 30, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (45, 6, CAST(0x0000A18901142B38 AS DateTime), 1360, 0, 0, 1340, 20, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (46, 7, CAST(0x0000A18901142B38 AS DateTime), 1600, 0, 0, 1600, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (47, 8, CAST(0x0000A18901142B38 AS DateTime), 1100, 0, 0, 1050, 50, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (48, 9, CAST(0x0000A18901142B38 AS DateTime), 1600, 0, 0, 1600, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (49, 10, CAST(0x0000A18901142B38 AS DateTime), 600, 0, 0, 380, 220, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (50, 11, CAST(0x0000A18901142B38 AS DateTime), 700, 0, 0, 460, 240, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (51, 12, CAST(0x0000A18901142B38 AS DateTime), 1900, 0, 0, 1415, 485, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (52, 13, CAST(0x0000A18901142B38 AS DateTime), 420, 0, 0, 400, 20, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (53, 14, CAST(0x0000A18901142B38 AS DateTime), 8300, 0, 0, 8160, 140, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (54, 15, CAST(0x0000A18901142B38 AS DateTime), 5000, 0, 0, 3175, 1825, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (55, 16, CAST(0x0000A18901142B38 AS DateTime), 6600, 0, 0, 7120, -520, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (56, 17, CAST(0x0000A18901142B38 AS DateTime), 25, 0, 0, 20, 5, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (57, 18, CAST(0x0000A18901142B38 AS DateTime), 95, 0, 0, 95, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (58, 19, CAST(0x0000A18901142B38 AS DateTime), 14, 0, 0, 13, 1, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (59, 20, CAST(0x0000A18901142B38 AS DateTime), 16, 0, 0, 16, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (60, 21, CAST(0x0000A18901142B38 AS DateTime), 2500, 0, 0, 2500, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (61, 22, CAST(0x0000A18901142B38 AS DateTime), 1000, 0, 0, 820, 180, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (62, 23, CAST(0x0000A18901142B38 AS DateTime), 500, 0, 0, 420, 80, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (63, 24, CAST(0x0000A18901142B38 AS DateTime), 4000, 0, 0, 3940, 60, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (64, 25, CAST(0x0000A18901142B38 AS DateTime), 30, 0, 0, 29, 1, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (65, 26, CAST(0x0000A18901142B38 AS DateTime), 250, 0, 0, 250, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (66, 27, CAST(0x0000A18901142B38 AS DateTime), 600, 0, 0, 600, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (67, 28, CAST(0x0000A18901142B38 AS DateTime), 126, 0, 0, 112, 14, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (68, 29, CAST(0x0000A18901142B38 AS DateTime), 133, 0, 0, 119, 14, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (69, 30, CAST(0x0000A18901142B38 AS DateTime), 1900, 0, 0, 1900, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (70, 31, CAST(0x0000A18901142B38 AS DateTime), 700, 0, 0, 700, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (71, 32, CAST(0x0000A18901142B38 AS DateTime), 1, 0, 0, 0, 1, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (72, 33, CAST(0x0000A18901142B38 AS DateTime), 3200, 0, 0, 3100, 100, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (73, 34, CAST(0x0000A18901142B38 AS DateTime), 1300, 0, 0, 1220, 80, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (74, 35, CAST(0x0000A18901142B38 AS DateTime), 600, 0, 0, 600, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (75, 36, CAST(0x0000A18901142B38 AS DateTime), 250, 0, 0, 250, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (76, 37, CAST(0x0000A18901142B38 AS DateTime), 1000, 0, 0, 1000, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (77, 38, CAST(0x0000A18901142B38 AS DateTime), 15, 0, 0, 14, 1, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (78, 39, CAST(0x0000A18901142B38 AS DateTime), 450, 0, 0, 450, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (79, 1, CAST(0x0000A18A01142B38 AS DateTime), 23000, 0, 0, 21620, 1380, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (80, 2, CAST(0x0000A18A01142B38 AS DateTime), 700, 0, 0, 770, -70, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (81, 3, CAST(0x0000A18A01142B38 AS DateTime), 1590, 0, 0, 1550, 40, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (82, 4, CAST(0x0000A18A01142B38 AS DateTime), 700, 0, 0, 700, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (83, 5, CAST(0x0000A18A01142B38 AS DateTime), 1020, 0, 0, 1010, 10, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (84, 6, CAST(0x0000A18A01142B38 AS DateTime), 1350, 0, 0, 1350, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (85, 7, CAST(0x0000A18A01142B38 AS DateTime), 1600, 0, 0, 1550, 50, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (86, 8, CAST(0x0000A18A01142B38 AS DateTime), 1050, 0, 0, 1010, 40, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (87, 9, CAST(0x0000A18A01142B38 AS DateTime), 1600, 0, 0, 1580, 20, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (88, 10, CAST(0x0000A18A01142B38 AS DateTime), 600, 0, 0, 700, -100, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (89, 11, CAST(0x0000A18A01142B38 AS DateTime), 450, 0, 0, 280, 170, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (90, 12, CAST(0x0000A18A01142B38 AS DateTime), 1400, 0, 0, 1080, 320, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (91, 13, CAST(0x0000A18A01142B38 AS DateTime), 400, 0, 0, 380, 20, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (92, 14, CAST(0x0000A18A01142B38 AS DateTime), 8200, 0, 0, 8100, 100, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (93, 15, CAST(0x0000A18A01142B38 AS DateTime), 3200, 0, 0, 1951, 1249, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (94, 16, CAST(0x0000A18A01142B38 AS DateTime), 6200, 0, 0, 5920, 280, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (95, 17, CAST(0x0000A18A01142B38 AS DateTime), 20, 0, 0, 20, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (96, 18, CAST(0x0000A18A01142B38 AS DateTime), 95, 0, 0, 91, 4, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (97, 19, CAST(0x0000A18A01142B38 AS DateTime), 13, 0, 0, 11, 2, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (98, 20, CAST(0x0000A18A01142B38 AS DateTime), 16, 0, 0, 15, 1, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (99, 21, CAST(0x0000A18A01142B38 AS DateTime), 2500, 0, 0, 2410, 90, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (100, 22, CAST(0x0000A18A01142B38 AS DateTime), 850, 0, 0, 800, 50, N'', 0)
GO
print 'Processed 100 total records'
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (101, 23, CAST(0x0000A18A01142B38 AS DateTime), 400, 0, 0, 320, 80, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (102, 24, CAST(0x0000A18A01142B38 AS DateTime), 3850, 0, 0, 3840, 10, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (103, 25, CAST(0x0000A18A01142B38 AS DateTime), 29, 0, 0, 28, 1, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (104, 26, CAST(0x0000A18A01142B38 AS DateTime), 250, 0, 0, 250, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (105, 27, CAST(0x0000A18A01142B38 AS DateTime), 600, 0, 0, 580, 20, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (106, 28, CAST(0x0000A18A01142B38 AS DateTime), 112, 0, 0, 90, 22, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (107, 29, CAST(0x0000A18A01142B38 AS DateTime), 117, 0, 1, 94, 23, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (108, 30, CAST(0x0000A18A01142B38 AS DateTime), 1900, 0, 0, 1710, 190, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (109, 31, CAST(0x0000A18A01142B38 AS DateTime), 700, 0, 0, 700, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (110, 32, CAST(0x0000A18A01142B38 AS DateTime), 1, 0, 0, 0, 1, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (111, 33, CAST(0x0000A18A01142B38 AS DateTime), 3000, 0, 0, 2900, 100, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (112, 34, CAST(0x0000A18A01142B38 AS DateTime), 1200, 0, 0, 1060, 140, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (113, 35, CAST(0x0000A18A01142B38 AS DateTime), 600, 0, 0, 520, 80, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (114, 36, CAST(0x0000A18A01142B38 AS DateTime), 250, 0, 0, 220, 30, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (115, 37, CAST(0x0000A18A01142B38 AS DateTime), 1000, 0, 0, 1000, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (116, 38, CAST(0x0000A18A01142B38 AS DateTime), 14, 0, 0, 13, 1, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (117, 39, CAST(0x0000A18A01142B38 AS DateTime), 450, 0, 0, 450, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (118, 1, CAST(0x0000A18B0186E5EC AS DateTime), 21620, 0, 0, 20210, 1410, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (119, 2, CAST(0x0000A18B0186E5EC AS DateTime), 770, 0, 0, 720, 50, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (120, 3, CAST(0x0000A18B0186E5EC AS DateTime), 1550, 0, 0, 1540, 10, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (121, 4, CAST(0x0000A18B0186E5EC AS DateTime), 700, 0, 0, 670, 30, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (122, 5, CAST(0x0000A18B0186E5EC AS DateTime), 1010, 0, 0, 1010, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (123, 6, CAST(0x0000A18B0186E5EC AS DateTime), 1350, 0, 0, 1310, 40, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (124, 7, CAST(0x0000A18B0186E5EC AS DateTime), 1550, 0, 0, 1550, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (125, 8, CAST(0x0000A18B0186E5EC AS DateTime), 1010, 3000, 0, 3400, 610, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (126, 9, CAST(0x0000A18B0186E5EC AS DateTime), 1580, 0, 0, 1480, 100, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (127, 10, CAST(0x0000A18B0186E5EC AS DateTime), 700, 0, 0, 586, 114, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (128, 11, CAST(0x0000A18B0186E5EC AS DateTime), 280, 0, 0, 0, 280, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (129, 12, CAST(0x0000A18B0186E5EC AS DateTime), 1080, 1000, 0, 1340, 740, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (130, 13, CAST(0x0000A18B0186E5EC AS DateTime), 380, 0, 0, 320, 60, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (131, 14, CAST(0x0000A18B0186E5EC AS DateTime), 8100, 0, 0, 7700, 400, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (132, 15, CAST(0x0000A18B0186E5EC AS DateTime), 1950, 0, 0, 1038, 912, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (133, 16, CAST(0x0000A18B0186E5EC AS DateTime), 5920, 0, 0, 5442, 478, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (134, 17, CAST(0x0000A18B0186E5EC AS DateTime), 20, 112, 0, 129, 3, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (135, 18, CAST(0x0000A18B0186E5EC AS DateTime), 91, 0, 0, 91, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (136, 19, CAST(0x0000A18B0186E5EC AS DateTime), 11, 12, 0, 22, 1, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (137, 20, CAST(0x0000A18B0186E5EC AS DateTime), 15, 0, 0, 15, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (138, 21, CAST(0x0000A18B0186E5EC AS DateTime), 2410, 0, 0, 2190, 220, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (139, 22, CAST(0x0000A18B0186E5EC AS DateTime), 800, 0, 0, 620, 180, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (140, 23, CAST(0x0000A18B0186E5EC AS DateTime), 320, 0, 0, 240, 80, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (141, 24, CAST(0x0000A18B0186E5EC AS DateTime), 3840, 0, 0, 3840, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (142, 25, CAST(0x0000A18B0186E5EC AS DateTime), 28, 0, 0, 26, 2, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (143, 26, CAST(0x0000A18B0186E5EC AS DateTime), 250, 0, 0, 165, 85, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (144, 27, CAST(0x0000A18B0186E5EC AS DateTime), 580, 0, 0, 580, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (145, 28, CAST(0x0000A18B0186E5EC AS DateTime), 90, 0, 0, 61, 29, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (146, 29, CAST(0x0000A18B0186E5EC AS DateTime), 94, 0, 0, 63, 31, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (147, 30, CAST(0x0000A18B0186E5EC AS DateTime), 1710, 0, 0, 1710, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (148, 31, CAST(0x0000A18B0186E5EC AS DateTime), 700, 0, 0, 685, 15, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (149, 32, CAST(0x0000A18B0186E5EC AS DateTime), 1, 0, 0, 0, 1, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (150, 33, CAST(0x0000A18B0186E5EC AS DateTime), 2900, 0, 0, 2580, 320, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (151, 34, CAST(0x0000A18B0186E5EC AS DateTime), 1060, 0, 0, 915, 145, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (152, 35, CAST(0x0000A18B0186E5EC AS DateTime), 520, 0, 0, 515, 5, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (153, 36, CAST(0x0000A18B0186E5EC AS DateTime), 220, 0, 0, 215, 5, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (154, 37, CAST(0x0000A18B0186E5EC AS DateTime), 1000, 0, 0, 0, 1000, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (155, 38, CAST(0x0000A18B0186E5EC AS DateTime), 13, 0, 0, 9, 4, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (156, 39, CAST(0x0000A18B0186E5EC AS DateTime), 450, 0, 0, 450, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (157, 1, CAST(0x0000A18C0186E5EC AS DateTime), 20250, 0, 0, 19155, 1095, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (158, 2, CAST(0x0000A18C0186E5EC AS DateTime), 720, 0, 0, 700, 20, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (159, 3, CAST(0x0000A18C0186E5EC AS DateTime), 1540, 0, 0, 1540, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (160, 4, CAST(0x0000A18C0186E5EC AS DateTime), 690, 0, 0, 640, 50, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (161, 5, CAST(0x0000A18C0186E5EC AS DateTime), 1010, 0, 0, 1010, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (162, 6, CAST(0x0000A18C0186E5EC AS DateTime), 1320, 0, 0, 1300, 20, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (163, 7, CAST(0x0000A18C0186E5EC AS DateTime), 1550, 0, 0, 1540, 10, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (164, 8, CAST(0x0000A18C0186E5EC AS DateTime), 3900, 0, 0, 3320, 580, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (165, 9, CAST(0x0000A18C0186E5EC AS DateTime), 1580, 0, 0, 1550, 30, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (166, 10, CAST(0x0000A18C0186E5EC AS DateTime), 570, 0, 0, 470, 100, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (167, 11, CAST(0x0000A18C0186E5EC AS DateTime), 190, 880, 0, 880, 190, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (168, 12, CAST(0x0000A18C0186E5EC AS DateTime), 1350, 0, 0, 820, 530, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (169, 13, CAST(0x0000A18C0186E5EC AS DateTime), 240, 0, 0, 310, -70, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (170, 14, CAST(0x0000A18C0186E5EC AS DateTime), 7860, 0, 0, 7615, 245, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (171, 15, CAST(0x0000A18C0186E5EC AS DateTime), 3160, 10800, 0, 12064, 1896, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (172, 16, CAST(0x0000A18C0186E5EC AS DateTime), 6200, 10800, 0, 15974, 1026, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (173, 17, CAST(0x0000A18C0186E5EC AS DateTime), 129, 0, 0, 129, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (174, 18, CAST(0x0000A18C0186E5EC AS DateTime), 91, 0, 0, 91, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (175, 19, CAST(0x0000A18C0186E5EC AS DateTime), 22, 0, 0, 22, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (176, 20, CAST(0x0000A18C0186E5EC AS DateTime), 15, 0, 0, 15, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (177, 21, CAST(0x0000A18C0186E5EC AS DateTime), 2200, 0, 0, 2150, 50, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (178, 22, CAST(0x0000A18C0186E5EC AS DateTime), 650, 0, 0, 550, 100, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (179, 23, CAST(0x0000A18C0186E5EC AS DateTime), 250, 0, 0, 90, 160, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (180, 24, CAST(0x0000A18C0186E5EC AS DateTime), 3850, 0, 0, 3800, 50, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (181, 25, CAST(0x0000A18C0186E5EC AS DateTime), 26, 0, 0, 24, 2, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (182, 26, CAST(0x0000A18C0186E5EC AS DateTime), 160, 0, 0, 0, 160, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (183, 27, CAST(0x0000A18C0186E5EC AS DateTime), 580, 0, 0, 520, 60, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (184, 28, CAST(0x0000A18C0186E5EC AS DateTime), 58, 500, 0, 536, 22, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (185, 29, CAST(0x0000A18C0186E5EC AS DateTime), 63, 500, 0, 540, 23, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (186, 30, CAST(0x0000A18C0186E5EC AS DateTime), 1680, 0, 0, 1650, 30, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (187, 31, CAST(0x0000A18C0186E5EC AS DateTime), 620, 0, 0, 620, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (188, 32, CAST(0x0000A18C0186E5EC AS DateTime), 1, 0, 0, 0, 1, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (189, 33, CAST(0x0000A18C0186E5EC AS DateTime), 2580, 0, 0, 2260, 320, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (190, 34, CAST(0x0000A18C0186E5EC AS DateTime), 920, 0, 0, 800, 120, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (191, 35, CAST(0x0000A18C0186E5EC AS DateTime), 420, 0, 0, 500, -80, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (192, 36, CAST(0x0000A18C0186E5EC AS DateTime), 220, 0, 0, 200, 20, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (193, 37, CAST(0x0000A18C0186E5EC AS DateTime), 0, 5000, 0, 5000, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (194, 38, CAST(0x0000A18C0186E5EC AS DateTime), 9, 0, 0, 6, 3, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (195, 39, CAST(0x0000A18C0186E5EC AS DateTime), 450, 0, 0, 450, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (196, 1, CAST(0x0000A18D0173CA84 AS DateTime), 19200, 0, 0, 17740, 1460, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (197, 2, CAST(0x0000A18D0173CA84 AS DateTime), 700, 0, 0, 700, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (198, 3, CAST(0x0000A18D0173CA84 AS DateTime), 1540, 0, 0, 1540, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (199, 4, CAST(0x0000A18D0173CA84 AS DateTime), 640, 0, 0, 640, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (200, 5, CAST(0x0000A18D0173CA84 AS DateTime), 1010, 0, 0, 1010, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (201, 6, CAST(0x0000A18D0173CA84 AS DateTime), 1280, 0, 0, 1260, 20, N'', 0)
GO
print 'Processed 200 total records'
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (202, 7, CAST(0x0000A18D0173CA84 AS DateTime), 1540, 0, 0, 1540, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (203, 8, CAST(0x0000A18D0173CA84 AS DateTime), 3600, 0, 0, 3519, 81, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (204, 9, CAST(0x0000A18D0173CA84 AS DateTime), 1600, 0, 0, 1600, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (205, 10, CAST(0x0000A18D0173CA84 AS DateTime), 570, 0, 0, 540, 30, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (206, 11, CAST(0x0000A18D0173CA84 AS DateTime), 980, 0, 0, 660, 320, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (207, 12, CAST(0x0000A18D0173CA84 AS DateTime), 640, 1300, 0, 1725, 215, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (208, 13, CAST(0x0000A18D0173CA84 AS DateTime), 310, 0, 0, 300, 10, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (209, 14, CAST(0x0000A18D0173CA84 AS DateTime), 7660, 0, 0, 7320, 340, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (210, 15, CAST(0x0000A18D0173CA84 AS DateTime), 12064, 0, 0, 11405, 659, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (211, 16, CAST(0x0000A18D0173CA84 AS DateTime), 15840, 0, 0, 16560, -720, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (212, 17, CAST(0x0000A18D0173CA84 AS DateTime), 129, 0, 0, 117, 12, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (213, 18, CAST(0x0000A18D0173CA84 AS DateTime), 91, 0, 0, 91, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (214, 19, CAST(0x0000A18D0173CA84 AS DateTime), 22, 0, 0, 22, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (215, 20, CAST(0x0000A18D0173CA84 AS DateTime), 15, 0, 0, 14, 1, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (216, 21, CAST(0x0000A18D0173CA84 AS DateTime), 2450, 0, 0, 2100, 350, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (217, 22, CAST(0x0000A18D0173CA84 AS DateTime), 600, 0, 0, 600, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (218, 23, CAST(0x0000A18D0173CA84 AS DateTime), 100, 0, 0, 0, 100, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (219, 24, CAST(0x0000A18D0173CA84 AS DateTime), 3800, 0, 0, 3600, 200, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (220, 25, CAST(0x0000A18D0173CA84 AS DateTime), 24, 0, 0, 21, 3, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (221, 26, CAST(0x0000A18D0173CA84 AS DateTime), 0, 2000, 0, 2000, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (222, 27, CAST(0x0000A18D0173CA84 AS DateTime), 580, 0, 0, 580, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (223, 28, CAST(0x0000A18D0173CA84 AS DateTime), 537, 0, 0, 512, 25, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (224, 29, CAST(0x0000A18D0173CA84 AS DateTime), 545, 0, 0, 522, 23, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (225, 30, CAST(0x0000A18D0173CA84 AS DateTime), 1620, 0, 0, 1600, 20, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (226, 31, CAST(0x0000A18D0173CA84 AS DateTime), 620, 0, 0, 620, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (227, 32, CAST(0x0000A18D0173CA84 AS DateTime), 1, 0, 0, 0, 1, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (228, 33, CAST(0x0000A18D0173CA84 AS DateTime), 2260, 0, 0, 2100, 160, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (229, 34, CAST(0x0000A18D0173CA84 AS DateTime), 800, 0, 0, 700, 100, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (230, 35, CAST(0x0000A18D0173CA84 AS DateTime), 500, 0, 0, 460, 40, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (231, 36, CAST(0x0000A18D0173CA84 AS DateTime), 200, 0, 0, 180, 20, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (232, 37, CAST(0x0000A18D0173CA84 AS DateTime), 5000, 0, 0, 4000, 1000, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (233, 38, CAST(0x0000A18D0173CA84 AS DateTime), 6, 0, 0, 5, 1, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (234, 39, CAST(0x0000A18D0173CA84 AS DateTime), 450, 0, 0, 450, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (235, 1, CAST(0x0000A18E0173CA84 AS DateTime), 17740, 0, 0, 16845, 895, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (236, 2, CAST(0x0000A18E0173CA84 AS DateTime), 700, 0, 0, 700, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (237, 3, CAST(0x0000A18E0173CA84 AS DateTime), 1540, 0, 0, 1540, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (238, 4, CAST(0x0000A18E0173CA84 AS DateTime), 640, 0, 0, 640, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (239, 5, CAST(0x0000A18E0173CA84 AS DateTime), 1010, 0, 0, 1010, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (240, 6, CAST(0x0000A18E0173CA84 AS DateTime), 1260, 0, 0, 1320, -60, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (241, 7, CAST(0x0000A18E0173CA84 AS DateTime), 1540, 0, 0, 1330, 210, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (242, 8, CAST(0x0000A18E0173CA84 AS DateTime), 3519, 0, 0, 3600, -81, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (243, 9, CAST(0x0000A18E0173CA84 AS DateTime), 1600, 0, 0, 1600, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (244, 10, CAST(0x0000A18E0173CA84 AS DateTime), 540, 0, 0, 540, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (245, 11, CAST(0x0000A18E0173CA84 AS DateTime), 660, 0, 0, 660, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (246, 12, CAST(0x0000A18E0173CA84 AS DateTime), 1725, 0, 0, 300, 1425, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (247, 13, CAST(0x0000A18E0173CA84 AS DateTime), 300, 0, 0, 275, 25, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (248, 14, CAST(0x0000A18E0173CA84 AS DateTime), 7320, 0, 0, 7250, 70, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (249, 15, CAST(0x0000A18E0173CA84 AS DateTime), 11405, 0, 0, 11071, 334, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (250, 16, CAST(0x0000A18E0173CA84 AS DateTime), 16560, 0, 0, 14680, 1880, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (251, 17, CAST(0x0000A18E0173CA84 AS DateTime), 117, 0, 0, 108, 9, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (252, 18, CAST(0x0000A18E0173CA84 AS DateTime), 91, 0, 0, 91, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (253, 19, CAST(0x0000A18E0173CA84 AS DateTime), 22, 0, 0, 22, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (254, 20, CAST(0x0000A18E0173CA84 AS DateTime), 14, 0, 0, 14, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (255, 21, CAST(0x0000A18E0173CA84 AS DateTime), 2100, 3000, 0, 5000, 100, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (256, 22, CAST(0x0000A18E0173CA84 AS DateTime), 600, 4000, 0, 4580, 20, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (257, 23, CAST(0x0000A18E0173CA84 AS DateTime), 0, 5000, 0, 4900, 100, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (258, 24, CAST(0x0000A18E0173CA84 AS DateTime), 3600, 0, 0, 3600, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (259, 25, CAST(0x0000A18E0173CA84 AS DateTime), 21, 0, 0, 20, 1, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (260, 26, CAST(0x0000A18E0173CA84 AS DateTime), 2000, 0, 0, 2000, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (261, 27, CAST(0x0000A18E0173CA84 AS DateTime), 580, 0, 0, 550, 30, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (262, 28, CAST(0x0000A18E0173CA84 AS DateTime), 512, 0, 2, 499, 13, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (263, 29, CAST(0x0000A18E0173CA84 AS DateTime), 522, 0, 0, 503, 19, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (264, 30, CAST(0x0000A18E0173CA84 AS DateTime), 1600, 0, 0, 1650, -50, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (265, 31, CAST(0x0000A18E0173CA84 AS DateTime), 620, 0, 0, 620, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (266, 32, CAST(0x0000A18E0173CA84 AS DateTime), 1, 0, 0, 0, 1, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (267, 33, CAST(0x0000A18E0173CA84 AS DateTime), 2100, 0, 5, 1860, 240, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (268, 34, CAST(0x0000A18E0173CA84 AS DateTime), 700, 0, 0, 550, 150, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (269, 35, CAST(0x0000A18E0173CA84 AS DateTime), 460, 0, 0, 420, 40, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (270, 36, CAST(0x0000A18E0173CA84 AS DateTime), 180, 0, 5, 135, 45, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (271, 37, CAST(0x0000A18E0173CA84 AS DateTime), 4000, 0, 500, 3500, 500, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (272, 38, CAST(0x0000A18E0173CA84 AS DateTime), 5, 0, 0, 5, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (273, 39, CAST(0x0000A18E0173CA84 AS DateTime), 450, 0, 0, 450, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (274, 1, CAST(0x0000A18F017E3CBC AS DateTime), 16845, 0, 0, 15950, 895, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (275, 2, CAST(0x0000A18F017E3CBC AS DateTime), 700, 0, 0, 690, 10, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (276, 3, CAST(0x0000A18F017E3CBC AS DateTime), 1540, 0, 0, 1530, 10, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (277, 4, CAST(0x0000A18F017E3CBC AS DateTime), 640, 0, 0, 640, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (278, 5, CAST(0x0000A18F017E3CBC AS DateTime), 1010, 0, 0, 1000, 10, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (279, 6, CAST(0x0000A18F017E3CBC AS DateTime), 1320, 0, 0, 1320, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (280, 7, CAST(0x0000A18F017E3CBC AS DateTime), 1530, 0, 0, 1530, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (281, 8, CAST(0x0000A18F017E3CBC AS DateTime), 3600, 0, 0, 3550, 50, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (282, 9, CAST(0x0000A18F017E3CBC AS DateTime), 1600, 0, 0, 1600, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (283, 10, CAST(0x0000A18F017E3CBC AS DateTime), 540, 0, 0, 540, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (284, 11, CAST(0x0000A18F017E3CBC AS DateTime), 660, 0, 0, 450, 210, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (285, 12, CAST(0x0000A18F017E3CBC AS DateTime), 300, 1300, 0, 1300, 300, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (286, 13, CAST(0x0000A18F017E3CBC AS DateTime), 275, 0, 0, 275, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (287, 14, CAST(0x0000A18F017E3CBC AS DateTime), 7250, 0, 0, 6950, 300, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (288, 15, CAST(0x0000A18F017E3CBC AS DateTime), 11071, 0, 0, 10700, 371, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (289, 16, CAST(0x0000A18F017E3CBC AS DateTime), 15680, 0, 0, 15180, 500, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (290, 17, CAST(0x0000A18F017E3CBC AS DateTime), 108, 0, 0, 99, 9, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (291, 18, CAST(0x0000A18F017E3CBC AS DateTime), 91, 0, 0, 87, 4, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (292, 19, CAST(0x0000A18F017E3CBC AS DateTime), 22, 0, 0, 22, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (293, 20, CAST(0x0000A18F017E3CBC AS DateTime), 14, 0, 0, 14, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (294, 21, CAST(0x0000A18F017E3CBC AS DateTime), 5000, 0, 0, 5000, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (295, 22, CAST(0x0000A18F017E3CBC AS DateTime), 4580, 0, 0, 4500, 80, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (296, 23, CAST(0x0000A18F017E3CBC AS DateTime), 4900, 0, 0, 4800, 100, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (297, 24, CAST(0x0000A18F017E3CBC AS DateTime), 3600, 0, 0, 3600, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (298, 25, CAST(0x0000A18F017E3CBC AS DateTime), 20, 0, 0, 18, 2, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (299, 26, CAST(0x0000A18F017E3CBC AS DateTime), 2000, 0, 0, 2000, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (300, 27, CAST(0x0000A18F017E3CBC AS DateTime), 550, 0, 0, 550, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (301, 28, CAST(0x0000A18F017E3CBC AS DateTime), 499, 0, 3, 475, 24, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (302, 29, CAST(0x0000A18F017E3CBC AS DateTime), 503, 0, 0, 478, 25, N'', 0)
GO
print 'Processed 300 total records'
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (303, 30, CAST(0x0000A18F017E3CBC AS DateTime), 1650, 0, 0, 1640, 10, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (304, 31, CAST(0x0000A18F017E3CBC AS DateTime), 620, 0, 0, 618, 2, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (305, 32, CAST(0x0000A18F017E3CBC AS DateTime), 1, 0, 0, 0, 1, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (306, 33, CAST(0x0000A18F017E3CBC AS DateTime), 1860, 0, 7.5, 1510, 350, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (307, 34, CAST(0x0000A18F017E3CBC AS DateTime), 550, 0, 0, 310, 240, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (308, 35, CAST(0x0000A18F017E3CBC AS DateTime), 420, 0, 0, 420, 0, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (309, 36, CAST(0x0000A18F017E3CBC AS DateTime), 135, 0, 7.5, 115, 20, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (310, 37, CAST(0x0000A18F017E3CBC AS DateTime), 3500, 0, 500, 2000, 1500, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (311, 38, CAST(0x0000A18F017E3CBC AS DateTime), 5, 0, 0, 3, 2, N'', 0)
INSERT [dbo].[NhatKyNguyenLieu] ([Id], [IdNguyenLieu], [Date], [TonDau], [Nhap], [Huy], [TonCuoi], [SuDung], [GhiChu], [DeleteFlag]) VALUES (312, 39, CAST(0x0000A18F017E3CBC AS DateTime), 450, 0, 0, 450, 0, N'', 0)
SET IDENTITY_INSERT [dbo].[NhatKyNguyenLieu] OFF
/****** Object:  Table [dbo].[KhachHang]    Script Date: 10/17/2013 10:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[KhachHang](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MaKhachHang] [varchar](10) NOT NULL,
	[IdGroup] [int] NOT NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[GioiTinh] [nvarchar](3) NOT NULL,
	[DOB] [date] NULL,
	[DiaChi] [nvarchar](100) NULL,
	[DTDD] [varchar](30) NULL,
	[Facebook] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[GhiChu] [nvarchar](200) NULL,
	[DeleteFlag] [bit] NOT NULL,
 CONSTRAINT [PK_KhachHang] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/17/2013 10:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdGroup] [int] NOT NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[GioiTinh] [nvarchar](3) NOT NULL,
	[DOB] [date] NULL,
	[CMND] [nvarchar](10) NULL,
	[NgayCap] [date] NULL,
	[NoiCap] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](100) NULL,
	[DienThoai] [nvarchar](30) NULL,
	[DTDD] [nvarchar](30) NULL,
	[Email] [nvarchar](50) NULL,
	[GhiChu] [nvarchar](200) NULL,
	[DeleteFlag] [bit] NOT NULL,
 CONSTRAINT [PK_NguoiDung] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [GioiTinh], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (20, 1, N'admin', N'admin', N'21232F297A57A5A743894AE4A801FC345454433454539', N'Nam', CAST(0x94360B00 AS Date), NULL, CAST(0x94360B00 AS Date), NULL, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [GioiTinh], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (21, 1, N'Dang Quoc Huy', N'huy.dang', N'945FC9611F55FD0E183FB8B44F1AFE45454433454539', N'Nam', CAST(0x7A100B00 AS Date), N'023853921', CAST(0xC5360B00 AS Date), N'Tp.HCM', N'68/58P Thich Quang Duc, F5, Q.Phu Nhuan, Tp.HCM', N'0908633204', N'0908633204', N'huysamdua@yahoo.com', N'Hien tai chua co gi de ghi chu nha', 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [GioiTinh], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (22, 2, N'Nguyen Van B', N'b', N'E1ADC3949BA59ABBE56E057F2F883E45454433454539', N'Nam', CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [GioiTinh], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (23, 3, N'Nguyen Van C', N'c', N'E1ADC3949BA59ABBE56E057F2F883E45454433454539', N'Nam', CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Table [dbo].[HoaDon]    Script Date: 10/17/2013 10:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDon](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[IdUser] [int] NOT NULL,
	[IdKhachHang] [int] NULL,
	[ThanhTien] [bigint] NOT NULL,
	[GhiChu] [nvarchar](200) NULL,
	[Duyet] [bit] NOT NULL,
	[DeleteFlag] [bit] NOT NULL,
 CONSTRAINT [PK_DonHang] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[HoaDon] ON
INSERT [dbo].[HoaDon] ([Id], [Date], [IdUser], [IdKhachHang], [ThanhTien], [GhiChu], [Duyet], [DeleteFlag]) VALUES (2, CAST(0x0000A17401726FA4 AS DateTime), 20, NULL, 35, N'', 0, 0)
INSERT [dbo].[HoaDon] ([Id], [Date], [IdUser], [IdKhachHang], [ThanhTien], [GhiChu], [Duyet], [DeleteFlag]) VALUES (3, CAST(0x0000A18E010D8CB0 AS DateTime), 21, NULL, 430000, N'', 0, 0)
INSERT [dbo].[HoaDon] ([Id], [Date], [IdUser], [IdKhachHang], [ThanhTien], [GhiChu], [Duyet], [DeleteFlag]) VALUES (4, CAST(0x0000A18D010D8CB0 AS DateTime), 21, NULL, 643000, N'', 0, 0)
INSERT [dbo].[HoaDon] ([Id], [Date], [IdUser], [IdKhachHang], [ThanhTien], [GhiChu], [Duyet], [DeleteFlag]) VALUES (5, CAST(0x0000A18C010D8CB0 AS DateTime), 21, NULL, 540000, N'', 0, 0)
INSERT [dbo].[HoaDon] ([Id], [Date], [IdUser], [IdKhachHang], [ThanhTien], [GhiChu], [Duyet], [DeleteFlag]) VALUES (6, CAST(0x0000A18B010D8CB0 AS DateTime), 21, NULL, 759000, N'', 0, 0)
INSERT [dbo].[HoaDon] ([Id], [Date], [IdUser], [IdKhachHang], [ThanhTien], [GhiChu], [Duyet], [DeleteFlag]) VALUES (7, CAST(0x0000A18A010D8CB0 AS DateTime), 21, NULL, 574000, N'', 0, 0)
INSERT [dbo].[HoaDon] ([Id], [Date], [IdUser], [IdKhachHang], [ThanhTien], [GhiChu], [Duyet], [DeleteFlag]) VALUES (8, CAST(0x0000A189010D8CB0 AS DateTime), 21, NULL, 360000, N'', 0, 0)
INSERT [dbo].[HoaDon] ([Id], [Date], [IdUser], [IdKhachHang], [ThanhTien], [GhiChu], [Duyet], [DeleteFlag]) VALUES (9, CAST(0x0000A18F0179B494 AS DateTime), 21, NULL, 349000, N'', 0, 0)
INSERT [dbo].[HoaDon] ([Id], [Date], [IdUser], [IdKhachHang], [ThanhTien], [GhiChu], [Duyet], [DeleteFlag]) VALUES (10, CAST(0x0000A1900179B494 AS DateTime), 21, NULL, 482000, N'', 0, 0)
INSERT [dbo].[HoaDon] ([Id], [Date], [IdUser], [IdKhachHang], [ThanhTien], [GhiChu], [Duyet], [DeleteFlag]) VALUES (11, CAST(0x0000A1910179B494 AS DateTime), 21, NULL, 604000, N'', 0, 0)
INSERT [dbo].[HoaDon] ([Id], [Date], [IdUser], [IdKhachHang], [ThanhTien], [GhiChu], [Duyet], [DeleteFlag]) VALUES (12, CAST(0x0000A1920179B494 AS DateTime), 21, NULL, 890000, N'', 0, 0)
INSERT [dbo].[HoaDon] ([Id], [Date], [IdUser], [IdKhachHang], [ThanhTien], [GhiChu], [Duyet], [DeleteFlag]) VALUES (13, CAST(0x0000A1930179B494 AS DateTime), 21, NULL, 319000, N'', 0, 0)
INSERT [dbo].[HoaDon] ([Id], [Date], [IdUser], [IdKhachHang], [ThanhTien], [GhiChu], [Duyet], [DeleteFlag]) VALUES (14, CAST(0x0000A1940179B494 AS DateTime), 21, NULL, 512000, N'', 0, 0)
INSERT [dbo].[HoaDon] ([Id], [Date], [IdUser], [IdKhachHang], [ThanhTien], [GhiChu], [Duyet], [DeleteFlag]) VALUES (15, CAST(0x0000A1EC00069432 AS DateTime), 20, NULL, 78000, NULL, 0, 0)
INSERT [dbo].[HoaDon] ([Id], [Date], [IdUser], [IdKhachHang], [ThanhTien], [GhiChu], [Duyet], [DeleteFlag]) VALUES (16, CAST(0x0000A1EC0006CEFE AS DateTime), 20, NULL, 78000, NULL, 0, 0)
INSERT [dbo].[HoaDon] ([Id], [Date], [IdUser], [IdKhachHang], [ThanhTien], [GhiChu], [Duyet], [DeleteFlag]) VALUES (17, CAST(0x0000A1F501797088 AS DateTime), 20, NULL, 524000, N'', 0, 0)
INSERT [dbo].[HoaDon] ([Id], [Date], [IdUser], [IdKhachHang], [ThanhTien], [GhiChu], [Duyet], [DeleteFlag]) VALUES (18, CAST(0x0000A1F5017998A7 AS DateTime), 20, NULL, 227000, N'', 0, 0)
INSERT [dbo].[HoaDon] ([Id], [Date], [IdUser], [IdKhachHang], [ThanhTien], [GhiChu], [Duyet], [DeleteFlag]) VALUES (19, CAST(0x0000A1F50179A903 AS DateTime), 20, NULL, 222000, N'', 0, 0)
INSERT [dbo].[HoaDon] ([Id], [Date], [IdUser], [IdKhachHang], [ThanhTien], [GhiChu], [Duyet], [DeleteFlag]) VALUES (20, CAST(0x0000A1F50179CBBA AS DateTime), 20, NULL, 66000, N'', 0, 0)
INSERT [dbo].[HoaDon] ([Id], [Date], [IdUser], [IdKhachHang], [ThanhTien], [GhiChu], [Duyet], [DeleteFlag]) VALUES (21, CAST(0x0000A245010DAFEA AS DateTime), 20, NULL, 54000, N'', 0, 0)
INSERT [dbo].[HoaDon] ([Id], [Date], [IdUser], [IdKhachHang], [ThanhTien], [GhiChu], [Duyet], [DeleteFlag]) VALUES (22, CAST(0x0000A24600A78721 AS DateTime), 20, NULL, 112000, N'', 1, 0)
SET IDENTITY_INSERT [dbo].[HoaDon] OFF
/****** Object:  Table [dbo].[GiaChinhThuc]    Script Date: 10/17/2013 10:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GiaChinhThuc](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdSanPham] [int] NOT NULL,
	[Gia] [bigint] NOT NULL,
 CONSTRAINT [PK_GiaChinhThuc] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[GiaChinhThuc] ON
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (1, 1, 22000)
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (2, 2, 29000)
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (3, 3, 27000)
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (4, 4, 27000)
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (5, 5, 27000)
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (6, 6, 29000)
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (7, 7, 27000)
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (8, 8, 25000)
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (9, 9, 24000)
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (10, 10, 27000)
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (11, 11, 29000)
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (12, 13, 20000)
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (13, 14, 24000)
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (14, 15, 27000)
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (15, 16, 24000)
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (16, 17, 24000)
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (17, 18, 24000)
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (18, 19, 29000)
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (19, 20, 27000)
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (20, 21, 27000)
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (21, 22, 27000)
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (22, 24, 19000)
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (23, 25, 19000)
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (24, 26, 19000)
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (25, 27, 19000)
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (26, 28, 19000)
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (27, 29, 19000)
INSERT [dbo].[GiaChinhThuc] ([Id], [IdSanPham], [Gia]) VALUES (28, 30, 0)
SET IDENTITY_INSERT [dbo].[GiaChinhThuc] OFF
/****** Object:  Table [dbo].[DinhLuong]    Script Date: 10/17/2013 10:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DinhLuong](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdSanPham] [int] NOT NULL,
	[IdNguyenLieu] [int] NOT NULL,
	[SoLuong] [float] NOT NULL,
	[GhiChu] [nvarchar](200) NULL,
 CONSTRAINT [PK_DinhLuong] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[DinhLuong] ON
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (1, 1, 1, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (2, 1, 12, 45, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (3, 1, 33, 10, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (4, 1, 26, 8, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (5, 1, 8, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (6, 30, 16, 250, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (7, 30, 14, 100, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (8, 30, 1, 100, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (9, 30, 25, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (10, 1, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (11, 1, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (12, 1, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (13, 1, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (14, 1, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (15, 2, 1, 60, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (16, 2, 15, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (17, 2, 12, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (18, 2, 33, 10, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (19, 2, 10, 40, N'30ml sauce caramel + 10ml trang tri')
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (20, 2, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (21, 2, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (22, 2, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (23, 2, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (24, 2, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (25, 3, 1, 60, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (26, 3, 15, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (27, 3, 12, 45, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (28, 3, 33, 10, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (29, 3, 36, 5, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (30, 3, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (31, 3, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (32, 3, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (33, 3, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (34, 3, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (35, 4, 1, 60, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (36, 4, 15, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (37, 4, 12, 45, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (38, 4, 33, 10, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (39, 4, 34, 25, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (40, 4, 9, 5, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (41, 4, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (42, 4, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (43, 4, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (44, 4, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (45, 4, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (46, 5, 1, 90, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (47, 5, 15, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (48, 5, 33, 10, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (49, 5, 35, 15, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (50, 5, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (51, 5, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (52, 5, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (53, 5, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (54, 5, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (55, 6, 1, 90, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (56, 6, 15, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (57, 6, 33, 10, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (58, 6, 35, 15, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (59, 6, 9, 15, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (60, 6, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (61, 6, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (62, 6, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (63, 6, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (64, 6, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (65, 7, 1, 60, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (66, 7, 15, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (67, 7, 12, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (68, 7, 33, 10, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (69, 7, 34, 15, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (70, 7, 27, 3, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (71, 7, 13, 5, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (72, 7, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (73, 7, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (74, 7, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (75, 7, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (76, 7, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (77, 8, 1, 75, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (78, 8, 15, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (79, 8, 12, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (80, 8, 33, 10, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (81, 8, 17, 3, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (82, 8, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (83, 8, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (84, 8, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (85, 8, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (86, 8, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (87, 9, 1, 90, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (88, 9, 15, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (89, 9, 12, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (90, 9, 33, 10, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (91, 9, 18, 4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (92, 9, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (93, 9, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (94, 9, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (95, 9, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (96, 9, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (97, 10, 1, 90, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (98, 10, 15, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (99, 10, 12, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (100, 10, 33, 10, NULL)
GO
print 'Processed 100 total records'
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (101, 10, 19, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (102, 10, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (103, 10, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (104, 10, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (105, 10, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (106, 10, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (107, 11, 1, 90, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (108, 11, 15, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (109, 11, 12, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (110, 11, 33, 10, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (111, 11, 20, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (112, 11, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (113, 11, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (114, 11, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (115, 11, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (116, 11, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (117, 13, 1, 60, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (118, 13, 15, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (119, 13, 12, 45, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (120, 13, 26, 8, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (121, 13, 8, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (122, 13, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (123, 13, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (124, 13, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (125, 13, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (126, 13, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (127, 14, 1, 120, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (128, 14, 15, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (129, 14, 12, 45, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (130, 14, 36, 5, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (131, 14, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (132, 14, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (133, 14, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (134, 14, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (135, 14, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (136, 15, 1, 120, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (137, 15, 15, 15, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (138, 15, 23, 60, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (139, 15, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (140, 15, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (141, 15, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (142, 15, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (143, 15, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (144, 16, 1, 120, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (145, 16, 15, 15, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (146, 16, 24, 60, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (147, 16, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (148, 16, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (149, 16, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (150, 16, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (151, 16, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (152, 17, 1, 120, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (153, 17, 15, 15, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (154, 17, 21, 60, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (155, 17, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (156, 17, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (157, 17, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (158, 17, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (159, 17, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (160, 18, 1, 120, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (161, 18, 15, 15, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (162, 18, 22, 60, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (163, 18, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (164, 18, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (165, 18, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (166, 18, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (167, 18, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (168, 19, 15, 15, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (169, 19, 12, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (170, 19, 33, 10, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (171, 19, 23, 60, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (172, 19, 11, 45, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (173, 19, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (174, 19, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (175, 19, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (176, 19, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (177, 19, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (178, 20, 15, 15, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (179, 20, 12, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (180, 20, 33, 10, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (181, 20, 24, 60, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (182, 20, 11, 45, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (183, 20, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (184, 20, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (185, 20, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (186, 20, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (187, 20, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (188, 21, 15, 15, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (189, 21, 12, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (190, 21, 33, 10, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (191, 21, 21, 60, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (192, 21, 11, 45, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (193, 21, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (194, 21, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (195, 21, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (196, 21, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (197, 21, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (198, 22, 15, 15, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (199, 22, 12, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (200, 22, 33, 10, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (201, 22, 22, 60, NULL)
GO
print 'Processed 200 total records'
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (202, 22, 11, 45, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (203, 22, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (204, 22, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (205, 22, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (206, 22, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (207, 22, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (208, 24, 12, 10, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (209, 24, 2, 20, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (210, 24, 38, 0.5, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (211, 24, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (212, 24, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (213, 24, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (214, 24, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (215, 24, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (216, 25, 12, 10, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (217, 25, 3, 20, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (218, 25, 38, 0.5, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (219, 25, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (220, 25, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (221, 25, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (222, 25, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (223, 25, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (224, 26, 12, 10, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (225, 26, 4, 20, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (226, 26, 38, 0.5, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (227, 26, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (228, 26, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (229, 26, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (230, 26, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (231, 26, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (232, 27, 12, 10, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (233, 27, 5, 20, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (234, 27, 38, 0.5, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (235, 27, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (236, 27, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (237, 27, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (238, 27, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (239, 27, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (240, 28, 12, 10, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (241, 28, 7, 20, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (242, 28, 38, 0.5, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (243, 28, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (244, 28, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (245, 28, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (246, 28, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (247, 28, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (248, 29, 12, 10, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (249, 29, 6, 20, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (250, 29, 38, 0.5, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (251, 29, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (252, 29, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (253, 29, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (254, 29, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (255, 29, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (256, 31, 36, 5, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (257, 31, 16, 200, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (258, 31, 1, 200, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (259, 31, 25, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (260, 32, 1, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (261, 32, 12, 45, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (262, 32, 33, 10, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (263, 32, 26, 8, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (264, 32, 8, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (265, 32, 41, 180, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (266, 32, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (267, 32, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (268, 32, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (269, 32, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (270, 32, 31, 1.2, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (271, 33, 1, 60, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (272, 33, 15, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (273, 33, 12, 45, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (274, 33, 26, 8, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (275, 33, 8, 30, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (276, 33, 41, 180, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (277, 33, 32, 0.02, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (278, 33, 28, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (279, 33, 29, 1, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (280, 33, 30, 1.4, NULL)
INSERT [dbo].[DinhLuong] ([Id], [IdSanPham], [IdNguyenLieu], [SoLuong], [GhiChu]) VALUES (281, 33, 31, 1.2, NULL)
SET IDENTITY_INSERT [dbo].[DinhLuong] OFF
/****** Object:  Table [dbo].[HoaDonDetail]    Script Date: 10/17/2013 10:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDonDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdHoaDon] [int] NOT NULL,
	[IdSanPham] [int] NOT NULL,
	[DonGia] [bigint] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[ThanhTien] [bigint] NOT NULL,
	[GhiChu] [nvarchar](200) NULL,
 CONSTRAINT [PK_DonHangDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[HoaDonDetail] ON
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (28, 2, 1, 1, 1, 1, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (29, 2, 2, 1, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (30, 2, 3, 1, 1, 1, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (31, 2, 4, 1, 5, 5, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (32, 2, 5, 1, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (33, 2, 6, 1, 4, 4, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (34, 2, 7, 1, 1, 1, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (35, 2, 8, 1, 3, 3, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (36, 2, 9, 1, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (37, 2, 10, 1, 3, 3, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (38, 2, 11, 1, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (39, 2, 13, 1, 4, 4, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (40, 2, 14, 1, 2, 2, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (41, 2, 15, 1, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (42, 2, 16, 1, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (43, 2, 17, 1, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (44, 2, 18, 1, 1, 1, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (45, 2, 19, 1, 5, 5, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (46, 2, 20, 1, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (47, 2, 21, 1, 2, 2, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (48, 2, 22, 1, 3, 3, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (49, 2, 24, 1, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (50, 2, 25, 1, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (51, 2, 26, 1, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (52, 2, 27, 1, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (53, 2, 28, 1, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (54, 2, 29, 1, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (55, 2, 30, 1, 3, 3, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (56, 3, 30, 0, 1, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (57, 3, 1, 22000, 2, 44000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (58, 3, 2, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (59, 3, 3, 27000, 2, 54000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (60, 3, 4, 27000, 4, 108000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (61, 3, 5, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (62, 3, 6, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (63, 3, 7, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (64, 3, 8, 25000, 3, 75000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (65, 3, 9, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (66, 3, 10, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (67, 3, 11, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (68, 3, 13, 20000, 2, 40000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (69, 3, 14, 24000, 1, 24000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (70, 3, 15, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (71, 3, 16, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (72, 3, 17, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (73, 3, 18, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (74, 3, 19, 29000, 2, 58000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (75, 3, 20, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (76, 3, 21, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (77, 3, 22, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (78, 3, 24, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (79, 3, 25, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (80, 3, 26, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (81, 3, 27, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (82, 3, 28, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (83, 3, 29, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (84, 4, 30, 0, 3, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (85, 4, 1, 22000, 2, 44000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (86, 4, 2, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (87, 4, 3, 27000, 4, 108000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (88, 4, 4, 27000, 2, 54000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (89, 4, 5, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (90, 4, 6, 29000, 2, 58000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (91, 4, 7, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (92, 4, 8, 25000, 4, 100000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (93, 4, 9, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (94, 4, 10, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (95, 4, 11, 29000, 1, 29000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (96, 4, 13, 20000, 2, 40000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (97, 4, 14, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (98, 4, 15, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (99, 4, 16, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (100, 4, 17, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (101, 4, 18, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (102, 4, 19, 29000, 1, 29000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (103, 4, 20, 27000, 3, 81000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (104, 4, 21, 27000, 2, 54000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (105, 4, 22, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (106, 4, 24, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (107, 4, 25, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (108, 4, 26, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (109, 4, 27, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (110, 4, 28, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (111, 4, 29, 19000, 1, 19000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (112, 5, 30, 0, 2, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (113, 5, 1, 22000, 2, 44000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (114, 5, 2, 29000, 1, 29000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (115, 5, 3, 27000, 3, 81000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (116, 5, 4, 27000, 3, 81000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (117, 5, 5, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (118, 5, 6, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (119, 5, 7, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (120, 5, 8, 25000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (121, 5, 9, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (122, 5, 10, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (123, 5, 11, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (124, 5, 13, 20000, 1, 20000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (125, 5, 14, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (126, 5, 15, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (127, 5, 16, 24000, 1, 24000, NULL)
GO
print 'Processed 100 total records'
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (128, 5, 17, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (129, 5, 18, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (130, 5, 19, 29000, 2, 58000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (131, 5, 20, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (132, 5, 21, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (133, 5, 22, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (134, 5, 24, 19000, 1, 19000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (135, 5, 25, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (136, 5, 26, 19000, 2, 38000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (137, 5, 27, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (138, 5, 28, 19000, 1, 19000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (139, 5, 29, 19000, 1, 19000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (140, 6, 30, 0, 2, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (141, 6, 1, 22000, 5, 110000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (142, 6, 2, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (143, 6, 3, 27000, 2, 54000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (144, 6, 4, 27000, 3, 81000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (145, 6, 5, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (146, 6, 6, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (147, 6, 7, 27000, 2, 54000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (148, 6, 8, 25000, 1, 25000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (149, 6, 9, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (150, 6, 10, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (151, 6, 11, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (152, 6, 13, 20000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (153, 6, 14, 24000, 1, 24000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (154, 6, 15, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (155, 6, 16, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (156, 6, 17, 24000, 1, 24000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (157, 6, 18, 24000, 3, 72000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (158, 6, 19, 29000, 2, 58000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (159, 6, 20, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (160, 6, 21, 27000, 4, 108000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (161, 6, 22, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (162, 6, 24, 19000, 2, 38000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (163, 6, 25, 19000, 1, 19000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (164, 6, 26, 19000, 1, 19000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (165, 6, 27, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (166, 6, 28, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (167, 6, 29, 19000, 1, 19000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (168, 7, 30, 0, 1, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (169, 7, 1, 22000, 1, 22000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (170, 7, 2, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (171, 7, 3, 27000, 2, 54000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (172, 7, 4, 27000, 2, 54000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (173, 7, 5, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (174, 7, 6, 29000, 1, 29000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (175, 7, 7, 27000, 3, 81000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (176, 7, 8, 25000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (177, 7, 9, 24000, 1, 24000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (178, 7, 10, 27000, 2, 54000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (179, 7, 11, 29000, 1, 29000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (180, 7, 13, 20000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (181, 7, 14, 24000, 2, 48000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (182, 7, 15, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (183, 7, 16, 24000, 1, 24000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (184, 7, 17, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (185, 7, 18, 24000, 1, 24000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (186, 7, 19, 29000, 2, 58000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (187, 7, 20, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (188, 7, 21, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (189, 7, 22, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (190, 7, 24, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (191, 7, 25, 19000, 1, 19000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (192, 7, 26, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (193, 7, 27, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (194, 7, 28, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (195, 7, 29, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (196, 8, 30, 0, 1, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (197, 8, 1, 22000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (198, 8, 2, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (199, 8, 3, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (200, 8, 4, 27000, 2, 54000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (201, 8, 5, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (202, 8, 6, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (203, 8, 7, 27000, 2, 54000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (204, 8, 8, 25000, 2, 50000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (205, 8, 9, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (206, 8, 10, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (207, 8, 11, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (208, 8, 13, 20000, 1, 20000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (209, 8, 14, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (210, 8, 15, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (211, 8, 16, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (212, 8, 17, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (213, 8, 18, 24000, 1, 24000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (214, 8, 19, 29000, 2, 58000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (215, 8, 20, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (216, 8, 21, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (217, 8, 22, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (218, 8, 24, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (219, 8, 25, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (220, 8, 26, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (221, 8, 27, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (222, 8, 28, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (223, 8, 29, 19000, 1, 19000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (224, 9, 30, 0, 2, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (225, 9, 1, 22000, 1, 22000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (226, 9, 2, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (227, 9, 3, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (228, 9, 4, 27000, 3, 81000, NULL)
GO
print 'Processed 200 total records'
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (229, 9, 5, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (230, 9, 6, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (231, 9, 7, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (232, 9, 8, 25000, 3, 75000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (233, 9, 9, 24000, 1, 24000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (234, 9, 10, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (235, 9, 11, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (236, 9, 13, 20000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (237, 9, 14, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (238, 9, 15, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (239, 9, 16, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (240, 9, 17, 24000, 1, 24000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (241, 9, 18, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (242, 9, 19, 29000, 2, 58000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (243, 9, 20, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (244, 9, 21, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (245, 9, 22, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (246, 9, 24, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (247, 9, 25, 19000, 1, 19000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (248, 9, 26, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (249, 9, 27, 19000, 1, 19000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (250, 9, 28, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (251, 9, 29, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (252, 10, 30, 0, 2, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (253, 10, 1, 22000, 2, 44000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (254, 10, 2, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (255, 10, 3, 27000, 3, 81000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (256, 10, 4, 27000, 2, 54000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (257, 10, 5, 27000, 4, 108000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (258, 10, 6, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (259, 10, 7, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (260, 10, 8, 25000, 1, 25000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (261, 10, 9, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (262, 10, 10, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (263, 10, 11, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (264, 10, 13, 20000, 1, 20000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (265, 10, 14, 24000, 1, 24000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (266, 10, 15, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (267, 10, 16, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (268, 10, 17, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (269, 10, 18, 24000, 1, 24000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (270, 10, 19, 29000, 1, 29000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (271, 10, 20, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (272, 10, 21, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (273, 10, 22, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (274, 10, 24, 19000, 1, 19000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (275, 10, 25, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (276, 10, 26, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (277, 10, 27, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (278, 10, 28, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (279, 10, 29, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (280, 11, 30, 0, 1, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (281, 11, 1, 22000, 6, 132000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (282, 11, 2, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (283, 11, 3, 27000, 2, 54000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (284, 11, 4, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (285, 11, 5, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (286, 11, 6, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (287, 11, 7, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (288, 11, 8, 25000, 4, 100000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (289, 11, 9, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (290, 11, 10, 27000, 3, 81000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (291, 11, 11, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (292, 11, 13, 20000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (293, 11, 14, 24000, 1, 24000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (294, 11, 15, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (295, 11, 16, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (296, 11, 17, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (297, 11, 18, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (298, 11, 19, 29000, 1, 29000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (299, 11, 20, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (300, 11, 21, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (301, 11, 22, 27000, 2, 54000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (302, 11, 24, 19000, 1, 19000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (303, 11, 25, 19000, 2, 38000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (304, 11, 26, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (305, 11, 27, 19000, 1, 19000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (306, 11, 28, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (307, 11, 29, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (308, 12, 30, 0, 2, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (309, 12, 1, 22000, 4, 88000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (310, 12, 2, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (311, 12, 3, 27000, 3, 81000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (312, 12, 4, 27000, 4, 108000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (313, 12, 5, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (314, 12, 6, 29000, 1, 29000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (315, 12, 7, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (316, 12, 8, 25000, 4, 100000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (317, 12, 9, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (318, 12, 10, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (319, 12, 11, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (320, 12, 13, 20000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (321, 12, 14, 24000, 2, 48000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (322, 12, 15, 27000, 2, 54000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (323, 12, 16, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (324, 12, 17, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (325, 12, 18, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (326, 12, 19, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (327, 12, 20, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (328, 12, 21, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (329, 12, 22, 27000, 3, 81000, NULL)
GO
print 'Processed 300 total records'
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (330, 12, 24, 19000, 4, 76000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (331, 12, 25, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (332, 12, 26, 19000, 4, 76000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (333, 12, 27, 19000, 3, 57000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (334, 12, 28, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (335, 12, 29, 19000, 2, 38000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (336, 13, 30, 0, 2, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (337, 13, 1, 22000, 2, 44000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (338, 13, 2, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (339, 13, 3, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (340, 13, 4, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (341, 13, 5, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (342, 13, 6, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (343, 13, 7, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (344, 13, 8, 25000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (345, 13, 9, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (346, 13, 10, 27000, 2, 54000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (347, 13, 11, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (348, 13, 13, 20000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (349, 13, 14, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (350, 13, 15, 27000, 3, 81000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (351, 13, 16, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (352, 13, 17, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (353, 13, 18, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (354, 13, 19, 29000, 1, 29000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (355, 13, 20, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (356, 13, 21, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (357, 13, 22, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (358, 13, 24, 19000, 1, 19000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (359, 13, 25, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (360, 13, 26, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (361, 13, 27, 19000, 1, 19000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (362, 13, 28, 19000, 1, 19000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (363, 13, 29, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (364, 14, 30, 0, 2, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (365, 14, 1, 22000, 3, 66000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (366, 14, 2, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (367, 14, 3, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (368, 14, 4, 27000, 2, 54000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (369, 14, 5, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (370, 14, 6, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (371, 14, 7, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (372, 14, 8, 25000, 1, 25000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (373, 14, 9, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (374, 14, 10, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (375, 14, 11, 29000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (376, 14, 13, 20000, 1, 20000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (377, 14, 14, 24000, 1, 24000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (378, 14, 15, 27000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (379, 14, 16, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (380, 14, 17, 24000, 2, 48000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (381, 14, 18, 24000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (382, 14, 19, 29000, 1, 29000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (383, 14, 20, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (384, 14, 21, 27000, 3, 81000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (385, 14, 22, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (386, 14, 24, 19000, 1, 19000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (387, 14, 25, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (388, 14, 26, 19000, 2, 38000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (389, 14, 27, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (390, 14, 28, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (391, 14, 29, 19000, 0, 0, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (392, 15, 1, 22000, 1, 22000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (393, 15, 2, 29000, 1, 29000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (394, 15, 3, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (395, 16, 1, 22000, 1, 22000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (396, 16, 2, 29000, 1, 29000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (397, 16, 3, 27000, 1, 27000, NULL)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (398, 17, 1, 22000, 6, 132000, N'')
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (399, 17, 2, 29000, 7, 203000, N'')
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (400, 17, 3, 27000, 4, 108000, N'')
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (401, 17, 5, 27000, 3, 81000, N'')
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (402, 18, 1, 22000, 4, 88000, N'')
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (403, 18, 2, 29000, 2, 58000, N'')
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (404, 18, 3, 27000, 3, 81000, N'')
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (405, 19, 1, 22000, 5, 110000, N'')
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (406, 19, 2, 29000, 2, 58000, N'')
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (407, 19, 3, 27000, 2, 54000, N'')
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (408, 20, 1, 22000, 3, 66000, N'')
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (409, 21, 31, 0, 1, 0, N'')
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (410, 21, 32, 0, 1, 0, N'')
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (411, 21, 7, 27000, 1, 27000, N'')
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (412, 21, 4, 27000, 1, 27000, N'')
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (413, 22, 31, 0, 1, 0, N'')
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (414, 22, 3, 27000, 1, 27000, N'')
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (415, 22, 2, 29000, 1, 29000, N'')
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (416, 22, 6, 29000, 1, 29000, N'')
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ThanhTien], [GhiChu]) VALUES (417, 22, 7, 27000, 1, 27000, N'')
SET IDENTITY_INSERT [dbo].[HoaDonDetail] OFF
/****** Object:  Default [DF_NguyenLieu_IsActive]    Script Date: 10/17/2013 10:43:36 ******/
ALTER TABLE [dbo].[NguyenLieu] ADD  CONSTRAINT [DF_NguyenLieu_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
/****** Object:  Default [DF_NguyenLieu_DeleteFlag]    Script Date: 10/17/2013 10:43:36 ******/
ALTER TABLE [dbo].[NguyenLieu] ADD  CONSTRAINT [DF_NguyenLieu_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
GO
/****** Object:  Default [DF_Setting_IsActive]    Script Date: 10/17/2013 10:43:36 ******/
ALTER TABLE [dbo].[Setting] ADD  CONSTRAINT [DF_Setting_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
/****** Object:  Default [DF_SanPham_IsActive]    Script Date: 10/17/2013 10:43:36 ******/
ALTER TABLE [dbo].[SanPham] ADD  CONSTRAINT [DF_SanPham_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
/****** Object:  Default [DF_SanPham_DeleteFlag]    Script Date: 10/17/2013 10:43:36 ******/
ALTER TABLE [dbo].[SanPham] ADD  CONSTRAINT [DF_SanPham_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
GO
/****** Object:  Default [DF_KhachHang_GioiTinh]    Script Date: 10/17/2013 10:43:36 ******/
ALTER TABLE [dbo].[KhachHang] ADD  CONSTRAINT [DF_KhachHang_GioiTinh]  DEFAULT (N'Nam') FOR [GioiTinh]
GO
/****** Object:  Default [DF_KhachHang_DeleteFlag]    Script Date: 10/17/2013 10:43:36 ******/
ALTER TABLE [dbo].[KhachHang] ADD  CONSTRAINT [DF_KhachHang_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
GO
/****** Object:  Default [DF_User_GioiTinh]    Script Date: 10/17/2013 10:43:36 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_GioiTinh]  DEFAULT (N'Nam') FOR [GioiTinh]
GO
/****** Object:  Default [DF_User_DeleteFlag]    Script Date: 10/17/2013 10:43:36 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
GO
/****** Object:  Default [DF_DonHang_ThanhTien]    Script Date: 10/17/2013 10:43:36 ******/
ALTER TABLE [dbo].[HoaDon] ADD  CONSTRAINT [DF_DonHang_ThanhTien]  DEFAULT ((0)) FOR [ThanhTien]
GO
/****** Object:  Default [DF_HoaDon_Duyet]    Script Date: 10/17/2013 10:43:36 ******/
ALTER TABLE [dbo].[HoaDon] ADD  CONSTRAINT [DF_HoaDon_Duyet]  DEFAULT ((0)) FOR [Duyet]
GO
/****** Object:  Default [DF_HoaDon_DeleteFlag]    Script Date: 10/17/2013 10:43:36 ******/
ALTER TABLE [dbo].[HoaDon] ADD  CONSTRAINT [DF_HoaDon_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
GO
/****** Object:  Default [DF_GiaChinhThuc_Gia]    Script Date: 10/17/2013 10:43:36 ******/
ALTER TABLE [dbo].[GiaChinhThuc] ADD  CONSTRAINT [DF_GiaChinhThuc_Gia]  DEFAULT ((0)) FOR [Gia]
GO
/****** Object:  Default [DF_HoaDonDetail_SoLuong]    Script Date: 10/17/2013 10:43:36 ******/
ALTER TABLE [dbo].[HoaDonDetail] ADD  CONSTRAINT [DF_HoaDonDetail_SoLuong]  DEFAULT ((1)) FOR [SoLuong]
GO
/****** Object:  Default [DF_DonHangDetail_ThanhTien]    Script Date: 10/17/2013 10:43:36 ******/
ALTER TABLE [dbo].[HoaDonDetail] ADD  CONSTRAINT [DF_DonHangDetail_ThanhTien]  DEFAULT ((0)) FOR [ThanhTien]
GO
/****** Object:  ForeignKey [FK_SanPham_SanPhamGroup]    Script Date: 10/17/2013 10:43:36 ******/
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_SanPhamGroup] FOREIGN KEY([IdGroup])
REFERENCES [dbo].[SanPhamGroup] ([Id])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_SanPhamGroup]
GO
/****** Object:  ForeignKey [FK_NhatKyNguyenLieu_NguyenLieu]    Script Date: 10/17/2013 10:43:36 ******/
ALTER TABLE [dbo].[NhatKyNguyenLieu]  WITH CHECK ADD  CONSTRAINT [FK_NhatKyNguyenLieu_NguyenLieu] FOREIGN KEY([IdNguyenLieu])
REFERENCES [dbo].[NguyenLieu] ([Id])
GO
ALTER TABLE [dbo].[NhatKyNguyenLieu] CHECK CONSTRAINT [FK_NhatKyNguyenLieu_NguyenLieu]
GO
/****** Object:  ForeignKey [FK_KhachHang_KhachHangGroup]    Script Date: 10/17/2013 10:43:36 ******/
ALTER TABLE [dbo].[KhachHang]  WITH CHECK ADD  CONSTRAINT [FK_KhachHang_KhachHangGroup] FOREIGN KEY([IdGroup])
REFERENCES [dbo].[KhachHangGroup] ([Id])
GO
ALTER TABLE [dbo].[KhachHang] CHECK CONSTRAINT [FK_KhachHang_KhachHangGroup]
GO
/****** Object:  ForeignKey [FK_User_UserGroup]    Script Date: 10/17/2013 10:43:36 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserGroup] FOREIGN KEY([IdGroup])
REFERENCES [dbo].[UserGroup] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserGroup]
GO
/****** Object:  ForeignKey [FK_HoaDon_KhachHang]    Script Date: 10/17/2013 10:43:36 ******/
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_KhachHang] FOREIGN KEY([IdKhachHang])
REFERENCES [dbo].[KhachHang] ([Id])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_KhachHang]
GO
/****** Object:  ForeignKey [FK_HoaDon_User]    Script Date: 10/17/2013 10:43:36 ******/
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_User]
GO
/****** Object:  ForeignKey [FK_GiaChinhThuc_SanPham]    Script Date: 10/17/2013 10:43:36 ******/
ALTER TABLE [dbo].[GiaChinhThuc]  WITH CHECK ADD  CONSTRAINT [FK_GiaChinhThuc_SanPham] FOREIGN KEY([IdSanPham])
REFERENCES [dbo].[SanPham] ([Id])
GO
ALTER TABLE [dbo].[GiaChinhThuc] CHECK CONSTRAINT [FK_GiaChinhThuc_SanPham]
GO
/****** Object:  ForeignKey [FK_DinhLuong_NguyenLieu]    Script Date: 10/17/2013 10:43:36 ******/
ALTER TABLE [dbo].[DinhLuong]  WITH CHECK ADD  CONSTRAINT [FK_DinhLuong_NguyenLieu] FOREIGN KEY([IdNguyenLieu])
REFERENCES [dbo].[NguyenLieu] ([Id])
GO
ALTER TABLE [dbo].[DinhLuong] CHECK CONSTRAINT [FK_DinhLuong_NguyenLieu]
GO
/****** Object:  ForeignKey [FK_DinhLuong_SanPham]    Script Date: 10/17/2013 10:43:36 ******/
ALTER TABLE [dbo].[DinhLuong]  WITH CHECK ADD  CONSTRAINT [FK_DinhLuong_SanPham] FOREIGN KEY([IdSanPham])
REFERENCES [dbo].[SanPham] ([Id])
GO
ALTER TABLE [dbo].[DinhLuong] CHECK CONSTRAINT [FK_DinhLuong_SanPham]
GO
/****** Object:  ForeignKey [FK_HoaDonDetail_HoaDon]    Script Date: 10/17/2013 10:43:36 ******/
ALTER TABLE [dbo].[HoaDonDetail]  WITH CHECK ADD  CONSTRAINT [FK_HoaDonDetail_HoaDon] FOREIGN KEY([IdHoaDon])
REFERENCES [dbo].[HoaDon] ([Id])
GO
ALTER TABLE [dbo].[HoaDonDetail] CHECK CONSTRAINT [FK_HoaDonDetail_HoaDon]
GO
/****** Object:  ForeignKey [FK_HoaDonDetail_SanPham]    Script Date: 10/17/2013 10:43:36 ******/
ALTER TABLE [dbo].[HoaDonDetail]  WITH CHECK ADD  CONSTRAINT [FK_HoaDonDetail_SanPham] FOREIGN KEY([IdSanPham])
REFERENCES [dbo].[SanPham] ([Id])
GO
ALTER TABLE [dbo].[HoaDonDetail] CHECK CONSTRAINT [FK_HoaDonDetail_SanPham]
GO
