USE [master]
GO
/****** Object:  Database [QuanLyDoanhSo]    Script Date: 07/08/2014 18:49:13 ******/
CREATE DATABASE [QuanLyDoanhSo] ON  PRIMARY 
( NAME = N'QuanLyDoanhSo', FILENAME = N'c:\Program Files (x86)\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\QuanLyDoanhSo.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'QuanLyDoanhSo_log', FILENAME = N'c:\Program Files (x86)\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\QuanLyDoanhSo_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [QuanLyDoanhSo] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuanLyDoanhSo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuanLyDoanhSo] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [QuanLyDoanhSo] SET ANSI_NULLS OFF
GO
ALTER DATABASE [QuanLyDoanhSo] SET ANSI_PADDING OFF
GO
ALTER DATABASE [QuanLyDoanhSo] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [QuanLyDoanhSo] SET ARITHABORT OFF
GO
ALTER DATABASE [QuanLyDoanhSo] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [QuanLyDoanhSo] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [QuanLyDoanhSo] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [QuanLyDoanhSo] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [QuanLyDoanhSo] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [QuanLyDoanhSo] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [QuanLyDoanhSo] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [QuanLyDoanhSo] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [QuanLyDoanhSo] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [QuanLyDoanhSo] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [QuanLyDoanhSo] SET  DISABLE_BROKER
GO
ALTER DATABASE [QuanLyDoanhSo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [QuanLyDoanhSo] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [QuanLyDoanhSo] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [QuanLyDoanhSo] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [QuanLyDoanhSo] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [QuanLyDoanhSo] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [QuanLyDoanhSo] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [QuanLyDoanhSo] SET  READ_WRITE
GO
ALTER DATABASE [QuanLyDoanhSo] SET RECOVERY SIMPLE
GO
ALTER DATABASE [QuanLyDoanhSo] SET  MULTI_USER
GO
ALTER DATABASE [QuanLyDoanhSo] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [QuanLyDoanhSo] SET DB_CHAINING OFF
GO
USE [QuanLyDoanhSo]
GO
/****** Object:  Table [dbo].[NguonCungCap]    Script Date: 07/08/2014 18:49:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NguonCungCap](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[DiaChi] [nvarchar](100) NULL,
	[DienThoai] [nvarchar](30) NULL,
	[DTDD] [nvarchar](30) NULL,
	[Email] [nvarchar](50) NULL,
	[Fax] [nvarchar](30) NULL,
	[MaSoThue] [nvarchar](20) NULL,
	[GhiChu] [nvarchar](200) NULL,
	[DeleteFlag] [bit] NOT NULL,
 CONSTRAINT [PK_NguonCungCap] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[NguonCungCap] ON
INSERT [dbo].[NguonCungCap] ([Id], [Ten], [DiaChi], [DienThoai], [DTDD], [Email], [Fax], [MaSoThue], [GhiChu], [DeleteFlag]) VALUES (1, N'Nguồn A', N'Bình Thạnh', N'', N'987654321', N'test@', N'', N'123', N'aaa', 0)
SET IDENTITY_INSERT [dbo].[NguonCungCap] OFF
/****** Object:  Table [dbo].[MenhGiaTien]    Script Date: 07/08/2014 18:49:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenhGiaTien](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[Gia] [int] NOT NULL,
	[GhiChu] [nvarchar](200) NULL,
	[IsActived] [bit] NOT NULL,
	[DeleteFlag] [bit] NOT NULL,
 CONSTRAINT [PK_NhomSanPham] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[MenhGiaTien] ON
INSERT [dbo].[MenhGiaTien] ([Id], [Ten], [Gia], [GhiChu], [IsActived], [DeleteFlag]) VALUES (1, N'500đ', 500, NULL, 1, 0)
INSERT [dbo].[MenhGiaTien] ([Id], [Ten], [Gia], [GhiChu], [IsActived], [DeleteFlag]) VALUES (2, N'1000đ', 1000, NULL, 1, 0)
INSERT [dbo].[MenhGiaTien] ([Id], [Ten], [Gia], [GhiChu], [IsActived], [DeleteFlag]) VALUES (3, N'2000đ', 2000, NULL, 1, 0)
INSERT [dbo].[MenhGiaTien] ([Id], [Ten], [Gia], [GhiChu], [IsActived], [DeleteFlag]) VALUES (4, N'5000đ', 5000, NULL, 1, 0)
INSERT [dbo].[MenhGiaTien] ([Id], [Ten], [Gia], [GhiChu], [IsActived], [DeleteFlag]) VALUES (5, N'10000đ', 10000, NULL, 1, 0)
INSERT [dbo].[MenhGiaTien] ([Id], [Ten], [Gia], [GhiChu], [IsActived], [DeleteFlag]) VALUES (6, N'20000đ', 20000, NULL, 1, 0)
INSERT [dbo].[MenhGiaTien] ([Id], [Ten], [Gia], [GhiChu], [IsActived], [DeleteFlag]) VALUES (7, N'50000đ', 50000, NULL, 1, 0)
INSERT [dbo].[MenhGiaTien] ([Id], [Ten], [Gia], [GhiChu], [IsActived], [DeleteFlag]) VALUES (8, N'100000đ', 100000, NULL, 1, 0)
INSERT [dbo].[MenhGiaTien] ([Id], [Ten], [Gia], [GhiChu], [IsActived], [DeleteFlag]) VALUES (9, N'200000đ', 200000, NULL, 1, 0)
INSERT [dbo].[MenhGiaTien] ([Id], [Ten], [Gia], [GhiChu], [IsActived], [DeleteFlag]) VALUES (10, N'500000đ', 500000, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[MenhGiaTien] OFF
/****** Object:  Table [dbo].[SanPham]    Script Date: 07/08/2014 18:49:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[Gia] [int] NOT NULL,
	[DonViTinh] [nvarchar](20) NULL,
	[GhiChu] [nvarchar](200) NULL,
	[IsActived] [bit] NOT NULL,
	[DeleteFlag] [bit] NOT NULL,
 CONSTRAINT [PK_SanPham] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[SanPham] ON
INSERT [dbo].[SanPham] ([Id], [Ten], [Gia], [DonViTinh], [GhiChu], [IsActived], [DeleteFlag]) VALUES (1, N'Vina TCTY', 15994, NULL, NULL, 1, 0)
INSERT [dbo].[SanPham] ([Id], [Ten], [Gia], [DonViTinh], [GhiChu], [IsActived], [DeleteFlag]) VALUES (2, N'Gold Seal', 6394, NULL, NULL, 1, 0)
INSERT [dbo].[SanPham] ([Id], [Ten], [Gia], [DonViTinh], [GhiChu], [IsActived], [DeleteFlag]) VALUES (3, N'Vidana', 8492, NULL, NULL, 1, 0)
INSERT [dbo].[SanPham] ([Id], [Ten], [Gia], [DonViTinh], [GhiChu], [IsActived], [DeleteFlag]) VALUES (4, N'Amore', 10494, NULL, NULL, 1, 0)
INSERT [dbo].[SanPham] ([Id], [Ten], [Gia], [DonViTinh], [GhiChu], [IsActived], [DeleteFlag]) VALUES (5, N'Select', 13992, NULL, NULL, 1, 0)
INSERT [dbo].[SanPham] ([Id], [Ten], [Gia], [DonViTinh], [GhiChu], [IsActived], [DeleteFlag]) VALUES (6, N'Quet', 0, NULL, NULL, 1, 0)
INSERT [dbo].[SanPham] ([Id], [Ten], [Gia], [DonViTinh], [GhiChu], [IsActived], [DeleteFlag]) VALUES (7, N'TLB', 0, NULL, NULL, 1, 0)
INSERT [dbo].[SanPham] ([Id], [Ten], [Gia], [DonViTinh], [GhiChu], [IsActived], [DeleteFlag]) VALUES (8, N'Test', 15000, N'Bao', N'', 1, 0)
SET IDENTITY_INSERT [dbo].[SanPham] OFF
/****** Object:  Table [dbo].[UserGroup]    Script Date: 07/08/2014 18:49:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[GhiChu] [nvarchar](200) NULL,
 CONSTRAINT [PK_NhomUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[UserGroup] ON
INSERT [dbo].[UserGroup] ([Id], [Ten], [GhiChu]) VALUES (1, N'Admin', NULL)
INSERT [dbo].[UserGroup] ([Id], [Ten], [GhiChu]) VALUES (2, N'User', NULL)
INSERT [dbo].[UserGroup] ([Id], [Ten], [GhiChu]) VALUES (3, N'Tổ trưởng', NULL)
SET IDENTITY_INSERT [dbo].[UserGroup] OFF
/****** Object:  Table [dbo].[User]    Script Date: 07/08/2014 18:49:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUserGroup] [int] NOT NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[GioiTinh] [nvarchar](3) NOT NULL,
	[To] [int] NULL,
	[DOB] [date] NULL,
	[CMND] [nvarchar](10) NULL,
	[NgayCap] [date] NULL,
	[NoiCap] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](100) NULL,
	[DienThoai] [nvarchar](30) NULL,
	[DTDD] [nvarchar](30) NULL,
	[Email] [nvarchar](50) NULL,
	[Ton] [int] NOT NULL,
	[GhiChu] [nvarchar](200) NULL,
	[DeleteFlag] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([Id], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [Ton], [GhiChu], [DeleteFlag]) VALUES (1, 1, N'admin', N'admin', N'21232F297A57A5A743894AE4A801FC345454433454539', N'Nam', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, 0)
INSERT [dbo].[User] ([Id], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [Ton], [GhiChu], [DeleteFlag]) VALUES (2, 2, N'Lý Phước An', N'an.ly', N'90150983CD24FB0D6963F7D28E17F7245454433454539', N'Nam', 1, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', 0, N'', 0)
INSERT [dbo].[User] ([Id], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [Ton], [GhiChu], [DeleteFlag]) VALUES (3, 2, N'Nguyễn Kim Hoa', N'hoa.nguyen', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 1, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', 0, N'', 0)
INSERT [dbo].[User] ([Id], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [Ton], [GhiChu], [DeleteFlag]) VALUES (4, 2, N'Cao Xuân Hồng', N'hong.cao', N'47BCE5C74F589F4867DBD57E9CA9F8845454433454539', N'Nam', 1, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', 0, N'', 0)
INSERT [dbo].[User] ([Id], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [Ton], [GhiChu], [DeleteFlag]) VALUES (5, 3, N'Nguyễn Hùng Diện', N'dien.hung', N'47BCE5C74F589F4867DBD57E9CA9F8845454433454539', N'Nam', 1, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', 0, N'', 0)
INSERT [dbo].[User] ([Id], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [Ton], [GhiChu], [DeleteFlag]) VALUES (6, 2, N'Phạm Đình Nho', N'nho.dinh', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 2, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', 0, N'', 0)
INSERT [dbo].[User] ([Id], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [Ton], [GhiChu], [DeleteFlag]) VALUES (7, 2, N'Vũ Văn Tiến', N'tien.vu', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 2, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', 0, N'', 0)
INSERT [dbo].[User] ([Id], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [Ton], [GhiChu], [DeleteFlag]) VALUES (8, 2, N'Trần Kiến Nghiệp', N'kien.nghiep', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 2, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', 0, N'', 0)
INSERT [dbo].[User] ([Id], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [Ton], [GhiChu], [DeleteFlag]) VALUES (9, 3, N'Trần Hải Đăng', N'dang.tran', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 2, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', 0, N'', 0)
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Table [dbo].[KhuyenMai]    Script Date: 07/08/2014 18:49:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhuyenMai](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdSanPham] [int] NOT NULL,
	[SoLuongSanPham] [int] NOT NULL,
	[IdSanPhamKhuyenMai] [int] NOT NULL,
	[SoLuongSanPhamKhuyenMai] [int] NOT NULL,
	[DonViLamTron] [float] NOT NULL,
	[GhiChu] [nvarchar](200) NULL,
 CONSTRAINT [PK_KhuyenMai] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[KhuyenMai] ON
INSERT [dbo].[KhuyenMai] ([Id], [IdSanPham], [SoLuongSanPham], [IdSanPhamKhuyenMai], [SoLuongSanPhamKhuyenMai], [DonViLamTron], [GhiChu]) VALUES (1, 4, 10, 7, 1, 0, N'aaa')
SET IDENTITY_INSERT [dbo].[KhuyenMai] OFF
/****** Object:  Table [dbo].[NhapHang]    Script Date: 07/08/2014 18:49:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhapHang](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUser] [int] NOT NULL,
	[IdNguonCungCap] [int] NULL,
	[Date] [datetime] NOT NULL,
	[GhiChu] [nvarchar](200) NULL,
	[DeleteFlag] [bit] NOT NULL,
 CONSTRAINT [PK_NhapHang] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[NhapHang] ON
INSERT [dbo].[NhapHang] ([Id], [IdUser], [IdNguonCungCap], [Date], [GhiChu], [DeleteFlag]) VALUES (1, 1, NULL, CAST(0x0000A344014DFC78 AS DateTime), N'', 0)
SET IDENTITY_INSERT [dbo].[NhapHang] OFF
/****** Object:  Table [dbo].[BanHang]    Script Date: 07/08/2014 18:49:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BanHang](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUser] [int] NULL,
	[Date] [datetime] NOT NULL,
	[ThanhTien] [int] NOT NULL,
	[GhiChu] [nvarchar](200) NULL,
	[DeleteFlag] [bit] NOT NULL,
 CONSTRAINT [PK_BanHang] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BanHang] ON
INSERT [dbo].[BanHang] ([Id], [IdUser], [Date], [ThanhTien], [GhiChu], [DeleteFlag]) VALUES (4, 1, CAST(0x0000A344015121FB AS DateTime), 239332, N'', 0)
INSERT [dbo].[BanHang] ([Id], [IdUser], [Date], [ThanhTien], [GhiChu], [DeleteFlag]) VALUES (5, 1, CAST(0x0000A34401521C55 AS DateTime), 25494, N'', 0)
INSERT [dbo].[BanHang] ([Id], [IdUser], [Date], [ThanhTien], [GhiChu], [DeleteFlag]) VALUES (6, 2, CAST(0x0000A34401538A56 AS DateTime), 10494, N'', 0)
INSERT [dbo].[BanHang] ([Id], [IdUser], [Date], [ThanhTien], [GhiChu], [DeleteFlag]) VALUES (7, 4, CAST(0x0000A362012798F9 AS DateTime), 207850, N'', 0)
INSERT [dbo].[BanHang] ([Id], [IdUser], [Date], [ThanhTien], [GhiChu], [DeleteFlag]) VALUES (8, 1, CAST(0x0000A3620128DC76 AS DateTime), 219940, N'', 0)
INSERT [dbo].[BanHang] ([Id], [IdUser], [Date], [ThanhTien], [GhiChu], [DeleteFlag]) VALUES (9, 2, CAST(0x0000A36101322F34 AS DateTime), 63940, N'', 0)
INSERT [dbo].[BanHang] ([Id], [IdUser], [Date], [ThanhTien], [GhiChu], [DeleteFlag]) VALUES (10, 2, CAST(0x0000A362013355FD AS DateTime), 139970, N'', 0)
INSERT [dbo].[BanHang] ([Id], [IdUser], [Date], [ThanhTien], [GhiChu], [DeleteFlag]) VALUES (11, 6, CAST(0x0000A3610134B290 AS DateTime), 254760, N'', 0)
SET IDENTITY_INSERT [dbo].[BanHang] OFF
/****** Object:  Table [dbo].[NhapHangChiTiet]    Script Date: 07/08/2014 18:49:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhapHangChiTiet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdNhapHang] [int] NOT NULL,
	[IdSanPham] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[Gia] [int] NOT NULL,
	[ThanhTien] [int] NOT NULL,
 CONSTRAINT [PK_NhapHangChiTiet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[NhapHangChiTiet] ON
INSERT [dbo].[NhapHangChiTiet] ([Id], [IdNhapHang], [IdSanPham], [SoLuong], [Gia], [ThanhTien]) VALUES (1, 1, 1, 1, 15994, 15994)
INSERT [dbo].[NhapHangChiTiet] ([Id], [IdNhapHang], [IdSanPham], [SoLuong], [Gia], [ThanhTien]) VALUES (2, 1, 2, 3, 6394, 19182)
SET IDENTITY_INSERT [dbo].[NhapHangChiTiet] OFF
/****** Object:  Table [dbo].[BanHangChiTiet]    Script Date: 07/08/2014 18:49:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BanHangChiTiet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdBanHang] [int] NOT NULL,
	[IdSanPham] [int] NOT NULL,
	[TonDau] [int] NOT NULL,
	[Nhan] [int] NOT NULL,
	[Ban] [int] NOT NULL,
	[TonCuoi] [int] NOT NULL,
	[ThuHoi] [int] NOT NULL,
	[Gia] [int] NOT NULL,
	[ThanhTien] [int] NOT NULL,
 CONSTRAINT [PK_BanHangChiTiet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BanHangChiTiet] ON
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (1, 4, 4, 0, 0, 1, 0, 0, 10494, 10494)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (2, 5, 8, 0, 0, 1, 0, 0, 15000, 15000)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (3, 5, 4, 0, 0, 1, 0, 0, 10494, 10494)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (4, 6, 4, 0, 0, 1, 0, 0, 10494, 10494)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (5, 7, 1, 0, 10, 5, 5, 5, 15994, 79970)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (6, 7, 2, 0, 20, 20, 0, 0, 6394, 127880)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (7, 7, 3, 0, 0, 0, 0, 0, 8492, 0)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (8, 7, 4, 0, 5, 3, 1, 1, 10494, 31482)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (9, 7, 5, 0, 5, 0, 5, 0, 13992, 0)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (10, 7, 8, 0, 0, 0, 0, 0, 15000, 0)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (11, 8, 1, 0, 10, 10, 0, 0, 15994, 159940)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (12, 8, 2, 0, 0, 0, 0, 0, 6394, 0)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (13, 8, 3, 0, 0, 0, 0, 0, 8492, 0)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (14, 8, 4, 0, 0, 0, 0, 0, 10494, 0)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (15, 8, 5, 0, 0, 0, 0, 0, 13992, 0)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (16, 8, 8, 0, 10, 4, 0, 6, 15000, 60000)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (17, 9, 1, 0, 0, 0, 0, 0, 15994, 0)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (18, 9, 2, 0, 10, 10, 0, 0, 6394, 63940)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (19, 9, 3, 0, 0, 0, 0, 0, 8492, 0)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (20, 9, 4, 0, 0, 0, 0, 0, 10494, 0)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (21, 9, 5, 0, 0, 0, 0, 0, 13992, 0)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (22, 9, 8, 0, 0, 0, 0, 0, 15000, 0)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (23, 10, 1, 0, 10, 5, 5, 0, 15994, 79970)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (24, 10, 2, 0, 0, 0, 0, 0, 6394, 0)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (25, 10, 3, 0, 0, 0, 0, 0, 8492, 0)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (26, 10, 4, 0, 0, 0, 0, 0, 10494, 0)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (27, 10, 5, 0, 0, 0, 0, 0, 13992, 0)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (28, 10, 8, 0, 10, 4, 0, 6, 15000, 60000)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (29, 11, 1, 0, 0, 0, 0, 0, 15994, 0)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (30, 11, 2, 0, 0, 0, 0, 0, 6394, 0)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (31, 11, 3, 0, 50, 30, 20, 0, 8492, 254760)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (32, 11, 4, 0, 0, 0, 0, 0, 10494, 0)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (33, 11, 5, 0, 0, 0, 0, 0, 13992, 0)
INSERT [dbo].[BanHangChiTiet] ([Id], [IdBanHang], [IdSanPham], [TonDau], [Nhan], [Ban], [TonCuoi], [ThuHoi], [Gia], [ThanhTien]) VALUES (34, 11, 8, 0, 0, 0, 0, 0, 15000, 0)
SET IDENTITY_INSERT [dbo].[BanHangChiTiet] OFF
/****** Object:  Table [dbo].[BanHang_MenhGiaTien]    Script Date: 07/08/2014 18:49:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BanHang_MenhGiaTien](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdBanHang] [int] NOT NULL,
	[IdMenhGiaTien] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[ThanhTien] [int] NOT NULL,
	[GhiChu] [nvarchar](200) NULL,
 CONSTRAINT [PK_BanHang_MenhGiaTien] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_NguonCungCap_DeleteFlag]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[NguonCungCap] ADD  CONSTRAINT [DF_NguonCungCap_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
GO
/****** Object:  Default [DF_MenhGia_IsActived]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[MenhGiaTien] ADD  CONSTRAINT [DF_MenhGia_IsActived]  DEFAULT ((1)) FOR [IsActived]
GO
/****** Object:  Default [DF_NhomTien_DeleteFlag]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[MenhGiaTien] ADD  CONSTRAINT [DF_NhomTien_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
GO
/****** Object:  Default [DF_SanPham_Gia]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[SanPham] ADD  CONSTRAINT [DF_SanPham_Gia]  DEFAULT ((0)) FOR [Gia]
GO
/****** Object:  Default [DF_SanPham_IsActived]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[SanPham] ADD  CONSTRAINT [DF_SanPham_IsActived]  DEFAULT ((1)) FOR [IsActived]
GO
/****** Object:  Default [DF_SanPham_DeleteFlag]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[SanPham] ADD  CONSTRAINT [DF_SanPham_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
GO
/****** Object:  Default [DF_User_GioiTinh]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_GioiTinh]  DEFAULT ((1)) FOR [GioiTinh]
GO
/****** Object:  Default [DF_User_Ton]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Ton]  DEFAULT ((0)) FOR [Ton]
GO
/****** Object:  Default [DF_User_DeleteFlag_1]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_DeleteFlag_1]  DEFAULT ((0)) FOR [DeleteFlag]
GO
/****** Object:  Default [DF_KhuyenMai_DonViLamTron]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[KhuyenMai] ADD  CONSTRAINT [DF_KhuyenMai_DonViLamTron]  DEFAULT ((1)) FOR [DonViLamTron]
GO
/****** Object:  Default [DF_NhapHang_DeleteFlag]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[NhapHang] ADD  CONSTRAINT [DF_NhapHang_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
GO
/****** Object:  Default [DF_BanHang_ThanhTien]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[BanHang] ADD  CONSTRAINT [DF_BanHang_ThanhTien]  DEFAULT ((0)) FOR [ThanhTien]
GO
/****** Object:  Default [DF_BanHang_DeleteFlag]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[BanHang] ADD  CONSTRAINT [DF_BanHang_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
GO
/****** Object:  Default [DF_Table_1_LuongNhan]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[NhapHangChiTiet] ADD  CONSTRAINT [DF_Table_1_LuongNhan]  DEFAULT ((0)) FOR [SoLuong]
GO
/****** Object:  Default [DF_NhapHangChiTiet_Gia]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[NhapHangChiTiet] ADD  CONSTRAINT [DF_NhapHangChiTiet_Gia]  DEFAULT ((0)) FOR [Gia]
GO
/****** Object:  Default [DF_NhapHangChiTiet_ThanhTien]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[NhapHangChiTiet] ADD  CONSTRAINT [DF_NhapHangChiTiet_ThanhTien]  DEFAULT ((0)) FOR [ThanhTien]
GO
/****** Object:  Default [DF_BanHangChiTiet_TonDau_1]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[BanHangChiTiet] ADD  CONSTRAINT [DF_BanHangChiTiet_TonDau_1]  DEFAULT ((0)) FOR [TonDau]
GO
/****** Object:  Default [DF_BanHangChiTiet_Nhan]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[BanHangChiTiet] ADD  CONSTRAINT [DF_BanHangChiTiet_Nhan]  DEFAULT ((0)) FOR [Nhan]
GO
/****** Object:  Default [DF_BanHangChiTiet_TonDau]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[BanHangChiTiet] ADD  CONSTRAINT [DF_BanHangChiTiet_TonDau]  DEFAULT ((0)) FOR [Ban]
GO
/****** Object:  Default [DF_BanHangChiTiet_TonCuoi_1]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[BanHangChiTiet] ADD  CONSTRAINT [DF_BanHangChiTiet_TonCuoi_1]  DEFAULT ((0)) FOR [TonCuoi]
GO
/****** Object:  Default [DF_BanHangChiTiet_TonCuoi]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[BanHangChiTiet] ADD  CONSTRAINT [DF_BanHangChiTiet_TonCuoi]  DEFAULT ((0)) FOR [ThuHoi]
GO
/****** Object:  Default [DF_BanHangChiTiet_Gia]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[BanHangChiTiet] ADD  CONSTRAINT [DF_BanHangChiTiet_Gia]  DEFAULT ((0)) FOR [Gia]
GO
/****** Object:  Default [DF_BanHangChiTiet_ThanhTien]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[BanHangChiTiet] ADD  CONSTRAINT [DF_BanHangChiTiet_ThanhTien]  DEFAULT ((0)) FOR [ThanhTien]
GO
/****** Object:  Default [DF_BanHang_MenhGiaTien_SoLuong]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[BanHang_MenhGiaTien] ADD  CONSTRAINT [DF_BanHang_MenhGiaTien_SoLuong]  DEFAULT ((0)) FOR [SoLuong]
GO
/****** Object:  Default [DF_BanHang_MenhGiaTien_ThanhTien]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[BanHang_MenhGiaTien] ADD  CONSTRAINT [DF_BanHang_MenhGiaTien_ThanhTien]  DEFAULT ((0)) FOR [ThanhTien]
GO
/****** Object:  ForeignKey [FK_User_UserNhom]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserNhom] FOREIGN KEY([IdUserGroup])
REFERENCES [dbo].[UserGroup] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserNhom]
GO
/****** Object:  ForeignKey [FK_KhuyenMai_SanPham]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[KhuyenMai]  WITH CHECK ADD  CONSTRAINT [FK_KhuyenMai_SanPham] FOREIGN KEY([IdSanPham])
REFERENCES [dbo].[SanPham] ([Id])
GO
ALTER TABLE [dbo].[KhuyenMai] CHECK CONSTRAINT [FK_KhuyenMai_SanPham]
GO
/****** Object:  ForeignKey [FK_KhuyenMai_SanPham1]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[KhuyenMai]  WITH CHECK ADD  CONSTRAINT [FK_KhuyenMai_SanPham1] FOREIGN KEY([IdSanPhamKhuyenMai])
REFERENCES [dbo].[SanPham] ([Id])
GO
ALTER TABLE [dbo].[KhuyenMai] CHECK CONSTRAINT [FK_KhuyenMai_SanPham1]
GO
/****** Object:  ForeignKey [FK_NhapHang_NguonCungCap]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[NhapHang]  WITH CHECK ADD  CONSTRAINT [FK_NhapHang_NguonCungCap] FOREIGN KEY([IdNguonCungCap])
REFERENCES [dbo].[NguonCungCap] ([Id])
GO
ALTER TABLE [dbo].[NhapHang] CHECK CONSTRAINT [FK_NhapHang_NguonCungCap]
GO
/****** Object:  ForeignKey [FK_NhapHang_User]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[NhapHang]  WITH CHECK ADD  CONSTRAINT [FK_NhapHang_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[NhapHang] CHECK CONSTRAINT [FK_NhapHang_User]
GO
/****** Object:  ForeignKey [FK_BanHang_User]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[BanHang]  WITH CHECK ADD  CONSTRAINT [FK_BanHang_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[BanHang] CHECK CONSTRAINT [FK_BanHang_User]
GO
/****** Object:  ForeignKey [FK_NhapHangChiTiet_NhapHang]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[NhapHangChiTiet]  WITH CHECK ADD  CONSTRAINT [FK_NhapHangChiTiet_NhapHang] FOREIGN KEY([IdNhapHang])
REFERENCES [dbo].[NhapHang] ([Id])
GO
ALTER TABLE [dbo].[NhapHangChiTiet] CHECK CONSTRAINT [FK_NhapHangChiTiet_NhapHang]
GO
/****** Object:  ForeignKey [FK_NhapHangChiTiet_SanPham]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[NhapHangChiTiet]  WITH CHECK ADD  CONSTRAINT [FK_NhapHangChiTiet_SanPham] FOREIGN KEY([IdSanPham])
REFERENCES [dbo].[SanPham] ([Id])
GO
ALTER TABLE [dbo].[NhapHangChiTiet] CHECK CONSTRAINT [FK_NhapHangChiTiet_SanPham]
GO
/****** Object:  ForeignKey [FK_BanHangChiTiet_BanHang]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[BanHangChiTiet]  WITH CHECK ADD  CONSTRAINT [FK_BanHangChiTiet_BanHang] FOREIGN KEY([IdBanHang])
REFERENCES [dbo].[BanHang] ([Id])
GO
ALTER TABLE [dbo].[BanHangChiTiet] CHECK CONSTRAINT [FK_BanHangChiTiet_BanHang]
GO
/****** Object:  ForeignKey [FK_BanHangChiTiet_SanPham]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[BanHangChiTiet]  WITH CHECK ADD  CONSTRAINT [FK_BanHangChiTiet_SanPham] FOREIGN KEY([IdSanPham])
REFERENCES [dbo].[SanPham] ([Id])
GO
ALTER TABLE [dbo].[BanHangChiTiet] CHECK CONSTRAINT [FK_BanHangChiTiet_SanPham]
GO
/****** Object:  ForeignKey [FK_BanHang_MenhGiaTien_BanHang]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[BanHang_MenhGiaTien]  WITH CHECK ADD  CONSTRAINT [FK_BanHang_MenhGiaTien_BanHang] FOREIGN KEY([IdBanHang])
REFERENCES [dbo].[BanHang] ([Id])
GO
ALTER TABLE [dbo].[BanHang_MenhGiaTien] CHECK CONSTRAINT [FK_BanHang_MenhGiaTien_BanHang]
GO
/****** Object:  ForeignKey [FK_BanHang_MenhGiaTien_MenhGiaTien]    Script Date: 07/08/2014 18:49:14 ******/
ALTER TABLE [dbo].[BanHang_MenhGiaTien]  WITH CHECK ADD  CONSTRAINT [FK_BanHang_MenhGiaTien_MenhGiaTien] FOREIGN KEY([IdMenhGiaTien])
REFERENCES [dbo].[MenhGiaTien] ([Id])
GO
ALTER TABLE [dbo].[BanHang_MenhGiaTien] CHECK CONSTRAINT [FK_BanHang_MenhGiaTien_MenhGiaTien]
GO
