USE [QuanLyKinhDoanh]
GO
/****** Object:  Table [dbo].[HoaDonType]    Script Date: 05/14/2012 22:52:53 ******/
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
SET IDENTITY_INSERT [dbo].[HoaDonType] ON
INSERT [dbo].[HoaDonType] ([Id], [Ten]) VALUES (1, N'Mua')
INSERT [dbo].[HoaDonType] ([Id], [Ten]) VALUES (2, N'Bán')
INSERT [dbo].[HoaDonType] ([Id], [Ten]) VALUES (3, N'Thu')
INSERT [dbo].[HoaDonType] ([Id], [Ten]) VALUES (4, N'Chi')
SET IDENTITY_INSERT [dbo].[HoaDonType] OFF
/****** Object:  Table [dbo].[HoaDonStatus]    Script Date: 05/14/2012 22:52:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDonStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](20) NULL,
 CONSTRAINT [PK_HoaDonStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[HoaDonStatus] ON
INSERT [dbo].[HoaDonStatus] ([Id], [Ten]) VALUES (1, N'Trả hết')
INSERT [dbo].[HoaDonStatus] ([Id], [Ten]) VALUES (2, N'Nợ')
SET IDENTITY_INSERT [dbo].[HoaDonStatus] OFF
/****** Object:  Table [dbo].[KhachHangGroup]    Script Date: 05/14/2012 22:52:53 ******/
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
	[Diem] [int] NOT NULL,
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
INSERT [dbo].[KhachHangGroup] ([Id], [Ma], [Ten], [Diem], [MoTa]) VALUES (1, N'KT', N'Khách thường', 0, NULL)
INSERT [dbo].[KhachHangGroup] ([Id], [Ma], [Ten], [Diem], [MoTa]) VALUES (2, N'KHTT', N'Khách hàng thân thiết', 0, NULL)
INSERT [dbo].[KhachHangGroup] ([Id], [Ma], [Ten], [Diem], [MoTa]) VALUES (3, N'KHND', N'Khách hàng Ngọc Đăng', 0, NULL)
INSERT [dbo].[KhachHangGroup] ([Id], [Ma], [Ten], [Diem], [MoTa]) VALUES (4, N'KS', N'Khách sĩ', 0, NULL)
INSERT [dbo].[KhachHangGroup] ([Id], [Ma], [Ten], [Diem], [MoTa]) VALUES (5, N'VIP', N'VIP', 0, NULL)
SET IDENTITY_INSERT [dbo].[KhachHangGroup] OFF
/****** Object:  Table [dbo].[SanPhamGroup]    Script Date: 05/14/2012 22:52:53 ******/
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
	[Mota] [nvarchar](200) NULL,
 CONSTRAINT [PK_SanPhamGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[SanPhamGroup] ON
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (5, N'GTTNa', N'Giày Thể Thao Nam', N'Giày Nam dáng thể thao cột dây - dán - sỏ')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (6, N'GTTNu', N'Giày Thể Thao Nữ', N'Giày Nữ dáng thể thao cột dây - dán - sỏ')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (7, N'GTNa', N'Giày Tây Nam', N'Giày Tây cột dây - dán - sỏ  ')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (8, N'GSDNa', N'Giày Sandal nam', N'giày dây')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (9, N'GSDNu', N'Giày Sandal Nữ ', N'giày dây thấp - cao ')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (10, N'DDNa', N'Dép Da Nam', N'Dép quai ngan nam')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (11, N'DDNu', N'Dép Da Nữ', N'Dép da nữ xẹp - cao ')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (12, N'DKNa', N'Dép kẹp Nam', N'Dép kẹp nam da - mousse thường ')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (13, N'DKNu', N'Dép kẹp nữ', N'Dép kẹp nữ da - mousse thường')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (14, N'DSPNa', N'Dép Sapo Nam', N'Dép bít mũi da thường')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (15, N'DSPNu', N'Dép Sapo nữ', N'Dép bít mũi nữ da - thường')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (16, N'VADcf', N'Vải áo dài chi fong', N'vải áo khổ thường 1m6 x 2m')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (17, N'MBH', N'Mủ bảo hiểm', N'Mủ bảo hiểm xe máy các loại ')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (18, N'VADTh', N'Vải Áo Dài Thường', N'vải áo dài dệt in hình hoa văn - bông -bi -....')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (19, N'DThom', N'Dầu thơm ', N'')
SET IDENTITY_INSERT [dbo].[SanPhamGroup] OFF
/****** Object:  Table [dbo].[XuatXu]    Script Date: 05/14/2012 22:52:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[XuatXu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[DiaChi] [nvarchar](100) NULL,
	[DienThoai] [nvarchar](20) NULL,
	[Fax] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[GhiChu] [nvarchar](200) NULL,
	[CreateBy] [nvarchar](50) NOT NULL,
	[CreateDate] [date] NOT NULL,
	[UpdateBy] [nvarchar](50) NOT NULL,
	[UpdateDate] [date] NOT NULL,
	[DeleteFlag] [bit] NOT NULL,
 CONSTRAINT [PK_XuatXu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[XuatXu] ON
INSERT [dbo].[XuatXu] ([Id], [Ten], [DiaChi], [DienThoai], [Fax], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (1, N'Test Xuat xu', N'aaaa', N'bbbb', N'cccc', N'dddd', N'eee', N'', CAST(0x99350B00 AS Date), N'', CAST(0x99350B00 AS Date), 0)
SET IDENTITY_INSERT [dbo].[XuatXu] OFF
/****** Object:  Table [dbo].[UserGroup]    Script Date: 05/14/2012 22:52:53 ******/
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
SET IDENTITY_INSERT [dbo].[UserGroup] ON
INSERT [dbo].[UserGroup] ([Id], [Ten], [Mota]) VALUES (1, N'Admin', NULL)
INSERT [dbo].[UserGroup] ([Id], [Ten], [Mota]) VALUES (2, N'Nhân viên', NULL)
SET IDENTITY_INSERT [dbo].[UserGroup] OFF
/****** Object:  Table [dbo].[User]    Script Date: 05/14/2012 22:52:53 ******/
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
SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (1, 1, N'Lê Đăng Khanh', N'ldkhanh', N'E1ADC3949BA59ABBE56E057F2F883E45454433454539', N'', N'Nam', N'', N'', N'', N'', CAST(0x81350B00 AS Date), N'', CAST(0x81350B00 AS Date), 0)
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Table [dbo].[SanPham]    Script Date: 05/14/2012 22:52:53 ******/
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
	[IdXuatXu] [int] NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[MoTa] [nvarchar](200) NULL,
	[GiaMua] [bigint] NOT NULL,
	[GiaBan] [bigint] NOT NULL,
	[LaiSuat] [float] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[DonViTinh] [nvarchar](10) NOT NULL,
	[Hieu] [nvarchar](50) NULL,
	[Size] [nvarchar](50) NULL,
	[ThoiHan] [tinyint] NULL,
	[DonViThoiHan] [nvarchar](5) NULL,
	[ThoiGianBaoHanh] [tinyint] NULL,
	[DonViBaoHanh] [nvarchar](5) NULL,
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
SET IDENTITY_INSERT [dbo].[SanPham] ON
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (30, N'DThom0001', 19, NULL, N'Dầu thơm 1', N'', 10000, 15000, 50, 3, N'Đôi', N'', N'', 0, N'Ngày', NULL, NULL, N'', CAST(0xAA350B00 AS Date), N'', CAST(0xAA350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (31, N'DThom0002', 19, NULL, N'Dầu thơm 2', N'', 15000, 18000, 20, 4, N'Đôi', N'', N'', 0, N'Ngày', NULL, NULL, N'', CAST(0xAA350B00 AS Date), N'', CAST(0xAA350B00 AS Date), 0)
SET IDENTITY_INSERT [dbo].[SanPham] OFF
/****** Object:  Table [dbo].[KhachHang]    Script Date: 05/14/2012 22:52:53 ******/
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
	[CMND] [nchar](10) NULL,
	[NgayCap] [date] NULL,
	[NoiCap] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](100) NULL,
	[DienThoai] [nvarchar](20) NULL,
	[Fax] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[TichLuy] [bigint] NULL,
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
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[KhachHang] ON
INSERT [dbo].[KhachHang] ([Id], [MaKhachHang], [IdGroup], [Ten], [GioiTinh], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [Fax], [Email], [TichLuy], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (1, N'KHTT0001', 2, N'Tuc', N'Nam', CAST(0x4B350B00 AS Date), NULL, NULL, NULL, N'', N'', N'', N'', 100000, N'', N'', CAST(0x9E350B00 AS Date), N'', CAST(0x9E350B00 AS Date), 0)
INSERT [dbo].[KhachHang] ([Id], [MaKhachHang], [IdGroup], [Ten], [GioiTinh], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [Fax], [Email], [TichLuy], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (2, N'VIP0001', 5, N'VIP 123', N'Nam', CAST(0x07240B00 AS Date), N'          ', CAST(0x07240B00 AS Date), N'', N'', N'', N'', N'', 0, N'', N'', CAST(0xAA350B00 AS Date), N'', CAST(0xAA350B00 AS Date), 0)
SET IDENTITY_INSERT [dbo].[KhachHang] OFF
/****** Object:  Table [dbo].[HoaDon]    Script Date: 05/14/2012 22:52:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HoaDon](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MaHoaDon] [char](6) NOT NULL,
	[IdType] [int] NOT NULL,
	[IdUser] [int] NULL,
	[IdKhachHang] [int] NULL,
	[IdStatus] [int] NOT NULL,
	[ThanhTien] [bigint] NOT NULL,
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
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[HoaDon] ON
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (29, N'M00001', 1, NULL, NULL, 1, 30000, N'', N'', CAST(0x0000A04F001F5891 AS DateTime), N'', CAST(0xAA350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (30, N'M00030', 1, NULL, NULL, 1, 60000, N'', N'', CAST(0x0000A04F001F71E6 AS DateTime), N'', CAST(0xAA350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (31, N'B00001', 2, NULL, NULL, 1, 15000, N'', N'', CAST(0x0000A05101427787 AS DateTime), N'', CAST(0xAC350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (33, N'B00002', 2, NULL, 1, 1, 15000, N'', N'', CAST(0x0000A051015C3BAC AS DateTime), N'', CAST(0xAC350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (34, N'B00003', 2, NULL, NULL, 1, 15000, N'', N'', CAST(0x0000A051015D59DF AS DateTime), N'', CAST(0xAC350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (35, N'B00004', 2, NULL, NULL, 1, 15000, N'', N'', CAST(0x0000A051015D9F30 AS DateTime), N'', CAST(0xAC350B00 AS Date), 0)
SET IDENTITY_INSERT [dbo].[HoaDon] OFF
/****** Object:  Table [dbo].[ChietKhau]    Script Date: 05/14/2012 22:52:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChietKhau](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdSanPham] [int] NOT NULL,
	[IdKhachHangGroup] [int] NULL,
	[Value] [int] NOT NULL,
	[CreateBy] [nvarchar](50) NOT NULL,
	[CreateDate] [date] NOT NULL,
	[UpdateBy] [nvarchar](50) NOT NULL,
	[UpdateDate] [date] NOT NULL,
	[DeleteFlag] [bit] NOT NULL,
 CONSTRAINT [PK_Discount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ChietKhau] ON
INSERT [dbo].[ChietKhau] ([Id], [IdSanPham], [IdKhachHangGroup], [Value], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (1, 30, NULL, 0, N'', CAST(0xAA350B00 AS Date), N'', CAST(0xAA350B00 AS Date), 0)
INSERT [dbo].[ChietKhau] ([Id], [IdSanPham], [IdKhachHangGroup], [Value], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (2, 31, NULL, 0, N'', CAST(0xAA350B00 AS Date), N'', CAST(0xAA350B00 AS Date), 0)
SET IDENTITY_INSERT [dbo].[ChietKhau] OFF
/****** Object:  Table [dbo].[HoaDonDetail]    Script Date: 05/14/2012 22:52:53 ******/
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
	[ChietKhau] [int] NOT NULL,
	[ThanhTien] [bigint] NOT NULL,
 CONSTRAINT [PK_DonHangDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[HoaDonDetail] ON
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (31, 29, 30, 0, 3, 0, 30000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (32, 30, 31, 0, 4, 0, 60000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (33, 31, 30, 0, 1, 0, 15000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (34, 33, 30, 15000, 1, 0, 15000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (35, 35, 30, 15000, 1, 16, 15000)
SET IDENTITY_INSERT [dbo].[HoaDonDetail] OFF
/****** Object:  Default [DF_HoaDon_IdStatus]    Script Date: 05/14/2012 22:52:53 ******/
ALTER TABLE [dbo].[HoaDon] ADD  CONSTRAINT [DF_HoaDon_IdStatus]  DEFAULT ((1)) FOR [IdStatus]
GO
/****** Object:  Default [DF_DonHang_ThanhTien]    Script Date: 05/14/2012 22:52:53 ******/
ALTER TABLE [dbo].[HoaDon] ADD  CONSTRAINT [DF_DonHang_ThanhTien]  DEFAULT ((0)) FOR [ThanhTien]
GO
/****** Object:  Default [DF_HoaDonDetail_SoLuong]    Script Date: 05/14/2012 22:52:53 ******/
ALTER TABLE [dbo].[HoaDonDetail] ADD  CONSTRAINT [DF_HoaDonDetail_SoLuong]  DEFAULT ((1)) FOR [SoLuong]
GO
/****** Object:  Default [DF_HoaDonDetail_ChietKhau]    Script Date: 05/14/2012 22:52:53 ******/
ALTER TABLE [dbo].[HoaDonDetail] ADD  CONSTRAINT [DF_HoaDonDetail_ChietKhau]  DEFAULT ((0)) FOR [ChietKhau]
GO
/****** Object:  Default [DF_DonHangDetail_ThanhTien]    Script Date: 05/14/2012 22:52:53 ******/
ALTER TABLE [dbo].[HoaDonDetail] ADD  CONSTRAINT [DF_DonHangDetail_ThanhTien]  DEFAULT ((0)) FOR [ThanhTien]
GO
/****** Object:  Default [DF_KhachHang_GioiTinh]    Script Date: 05/14/2012 22:52:53 ******/
ALTER TABLE [dbo].[KhachHang] ADD  CONSTRAINT [DF_KhachHang_GioiTinh]  DEFAULT (N'Nam') FOR [GioiTinh]
GO
/****** Object:  Default [DF_KhachHang_Diem]    Script Date: 05/14/2012 22:52:53 ******/
ALTER TABLE [dbo].[KhachHang] ADD  CONSTRAINT [DF_KhachHang_Diem]  DEFAULT ((0)) FOR [TichLuy]
GO
/****** Object:  Default [DF_SanPham_LaiSuat]    Script Date: 05/14/2012 22:52:53 ******/
ALTER TABLE [dbo].[SanPham] ADD  CONSTRAINT [DF_SanPham_LaiSuat]  DEFAULT ((0)) FOR [LaiSuat]
GO
/****** Object:  Default [DF_SanPham_SoLuong]    Script Date: 05/14/2012 22:52:53 ******/
ALTER TABLE [dbo].[SanPham] ADD  CONSTRAINT [DF_SanPham_SoLuong]  DEFAULT ((0)) FOR [SoLuong]
GO
/****** Object:  ForeignKey [FK_ChietKhau_KhachHangGroup]    Script Date: 05/14/2012 22:52:53 ******/
ALTER TABLE [dbo].[ChietKhau]  WITH CHECK ADD  CONSTRAINT [FK_ChietKhau_KhachHangGroup] FOREIGN KEY([IdKhachHangGroup])
REFERENCES [dbo].[KhachHangGroup] ([Id])
GO
ALTER TABLE [dbo].[ChietKhau] CHECK CONSTRAINT [FK_ChietKhau_KhachHangGroup]
GO
/****** Object:  ForeignKey [FK_ChietKhau_SanPham]    Script Date: 05/14/2012 22:52:53 ******/
ALTER TABLE [dbo].[ChietKhau]  WITH CHECK ADD  CONSTRAINT [FK_ChietKhau_SanPham] FOREIGN KEY([IdSanPham])
REFERENCES [dbo].[SanPham] ([Id])
GO
ALTER TABLE [dbo].[ChietKhau] CHECK CONSTRAINT [FK_ChietKhau_SanPham]
GO
/****** Object:  ForeignKey [FK_HoaDon_HoaDonGroup]    Script Date: 05/14/2012 22:52:53 ******/
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_HoaDonGroup] FOREIGN KEY([IdType])
REFERENCES [dbo].[HoaDonType] ([Id])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_HoaDonGroup]
GO
/****** Object:  ForeignKey [FK_HoaDon_HoaDonStatus]    Script Date: 05/14/2012 22:52:53 ******/
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_HoaDonStatus] FOREIGN KEY([IdStatus])
REFERENCES [dbo].[HoaDonStatus] ([Id])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_HoaDonStatus]
GO
/****** Object:  ForeignKey [FK_HoaDon_KhachHang]    Script Date: 05/14/2012 22:52:53 ******/
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_KhachHang] FOREIGN KEY([IdKhachHang])
REFERENCES [dbo].[KhachHang] ([Id])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_KhachHang]
GO
/****** Object:  ForeignKey [FK_HoaDon_User]    Script Date: 05/14/2012 22:52:53 ******/
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_User]
GO
/****** Object:  ForeignKey [FK_HoaDonDetail_HoaDon]    Script Date: 05/14/2012 22:52:53 ******/
ALTER TABLE [dbo].[HoaDonDetail]  WITH CHECK ADD  CONSTRAINT [FK_HoaDonDetail_HoaDon] FOREIGN KEY([IdHoaDon])
REFERENCES [dbo].[HoaDon] ([Id])
GO
ALTER TABLE [dbo].[HoaDonDetail] CHECK CONSTRAINT [FK_HoaDonDetail_HoaDon]
GO
/****** Object:  ForeignKey [FK_HoaDonDetail_SanPham]    Script Date: 05/14/2012 22:52:53 ******/
ALTER TABLE [dbo].[HoaDonDetail]  WITH CHECK ADD  CONSTRAINT [FK_HoaDonDetail_SanPham] FOREIGN KEY([IdSanPham])
REFERENCES [dbo].[SanPham] ([Id])
GO
ALTER TABLE [dbo].[HoaDonDetail] CHECK CONSTRAINT [FK_HoaDonDetail_SanPham]
GO
/****** Object:  ForeignKey [FK_KhachHang_KhachHangGroup]    Script Date: 05/14/2012 22:52:53 ******/
ALTER TABLE [dbo].[KhachHang]  WITH CHECK ADD  CONSTRAINT [FK_KhachHang_KhachHangGroup] FOREIGN KEY([IdGroup])
REFERENCES [dbo].[KhachHangGroup] ([Id])
GO
ALTER TABLE [dbo].[KhachHang] CHECK CONSTRAINT [FK_KhachHang_KhachHangGroup]
GO
/****** Object:  ForeignKey [FK_SanPham_SanPhamGroup]    Script Date: 05/14/2012 22:52:53 ******/
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_SanPhamGroup] FOREIGN KEY([IdGroup])
REFERENCES [dbo].[SanPhamGroup] ([Id])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_SanPhamGroup]
GO
/****** Object:  ForeignKey [FK_SanPham_XuatXu]    Script Date: 05/14/2012 22:52:53 ******/
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_XuatXu] FOREIGN KEY([IdXuatXu])
REFERENCES [dbo].[XuatXu] ([Id])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_XuatXu]
GO
/****** Object:  ForeignKey [FK_User_UserGroup]    Script Date: 05/14/2012 22:52:53 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserGroup] FOREIGN KEY([IdGroup])
REFERENCES [dbo].[UserGroup] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserGroup]
GO
