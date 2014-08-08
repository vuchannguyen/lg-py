USE [master]
GO
/****** Object:  Database [QuanLyDoanhSo]    Script Date: 08/08/2014 19:19:34 ******/
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
/****** Object:  Table [dbo].[NguonCungCap]    Script Date: 08/08/2014 19:19:35 ******/
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
/****** Object:  Table [dbo].[LoaiTien]    Script Date: 08/08/2014 19:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiTien](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[Gia] [int] NOT NULL,
	[GhiChu] [nvarchar](200) NULL,
	[IsActived] [bit] NOT NULL,
	[DeleteFlag] [bit] NOT NULL,
 CONSTRAINT [PK_LoaiTien] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[LoaiTien] ON
INSERT [dbo].[LoaiTien] ([Id], [Ten], [Gia], [GhiChu], [IsActived], [DeleteFlag]) VALUES (1, N'500đ', 500, NULL, 1, 0)
INSERT [dbo].[LoaiTien] ([Id], [Ten], [Gia], [GhiChu], [IsActived], [DeleteFlag]) VALUES (2, N'1000đ', 1000, NULL, 1, 0)
INSERT [dbo].[LoaiTien] ([Id], [Ten], [Gia], [GhiChu], [IsActived], [DeleteFlag]) VALUES (3, N'2000đ', 2000, NULL, 1, 0)
INSERT [dbo].[LoaiTien] ([Id], [Ten], [Gia], [GhiChu], [IsActived], [DeleteFlag]) VALUES (4, N'5000đ', 5000, NULL, 1, 0)
INSERT [dbo].[LoaiTien] ([Id], [Ten], [Gia], [GhiChu], [IsActived], [DeleteFlag]) VALUES (5, N'10000đ', 10000, NULL, 1, 0)
INSERT [dbo].[LoaiTien] ([Id], [Ten], [Gia], [GhiChu], [IsActived], [DeleteFlag]) VALUES (6, N'20000đ', 20000, NULL, 1, 0)
INSERT [dbo].[LoaiTien] ([Id], [Ten], [Gia], [GhiChu], [IsActived], [DeleteFlag]) VALUES (7, N'50000đ', 50000, NULL, 1, 0)
INSERT [dbo].[LoaiTien] ([Id], [Ten], [Gia], [GhiChu], [IsActived], [DeleteFlag]) VALUES (8, N'100000đ', 100000, NULL, 1, 0)
INSERT [dbo].[LoaiTien] ([Id], [Ten], [Gia], [GhiChu], [IsActived], [DeleteFlag]) VALUES (9, N'200000đ', 200000, NULL, 1, 0)
INSERT [dbo].[LoaiTien] ([Id], [Ten], [Gia], [GhiChu], [IsActived], [DeleteFlag]) VALUES (10, N'500000đ', 500000, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[LoaiTien] OFF
/****** Object:  Table [dbo].[SanPham]    Script Date: 08/08/2014 19:19:35 ******/
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
INSERT [dbo].[SanPham] ([Id], [Ten], [Gia], [DonViTinh], [GhiChu], [IsActived], [DeleteFlag]) VALUES (6, N'Quẹt', 0, NULL, NULL, 1, 0)
INSERT [dbo].[SanPham] ([Id], [Ten], [Gia], [DonViTinh], [GhiChu], [IsActived], [DeleteFlag]) VALUES (7, N'TLB', 0, NULL, NULL, 1, 0)
INSERT [dbo].[SanPham] ([Id], [Ten], [Gia], [DonViTinh], [GhiChu], [IsActived], [DeleteFlag]) VALUES (8, N'Test', 15000, N'Bao', N'', 1, 0)
SET IDENTITY_INSERT [dbo].[SanPham] OFF
/****** Object:  Table [dbo].[UserGroup]    Script Date: 08/08/2014 19:19:35 ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 08/08/2014 19:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ma] [nvarchar](10) NULL,
	[IdUserGroup] [int] NOT NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[GioiTinh] [nvarchar](3) NOT NULL,
	[To] [int] NULL,
	[Quan] [nvarchar](30) NULL,
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
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (1, NULL, 1, N'admin', N'admin', N'21232F297A57A5A743894AE4A801FC345454433454539', N'Nam', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (2, NULL, 2, N'LÝ PHƯỚC AN', N'an.ly', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 1, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (3, NULL, 2, N'NG KIM HÒA', N'hoa.nguyen', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 1, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (4, N'BH01', 2, N'CAO XUÂN HỒNG', N'hong.cao', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 1, N'5', CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (5, NULL, 3, N'NGUYỄN HÙNG DIỆN', N'dien.hung', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 1, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (6, NULL, 2, N'PHẠM ĐÌNH NHO', N'nho.dinh', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 2, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (7, NULL, 2, N'VŨ VĂN TIẾN', N'tien.vu', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 2, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (8, NULL, 2, N'TRẦN KIẾN NGHIỆP', N'kien.nghiep', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 2, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (9, NULL, 3, N'TRẦN HẢI ĐĂNG', N'dang.tran', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 2, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (10, NULL, 2, N'PHẠM Q TRƯỜNG', N'truong.pham', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 3, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (11, NULL, 2, N'THÁI QUAN PHƯỢNG', N'phuong.thai', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 3, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (12, NULL, 2, N'ĐẶNG TH QUANG', N'quang.dang', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 3, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (13, NULL, 2, N'VÕ NG TRUNG NAM', N'nam.vo', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 3, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (14, NULL, 3, N'PHẠM XUÂN SỰ', N'su.pham', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 3, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (15, NULL, 2, N'NG HÙNG ÂN', N'an.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 4, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (16, NULL, 2, N'NG BÁ TÒNG', N'tong.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 4, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (17, NULL, 2, N'LƯU KIM TRUNG TÍN', N'tin.luu', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 4, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (18, NULL, 3, N'BÙI VĂN BẰNG', N'bang.bui', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 4, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (19, NULL, 2, N'HOÀNG ANH VIỆT ', N'an.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 5, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (20, NULL, 2, N'ĐẬNG MAI HIẾU', N'tong.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 5, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (21, NULL, 2, N'DIỆP  GIA MINH', N'tin.luu', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 5, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (22, NULL, 3, N'BÙI THÀNH TRUNG', N'bang.bui', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 5, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (23, NULL, 2, N'TRẦN ANH DŨNG', N'an.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 6, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (24, NULL, 2, N'NG THỌ HƯỚNG', N'tong.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 6, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (25, NULL, 2, N'PHẠM VĂN LONG', N'an.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 6, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (26, NULL, 2, N'VÕ THÀNH XUẤT', N'tong.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 6, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (27, NULL, 2, N'ĐÔN MINH TUẤN', N'tin.luu', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 6, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (28, NULL, 3, N'PHẠM MAI PHÚC KHÁNH', N'bang.bui', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 6, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (29, NULL, 2, N'BÙI THANH KHIÊM', N'an.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 7, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (30, NULL, 2, N'ĐIỀN HUY TRUNG', N'tong.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 7, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (31, NULL, 2, N'NG XUÂN TRƯỜNG', N'tin.luu', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 7, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (32, NULL, 3, N'NGUYỄN VIẾT HÙNG', N'bang.bui', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 7, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (33, NULL, 2, N'NG TUẤN CẢNH', N'tong.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 8, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (34, NULL, 2, N'ĐỖ VĂN TUẤN', N'an.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 8, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (35, NULL, 2, N'HUỲNH MINH HIẾU', N'tong.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 8, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (36, NULL, 2, N'TRẦN VŨ LUÂN', N'tin.luu', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 8, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (37, NULL, 3, N'VÕ PHÚC HẬU', N'bang.bui', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 8, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (38, NULL, 2, N'HỒ TẤN PHƯỚC', N'tong.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 9, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (39, NULL, 2, N'THÁI MỸ ANH', N'an.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 9, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (40, NULL, 2, N'TRẦN ANH QUỐC', N'tong.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 9, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (41, NULL, 2, N'NG THANH TUẤN', N'tin.luu', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 9, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (42, NULL, 3, N'VÕ HỒNG PHÚC', N'bang.bui', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 9, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (43, NULL, 2, N'TỐNG CÔNG QUYỀN', N'an.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 10, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (44, NULL, 2, N'NG H TRIỆU LONG', N'tong.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 10, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (45, NULL, 2, N'NG H CHƯƠNG', N'an.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 10, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (46, NULL, 2, N'BÙI NHƯ HÀO', N'tong.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 10, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (47, NULL, 2, N'NGÔ PHÚC NGUYÊN', N'tin.luu', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 10, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (48, NULL, 3, N'NGUYỄN HỒNG PHƯỚC', N'bang.bui', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 10, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (49, NULL, 2, N'HUỲNH THỊ ÚT DUNG', N'tong.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 11, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (50, NULL, 2, N'NG THANH SƠN', N'an.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 11, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (51, NULL, 2, N'NGUYỄN THỊ THƠM', N'tong.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 11, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (52, NULL, 2, N'NG CÁT TÂM KHÔNG', N'tin.luu', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 11, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (53, NULL, 3, N'NG HỒNG PHƯỚC', N'bang.bui', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 11, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (54, NULL, 2, N'NGUYỄN ĐỨC MINH', N'an.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 12, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (55, NULL, 2, N'NGUYÊN NGỌC THANH', N'tong.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 12, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (56, NULL, 2, N'ĐỔ LÊ DỰ', N'an.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 12, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (57, NULL, 2, N'NGUYỄN TẤN TÍN', N'tong.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 12, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (58, NULL, 2, N'HỨA TRỌNG TÂM', N'an.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 12, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (59, NULL, 2, N'NGUYỄN NGỌC TÀI', N'tong.ng', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 12, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (60, NULL, 2, N'TRẦN NHƯ TOÀN', N'tin.luu', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 12, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
INSERT [dbo].[User] ([Id], [Ma], [IdUserGroup], [Ten], [UserName], [Password], [GioiTinh], [To], [Quan], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [DTDD], [Email], [GhiChu], [DeleteFlag]) VALUES (61, NULL, 3, N'NGUYỄN QUỐC VIỆT', N'bang.bui', N'EE11CBB19052E4B7AAC0CA6C23EE45454433454539', N'Nam', 12, NULL, CAST(0x07240B00 AS Date), N'', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', N'', 0)
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Table [dbo].[KhuyenMai]    Script Date: 08/08/2014 19:19:35 ******/
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
INSERT [dbo].[KhuyenMai] ([Id], [IdSanPham], [SoLuongSanPham], [IdSanPhamKhuyenMai], [SoLuongSanPhamKhuyenMai], [DonViLamTron], [GhiChu]) VALUES (1, 4, 10, 6, 1, 1, N'aaa')
INSERT [dbo].[KhuyenMai] ([Id], [IdSanPham], [SoLuongSanPham], [IdSanPhamKhuyenMai], [SoLuongSanPhamKhuyenMai], [DonViLamTron], [GhiChu]) VALUES (2, 1, 10, 6, 2, 0.5, N'')
INSERT [dbo].[KhuyenMai] ([Id], [IdSanPham], [SoLuongSanPham], [IdSanPhamKhuyenMai], [SoLuongSanPhamKhuyenMai], [DonViLamTron], [GhiChu]) VALUES (3, 2, 20, 7, 1, 0.5, N'')
INSERT [dbo].[KhuyenMai] ([Id], [IdSanPham], [SoLuongSanPham], [IdSanPhamKhuyenMai], [SoLuongSanPhamKhuyenMai], [DonViLamTron], [GhiChu]) VALUES (4, 3, 10, 6, 1, 0.5, N'')
SET IDENTITY_INSERT [dbo].[KhuyenMai] OFF
/****** Object:  Table [dbo].[NhapHang]    Script Date: 08/08/2014 19:19:35 ******/
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
/****** Object:  Table [dbo].[BanHang]    Script Date: 08/08/2014 19:19:35 ******/
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
/****** Object:  Table [dbo].[NhapHangChiTiet]    Script Date: 08/08/2014 19:19:35 ******/
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
/****** Object:  Table [dbo].[BanHangChiTiet]    Script Date: 08/08/2014 19:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BanHangChiTiet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdBanHang] [int] NOT NULL,
	[IdSanPham] [int] NOT NULL,
	[TonDau] [int] NOT NULL,
	[ThuHoiNgayTruoc] [int] NOT NULL,
	[Nhan] [int] NOT NULL,
	[Ban] [int] NOT NULL,
	[ThuHoi] [int] NOT NULL,
	[Gia] [int] NOT NULL,
	[ThanhTien] [int] NOT NULL,
	[TonCuoi] [int] NOT NULL,
	[DiemMoi] [int] NULL,
	[LuotBan] [int] NULL,
 CONSTRAINT [PK_BanHangChiTiet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BanHang_LoaiTien]    Script Date: 08/08/2014 19:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BanHang_LoaiTien](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdBanHang] [int] NOT NULL,
	[IdLoaiTien] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[ThanhTien] [int] NOT NULL,
 CONSTRAINT [PK_BanHang_LoaiTien] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_NguonCungCap_DeleteFlag]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[NguonCungCap] ADD  CONSTRAINT [DF_NguonCungCap_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
GO
/****** Object:  Default [DF_LoaiTien_IsActived]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[LoaiTien] ADD  CONSTRAINT [DF_LoaiTien_IsActived]  DEFAULT ((1)) FOR [IsActived]
GO
/****** Object:  Default [DF_LoaiTien_DeleteFlag]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[LoaiTien] ADD  CONSTRAINT [DF_LoaiTien_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
GO
/****** Object:  Default [DF_SanPham_Gia]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[SanPham] ADD  CONSTRAINT [DF_SanPham_Gia]  DEFAULT ((0)) FOR [Gia]
GO
/****** Object:  Default [DF_SanPham_IsActived]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[SanPham] ADD  CONSTRAINT [DF_SanPham_IsActived]  DEFAULT ((1)) FOR [IsActived]
GO
/****** Object:  Default [DF_SanPham_DeleteFlag]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[SanPham] ADD  CONSTRAINT [DF_SanPham_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
GO
/****** Object:  Default [DF_User_GioiTinh]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_GioiTinh]  DEFAULT ((1)) FOR [GioiTinh]
GO
/****** Object:  Default [DF_User_DeleteFlag_1]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_DeleteFlag_1]  DEFAULT ((0)) FOR [DeleteFlag]
GO
/****** Object:  Default [DF_KhuyenMai_DonViLamTron]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[KhuyenMai] ADD  CONSTRAINT [DF_KhuyenMai_DonViLamTron]  DEFAULT ((1)) FOR [DonViLamTron]
GO
/****** Object:  Default [DF_NhapHang_DeleteFlag]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[NhapHang] ADD  CONSTRAINT [DF_NhapHang_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
GO
/****** Object:  Default [DF_BanHang_ThanhTien]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[BanHang] ADD  CONSTRAINT [DF_BanHang_ThanhTien]  DEFAULT ((0)) FOR [ThanhTien]
GO
/****** Object:  Default [DF_BanHang_DeleteFlag]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[BanHang] ADD  CONSTRAINT [DF_BanHang_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
GO
/****** Object:  Default [DF_Table_1_LuongNhan]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[NhapHangChiTiet] ADD  CONSTRAINT [DF_Table_1_LuongNhan]  DEFAULT ((0)) FOR [SoLuong]
GO
/****** Object:  Default [DF_NhapHangChiTiet_Gia]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[NhapHangChiTiet] ADD  CONSTRAINT [DF_NhapHangChiTiet_Gia]  DEFAULT ((0)) FOR [Gia]
GO
/****** Object:  Default [DF_NhapHangChiTiet_ThanhTien]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[NhapHangChiTiet] ADD  CONSTRAINT [DF_NhapHangChiTiet_ThanhTien]  DEFAULT ((0)) FOR [ThanhTien]
GO
/****** Object:  Default [DF_BanHangChiTiet_TonDau_1]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[BanHangChiTiet] ADD  CONSTRAINT [DF_BanHangChiTiet_TonDau_1]  DEFAULT ((0)) FOR [TonDau]
GO
/****** Object:  Default [DF_BanHangChiTiet_ThuHoiNgayTruoc]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[BanHangChiTiet] ADD  CONSTRAINT [DF_BanHangChiTiet_ThuHoiNgayTruoc]  DEFAULT ((0)) FOR [ThuHoiNgayTruoc]
GO
/****** Object:  Default [DF_BanHangChiTiet_Nhan]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[BanHangChiTiet] ADD  CONSTRAINT [DF_BanHangChiTiet_Nhan]  DEFAULT ((0)) FOR [Nhan]
GO
/****** Object:  Default [DF_BanHangChiTiet_TonDau]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[BanHangChiTiet] ADD  CONSTRAINT [DF_BanHangChiTiet_TonDau]  DEFAULT ((0)) FOR [Ban]
GO
/****** Object:  Default [DF_BanHangChiTiet_TonCuoi]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[BanHangChiTiet] ADD  CONSTRAINT [DF_BanHangChiTiet_TonCuoi]  DEFAULT ((0)) FOR [ThuHoi]
GO
/****** Object:  Default [DF_BanHangChiTiet_Gia]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[BanHangChiTiet] ADD  CONSTRAINT [DF_BanHangChiTiet_Gia]  DEFAULT ((0)) FOR [Gia]
GO
/****** Object:  Default [DF_BanHangChiTiet_ThanhTien]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[BanHangChiTiet] ADD  CONSTRAINT [DF_BanHangChiTiet_ThanhTien]  DEFAULT ((0)) FOR [ThanhTien]
GO
/****** Object:  Default [DF_BanHangChiTiet_TonCuoi_1]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[BanHangChiTiet] ADD  CONSTRAINT [DF_BanHangChiTiet_TonCuoi_1]  DEFAULT ((0)) FOR [TonCuoi]
GO
/****** Object:  Default [DF_BanHangChiTiet_DiemMoi]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[BanHangChiTiet] ADD  CONSTRAINT [DF_BanHangChiTiet_DiemMoi]  DEFAULT ((0)) FOR [DiemMoi]
GO
/****** Object:  Default [DF_BanHangChiTiet_LuotBan]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[BanHangChiTiet] ADD  CONSTRAINT [DF_BanHangChiTiet_LuotBan]  DEFAULT ((0)) FOR [LuotBan]
GO
/****** Object:  Default [DF_BanHang_LoaiTien_SoLuong]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[BanHang_LoaiTien] ADD  CONSTRAINT [DF_BanHang_LoaiTien_SoLuong]  DEFAULT ((0)) FOR [SoLuong]
GO
/****** Object:  Default [DF_BanHang_LoaiTien_ThanhTien]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[BanHang_LoaiTien] ADD  CONSTRAINT [DF_BanHang_LoaiTien_ThanhTien]  DEFAULT ((0)) FOR [ThanhTien]
GO
/****** Object:  ForeignKey [FK_User_UserNhom]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserNhom] FOREIGN KEY([IdUserGroup])
REFERENCES [dbo].[UserGroup] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserNhom]
GO
/****** Object:  ForeignKey [FK_KhuyenMai_SanPham]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[KhuyenMai]  WITH CHECK ADD  CONSTRAINT [FK_KhuyenMai_SanPham] FOREIGN KEY([IdSanPham])
REFERENCES [dbo].[SanPham] ([Id])
GO
ALTER TABLE [dbo].[KhuyenMai] CHECK CONSTRAINT [FK_KhuyenMai_SanPham]
GO
/****** Object:  ForeignKey [FK_KhuyenMai_SanPham1]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[KhuyenMai]  WITH CHECK ADD  CONSTRAINT [FK_KhuyenMai_SanPham1] FOREIGN KEY([IdSanPhamKhuyenMai])
REFERENCES [dbo].[SanPham] ([Id])
GO
ALTER TABLE [dbo].[KhuyenMai] CHECK CONSTRAINT [FK_KhuyenMai_SanPham1]
GO
/****** Object:  ForeignKey [FK_NhapHang_NguonCungCap]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[NhapHang]  WITH CHECK ADD  CONSTRAINT [FK_NhapHang_NguonCungCap] FOREIGN KEY([IdNguonCungCap])
REFERENCES [dbo].[NguonCungCap] ([Id])
GO
ALTER TABLE [dbo].[NhapHang] CHECK CONSTRAINT [FK_NhapHang_NguonCungCap]
GO
/****** Object:  ForeignKey [FK_NhapHang_User]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[NhapHang]  WITH CHECK ADD  CONSTRAINT [FK_NhapHang_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[NhapHang] CHECK CONSTRAINT [FK_NhapHang_User]
GO
/****** Object:  ForeignKey [FK_BanHang_User]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[BanHang]  WITH CHECK ADD  CONSTRAINT [FK_BanHang_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[BanHang] CHECK CONSTRAINT [FK_BanHang_User]
GO
/****** Object:  ForeignKey [FK_NhapHangChiTiet_NhapHang]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[NhapHangChiTiet]  WITH CHECK ADD  CONSTRAINT [FK_NhapHangChiTiet_NhapHang] FOREIGN KEY([IdNhapHang])
REFERENCES [dbo].[NhapHang] ([Id])
GO
ALTER TABLE [dbo].[NhapHangChiTiet] CHECK CONSTRAINT [FK_NhapHangChiTiet_NhapHang]
GO
/****** Object:  ForeignKey [FK_NhapHangChiTiet_SanPham]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[NhapHangChiTiet]  WITH CHECK ADD  CONSTRAINT [FK_NhapHangChiTiet_SanPham] FOREIGN KEY([IdSanPham])
REFERENCES [dbo].[SanPham] ([Id])
GO
ALTER TABLE [dbo].[NhapHangChiTiet] CHECK CONSTRAINT [FK_NhapHangChiTiet_SanPham]
GO
/****** Object:  ForeignKey [FK_BanHangChiTiet_BanHang]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[BanHangChiTiet]  WITH CHECK ADD  CONSTRAINT [FK_BanHangChiTiet_BanHang] FOREIGN KEY([IdBanHang])
REFERENCES [dbo].[BanHang] ([Id])
GO
ALTER TABLE [dbo].[BanHangChiTiet] CHECK CONSTRAINT [FK_BanHangChiTiet_BanHang]
GO
/****** Object:  ForeignKey [FK_BanHangChiTiet_SanPham]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[BanHangChiTiet]  WITH CHECK ADD  CONSTRAINT [FK_BanHangChiTiet_SanPham] FOREIGN KEY([IdSanPham])
REFERENCES [dbo].[SanPham] ([Id])
GO
ALTER TABLE [dbo].[BanHangChiTiet] CHECK CONSTRAINT [FK_BanHangChiTiet_SanPham]
GO
/****** Object:  ForeignKey [FK_BanHang_LoaiTien_BanHang]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[BanHang_LoaiTien]  WITH CHECK ADD  CONSTRAINT [FK_BanHang_LoaiTien_BanHang] FOREIGN KEY([IdBanHang])
REFERENCES [dbo].[BanHang] ([Id])
GO
ALTER TABLE [dbo].[BanHang_LoaiTien] CHECK CONSTRAINT [FK_BanHang_LoaiTien_BanHang]
GO
/****** Object:  ForeignKey [FK_BanHang_LoaiTien_LoaiTien]    Script Date: 08/08/2014 19:19:35 ******/
ALTER TABLE [dbo].[BanHang_LoaiTien]  WITH CHECK ADD  CONSTRAINT [FK_BanHang_LoaiTien_LoaiTien] FOREIGN KEY([IdLoaiTien])
REFERENCES [dbo].[LoaiTien] ([Id])
GO
ALTER TABLE [dbo].[BanHang_LoaiTien] CHECK CONSTRAINT [FK_BanHang_LoaiTien_LoaiTien]
GO
