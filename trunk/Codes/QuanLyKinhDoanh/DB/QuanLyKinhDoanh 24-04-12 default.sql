USE [QuanLyKinhDoanh]
GO
/****** Object:  Table [dbo].[HoaDonType]    Script Date: 04/24/2012 21:57:38 ******/
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
/****** Object:  Table [dbo].[HoaDonStatus]    Script Date: 04/24/2012 21:57:38 ******/
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
INSERT [dbo].[HoaDonStatus] ([Id], [Ten]) VALUES (1, N'Hết')
INSERT [dbo].[HoaDonStatus] ([Id], [Ten]) VALUES (2, N'Nợ')
SET IDENTITY_INSERT [dbo].[HoaDonStatus] OFF
/****** Object:  Table [dbo].[KhachHangGroup]    Script Date: 04/24/2012 21:57:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHangGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[Diem] [int] NOT NULL,
	[MoTa] [nvarchar](200) NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[KhachHangGroup] ON
INSERT [dbo].[KhachHangGroup] ([Id], [Ten], [Diem], [MoTa]) VALUES (1, N'Khách thường', 0, NULL)
INSERT [dbo].[KhachHangGroup] ([Id], [Ten], [Diem], [MoTa]) VALUES (2, N'Khách hàng thân thiết', 0, NULL)
INSERT [dbo].[KhachHangGroup] ([Id], [Ten], [Diem], [MoTa]) VALUES (3, N'VIP', 0, NULL)
INSERT [dbo].[KhachHangGroup] ([Id], [Ten], [Diem], [MoTa]) VALUES (4, N'Khách sĩ', 0, NULL)
SET IDENTITY_INSERT [dbo].[KhachHangGroup] OFF
/****** Object:  Table [dbo].[SanPhamGroup]    Script Date: 04/24/2012 21:57:38 ******/
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
/****** Object:  Table [dbo].[XuatXu]    Script Date: 04/24/2012 21:57:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[XuatXu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](100) NULL,
	[DienThoai] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[GhiChu] [nvarchar](200) NULL,
 CONSTRAINT [PK_XuatXu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserGroup]    Script Date: 04/24/2012 21:57:38 ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 04/24/2012 21:57:38 ******/
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
/****** Object:  Table [dbo].[SanPham]    Script Date: 04/24/2012 21:57:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdSanPham] [nvarchar](10) NOT NULL,
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
	[DonViThoiHan] [nvarchar](10) NULL,
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
SET IDENTITY_INSERT [dbo].[SanPham] ON
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (5, N'GNaTT0001', 5, NULL, N'Giày TT Moschino', N'', 0, 0, 0, 0, N'ĐÔI', N'Mochino', N'44', NULL, NULL, 1, N'Năm', N'', CAST(0x85350B00 AS Date), N'', CAST(0x85350B00 AS Date), 1)
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (6, N'GTTNa0006', 5, NULL, N'Giày Mochino', N'', 200000, 260000, 130, 7, N'ĐÔI', N'', N'44', NULL, NULL, 1, N'Năm', N'', CAST(0x85350B00 AS Date), N'', CAST(0x85350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (7, N'GTTNa0007', 5, NULL, N'giày mochino the thao', N'', 0, 0, 0, 0, N'ĐÔI', N'', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x85350B00 AS Date), N'', CAST(0x85350B00 AS Date), 1)
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (8, N'GTTNu0008', 6, NULL, N'Giày thể thao cao cổ nữ', N'giày thê thao cao cổ nữ cột dây màu hông', 190000, 265000, 39, 3, N'ĐÔI', N'', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8B350B00 AS Date), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (9, N'GTTNu0009', 6, NULL, N'Giày thể thao cao cổ', N'Giày thể thao cao cổ cột dây màu vàng', 190000, 265000, 39, 1, N'ĐÔI', N'', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8B350B00 AS Date), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (10, N'GTNa0002', 7, NULL, N'Giầy tây sỏ màu đỏ', N'giày tây sỏ màu đỏ đô', 190000, 270000, 42, 1, N'ĐÔI', N'', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8B350B00 AS Date), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (11, N'GTNa0003', 7, NULL, N'Giày tây sỏ', N'giày tây sỏ màu đỏ đô có hình cá sấu', 215000, 290000, 35, 1, N'ĐÔI', N'', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8B350B00 AS Date), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (12, N'GTTNa0008', 5, NULL, N'Giày thể thao dán', N'giày thể thao màu bò dán có dây kéo ngang có chữ M', 200000, 260000, 30, 1, N'ĐÔI', N'', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8B350B00 AS Date), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (13, N'GTTNa0009', 5, NULL, N'Giày thể thao dán', N'giày thể thao màu nâu dán có chữ Mr.LOVE', 173076, 225000, 30, 1, N'ĐÔI', N'', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8B350B00 AS Date), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (14, N'GTTNa0010', 5, NULL, N'Giày thể thao sỏ', N'giày thể thao màu xám dán có chữ A', 173076, 225000, 30, 2, N'ĐÔI', N'', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8B350B00 AS Date), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (15, N'GTTNa0011', 5, NULL, N'Giày thể thao sỏ', N'giày dáng thể thao hiệu Moschino màu trắng đen', 276923, 360000, 30, 2, N'ĐÔI', N'moschino', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8B350B00 AS Date), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (16, N'GTTNa0016', 5, NULL, N'Giày thể thao sỏ', N'giày dáng thể thao hiệu Moschino màu đen trăng', 276923, 360000, 30, 2, N'ĐÔI', N'moschino', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8B350B00 AS Date), N'', CAST(0x90350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (17, N'MBH0001', 17, NULL, N'Nón Kiếng Andes', N'nón kiếng andes - kiếng trong đỏ đen Mã hàng 108MK', 214814, 290000, 35, 2, N'CÁI', N'Andes', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8E350B00 AS Date), N'', CAST(0x8E350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (18, N'MBH0002', 17, NULL, N'Nón Kiếng Asia', N'nón kiếng á châu giấu kiếng màu đỏ', 133333, 180000, 35, 1, N'CÁI', N'Asia', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8E350B00 AS Date), N'', CAST(0x8E350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (19, N'MBH0003', 17, NULL, N'Nón Kiếng Đức Huy', N'nón kiếng Đức Huy giấu kiếng màu Trắng', 200000, 270000, 35, 1, N'CÁI', N'Đức Huy', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8E350B00 AS Date), N'', CAST(0x8E350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (20, N'MBH0004', 17, NULL, N'Nón Kiếng ASIA', N'nón kiếng bé Asia màu xanh kiếng trắng', 66, 89, 35, 1, N'CÁI', N'Asia', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8E350B00 AS Date), N'', CAST(0x92350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (21, N'MBH0005', 17, NULL, N'Nữa đầu Andes', N'nón nữa đầu  kết rời đen trắng mã hàng 110/110L', 151851, 205000, 35, 2, N'CÁI', N'Andes', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8E350B00 AS Date), N'', CAST(0x8E350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (22, N'MBH0006', 17, NULL, N'Nữa đầu Andes', N'nón nữa đầu  kết rời đen  mã hàng 108', 159259, 215000, 35, 1, N'CÁI', N'Andes', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8E350B00 AS Date), N'', CAST(0x8E350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (23, N'VADcf0001', 16, NULL, N'Áo dài chifong', N'vải áo dài dệt in hình sẵn bông - hoa văn - bi ....', 130769, 170000, 30, 40, N'BỘ', N'', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x91350B00 AS Date), N'', CAST(0x91350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (24, N'VADcf0002', 16, NULL, N'Áo dài thường', N'vải áo dài dệt in hình sẵn bông - hoa văn - bi ....', 100000, 130000, 30, 5, N'BỘ', N'', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x91350B00 AS Date), N'', CAST(0x91350B00 AS Date), 1)
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (25, N'ADTh0001', 18, NULL, N'Vải thường', N'vải thường dệt in hình hoa văn - bông -bi ...', 100000, 130000, 30, 5, N'BỘ', N'', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x91350B00 AS Date), N'', CAST(0x91350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (26, N'VADTh0002', 18, NULL, N'Vải áo dài thường', N'vải áo dài dệt in hình hoa văn - bông - bi ...', 100000, 130000, 30, 5, N'BỘ', N'', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x92350B00 AS Date), N'', CAST(0x92350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (27, N'DThom0001', 19, NULL, N'dầu thơm MD xanh', N'dầu thơm thường hiệu MD cty Quang Duc', 20000, 25000, 25, 7, N'CÁI', N'MD', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x96350B00 AS Date), N'', CAST(0x96350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (28, N'DThom0002', 19, NULL, N'dầu thơm MD tím', N'dầu thơm thường hiệu MD cty Quang Duc', 20000, 25000, 25, 2, N'CÁI', N'MD', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x96350B00 AS Date), N'', CAST(0x96350B00 AS Date), 0)
SET IDENTITY_INSERT [dbo].[SanPham] OFF
/****** Object:  Table [dbo].[KhachHang]    Script Date: 04/24/2012 21:57:38 ******/
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
/****** Object:  Table [dbo].[HoaDon]    Script Date: 04/24/2012 21:57:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HoaDon](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdHoaDon] [char](6) NOT NULL,
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
INSERT [dbo].[HoaDon] ([Id], [IdHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (4, N'M00001', 1, NULL, NULL, 1, 560000, N'', N'', CAST(0x0000A02A0141F944 AS DateTime), N'', CAST(0x85350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [IdHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (5, N'M00005', 1, NULL, NULL, 1, 1000000, N'', N'', CAST(0x0000A02A0149BB58 AS DateTime), N'', CAST(0x85350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [IdHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (6, N'M00005', 1, NULL, NULL, 1, 1000000, N'', N'', CAST(0x0000A02A0149F561 AS DateTime), N'', CAST(0x85350B00 AS Date), 1)
INSERT [dbo].[HoaDon] ([Id], [IdHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (7, N'M00006', 1, NULL, NULL, 1, 570000, N'size 35 - 37 - 38', N'', CAST(0x0000A03000A42CF4 AS DateTime), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [IdHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (8, N'M00007', 1, NULL, NULL, 1, 190000, N'còn size 37', N'', CAST(0x0000A03000C68087 AS DateTime), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [IdHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (9, N'M00008', 1, NULL, NULL, 1, 190000, N'', N'', CAST(0x0000A03001146A4B AS DateTime), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [IdHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (10, N'M00009', 1, NULL, NULL, 1, 215000, N'còn size 42', N'', CAST(0x0000A03001612447 AS DateTime), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [IdHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (11, N'M00010', 1, NULL, NULL, 1, 200000, N'', N'', CAST(0x0000A030016F4119 AS DateTime), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [IdHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (12, N'M00011', 1, NULL, NULL, 1, 173076, N'', N'', CAST(0x0000A030016FAA8E AS DateTime), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [IdHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (13, N'M00012', 1, NULL, NULL, 1, 346152, N'còn 2 size 40 - 43', N'', CAST(0x0000A0300170153E AS DateTime), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [IdHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (14, N'M00013', 1, NULL, NULL, 1, 553846, N'còn 02 đôi 40 -44', N'', CAST(0x0000A03001714E62 AS DateTime), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [IdHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (15, N'M00014', 1, NULL, NULL, 1, 553846, N'còn 02 đôi 40 -44', N'', CAST(0x0000A03001718551 AS DateTime), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [IdHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (16, N'M00015', 1, NULL, NULL, 1, 429628, N'còn 2 màu đỏ đ3n', N'', CAST(0x0000A03301367650 AS DateTime), N'', CAST(0x8E350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [IdHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (17, N'M00016', 1, NULL, NULL, 1, 133333, N'màu đỏ ', N'', CAST(0x0000A0330136FA6F AS DateTime), N'', CAST(0x8E350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [IdHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (18, N'M00017', 1, NULL, NULL, 1, 200000, N'màu Trắng', N'', CAST(0x0000A03301375165 AS DateTime), N'', CAST(0x8E350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [IdHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (19, N'M00018', 1, NULL, NULL, 1, 66, N'màu  xanh', N'', CAST(0x0000A0330137FC62 AS DateTime), N'', CAST(0x8E350B00 AS Date), 1)
INSERT [dbo].[HoaDon] ([Id], [IdHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (20, N'M00019', 1, NULL, NULL, 1, 303702, N'nón nữa đầu kết rời ', N'', CAST(0x0000A033013BBF56 AS DateTime), N'', CAST(0x8E350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [IdHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (21, N'M00020', 1, NULL, NULL, 1, 159259, N'nón nữa đầu kết rời ', N'', CAST(0x0000A033013BFAA0 AS DateTime), N'', CAST(0x8E350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [IdHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (22, N'M00021', 1, NULL, NULL, 1, 5230760, N'1 bộ áo dài không lấy quần trừ 35 nghìn', N'', CAST(0x0000A03601064F0A AS DateTime), N'', CAST(0x91350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [IdHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (23, N'M00022', 1, NULL, NULL, 1, 500000, N'1 bộ áo dài không lấy quần trừ 30 nghìn', N'', CAST(0x0000A0360106A188 AS DateTime), N'', CAST(0x91350B00 AS Date), 1)
INSERT [dbo].[HoaDon] ([Id], [IdHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (24, N'M00023', 1, NULL, NULL, 1, 500000, N'nếu không lấy quần trừ 30 nghìn', N'', CAST(0x0000A03601457F0E AS DateTime), N'', CAST(0x91350B00 AS Date), 1)
INSERT [dbo].[HoaDon] ([Id], [IdHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (25, N'M00024', 1, NULL, NULL, 1, 500000, N'trừ quần 30 ngàn', N'', CAST(0x0000A037010A4A4A AS DateTime), N'', CAST(0x92350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [IdHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (26, N'M00025', 1, NULL, NULL, 1, 140000, N'', N'', CAST(0x0000A03B00B1B11E AS DateTime), N'', CAST(0x96350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [IdHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (27, N'M00026', 1, NULL, NULL, 1, 40000, N'', N'', CAST(0x0000A03B00B1CB88 AS DateTime), N'', CAST(0x96350B00 AS Date), 0)
SET IDENTITY_INSERT [dbo].[HoaDon] OFF
/****** Object:  Table [dbo].[HoaDonDetail]    Script Date: 04/24/2012 21:57:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDonDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdHoaDon] [int] NOT NULL,
	[IdSanPham] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[ThanhTien] [bigint] NOT NULL,
 CONSTRAINT [PK_DonHangDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[HoaDonDetail] ON
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [SoLuong], [ThanhTien]) VALUES (4, 4, 6, 2, 560000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [SoLuong], [ThanhTien]) VALUES (5, 6, 6, 5, 1000000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [SoLuong], [ThanhTien]) VALUES (6, 7, 8, 3, 570000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [SoLuong], [ThanhTien]) VALUES (7, 8, 9, 1, 190000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [SoLuong], [ThanhTien]) VALUES (8, 9, 10, 1, 190000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [SoLuong], [ThanhTien]) VALUES (9, 10, 11, 1, 215000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [SoLuong], [ThanhTien]) VALUES (10, 11, 12, 1, 200000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [SoLuong], [ThanhTien]) VALUES (11, 12, 13, 1, 173076)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [SoLuong], [ThanhTien]) VALUES (12, 13, 14, 2, 346152)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [SoLuong], [ThanhTien]) VALUES (13, 14, 15, 2, 553846)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [SoLuong], [ThanhTien]) VALUES (14, 15, 16, 2, 553846)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [SoLuong], [ThanhTien]) VALUES (15, 16, 17, 2, 429628)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [SoLuong], [ThanhTien]) VALUES (16, 17, 18, 1, 133333)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [SoLuong], [ThanhTien]) VALUES (17, 18, 19, 1, 200000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [SoLuong], [ThanhTien]) VALUES (18, 19, 20, 1, 66)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [SoLuong], [ThanhTien]) VALUES (19, 20, 21, 2, 303702)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [SoLuong], [ThanhTien]) VALUES (20, 21, 22, 1, 159259)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [SoLuong], [ThanhTien]) VALUES (21, 22, 23, 40, 5230760)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [SoLuong], [ThanhTien]) VALUES (22, 23, 24, 5, 500000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [SoLuong], [ThanhTien]) VALUES (23, 24, 25, 5, 500000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [SoLuong], [ThanhTien]) VALUES (24, 25, 26, 5, 500000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [SoLuong], [ThanhTien]) VALUES (25, 26, 27, 7, 140000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [SoLuong], [ThanhTien]) VALUES (26, 27, 28, 2, 40000)
SET IDENTITY_INSERT [dbo].[HoaDonDetail] OFF
/****** Object:  Default [DF_DonHang_ThanhTien]    Script Date: 04/24/2012 21:57:38 ******/
ALTER TABLE [dbo].[HoaDon] ADD  CONSTRAINT [DF_DonHang_ThanhTien]  DEFAULT ((0)) FOR [ThanhTien]
GO
/****** Object:  Default [DF_DonHangDetail_ThanhTien]    Script Date: 04/24/2012 21:57:38 ******/
ALTER TABLE [dbo].[HoaDonDetail] ADD  CONSTRAINT [DF_DonHangDetail_ThanhTien]  DEFAULT ((0)) FOR [ThanhTien]
GO
/****** Object:  Default [DF_KhachHang_GioiTinh]    Script Date: 04/24/2012 21:57:38 ******/
ALTER TABLE [dbo].[KhachHang] ADD  CONSTRAINT [DF_KhachHang_GioiTinh]  DEFAULT (N'Nam') FOR [GioiTinh]
GO
/****** Object:  Default [DF_KhachHang_Diem]    Script Date: 04/24/2012 21:57:38 ******/
ALTER TABLE [dbo].[KhachHang] ADD  CONSTRAINT [DF_KhachHang_Diem]  DEFAULT ((0)) FOR [Diem]
GO
/****** Object:  Default [DF_SanPham_SoLuong]    Script Date: 04/24/2012 21:57:38 ******/
ALTER TABLE [dbo].[SanPham] ADD  CONSTRAINT [DF_SanPham_SoLuong]  DEFAULT ((0)) FOR [SoLuong]
GO
/****** Object:  ForeignKey [FK_HoaDon_HoaDonGroup]    Script Date: 04/24/2012 21:57:38 ******/
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_HoaDonGroup] FOREIGN KEY([IdType])
REFERENCES [dbo].[HoaDonType] ([Id])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_HoaDonGroup]
GO
/****** Object:  ForeignKey [FK_HoaDon_HoaDonStatus]    Script Date: 04/24/2012 21:57:38 ******/
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_HoaDonStatus] FOREIGN KEY([IdStatus])
REFERENCES [dbo].[HoaDonStatus] ([Id])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_HoaDonStatus]
GO
/****** Object:  ForeignKey [FK_HoaDon_KhachHang]    Script Date: 04/24/2012 21:57:38 ******/
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_KhachHang] FOREIGN KEY([IdKhachHang])
REFERENCES [dbo].[KhachHang] ([Id])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_KhachHang]
GO
/****** Object:  ForeignKey [FK_HoaDon_User]    Script Date: 04/24/2012 21:57:38 ******/
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_User]
GO
/****** Object:  ForeignKey [FK_HoaDonDetail_HoaDon]    Script Date: 04/24/2012 21:57:38 ******/
ALTER TABLE [dbo].[HoaDonDetail]  WITH CHECK ADD  CONSTRAINT [FK_HoaDonDetail_HoaDon] FOREIGN KEY([IdHoaDon])
REFERENCES [dbo].[HoaDon] ([Id])
GO
ALTER TABLE [dbo].[HoaDonDetail] CHECK CONSTRAINT [FK_HoaDonDetail_HoaDon]
GO
/****** Object:  ForeignKey [FK_HoaDonDetail_SanPham]    Script Date: 04/24/2012 21:57:38 ******/
ALTER TABLE [dbo].[HoaDonDetail]  WITH CHECK ADD  CONSTRAINT [FK_HoaDonDetail_SanPham] FOREIGN KEY([IdSanPham])
REFERENCES [dbo].[SanPham] ([Id])
GO
ALTER TABLE [dbo].[HoaDonDetail] CHECK CONSTRAINT [FK_HoaDonDetail_SanPham]
GO
/****** Object:  ForeignKey [FK_KhachHang_KhachHangGroup]    Script Date: 04/24/2012 21:57:38 ******/
ALTER TABLE [dbo].[KhachHang]  WITH CHECK ADD  CONSTRAINT [FK_KhachHang_KhachHangGroup] FOREIGN KEY([IdGroup])
REFERENCES [dbo].[KhachHangGroup] ([Id])
GO
ALTER TABLE [dbo].[KhachHang] CHECK CONSTRAINT [FK_KhachHang_KhachHangGroup]
GO
/****** Object:  ForeignKey [FK_SanPham_SanPhamGroup]    Script Date: 04/24/2012 21:57:38 ******/
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_SanPhamGroup] FOREIGN KEY([IdGroup])
REFERENCES [dbo].[SanPhamGroup] ([Id])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_SanPhamGroup]
GO
/****** Object:  ForeignKey [FK_SanPham_XuatXu]    Script Date: 04/24/2012 21:57:38 ******/
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_XuatXu] FOREIGN KEY([IdXuatXu])
REFERENCES [dbo].[XuatXu] ([Id])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_XuatXu]
GO
/****** Object:  ForeignKey [FK_User_UserGroup]    Script Date: 04/24/2012 21:57:38 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserGroup] FOREIGN KEY([IdGroup])
REFERENCES [dbo].[UserGroup] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserGroup]
GO
