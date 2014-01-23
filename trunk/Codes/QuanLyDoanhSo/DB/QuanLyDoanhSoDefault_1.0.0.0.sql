USE [master]
GO
/****** Object:  Database [QuanLyDoanhSo]    Script Date: 01/23/2014 16:01:03 ******/
CREATE DATABASE [QuanLyDoanhSo] ON  PRIMARY 
( NAME = N'QuanLyDoanhSo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\QuanLyDoanhSo.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'QuanLyDoanhSo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\QuanLyDoanhSo_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
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
/****** Object:  Table [dbo].[NguonCungCap]    Script Date: 01/23/2014 16:01:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NguonCungCap](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[DiaChi] [nvarchar](200) NULL,
	[DienThoai] [nvarchar](20) NULL,
	[DiDong] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[Fax] [nvarchar](20) NULL,
	[MaSoThue] [nvarchar](20) NULL,
	[GhiChu] [nvarchar](200) NULL,
	[DeleteFlag] [bit] NOT NULL,
 CONSTRAINT [PK_NguonCungCap] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenhGiaTien]    Script Date: 01/23/2014 16:01:04 ******/
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
/****** Object:  Table [dbo].[SanPham]    Script Date: 01/23/2014 16:01:04 ******/
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
SET IDENTITY_INSERT [dbo].[SanPham] OFF
/****** Object:  Table [dbo].[UserNhom]    Script Date: 01/23/2014 16:01:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserNhom](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[GhiChu] [nvarchar](200) NULL,
 CONSTRAINT [PK_NhomUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[UserNhom] ON
INSERT [dbo].[UserNhom] ([Id], [Ten], [GhiChu]) VALUES (1, N'Admin', NULL)
INSERT [dbo].[UserNhom] ([Id], [Ten], [GhiChu]) VALUES (2, N'User', NULL)
SET IDENTITY_INSERT [dbo].[UserNhom] OFF
/****** Object:  Table [dbo].[User]    Script Date: 01/23/2014 16:01:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUserNhom] [int] NOT NULL,
	[UserName] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[To] [int] NULL,
	[GioiTinh] [bit] NOT NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[DiaChi] [nvarchar](200) NULL,
	[Email] [nchar](10) NULL,
	[DienThoai] [nvarchar](20) NULL,
	[DiDong] [nvarchar](20) NULL,
	[CMND] [nvarchar](20) NULL,
	[NgaySinh] [nvarchar](20) NULL,
	[QueQuan] [nvarchar](50) NULL,
	[GhiChu] [nvarchar](50) NULL,
	[DeleteFlag] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([Id], [IdUserNhom], [UserName], [Password], [To], [GioiTinh], [Ten], [DiaChi], [Email], [DienThoai], [DiDong], [CMND], [NgaySinh], [QueQuan], [GhiChu], [DeleteFlag]) VALUES (1, 1, N'admin', N'21232F297A57A5A743894AE4A801FC345454433454539', NULL, 1, N'admin', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Table [dbo].[KhuyenMai]    Script Date: 01/23/2014 16:01:04 ******/
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
/****** Object:  Table [dbo].[NhapHang]    Script Date: 01/23/2014 16:01:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhapHang](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUser] [int] NOT NULL,
	[IdNguonCungCap] [int] NULL,
	[Ngay] [datetime] NOT NULL,
	[GhiChu] [nvarchar](200) NULL,
	[DeleteFlag] [bit] NOT NULL,
 CONSTRAINT [PK_NhapHang] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BanHang]    Script Date: 01/23/2014 16:01:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BanHang](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUser] [int] NULL,
	[IdSanPham] [int] NOT NULL,
	[IdSanPhamKhuyenMai] [int] NULL,
	[Ngay] [datetime] NOT NULL,
	[GhiChu] [nvarchar](200) NULL,
	[DeleteFlag] [bit] NOT NULL,
 CONSTRAINT [PK_BanHang] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhapHangChiTiet]    Script Date: 01/23/2014 16:01:04 ******/
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
/****** Object:  Table [dbo].[BanHangChiTiet]    Script Date: 01/23/2014 16:01:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BanHangChiTiet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdBanHang] [int] NOT NULL,
	[IdSanPham] [int] NOT NULL,
	[TonDau] [int] NOT NULL,
	[LuongNhan] [int] NOT NULL,
	[LuongBan] [int] NOT NULL,
	[TonCuoi] [int] NOT NULL,
	[IdSanPhamKhuyenMai] [int] NULL,
	[SoLuongKhuyenMai] [int] NULL,
	[Gia] [int] NOT NULL,
	[ThanhTien] [int] NOT NULL,
 CONSTRAINT [PK_BanHangChiTiet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BanHang_MenhGiaTien]    Script Date: 01/23/2014 16:01:04 ******/
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
/****** Object:  Default [DF_NguonCungCap_DeleteFlag]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[NguonCungCap] ADD  CONSTRAINT [DF_NguonCungCap_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
GO
/****** Object:  Default [DF_MenhGia_IsActived]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[MenhGiaTien] ADD  CONSTRAINT [DF_MenhGia_IsActived]  DEFAULT ((1)) FOR [IsActived]
GO
/****** Object:  Default [DF_NhomTien_DeleteFlag]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[MenhGiaTien] ADD  CONSTRAINT [DF_NhomTien_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
GO
/****** Object:  Default [DF_SanPham_Gia]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[SanPham] ADD  CONSTRAINT [DF_SanPham_Gia]  DEFAULT ((0)) FOR [Gia]
GO
/****** Object:  Default [DF_SanPham_IsActived]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[SanPham] ADD  CONSTRAINT [DF_SanPham_IsActived]  DEFAULT ((1)) FOR [IsActived]
GO
/****** Object:  Default [DF_SanPham_DeleteFlag]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[SanPham] ADD  CONSTRAINT [DF_SanPham_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
GO
/****** Object:  Default [DF_User_GioiTinh]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_GioiTinh]  DEFAULT ((1)) FOR [GioiTinh]
GO
/****** Object:  Default [DF_User_DeleteFlag_1]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_DeleteFlag_1]  DEFAULT ((0)) FOR [DeleteFlag]
GO
/****** Object:  Default [DF_KhuyenMai_DonViLamTron]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[KhuyenMai] ADD  CONSTRAINT [DF_KhuyenMai_DonViLamTron]  DEFAULT ((1)) FOR [DonViLamTron]
GO
/****** Object:  Default [DF_NhapHang_DeleteFlag]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[NhapHang] ADD  CONSTRAINT [DF_NhapHang_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
GO
/****** Object:  Default [DF_BanHang_DeleteFlag]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[BanHang] ADD  CONSTRAINT [DF_BanHang_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
GO
/****** Object:  Default [DF_Table_1_LuongNhan]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[NhapHangChiTiet] ADD  CONSTRAINT [DF_Table_1_LuongNhan]  DEFAULT ((0)) FOR [SoLuong]
GO
/****** Object:  Default [DF_NhapHangChiTiet_Gia]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[NhapHangChiTiet] ADD  CONSTRAINT [DF_NhapHangChiTiet_Gia]  DEFAULT ((0)) FOR [Gia]
GO
/****** Object:  Default [DF_NhapHangChiTiet_ThanhTien]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[NhapHangChiTiet] ADD  CONSTRAINT [DF_NhapHangChiTiet_ThanhTien]  DEFAULT ((0)) FOR [ThanhTien]
GO
/****** Object:  Default [DF_BanHangChiTiet_TonDau]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[BanHangChiTiet] ADD  CONSTRAINT [DF_BanHangChiTiet_TonDau]  DEFAULT ((0)) FOR [TonDau]
GO
/****** Object:  Default [DF_BanHangChiTiet_LuongNhan]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[BanHangChiTiet] ADD  CONSTRAINT [DF_BanHangChiTiet_LuongNhan]  DEFAULT ((0)) FOR [LuongNhan]
GO
/****** Object:  Default [DF_BanHangChiTiet_LuongBan]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[BanHangChiTiet] ADD  CONSTRAINT [DF_BanHangChiTiet_LuongBan]  DEFAULT ((0)) FOR [LuongBan]
GO
/****** Object:  Default [DF_BanHangChiTiet_TonCuoi]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[BanHangChiTiet] ADD  CONSTRAINT [DF_BanHangChiTiet_TonCuoi]  DEFAULT ((0)) FOR [TonCuoi]
GO
/****** Object:  Default [DF_BanHangChiTiet_Gia]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[BanHangChiTiet] ADD  CONSTRAINT [DF_BanHangChiTiet_Gia]  DEFAULT ((0)) FOR [Gia]
GO
/****** Object:  Default [DF_BanHangChiTiet_ThanhTien]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[BanHangChiTiet] ADD  CONSTRAINT [DF_BanHangChiTiet_ThanhTien]  DEFAULT ((0)) FOR [ThanhTien]
GO
/****** Object:  Default [DF_BanHang_MenhGiaTien_SoLuong]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[BanHang_MenhGiaTien] ADD  CONSTRAINT [DF_BanHang_MenhGiaTien_SoLuong]  DEFAULT ((0)) FOR [SoLuong]
GO
/****** Object:  Default [DF_BanHang_MenhGiaTien_ThanhTien]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[BanHang_MenhGiaTien] ADD  CONSTRAINT [DF_BanHang_MenhGiaTien_ThanhTien]  DEFAULT ((0)) FOR [ThanhTien]
GO
/****** Object:  ForeignKey [FK_User_UserNhom]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserNhom] FOREIGN KEY([IdUserNhom])
REFERENCES [dbo].[UserNhom] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserNhom]
GO
/****** Object:  ForeignKey [FK_KhuyenMai_SanPham]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[KhuyenMai]  WITH CHECK ADD  CONSTRAINT [FK_KhuyenMai_SanPham] FOREIGN KEY([IdSanPham])
REFERENCES [dbo].[SanPham] ([Id])
GO
ALTER TABLE [dbo].[KhuyenMai] CHECK CONSTRAINT [FK_KhuyenMai_SanPham]
GO
/****** Object:  ForeignKey [FK_KhuyenMai_SanPham1]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[KhuyenMai]  WITH CHECK ADD  CONSTRAINT [FK_KhuyenMai_SanPham1] FOREIGN KEY([IdSanPhamKhuyenMai])
REFERENCES [dbo].[SanPham] ([Id])
GO
ALTER TABLE [dbo].[KhuyenMai] CHECK CONSTRAINT [FK_KhuyenMai_SanPham1]
GO
/****** Object:  ForeignKey [FK_NhapHang_NguonCungCap]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[NhapHang]  WITH CHECK ADD  CONSTRAINT [FK_NhapHang_NguonCungCap] FOREIGN KEY([IdNguonCungCap])
REFERENCES [dbo].[NguonCungCap] ([Id])
GO
ALTER TABLE [dbo].[NhapHang] CHECK CONSTRAINT [FK_NhapHang_NguonCungCap]
GO
/****** Object:  ForeignKey [FK_NhapHang_User]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[NhapHang]  WITH CHECK ADD  CONSTRAINT [FK_NhapHang_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[NhapHang] CHECK CONSTRAINT [FK_NhapHang_User]
GO
/****** Object:  ForeignKey [FK_BanHang_SanPham]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[BanHang]  WITH CHECK ADD  CONSTRAINT [FK_BanHang_SanPham] FOREIGN KEY([IdSanPham])
REFERENCES [dbo].[SanPham] ([Id])
GO
ALTER TABLE [dbo].[BanHang] CHECK CONSTRAINT [FK_BanHang_SanPham]
GO
/****** Object:  ForeignKey [FK_BanHang_SanPham1]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[BanHang]  WITH CHECK ADD  CONSTRAINT [FK_BanHang_SanPham1] FOREIGN KEY([IdSanPhamKhuyenMai])
REFERENCES [dbo].[SanPham] ([Id])
GO
ALTER TABLE [dbo].[BanHang] CHECK CONSTRAINT [FK_BanHang_SanPham1]
GO
/****** Object:  ForeignKey [FK_BanHang_User]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[BanHang]  WITH CHECK ADD  CONSTRAINT [FK_BanHang_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[BanHang] CHECK CONSTRAINT [FK_BanHang_User]
GO
/****** Object:  ForeignKey [FK_NhapHangChiTiet_NhapHang]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[NhapHangChiTiet]  WITH CHECK ADD  CONSTRAINT [FK_NhapHangChiTiet_NhapHang] FOREIGN KEY([IdNhapHang])
REFERENCES [dbo].[NhapHang] ([Id])
GO
ALTER TABLE [dbo].[NhapHangChiTiet] CHECK CONSTRAINT [FK_NhapHangChiTiet_NhapHang]
GO
/****** Object:  ForeignKey [FK_NhapHangChiTiet_SanPham]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[NhapHangChiTiet]  WITH CHECK ADD  CONSTRAINT [FK_NhapHangChiTiet_SanPham] FOREIGN KEY([IdSanPham])
REFERENCES [dbo].[SanPham] ([Id])
GO
ALTER TABLE [dbo].[NhapHangChiTiet] CHECK CONSTRAINT [FK_NhapHangChiTiet_SanPham]
GO
/****** Object:  ForeignKey [FK_BanHangChiTiet_BanHang]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[BanHangChiTiet]  WITH CHECK ADD  CONSTRAINT [FK_BanHangChiTiet_BanHang] FOREIGN KEY([IdBanHang])
REFERENCES [dbo].[BanHang] ([Id])
GO
ALTER TABLE [dbo].[BanHangChiTiet] CHECK CONSTRAINT [FK_BanHangChiTiet_BanHang]
GO
/****** Object:  ForeignKey [FK_BanHangChiTiet_SanPham]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[BanHangChiTiet]  WITH CHECK ADD  CONSTRAINT [FK_BanHangChiTiet_SanPham] FOREIGN KEY([IdSanPham])
REFERENCES [dbo].[SanPham] ([Id])
GO
ALTER TABLE [dbo].[BanHangChiTiet] CHECK CONSTRAINT [FK_BanHangChiTiet_SanPham]
GO
/****** Object:  ForeignKey [FK_BanHangChiTiet_SanPham1]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[BanHangChiTiet]  WITH CHECK ADD  CONSTRAINT [FK_BanHangChiTiet_SanPham1] FOREIGN KEY([IdSanPhamKhuyenMai])
REFERENCES [dbo].[SanPham] ([Id])
GO
ALTER TABLE [dbo].[BanHangChiTiet] CHECK CONSTRAINT [FK_BanHangChiTiet_SanPham1]
GO
/****** Object:  ForeignKey [FK_BanHang_MenhGiaTien_BanHang]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[BanHang_MenhGiaTien]  WITH CHECK ADD  CONSTRAINT [FK_BanHang_MenhGiaTien_BanHang] FOREIGN KEY([IdBanHang])
REFERENCES [dbo].[BanHang] ([Id])
GO
ALTER TABLE [dbo].[BanHang_MenhGiaTien] CHECK CONSTRAINT [FK_BanHang_MenhGiaTien_BanHang]
GO
/****** Object:  ForeignKey [FK_BanHang_MenhGiaTien_MenhGiaTien]    Script Date: 01/23/2014 16:01:04 ******/
ALTER TABLE [dbo].[BanHang_MenhGiaTien]  WITH CHECK ADD  CONSTRAINT [FK_BanHang_MenhGiaTien_MenhGiaTien] FOREIGN KEY([IdMenhGiaTien])
REFERENCES [dbo].[MenhGiaTien] ([Id])
GO
ALTER TABLE [dbo].[BanHang_MenhGiaTien] CHECK CONSTRAINT [FK_BanHang_MenhGiaTien_MenhGiaTien]
GO
