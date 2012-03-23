USE [master]
GO
/****** Object:  Database [QuanLyKinhDoanh]    Script Date: 03/23/2012 18:00:19 ******/
CREATE DATABASE [QuanLyKinhDoanh] ON  PRIMARY 
( NAME = N'QuanLyKinhDoanh', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\QuanLyKinhDoanh.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'QuanLyKinhDoanh_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\QuanLyKinhDoanh_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [QuanLyKinhDoanh] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuanLyKinhDoanh].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuanLyKinhDoanh] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [QuanLyKinhDoanh] SET ANSI_NULLS OFF
GO
ALTER DATABASE [QuanLyKinhDoanh] SET ANSI_PADDING OFF
GO
ALTER DATABASE [QuanLyKinhDoanh] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [QuanLyKinhDoanh] SET ARITHABORT OFF
GO
ALTER DATABASE [QuanLyKinhDoanh] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [QuanLyKinhDoanh] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [QuanLyKinhDoanh] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [QuanLyKinhDoanh] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [QuanLyKinhDoanh] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [QuanLyKinhDoanh] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [QuanLyKinhDoanh] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [QuanLyKinhDoanh] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [QuanLyKinhDoanh] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [QuanLyKinhDoanh] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [QuanLyKinhDoanh] SET  DISABLE_BROKER
GO
ALTER DATABASE [QuanLyKinhDoanh] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [QuanLyKinhDoanh] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [QuanLyKinhDoanh] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [QuanLyKinhDoanh] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [QuanLyKinhDoanh] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [QuanLyKinhDoanh] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [QuanLyKinhDoanh] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [QuanLyKinhDoanh] SET  READ_WRITE
GO
ALTER DATABASE [QuanLyKinhDoanh] SET RECOVERY SIMPLE
GO
ALTER DATABASE [QuanLyKinhDoanh] SET  MULTI_USER
GO
ALTER DATABASE [QuanLyKinhDoanh] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [QuanLyKinhDoanh] SET DB_CHAINING OFF
GO
USE [QuanLyKinhDoanh]
GO
/****** Object:  Table [dbo].[UserGroup]    Script Date: 03/23/2012 18:00:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](20) NOT NULL,
	[Mota] [nvarchar](200) NULL,
 CONSTRAINT [PK_NhomNguoiDung] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoaDonType]    Script Date: 03/23/2012 18:00:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDonType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nchar](3) NOT NULL,
 CONSTRAINT [PK_NhomDonHang] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoaDonStatus]    Script Date: 03/23/2012 18:00:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDonStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_StatusDonHang] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanPhamGroup]    Script Date: 03/23/2012 18:00:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SanPhamGroup](
	[Id] [varchar](6) NOT NULL,
	[Ten] [nvarchar](20) NOT NULL,
	[Mota] [nvarchar](200) NULL,
 CONSTRAINT [PK_NhomSanPham] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 03/23/2012 18:00:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SanPham](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdSanPham] [nchar](15) NOT NULL,
	[IdGroup] [varchar](6) NOT NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[MoTa] [nvarchar](200) NULL,
	[GiaMua] [money] NOT NULL,
	[GiaBan] [money] NOT NULL,
	[LaiSuat] [float] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[DonViTinh] [nvarchar](10) NOT NULL,
	[XuatXu] [nvarchar](50) NULL,
	[Hieu] [nvarchar](50) NULL,
	[Size] [nvarchar](50) NULL,
	[ThoiGianBaoHanh] [tinyint] NULL,
	[DonViBaoHanh] [nvarchar](5) NULL,
	[GhiChu] [nvarchar](200) NULL,
	[CreateBy] [nvarchar](50) NOT NULL,
	[CreateDate] [date] NOT NULL,
	[UpdateBy] [nvarchar](50) NOT NULL,
	[UpdateDate] [date] NOT NULL,
	[DeleteFlag] [bit] NOT NULL,
 CONSTRAINT [PK_SanPham] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[KhachHangGroup]    Script Date: 03/23/2012 18:00:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHangGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](20) NOT NULL,
	[Diem] [int] NOT NULL,
	[Mota] [nvarchar](200) NULL,
 CONSTRAINT [PK_NhomKhachHang] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 03/23/2012 18:00:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHang](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdGroup] [int] NOT NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[GioiTinh] [nvarchar](3) NOT NULL,
	[DiaChi] [nvarchar](100) NULL,
	[DienThoai] [nvarchar](20) NULL,
	[Fax] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[Diem] [int] NULL,
	[GhiChu] [nvarchar](200) NULL,
	[CreateBy] [nvarchar](50) NOT NULL,
	[CreateDate] [date] NOT NULL,
	[UpdateBy] [nvarchar](50) NOT NULL,
	[UpdateDate] [date] NOT NULL,
	[DeleteFlag] [bit] NOT NULL,
 CONSTRAINT [PK_KhachHang] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 03/23/2012 18:00:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdGroup] [int] NOT NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[MatKhau] [nvarchar](20) NOT NULL,
	[CMND] [nvarchar](10) NULL,
	[GioiTinh] [nvarchar](3) NOT NULL,
	[DienThoai] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[GhiChu] [nvarchar](200) NULL,
	[CreateBy] [nvarchar](50) NOT NULL,
	[CreateDate] [date] NOT NULL,
	[UpdateBy] [nvarchar](50) NOT NULL,
	[UpdateDate] [date] NOT NULL,
	[DeleteFlag] [bit] NOT NULL,
 CONSTRAINT [PK_NguoiDung] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 03/23/2012 18:00:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDon](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdType] [int] NOT NULL,
	[IdStatus] [int] NOT NULL,
	[IdUser] [int] NOT NULL,
	[IdKhachHang] [int] NULL,
	[ThanhTien] [money] NOT NULL,
	[GhiChu] [nvarchar](200) NULL,
	[CreateBy] [nvarchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateBy] [nvarchar](50) NOT NULL,
	[UpdateDate] [date] NOT NULL,
	[DeleteFlag] [bit] NOT NULL,
 CONSTRAINT [PK_DonHang] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoaDonDetail]    Script Date: 03/23/2012 18:00:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDonDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdHoaDon] [int] NOT NULL,
	[IdSanPham] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[ThanhTien] [money] NOT NULL,
 CONSTRAINT [PK_DonHangDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_SanPham_SoLuong]    Script Date: 03/23/2012 18:00:21 ******/
ALTER TABLE [dbo].[SanPham] ADD  CONSTRAINT [DF_SanPham_SoLuong]  DEFAULT ((0)) FOR [SoLuong]
GO
/****** Object:  Default [DF_KhachHang_GioiTinh]    Script Date: 03/23/2012 18:00:21 ******/
ALTER TABLE [dbo].[KhachHang] ADD  CONSTRAINT [DF_KhachHang_GioiTinh]  DEFAULT (N'Nam') FOR [GioiTinh]
GO
/****** Object:  Default [DF_KhachHang_Diem]    Script Date: 03/23/2012 18:00:21 ******/
ALTER TABLE [dbo].[KhachHang] ADD  CONSTRAINT [DF_KhachHang_Diem]  DEFAULT ((0)) FOR [Diem]
GO
/****** Object:  Default [DF_DonHang_ThanhTien]    Script Date: 03/23/2012 18:00:21 ******/
ALTER TABLE [dbo].[HoaDon] ADD  CONSTRAINT [DF_DonHang_ThanhTien]  DEFAULT ((0)) FOR [ThanhTien]
GO
/****** Object:  Default [DF_DonHangDetail_ThanhTien]    Script Date: 03/23/2012 18:00:21 ******/
ALTER TABLE [dbo].[HoaDonDetail] ADD  CONSTRAINT [DF_DonHangDetail_ThanhTien]  DEFAULT ((0)) FOR [ThanhTien]
GO
/****** Object:  ForeignKey [FK_KhachHang_NhomKhachHang]    Script Date: 03/23/2012 18:00:21 ******/
ALTER TABLE [dbo].[KhachHang]  WITH CHECK ADD  CONSTRAINT [FK_KhachHang_NhomKhachHang] FOREIGN KEY([IdGroup])
REFERENCES [dbo].[KhachHangGroup] ([Id])
GO
ALTER TABLE [dbo].[KhachHang] CHECK CONSTRAINT [FK_KhachHang_NhomKhachHang]
GO
/****** Object:  ForeignKey [FK_User_UserGroup]    Script Date: 03/23/2012 18:00:21 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserGroup] FOREIGN KEY([IdGroup])
REFERENCES [dbo].[UserGroup] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserGroup]
GO
/****** Object:  ForeignKey [FK_HoaDon_HoaDonGroup]    Script Date: 03/23/2012 18:00:21 ******/
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_HoaDonGroup] FOREIGN KEY([IdType])
REFERENCES [dbo].[HoaDonType] ([Id])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_HoaDonGroup]
GO
/****** Object:  ForeignKey [FK_HoaDon_HoaDonStatus]    Script Date: 03/23/2012 18:00:21 ******/
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_HoaDonStatus] FOREIGN KEY([IdStatus])
REFERENCES [dbo].[HoaDonStatus] ([Id])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_HoaDonStatus]
GO
/****** Object:  ForeignKey [FK_HoaDon_KhachHang]    Script Date: 03/23/2012 18:00:21 ******/
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_KhachHang] FOREIGN KEY([IdKhachHang])
REFERENCES [dbo].[KhachHang] ([Id])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_KhachHang]
GO
/****** Object:  ForeignKey [FK_HoaDon_User]    Script Date: 03/23/2012 18:00:21 ******/
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_User]
GO
/****** Object:  ForeignKey [FK_HoaDonDetail_HoaDon]    Script Date: 03/23/2012 18:00:21 ******/
ALTER TABLE [dbo].[HoaDonDetail]  WITH CHECK ADD  CONSTRAINT [FK_HoaDonDetail_HoaDon] FOREIGN KEY([IdHoaDon])
REFERENCES [dbo].[HoaDon] ([Id])
GO
ALTER TABLE [dbo].[HoaDonDetail] CHECK CONSTRAINT [FK_HoaDonDetail_HoaDon]
GO
/****** Object:  ForeignKey [FK_HoaDonDetail_SanPham]    Script Date: 03/23/2012 18:00:21 ******/
ALTER TABLE [dbo].[HoaDonDetail]  WITH CHECK ADD  CONSTRAINT [FK_HoaDonDetail_SanPham] FOREIGN KEY([IdSanPham])
REFERENCES [dbo].[SanPham] ([Id])
GO
ALTER TABLE [dbo].[HoaDonDetail] CHECK CONSTRAINT [FK_HoaDonDetail_SanPham]
GO
