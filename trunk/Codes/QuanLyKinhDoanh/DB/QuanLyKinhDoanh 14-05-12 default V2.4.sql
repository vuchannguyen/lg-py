USE [QuanLyKinhDoanh]
GO
/****** Object:  Table [dbo].[HoaDonType]    Script Date: 05/14/2012 23:47:35 ******/
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
/****** Object:  Table [dbo].[HoaDonStatus]    Script Date: 05/14/2012 23:47:35 ******/
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
/****** Object:  Table [dbo].[KhachHangGroup]    Script Date: 05/14/2012 23:47:35 ******/
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
INSERT [dbo].[KhachHangGroup] ([Id], [Ma], [Ten], [Diem], [MoTa]) VALUES (3, N'KHND', N'Thành viên Ngọc Ðăng', 0, NULL)
INSERT [dbo].[KhachHangGroup] ([Id], [Ma], [Ten], [Diem], [MoTa]) VALUES (4, N'KS', N'Khách sĩ', 0, NULL)
INSERT [dbo].[KhachHangGroup] ([Id], [Ma], [Ten], [Diem], [MoTa]) VALUES (5, N'VIP', N'VIP', 0, NULL)
SET IDENTITY_INSERT [dbo].[KhachHangGroup] OFF
/****** Object:  Table [dbo].[SanPhamGroup]    Script Date: 05/14/2012 23:47:35 ******/
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
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (20, N'Balxep', N'giày bal xẹp', N'giày bal(baby) không gót')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (21, N'BalG9F', N'giày bal gót 9 phân', N'giày bal gót rời cao 9F gót rời')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (22, N'BalG3F', N'giày bal gót 3 F', N'giày bal gót rời 3 phân gót rời')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (23, N'BalG5F', N'giày bal gót 5F', N'giày bal gót rời cao 5 phân')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (24, N'BalX3F', N'Giày bal xuồng 3F', N'giày bal đế xuồng cao 3F')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (25, N'BalX5F', N'giày bal xuồng 5F', N'Giày bal đế xuồng cao 5F')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (26, N'BalG7F', N'giày bal gót 7F', N'giày bal gót rời cao 7F')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (27, N'DKX7F', N'Dép kẹp xuồng 7F', N'Dép kẹp xuồng cao 7 phân')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (28, N'DDX7F', N'Dép đế xuồng 7F', N'Dép đế xuồng cao 7 F quai ngang')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (29, N'GSdX', N'Giày Sandal xuồng', N'giày sandal đế xuồng ')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (30, N'VDun', N'Vải Dún', N'vải áo dài dún - kiểu')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (31, N'VCuoi', N'Vải ao dài Cưới', N'tất cả các loại vải thiết kế đặc biệt co Cô Dâu')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (32, N'VADVol', N'vải Vol may ao dài', N'')
SET IDENTITY_INSERT [dbo].[SanPhamGroup] OFF
/****** Object:  Table [dbo].[XuatXu]    Script Date: 05/14/2012 23:47:35 ******/
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
INSERT [dbo].[XuatXu] ([Id], [Ten], [DiaChi], [DienThoai], [Fax], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (2, N'Nón Phong Phú', N'sạp 330-333,407-408 chợ Bình Tây/ CH 06 Nguyễn Hữu Thuận,F2,Q6', N'08.2212997', N'', N'', N'Khẩu trang 0862762968 - 0835544041', N'', CAST(0x9A350B00 AS Date), N'', CAST(0x9A350B00 AS Date), 0)
INSERT [dbo].[XuatXu] ([Id], [Ten], [DiaChi], [DienThoai], [Fax], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (3, N'Kim Hoa ', N'sạp 409-411 398 348 Chợ Bình Tây', N'0839507516', N'', N'', N'NR: 47 T cư xá Phú Lâm D Đ.Lý Chiêu Hoàng f10 Q6 ĐT:0903732671', N'', CAST(0x9A350B00 AS Date), N'', CAST(0x9A350B00 AS Date), 0)
INSERT [dbo].[XuatXu] ([Id], [Ten], [DiaChi], [DienThoai], [Fax], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (4, N'Nhã Vân', N'Lô I,33-35 Tầng Trệt chợ An Đông', N'0862722021', N'DĐ:0988330770', N'', N'', N'', CAST(0x9A350B00 AS Date), N'', CAST(0x9A350B00 AS Date), 0)
INSERT [dbo].[XuatXu] ([Id], [Ten], [DiaChi], [DienThoai], [Fax], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (5, N'Tiến Tài', N'sạp L6 L7 K(1)19 k(1)20 chợ An Đông', N'08-35059644-35075412', N'', N'', N'DĐ 0918928989 STK:Trần Tài 0101084862 Tại Ngân Hàng Đông Á Q5', N'', CAST(0x9A350B00 AS Date), N'', CAST(0x9A350B00 AS Date), 0)
INSERT [dbo].[XuatXu] ([Id], [Ten], [DiaChi], [DienThoai], [Fax], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (6, N'Phương Vy', N'sạp G12-14 tầng trệt chợ An Đông', N'0909515871', N'', N'cnhtran@gmail.com', N'Giày dép nam nữ', N'', CAST(0x9A350B00 AS Date), N'', CAST(0x9A350B00 AS Date), 0)
INSERT [dbo].[XuatXu] ([Id], [Ten], [DiaChi], [DienThoai], [Fax], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (7, N'Hồng Tính', N'sạp B18 tầng trệt', N'08.38324620-2664', N'DĐ0908328476', N'', N'Điện thoại nhà (tối) 08.38204567', N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[XuatXu] ([Id], [Ten], [DiaChi], [DienThoai], [Fax], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (8, N'Đại Hoàng Hoa', N'556 Trần Hưng Đạo B Q5-Chợ Soái Kình Lâm', N'0838536584', N'', N'', N'', N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[XuatXu] ([Id], [Ten], [DiaChi], [DienThoai], [Fax], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (9, N'Ngọc Hảo', N'485 Trần Hưng Đạo B F14 Q5', N'0839509612', N'', N'', N'', N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[XuatXu] ([Id], [Ten], [DiaChi], [DienThoai], [Fax], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (10, N'Như Hồng Đào', N'489 Trần Hưng Đạo B f14 Q5', N'0838590770', N'', N'', N'', N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
SET IDENTITY_INSERT [dbo].[XuatXu] OFF
/****** Object:  Table [dbo].[UserGroup]    Script Date: 05/14/2012 23:47:35 ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 05/14/2012 23:47:35 ******/
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
/****** Object:  Table [dbo].[SanPham]    Script Date: 05/14/2012 23:47:35 ******/
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
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (5, N'GNaTT0001', 5, NULL, N'Giày TT Moschino', N'', 0, 0, 0, 0, N'ĐÔI', N'Mochino', N'44', NULL, NULL, 1, N'Năm', N'', CAST(0x85350B00 AS Date), N'', CAST(0x85350B00 AS Date), 1)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (6, N'GTTNa0006', 5, NULL, N'Giày Mochino', N'', 200000, 260000, 130, 7, N'ĐÔI', N'', N'44', NULL, NULL, 1, N'Năm', N'', CAST(0x85350B00 AS Date), N'', CAST(0x85350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (7, N'GTTNa0007', 5, NULL, N'giày mochino the thao', N'', 0, 0, 0, 0, N'ĐÔI', N'', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x85350B00 AS Date), N'', CAST(0x85350B00 AS Date), 1)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (8, N'GTTNu0001', 6, NULL, N'Giày thể thao cao cổ nữ', N'giày thê thao cao cổ nữ cột dây màu hông', 190000, 264100, 39, 3, N'ĐÔI', N'', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8B350B00 AS Date), N'', CAST(0x9A350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (9, N'GTTNu0009', 6, NULL, N'Giày thể thao cao cổ', N'Giày thể thao cao cổ cột dây màu vàng', 190000, 265000, 39, 1, N'ĐÔI', N'', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8B350B00 AS Date), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (10, N'GTNa0002', 7, NULL, N'Giầy tây sỏ màu đỏ', N'giày tây sỏ màu đỏ đô', 190000, 270000, 42, 1, N'ĐÔI', N'', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8B350B00 AS Date), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (11, N'GTNa0003', 7, NULL, N'Giày tây sỏ', N'giày tây sỏ màu đỏ đô có hình cá sấu', 215000, 290000, 35, 1, N'ĐÔI', N'', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8B350B00 AS Date), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (12, N'GTTNa0008', 5, NULL, N'Giày thể thao dán', N'giày thể thao màu bò dán có dây kéo ngang có chữ M', 200000, 260000, 30, 1, N'ĐÔI', N'', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8B350B00 AS Date), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (13, N'GTTNa0009', 5, NULL, N'Giày thể thao dán', N'giày thể thao màu nâu dán có chữ Mr.LOVE', 173076, 225000, 30, 1, N'ĐÔI', N'', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8B350B00 AS Date), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (14, N'GTTNa0010', 5, NULL, N'Giày thể thao sỏ', N'giày thể thao màu xám dán có chữ A', 173076, 225000, 30, 2, N'ĐÔI', N'', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8B350B00 AS Date), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (15, N'GTTNa0011', 5, NULL, N'Giày thể thao sỏ', N'giày dáng thể thao hiệu Moschino màu trắng đen', 276923, 360000, 30, 2, N'ĐÔI', N'moschino', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8B350B00 AS Date), N'', CAST(0x8B350B00 AS Date), 1)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (16, N'GTTNa0016', 5, NULL, N'Giày thể thao sỏ', N'giày dáng thể thao hiệu Moschino màu đen trăng', 276923, 360000, 30, 2, N'ĐÔI', N'moschino', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8B350B00 AS Date), N'', CAST(0x90350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (17, N'MBH0001', 17, NULL, N'Nón Kiếng Andes', N'nón kiếng andes - kiếng trong đỏ đen Mã hàng 108MK', 214814, 290000, 35, 2, N'CÁI', N'Andes', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8E350B00 AS Date), N'', CAST(0x8E350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (18, N'MBH0002', 17, NULL, N'Nón Kiếng Asia', N'nón kiếng á châu giấu kiếng màu đỏ', 133333, 180000, 35, 1, N'CÁI', N'Asia', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8E350B00 AS Date), N'', CAST(0x8E350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (19, N'MBH0003', 17, NULL, N'Nón Kiếng Đức Huy', N'nón kiếng Đức Huy giấu kiếng màu Trắng', 200000, 270000, 35, 1, N'CÁI', N'Đức Huy', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8E350B00 AS Date), N'', CAST(0x8E350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (20, N'MBH0004', 17, NULL, N'Nón Kiếng ASIA', N'nón kiếng bé Asia màu xanh kiếng trắng', 66, 89, 35, 1, N'CÁI', N'Asia', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8E350B00 AS Date), N'', CAST(0x92350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (21, N'MBH0005', 17, NULL, N'Nữa đầu Andes', N'nón nữa đầu  kết rời đen trắng mã hàng 110/110L', 151851, 205000, 35, 2, N'CÁI', N'Andes', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8E350B00 AS Date), N'', CAST(0x8E350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (22, N'MBH0006', 17, NULL, N'Nữa đầu Andes', N'nón nữa đầu  kết rời đen  mã hàng 108', 159259, 215000, 35, 1, N'CÁI', N'Andes', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x8E350B00 AS Date), N'', CAST(0x8E350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (23, N'VADcf0006', 16, NULL, N'Áo dài chifong', N'vải áo dài dệt in hình sẵn bông - hoa văn - bi ....', 130769, 170000, 30, 40, N'Bộ', N'', N'', 0, N'', 0, N'Ngày', N'', CAST(0x91350B00 AS Date), N'', CAST(0x9A350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (24, N'VADcf0002', 16, NULL, N'Áo dài thường', N'vải áo dài dệt in hình sẵn bông - hoa văn - bi ....', 100000, 130000, 30, 5, N'BỘ', N'', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x91350B00 AS Date), N'', CAST(0x91350B00 AS Date), 1)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (25, N'ADTh0001', 18, NULL, N'Vải thường', N'vải thường dệt in hình hoa văn - bông -bi ...', 100000, 130000, 30, 5, N'BỘ', N'', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x91350B00 AS Date), N'', CAST(0x91350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (26, N'VADTh0002', 18, NULL, N'Vải áo dài thường', N'vải áo dài dệt in hình hoa văn - bông - bi ...', 100000, 130000, 30, 5, N'BỘ', N'', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x92350B00 AS Date), N'', CAST(0x92350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (27, N'DThom0001', 19, NULL, N'dầu thơm MD xanh', N'dầu thơm thường hiệu MD cty Quang Duc', 20000, 25000, 25, 7, N'CÁI', N'MD', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x96350B00 AS Date), N'', CAST(0x96350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (28, N'DThom0002', 19, NULL, N'dầu thơm MD tím', N'dầu thơm thường hiệu MD cty Quang Duc', 20000, 25000, 25, 2, N'CÁI', N'MD', N'', NULL, NULL, 0, N'Ngày', N'', CAST(0x96350B00 AS Date), N'', CAST(0x96350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (29, N'GSDNu0001', 9, 4, N'sandal kep kép', N'san dal kep hình tam giác kết sò', 72000, 105000, 46, 5, N'Đôi', N'', N'35-39', 6, N'Tháng', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (30, N'GSDNu0002', 9, 4, N'sandal kẹp bông cổ sơmi', N'san dal kẹp bông xéo màu kem cổ sơ mi kéo', 70000, 105000, 50, 5, N'Đôi', N'', N'35-39', 0, N'', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (31, N'GSDNu0003', 9, 4, N'sandal kẹp nút đá', N'san dal kẹp kết nút đá màu hồng', 75000, 109000, 45, 5, N'Đôi', N'', N'35-39', 6, N'Tháng', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (32, N'Balxep0001', 20, 4, N'giày bal xẹp khóa trắng', N'giày bal xẹp 2 khóa màu trắng hàng cty', 60000, 79000, 32, 5, N'Đôi', N'', N'35-39', 6, N'Tháng', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (33, N'Balxep0002', 20, 4, N'giày bal xẹp cột nơ kem vàng', N'giày bal xẹp cột nơ màu kem vàng', 60000, 79000, 32, 5, N'Đôi', N'', N'35-39', 6, N'Tháng', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (34, N'Balxep0001', 20, 4, N'giày bal xẹp kết đá nâu', N'giày bal xẹp kết đá màu nâu', 70000, 89000, 31, 5, N'Đôi', N'', N'35-39', 0, N'', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (35, N'BalG9F0001', 21, 4, N'giày bal gót rời màu đen', N'giày bal gót rời 9f màu đen đóng nút hở mũi', 94000, 129000, 37, 5, N'Đôi', N'', N'35-39', 6, N'Tháng', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (36, N'BalG9F0002', 21, 4, N'giày bal gót rời màu da bò', N'giày bal gót rời 9f màu da bò đóng nút hở mũi', 94000, 129000, 37, 5, N'Đôi', N'', N'35-39', 6, N'Tháng', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (37, N'BalG9F0003', 21, 4, N'giày bal gót rời màu trắng', N'giày bal gót rời 9f màu trắng khóa bên hông', 94000, 129000, 37, 5, N'Đôi', N'', N'35-39', 6, N'Tháng', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (38, N'Balxep0004', 20, 4, N'giày bal lưới đen', N'giày bal xẹp lưới màu đen', 46000, 65000, 41, 5, N'Đôi', N'', N'35-39', 6, N'Tháng', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (39, N'Balxep0005', 20, 4, N'giày bal lưới nâu', N'giày bal xẹp lưới màu nâu', 46000, 65000, 41, 5, N'Đôi', N'', N'35-39', 6, N'Tháng', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (40, N'Balxep0001', 20, 5, N'Bal thun m.bò', N'giày bal thun màu da bò', 130000, 188000, 34, 5, N'Đôi', N'', N'35-39', 0, N'', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (41, N'Balxep0001', 20, 5, N'Bal thun m.cam', N'giày bal xẹp thun màu cam', 130000, 188000, 34, 5, N'Đôi', N'', N'35-39', 0, N'', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (42, N'Balxep0001', 20, 5, N'Bal thun m.đen', N'giày bal xẹp thun màu đen', 130000, 188000, 34, 5, N'Đôi', N'', N'35-39', 0, N'', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (43, N'BalX3F0001', 24, 5, N'Bal X3F màu kem ', N'giày bal đế xuồng cao 3 phân DG màu kem đục hở mũi', 178000, 249000, 40, 5, N'Đôi', N'', N'35-39', 0, N'', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (44, N'BalG5F0001', 23, 5, N'Bal G5F nâu ', N'giày bal gót rời hở mũi màu nâu DG', 170000, 239000, 41, 5, N'Đôi', N'', N'35-39', 8, N'Tháng', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (45, N'BalG5F0002', 23, 5, N'Bal G5F đen', N'giày bal gót rời hở mũi màu đen DG', 170000, 239000, 41, 5, N'Đôi', N'', N'35-39', 8, N'Tháng', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (46, N'BalG5F0003', 23, 5, N'Bal G5F đen', N'giày bal gót rời hở mũi màu đen trơn kết hột đá', 193000, 269000, 39, 5, N'Đôi', N'', N'35-39', 8, N'Tháng', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (47, N'BalG7F0001', 26, NULL, N'Bal G7F đen', N'giày bal gót rời hở mũi màu đen DG', 175000, 248500, 42, 5, N'Đôi', N'', N'35-39', 8, N'Tháng', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (48, N'DKX7F0001', 27, 5, N'Dkẹp7F chiếc lá trắng', N'dép kẹp đế xuồng hình chiếc lá màu trắng cao 7F', 115000, 165000, 43, 3, N'Đôi', N'', N'37-38-39', 8, N'Tháng', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (49, N'DDX7F0001', 28, 7, N'DĐế X7F cột dây đỏ', N'dép đế xuồng cột dây màu đỏ cao 7F', 75000, 99000, 32, 5, N'Đôi', N'', N'35-39', 6, N'Tháng', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (50, N'DDX7F0002', 28, 7, N'DĐế X7F cột dây nâu', N'dép đế xuồng cột dây màu nâu cao 7F', 75000, 99000, 32, 5, N'Đôi', N'', N'35-39', 6, N'Tháng', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (51, N'GSdX0001', 29, NULL, N'GSd lưới cột dây', N'giày sandal lưới cột dây đế xuồng lưới màu nâu', 105000, 149000, 42, 5, N'Đôi', N'', N'35-39', 0, N'Ngày', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (52, N'DKNu0001', 13, 7, N'dép kẹp xẹp đỏ', N'dép kẹp xốp nữ hình PUMA', 22000, 30000, 36, 5, N'Đôi', N'', N'36-39', 6, N'Tháng', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (53, N'DKNu0002', 13, 7, N'dép kẹp xẹp vàng', N'dép kẹp xốp nữ hình Pink', 22000, 30000, 36, 5, N'Đôi', N'', N'36-39', 6, N'Tháng', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (54, N'DKNu0003', 13, 7, N'dép kẹp xẹp hồng', N'dép kẹp xốp nữ hình bàn chân', 22000, 30000, 36, 5, N'Đôi', N'', N'36-39', 6, N'Tháng', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (55, N'VADcf0001', 16, 10, N'Vải ChiFong NHĐ', N'Vải may áo dài chifong Áo+ quần', 130000, 170000, 31, 21, N'Bộ', N'', N'', 0, N'', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 1)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (56, N'VADcf0001', 16, 8, N'Vải ChiFong ĐHH', N'Vải may áo dài chifong Áo+ quần', 130000, 170000, 31, 8, N'Bộ', N'', N'', 0, N'', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 1)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (57, N'VADcf0001', 16, 9, N'Vải ChiFong NH', N'Vải may áo dài chifong Áo+ quần', 130000, 170000, 31, 30, N'Bộ', N'', N'', 0, N'', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 1)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (58, N'VDun0001', 30, 8, N'V.Dún thêu lai chân', N'Vải may áo dài dún thêu lai chân', 220000, 295000, 34, 7, N'Bộ', N'', N'', 0, N'', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 1)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (59, N'VDun0001', 30, 9, N'V.Dún diên kết cườm', N'Vải may áo dài dún kim tuyến kết cườm', 180000, 255000, 42, 10, N'Bộ', N'', N'', 0, N'', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 1)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (60, N'VCuoi0001', 31, NULL, N'Vải áo cưới', N'Vải may áo dài dún kim tuyến (đồ cưới)', 215000, 315000, 47, 2, N'Bộ', N'', N'', 1, N'Năm', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (61, N'VDun0001', 30, 10, N'V.Dún k.đá ren ngực', N'Vải may áo dài dún kim tuyến', 205000, 295000, 44, 1, N'Bộ', N'', N'', 0, N'', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 1)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (62, N'VADDun0004', 30, 10, N'V.Dún k.đá ren ngực', N'Vải may áo dài dún kim tuyến', 205000, 295000, 44, 1, N'Bộ', N'', N'', 1, N'Năm', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 1)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (63, N'VADDun0005', 30, 10, N'V.Dún g/h tay K.sa', N'Vải may áo dài dún gh tay kim sa', 195000, 275000, 41, 1, N'Bộ', N'', N'', 1, N'Năm', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 1)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (64, N'VADVol0001', 32, NULL, N'V.vol thêu lai', N'Vải may áo dài dún gh tay kim sa', 175000, 255000, 46, 2, N'Bộ', N'', N'', 1, N'Năm', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (65, N'VDun0002', 30, 8, N'VDún thêu lai chân', N'vải áo dài dún thêu lai chân', 220000, 295000, 34, 7, N'Bộ', N'', N'', 1, N'Năm', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (66, N'VDun0003', 30, 9, N'VDún diên kết cườm', N'vải áo dài dún kết cườm', 180000, 255000, 42, 10, N'Bộ', N'', N'', 1, N'Năm', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (67, N'VCuoi0002', 31, 10, N'VDún đá ren ngực', N'vải áo dài dún kết đá ren ngực', 210000, 295000, 40, 1, N'Bộ', N'', N'', 1, N'Năm', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 1)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (68, N'VDun0004', 30, 10, N'VDún đá ren ngực', N'vải áo dài dún kết đá ren ngực', 210000, 295000, 40, 1, N'Bộ', N'', N'', 1, N'Năm', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (69, N'VDun0005', 30, 10, N'VDún phi', N'vải áo dài dún kết đá ren ngực', 180000, 245000, 36, 5, N'Bộ', N'', N'', 1, N'Năm', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (70, N'VDun0006', 30, 10, N'VDún gh tay kim sa', N'vải áo dài dúngh tay kim sa', 195000, 265000, 36, 1, N'Bộ', N'', N'', 1, N'Năm', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (71, N'VADcf0007', 16, 8, N'Vải Chifong ĐHH', N'vải áo dài chifong áo + quần', 130000, 170000, 31, 8, N'Bộ', N'', N'', 1, N'Năm', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (72, N'VADcf0008', 16, 9, N'Vải Chifong NH', N'vải áo dài chifong áo + quần', 130000, 170000, 31, 30, N'Bộ', N'', N'', 1, N'Năm', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (73, N'VADcf0009', 16, 10, N'Vải Chifong NHĐ', N'vải áo dài chifong áo + quần', 130000, 170000, 31, 21, N'Bộ', N'', N'', 1, N'Năm', NULL, NULL, N'', CAST(0x9B350B00 AS Date), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (74, N'DDNu0001', 11, 6, N'bản si nâu', N'dép quai ngang dán si', 70000, 98000, 40, 5, N'Đôi', N'', N'', 6, N'Tháng', NULL, NULL, N'', CAST(0x9D350B00 AS Date), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (75, N'Balxep0006', 20, 6, N'Bal LAO3 hồng', N'bal xẹp màu hồng viền trắng khóa ', 65000, 89000, 37, 5, N'Đôi', N'', N'', 6, N'Tháng', NULL, NULL, N'', CAST(0x9D350B00 AS Date), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (76, N'Balxep0007', 20, 6, N'Bal LAO3 trắng', N'bal xẹp màu trắng viền xanh khóa ', 65000, 89000, 37, 5, N'Đôi', N'', N'', 6, N'Tháng', NULL, NULL, N'', CAST(0x9D350B00 AS Date), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (77, N'GTTNu0010', 6, 6, N'TT cột dây kem', N'giày thể thao màu kem cột dây', 90000, 129000, 43, 5, N'Đôi', N'', N'', 6, N'Tháng', NULL, NULL, N'', CAST(0x9D350B00 AS Date), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (78, N'GTTNu0011', 6, 6, N'TT 3 xé vàng', N'giày thể thao màu vàng 3 miếng xé', 90000, 129000, 43, 5, N'Đôi', N'', N'', 6, N'Tháng', NULL, NULL, N'', CAST(0x9D350B00 AS Date), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (79, N'GTTNu0012', 6, 6, N'TT sỏ 1 xé kem', N'giày thể thao sỏ 1 quai xé màu kem', 140000, 219000, 56, 5, N'Đôi', N'', N'', 6, N'Tháng', NULL, NULL, N'', CAST(0x9D350B00 AS Date), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (80, N'GTTNu0013', 6, 6, N'TT cột đen', N'giày thể thao cột dây đen bi trắng ', 140000, 198000, 41, 5, N'Đôi', N'', N'', 6, N'Tháng', NULL, NULL, N'', CAST(0x9D350B00 AS Date), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (81, N'GTTNu0014', 6, 6, N'TT 1xé kem', N'giày thể thao 1 xé màu kem', 140000, 198000, 41, 5, N'Đôi', N'', N'', 6, N'Tháng', NULL, NULL, N'', CAST(0x9D350B00 AS Date), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (82, N'GTTNa0017', 5, 6, N'TT sỏ đen', N'giày thể thao sỏ cổ trước cao', 145000, 219000, 51, 5, N'Đôi', N'', N'', 6, N'Tháng', NULL, NULL, N'', CAST(0x9D350B00 AS Date), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (83, N'GTTNa0018', 5, 6, N'TT sỏ kem', N'giày thể thao sỏ màu kem Dior (8003)', 155000, 229000, 48, 5, N'Đôi', N'', N'', 6, N'Tháng', NULL, NULL, N'', CAST(0x9D350B00 AS Date), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (84, N'DDNu0002', 11, 6, N'D TT Nike nâu', N'dép dáng thể thao hiệu Nike màu nâu viền vàng', 90000, 120000, 33, 5, N'Đôi', N'', N'', 6, N'Tháng', NULL, NULL, N'', CAST(0x9D350B00 AS Date), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (85, N'VDun0007', 30, 10, N'Dún dán phi', N'vải dún dán phi kim tuyến', 180000, 255000, 42, 5, N'Đôi', N'', N'', 1, N'Năm', NULL, NULL, N'', CAST(0x9D350B00 AS Date), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (86, N'VDun0008', 30, 10, N'Vdún dán phi', N'vải dún dán phi kim tuyến', 180000, 255000, 42, 5, N'Bộ', N'', N'', 1, N'Năm', NULL, NULL, N'', CAST(0x9D350B00 AS Date), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (87, N'GTTNa0019', 5, 6, N'xỏ vải nâu 1 xé', N'giày thể thao nam vải 1 xé bên góc màu nâu', 100000, 145000, 45, 5, N'Đôi', N'', N'', 8, N'Tháng', NULL, NULL, N'', CAST(0x9D350B00 AS Date), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [MaSanPham], [IdGroup], [IdXuatXu], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [Hieu], [Size], [ThoiHan], [DonViThoiHan], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (88, N'GTTNa0020', 5, 6, N'xỏ si nâu 1 xé', N'giày thể thao nam si 1 xé khóa bên góc màu nâu', 100000, 145000, 45, 5, N'Đôi', N'', N'', 8, N'Tháng', NULL, NULL, N'', CAST(0x9D350B00 AS Date), N'', CAST(0x9D350B00 AS Date), 0)
SET IDENTITY_INSERT [dbo].[SanPham] OFF
/****** Object:  Table [dbo].[KhachHang]    Script Date: 05/14/2012 23:47:35 ******/
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
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[KhachHang] ON
INSERT [dbo].[KhachHang] ([Id], [MaKhachHang], [IdGroup], [Ten], [GioiTinh], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [Fax], [Email], [TichLuy], [Diem], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (1, N'KHTT0001', 2, N'Lê Đăng Khanh', N'Nam', CAST(0x600B0B00 AS Date), N'1234567   ', CAST(0xAC350B00 AS Date), N'sdfghjkl', N'Định Bình long thới tiểu cần Trà Vinh', N'0932824616', N'', N'ldkhanh81@yahoo.com', 100000, NULL, N'ngày sinh con gái... kỷ niệm ngày cưới...', N'', CAST(0xA8350B00 AS Date), N'', CAST(0xAC350B00 AS Date), 0)
INSERT [dbo].[KhachHang] ([Id], [MaKhachHang], [IdGroup], [Ten], [GioiTinh], [DOB], [CMND], [NgayCap], [NoiCap], [DiaChi], [DienThoai], [Fax], [Email], [TichLuy], [Diem], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (2, N'VIP0001', 3, N'Lê Ngọc', N'Nữ', CAST(0x9B160B00 AS Date), NULL, NULL, NULL, N'Định phú B long thới Tiểu cần TV', N'0743616794', N'', N'', 0, NULL, N'', N'', CAST(0xA8350B00 AS Date), N'', CAST(0xA8350B00 AS Date), 0)
SET IDENTITY_INSERT [dbo].[KhachHang] OFF
/****** Object:  Table [dbo].[HoaDon]    Script Date: 05/14/2012 23:47:35 ******/
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
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (4, N'M00001', 1, NULL, NULL, 1, 560000, N'', N'', CAST(0x0000A02A0141F944 AS DateTime), N'', CAST(0x85350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (5, N'M00005', 1, NULL, NULL, 1, 1000000, N'', N'', CAST(0x0000A02A0149BB58 AS DateTime), N'', CAST(0x85350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (6, N'M00005', 1, NULL, NULL, 1, 1000000, N'', N'', CAST(0x0000A02A0149F561 AS DateTime), N'', CAST(0x85350B00 AS Date), 1)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (7, N'M00006', 1, NULL, NULL, 1, 570000, N'size 35 - 37 - 38', N'', CAST(0x0000A03000A42CF4 AS DateTime), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (8, N'M00007', 1, NULL, NULL, 1, 190000, N'còn size 37', N'', CAST(0x0000A03000C68087 AS DateTime), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (9, N'M00008', 1, NULL, NULL, 1, 190000, N'', N'', CAST(0x0000A03001146A4B AS DateTime), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (10, N'M00009', 1, NULL, NULL, 1, 215000, N'còn size 42', N'', CAST(0x0000A03001612447 AS DateTime), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (11, N'M00010', 1, NULL, NULL, 1, 200000, N'', N'', CAST(0x0000A030016F4119 AS DateTime), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (12, N'M00011', 1, NULL, NULL, 1, 173076, N'', N'', CAST(0x0000A030016FAA8E AS DateTime), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (13, N'M00012', 1, NULL, NULL, 1, 346152, N'còn 2 size 40 - 43', N'', CAST(0x0000A0300170153E AS DateTime), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (14, N'M00013', 1, NULL, NULL, 1, 553846, N'còn 02 đôi 40 -44', N'', CAST(0x0000A03001714E62 AS DateTime), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (15, N'M00014', 1, NULL, NULL, 1, 553846, N'còn 02 đôi 40 -44', N'', CAST(0x0000A03001718551 AS DateTime), N'', CAST(0x8B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (16, N'M00015', 1, NULL, NULL, 1, 429628, N'còn 2 màu đỏ đ3n', N'', CAST(0x0000A03301367650 AS DateTime), N'', CAST(0x8E350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (17, N'M00016', 1, NULL, NULL, 1, 133333, N'màu đỏ ', N'', CAST(0x0000A0330136FA6F AS DateTime), N'', CAST(0x8E350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (18, N'M00017', 1, NULL, NULL, 1, 200000, N'màu Trắng', N'', CAST(0x0000A03301375165 AS DateTime), N'', CAST(0x8E350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (19, N'M00018', 1, NULL, NULL, 1, 66, N'màu  xanh', N'', CAST(0x0000A0330137FC62 AS DateTime), N'', CAST(0x8E350B00 AS Date), 1)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (20, N'M00019', 1, NULL, NULL, 1, 303702, N'nón nữa đầu kết rời ', N'', CAST(0x0000A033013BBF56 AS DateTime), N'', CAST(0x8E350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (21, N'M00020', 1, NULL, NULL, 1, 159259, N'nón nữa đầu kết rời ', N'', CAST(0x0000A033013BFAA0 AS DateTime), N'', CAST(0x8E350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (22, N'M00021', 1, NULL, NULL, 1, 5230760, N'1 bộ áo dài không lấy quần trừ 35 nghìn', N'', CAST(0x0000A03601064F0A AS DateTime), N'', CAST(0x91350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (23, N'M00022', 1, NULL, NULL, 1, 500000, N'1 bộ áo dài không lấy quần trừ 30 nghìn', N'', CAST(0x0000A0360106A188 AS DateTime), N'', CAST(0x91350B00 AS Date), 1)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (24, N'M00023', 1, NULL, NULL, 1, 500000, N'nếu không lấy quần trừ 30 nghìn', N'', CAST(0x0000A03601457F0E AS DateTime), N'', CAST(0x91350B00 AS Date), 1)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (25, N'M00024', 1, NULL, NULL, 1, 500000, N'trừ quần 30 ngàn', N'', CAST(0x0000A037010A4A4A AS DateTime), N'', CAST(0x92350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (26, N'M00025', 1, NULL, NULL, 1, 140000, N'', N'', CAST(0x0000A03B00B1B11E AS DateTime), N'', CAST(0x96350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (27, N'M00026', 1, NULL, NULL, 1, 40000, N'', N'', CAST(0x0000A03B00B1CB88 AS DateTime), N'', CAST(0x96350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (28, N'M00027', 1, NULL, NULL, 1, 360000, N'', N'', CAST(0x0000A040009F4387 AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (29, N'M00028', 1, NULL, NULL, 1, 350000, N'', N'', CAST(0x0000A040009F958C AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (30, N'M00029', 1, NULL, NULL, 1, 375000, N'', N'', CAST(0x0000A040009FEEFF AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (31, N'M00030', 1, NULL, NULL, 1, 300000, N'', N'', CAST(0x0000A04000A30F8B AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (32, N'M00031', 1, NULL, NULL, 1, 300000, N'', N'', CAST(0x0000A04000A3636B AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (33, N'M00032', 1, NULL, NULL, 1, 350000, N'', N'', CAST(0x0000A04000A3E877 AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (34, N'M00033', 1, NULL, NULL, 1, 470000, N'', N'', CAST(0x0000A04000A493A1 AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (35, N'M00034', 1, NULL, NULL, 1, 470000, N'', N'', CAST(0x0000A04000A4B314 AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (36, N'M00035', 1, NULL, NULL, 1, 470000, N'', N'', CAST(0x0000A04000A4D743 AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (37, N'M00036', 1, NULL, NULL, 1, 230000, N'', N'', CAST(0x0000A04000A52E2D AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (38, N'M00037', 1, NULL, NULL, 1, 230000, N'', N'', CAST(0x0000A04000A5445E AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (39, N'M00038', 1, NULL, NULL, 1, 650000, N'', N'', CAST(0x0000A04000B17129 AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (40, N'M00039', 1, NULL, NULL, 1, 650000, N'', N'', CAST(0x0000A04000B1A0E4 AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (41, N'M00040', 1, NULL, NULL, 1, 650000, N'', N'', CAST(0x0000A04000B1C0BB AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (42, N'M00041', 1, NULL, NULL, 1, 890000, N'', N'', CAST(0x0000A04000B34615 AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (43, N'M00042', 1, NULL, NULL, 1, 850000, N'', N'', CAST(0x0000A04000B76069 AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (44, N'M00043', 1, NULL, NULL, 1, 850000, N'', N'', CAST(0x0000A04000B784D3 AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (45, N'M00044', 1, NULL, NULL, 1, 965000, N'', N'', CAST(0x0000A04000B7FD77 AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (46, N'M00045', 1, NULL, NULL, 1, 875000, N'', N'', CAST(0x0000A04000B89A4D AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (47, N'M00046', 1, NULL, NULL, 1, 345000, N'', N'', CAST(0x0000A04000B9E0B4 AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (48, N'M00047', 1, NULL, NULL, 1, 375000, N'', N'', CAST(0x0000A04000BC3374 AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (49, N'M00048', 1, NULL, NULL, 1, 375000, N'', N'', CAST(0x0000A04000BC4AA2 AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (50, N'M00049', 1, NULL, NULL, 1, 525000, N'', N'', CAST(0x0000A0400104C8CA AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (51, N'M00050', 1, NULL, NULL, 1, 110000, N'', N'', CAST(0x0000A04001055420 AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (52, N'M00051', 1, NULL, NULL, 1, 110000, N'', N'', CAST(0x0000A0400105763E AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (53, N'M00052', 1, NULL, NULL, 1, 110000, N'', N'', CAST(0x0000A04001058757 AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (54, N'M00053', 1, NULL, NULL, 1, 2730000, N'không lấy quần trừ 35.000đ', N'', CAST(0x0000A040016694D0 AS DateTime), N'', CAST(0x9B350B00 AS Date), 1)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (55, N'M00054', 1, NULL, NULL, 1, 1040000, N'không lấy quần trừ 35.000đ', N'', CAST(0x0000A04001670F7C AS DateTime), N'', CAST(0x9B350B00 AS Date), 1)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (56, N'M00055', 1, NULL, NULL, 1, 3900000, N'không lấy quần trừ 35.000đ', N'', CAST(0x0000A04001674232 AS DateTime), N'', CAST(0x9B350B00 AS Date), 1)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (57, N'M00056', 1, NULL, NULL, 1, 1540000, N'không lấy quần trừ 35.000đ', N'', CAST(0x0000A040016907BE AS DateTime), N'', CAST(0x9B350B00 AS Date), 1)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (58, N'M00057', 1, NULL, NULL, 1, 1800000, N'không lấy quần trừ 35.000đ', N'', CAST(0x0000A040016A6A31 AS DateTime), N'', CAST(0x9B350B00 AS Date), 1)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (59, N'M00058', 1, NULL, NULL, 1, 430000, N'không lấy quần trừ 35.000đ', N'', CAST(0x0000A040016B76AB AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (60, N'M00059', 1, NULL, NULL, 1, 205000, N'không lấy quần trừ 35.000đ', N'', CAST(0x0000A040016C3C2E AS DateTime), N'', CAST(0x9B350B00 AS Date), 1)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (61, N'M00060', 1, NULL, NULL, 1, 205000, N'không lấy quần trừ 35.000đ', N'', CAST(0x0000A040016C72BD AS DateTime), N'', CAST(0x9B350B00 AS Date), 1)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (62, N'M00061', 1, NULL, NULL, 1, 195000, N'không lấy quần trừ 35.000đ', N'', CAST(0x0000A040016DBBFE AS DateTime), N'', CAST(0x9B350B00 AS Date), 1)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (63, N'M00062', 1, NULL, NULL, 1, 350000, N'không lấy quần trừ 35.000đ', N'', CAST(0x0000A040016E472A AS DateTime), N'', CAST(0x9B350B00 AS Date), 1)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (64, N'M00063', 1, NULL, NULL, 1, 1540000, N'', N'', CAST(0x0000A04001710245 AS DateTime), N'', CAST(0x9B350B00 AS Date), 1)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (65, N'M00064', 1, NULL, NULL, 1, 1800000, N'không lấy quần trừ 35 nghìn', N'', CAST(0x0000A04001716E3C AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (66, N'M00065', 1, NULL, NULL, 1, 210000, N'không lấy quần trừ 35 nghìn', N'', CAST(0x0000A0400171CAD4 AS DateTime), N'', CAST(0x9B350B00 AS Date), 1)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (67, N'M00066', 1, NULL, NULL, 1, 210000, N'không lấy quần trừ 35 nghìn', N'', CAST(0x0000A0400171E054 AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (68, N'M00067', 1, NULL, NULL, 1, 900000, N'không lấy quần trừ 35 nghìn', N'', CAST(0x0000A04001728817 AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (69, N'M00068', 1, NULL, NULL, 1, 195000, N'không lấy quần trừ 35 nghìn', N'', CAST(0x0000A0400172DE92 AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (70, N'M00069', 1, NULL, NULL, 1, 1040000, N'không lấy quần trừ 35.000đ', N'', CAST(0x0000A0400174A352 AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (71, N'M00070', 1, NULL, NULL, 1, 3900000, N'không lấy quần trừ 35.000đ', N'', CAST(0x0000A0400174C39A AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (72, N'M00071', 1, NULL, NULL, 1, 2730000, N'không lấy quần trừ 35.000đ', N'', CAST(0x0000A0400174E25D AS DateTime), N'', CAST(0x9B350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (73, N'M00072', 1, NULL, NULL, 1, 350000, N'', N'', CAST(0x0000A04200E7F554 AS DateTime), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (74, N'M00073', 1, NULL, NULL, 1, 325000, N'', N'', CAST(0x0000A04200E8B7A0 AS DateTime), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (75, N'M00074', 1, NULL, NULL, 1, 325000, N'', N'', CAST(0x0000A04200E8DA7A AS DateTime), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (76, N'M00075', 1, NULL, NULL, 1, 450000, N'', N'', CAST(0x0000A04200E9543C AS DateTime), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (77, N'M00076', 1, NULL, NULL, 1, 450000, N'', N'', CAST(0x0000A04200E99E77 AS DateTime), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (78, N'M00077', 1, NULL, NULL, 1, 700000, N'', N'', CAST(0x0000A04200EB19F8 AS DateTime), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (79, N'M00078', 1, NULL, NULL, 1, 700000, N'', N'', CAST(0x0000A04200EB9B01 AS DateTime), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (80, N'M00079', 1, NULL, NULL, 1, 700000, N'', N'', CAST(0x0000A04200EC2E94 AS DateTime), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (81, N'M00080', 1, NULL, NULL, 1, 725000, N'', N'', CAST(0x0000A04200F92CE7 AS DateTime), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (82, N'M00081', 1, NULL, NULL, 1, 775000, N'', N'', CAST(0x0000A04200F97C6F AS DateTime), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (83, N'M00082', 1, NULL, NULL, 1, 450000, N'', N'', CAST(0x0000A04200F9E3A6 AS DateTime), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (84, N'M00083', 1, NULL, NULL, 1, 900000, N'', N'', CAST(0x0000A04200FA98DE AS DateTime), N'', CAST(0x9D350B00 AS Date), 1)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (85, N'M00084', 1, NULL, NULL, 1, 900000, N'', N'', CAST(0x0000A04200FB345E AS DateTime), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (86, N'M00085', 1, NULL, NULL, 1, 500000, N'', N'', CAST(0x0000A04200FE818E AS DateTime), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (87, N'M00086', 1, NULL, NULL, 1, 500000, N'', N'', CAST(0x0000A04200FEB317 AS DateTime), N'', CAST(0x9D350B00 AS Date), 0)
INSERT [dbo].[HoaDon] ([Id], [MaHoaDon], [IdType], [IdUser], [IdKhachHang], [IdStatus], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (88, N'B00001', 2, NULL, NULL, 1, 648000, N'', N'', CAST(0x0000A05101836F54 AS DateTime), N'', CAST(0xAC350B00 AS Date), 0)
SET IDENTITY_INSERT [dbo].[HoaDon] OFF
/****** Object:  Table [dbo].[ChietKhau]    Script Date: 05/14/2012 23:47:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChietKhau](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdSanPham] [int] NOT NULL,
	[idKhachHangGroup] [int] NULL,
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
/****** Object:  Table [dbo].[HoaDonDetail]    Script Date: 05/14/2012 23:47:35 ******/
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
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (5, 6, 6, 200000, 5, 0, 1000000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (6, 7, 8, 190000, 3, 0, 570000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (7, 8, 9, 190000, 1, 0, 190000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (8, 9, 10, 190000, 1, 0, 190000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (9, 10, 11, 215000, 1, 0, 215000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (10, 11, 12, 200000, 1, 0, 200000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (11, 12, 13, 173076, 1, 0, 173076)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (12, 13, 14, 173076, 2, 0, 346152)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (13, 14, 15, 276923, 2, 0, 553846)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (14, 15, 16, 276923, 2, 0, 553846)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (15, 16, 17, 214814, 2, 0, 429628)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (16, 17, 18, 133333, 1, 0, 133333)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (17, 18, 19, 200000, 1, 0, 200000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (18, 19, 20, 66, 1, 0, 66)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (19, 20, 21, 151851, 2, 0, 303702)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (20, 21, 22, 159259, 1, 0, 159259)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (21, 22, 23, 130769, 40, 0, 5230760)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (22, 23, 24, 100000, 5, 0, 500000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (23, 24, 25, 100000, 5, 0, 500000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (24, 25, 26, 100000, 5, 0, 500000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (25, 26, 27, 20000, 7, 0, 140000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (26, 27, 28, 20000, 2, 0, 40000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (27, 28, 29, 0, 5, 0, 360000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (28, 29, 30, 0, 5, 0, 350000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (29, 30, 31, 0, 5, 0, 375000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (30, 31, 32, 0, 5, 0, 300000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (31, 32, 33, 0, 5, 0, 300000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (32, 33, 34, 0, 5, 0, 350000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (33, 34, 35, 0, 5, 0, 470000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (34, 35, 36, 0, 5, 0, 470000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (35, 36, 37, 0, 5, 0, 470000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (36, 37, 38, 0, 5, 0, 230000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (37, 38, 39, 0, 5, 0, 230000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (38, 39, 40, 0, 5, 0, 650000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (39, 40, 41, 0, 5, 0, 650000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (40, 41, 42, 0, 5, 0, 650000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (41, 42, 43, 0, 5, 0, 890000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (42, 43, 44, 0, 5, 0, 850000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (43, 44, 45, 0, 5, 0, 850000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (44, 45, 46, 0, 5, 0, 965000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (45, 46, 47, 0, 5, 0, 875000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (46, 47, 48, 0, 3, 0, 345000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (47, 48, 49, 0, 5, 0, 375000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (48, 49, 50, 0, 5, 0, 375000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (49, 50, 51, 0, 5, 0, 525000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (50, 51, 52, 0, 5, 0, 110000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (51, 52, 53, 0, 5, 0, 110000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (52, 53, 54, 0, 5, 0, 110000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (53, 54, 55, 0, 21, 0, 2730000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (54, 55, 56, 0, 8, 0, 1040000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (55, 56, 57, 0, 30, 0, 3900000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (56, 57, 58, 0, 7, 0, 1540000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (57, 58, 59, 0, 10, 0, 1800000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (58, 59, 60, 0, 2, 0, 430000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (59, 60, 61, 0, 1, 0, 205000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (60, 61, 62, 0, 1, 0, 205000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (61, 62, 63, 0, 1, 0, 195000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (62, 63, 64, 0, 2, 0, 350000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (63, 64, 65, 0, 7, 0, 1540000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (64, 65, 66, 0, 10, 0, 1800000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (65, 66, 67, 0, 1, 0, 210000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (66, 67, 68, 0, 1, 0, 210000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (67, 68, 69, 0, 5, 0, 900000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (68, 69, 70, 0, 1, 0, 195000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (69, 70, 71, 0, 8, 0, 1040000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (70, 71, 72, 0, 30, 0, 3900000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (71, 72, 73, 0, 21, 0, 2730000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (72, 73, 74, 0, 5, 0, 350000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (73, 74, 75, 0, 5, 0, 325000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (74, 75, 76, 0, 5, 0, 325000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (75, 76, 77, 0, 5, 0, 450000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (76, 77, 78, 0, 5, 0, 450000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (77, 78, 79, 0, 5, 0, 700000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (78, 79, 80, 0, 5, 0, 700000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (79, 80, 81, 0, 5, 0, 700000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (80, 81, 82, 0, 5, 0, 725000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (81, 82, 83, 0, 5, 0, 775000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (82, 83, 84, 0, 5, 0, 450000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (83, 84, 85, 0, 5, 0, 900000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (84, 85, 86, 0, 5, 0, 900000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (85, 86, 87, 0, 5, 0, 500000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (86, 87, 88, 0, 5, 0, 500000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (87, 88, 23, 170000, 1, 15, 170000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (88, 88, 45, 239000, 1, 0, 239000)
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [DonGia], [SoLuong], [ChietKhau], [ThanhTien]) VALUES (89, 88, 45, 239000, 1, 10, 239000)
SET IDENTITY_INSERT [dbo].[HoaDonDetail] OFF
/****** Object:  Default [DF_HoaDon_IdStatus]    Script Date: 05/14/2012 23:47:35 ******/
ALTER TABLE [dbo].[HoaDon] ADD  CONSTRAINT [DF_HoaDon_IdStatus]  DEFAULT ((1)) FOR [IdStatus]
GO
/****** Object:  Default [DF_DonHang_ThanhTien]    Script Date: 05/14/2012 23:47:35 ******/
ALTER TABLE [dbo].[HoaDon] ADD  CONSTRAINT [DF_DonHang_ThanhTien]  DEFAULT ((0)) FOR [ThanhTien]
GO
/****** Object:  Default [DF_HoaDonDetail_SoLuong]    Script Date: 05/14/2012 23:47:35 ******/
ALTER TABLE [dbo].[HoaDonDetail] ADD  CONSTRAINT [DF_HoaDonDetail_SoLuong]  DEFAULT ((1)) FOR [SoLuong]
GO
/****** Object:  Default [DF_HoaDonDetail_ChietKhau]    Script Date: 05/14/2012 23:47:35 ******/
ALTER TABLE [dbo].[HoaDonDetail] ADD  CONSTRAINT [DF_HoaDonDetail_ChietKhau]  DEFAULT ((0)) FOR [ChietKhau]
GO
/****** Object:  Default [DF_DonHangDetail_ThanhTien]    Script Date: 05/14/2012 23:47:35 ******/
ALTER TABLE [dbo].[HoaDonDetail] ADD  CONSTRAINT [DF_DonHangDetail_ThanhTien]  DEFAULT ((0)) FOR [ThanhTien]
GO
/****** Object:  Default [DF_KhachHang_GioiTinh]    Script Date: 05/14/2012 23:47:35 ******/
ALTER TABLE [dbo].[KhachHang] ADD  CONSTRAINT [DF_KhachHang_GioiTinh]  DEFAULT (N'Nam') FOR [GioiTinh]
GO
/****** Object:  Default [DF_KhachHang_Diem]    Script Date: 05/14/2012 23:47:35 ******/
ALTER TABLE [dbo].[KhachHang] ADD  CONSTRAINT [DF_KhachHang_Diem]  DEFAULT ((0)) FOR [TichLuy]
GO
/****** Object:  Default [DF_KhachHang_Diem_1]    Script Date: 05/14/2012 23:47:35 ******/
ALTER TABLE [dbo].[KhachHang] ADD  CONSTRAINT [DF_KhachHang_Diem_1]  DEFAULT ((0)) FOR [Diem]
GO
/****** Object:  Default [DF_SanPham_LaiSuat]    Script Date: 05/14/2012 23:47:35 ******/
ALTER TABLE [dbo].[SanPham] ADD  CONSTRAINT [DF_SanPham_LaiSuat]  DEFAULT ((0)) FOR [LaiSuat]
GO
/****** Object:  Default [DF_SanPham_SoLuong]    Script Date: 05/14/2012 23:47:35 ******/
ALTER TABLE [dbo].[SanPham] ADD  CONSTRAINT [DF_SanPham_SoLuong]  DEFAULT ((0)) FOR [SoLuong]
GO
/****** Object:  ForeignKey [FK_ChietKhau_KhachHangGroup]    Script Date: 05/14/2012 23:47:35 ******/
ALTER TABLE [dbo].[ChietKhau]  WITH CHECK ADD  CONSTRAINT [FK_ChietKhau_KhachHangGroup] FOREIGN KEY([idKhachHangGroup])
REFERENCES [dbo].[KhachHangGroup] ([Id])
GO
ALTER TABLE [dbo].[ChietKhau] CHECK CONSTRAINT [FK_ChietKhau_KhachHangGroup]
GO
/****** Object:  ForeignKey [FK_ChietKhau_SanPham]    Script Date: 05/14/2012 23:47:35 ******/
ALTER TABLE [dbo].[ChietKhau]  WITH CHECK ADD  CONSTRAINT [FK_ChietKhau_SanPham] FOREIGN KEY([IdSanPham])
REFERENCES [dbo].[SanPham] ([Id])
GO
ALTER TABLE [dbo].[ChietKhau] CHECK CONSTRAINT [FK_ChietKhau_SanPham]
GO
/****** Object:  ForeignKey [FK_HoaDon_HoaDonGroup]    Script Date: 05/14/2012 23:47:35 ******/
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_HoaDonGroup] FOREIGN KEY([IdType])
REFERENCES [dbo].[HoaDonType] ([Id])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_HoaDonGroup]
GO
/****** Object:  ForeignKey [FK_HoaDon_HoaDonStatus]    Script Date: 05/14/2012 23:47:35 ******/
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_HoaDonStatus] FOREIGN KEY([IdStatus])
REFERENCES [dbo].[HoaDonStatus] ([Id])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_HoaDonStatus]
GO
/****** Object:  ForeignKey [FK_HoaDon_KhachHang]    Script Date: 05/14/2012 23:47:35 ******/
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_KhachHang] FOREIGN KEY([IdKhachHang])
REFERENCES [dbo].[KhachHang] ([Id])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_KhachHang]
GO
/****** Object:  ForeignKey [FK_HoaDon_User]    Script Date: 05/14/2012 23:47:35 ******/
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_User]
GO
/****** Object:  ForeignKey [FK_HoaDonDetail_HoaDon]    Script Date: 05/14/2012 23:47:35 ******/
ALTER TABLE [dbo].[HoaDonDetail]  WITH CHECK ADD  CONSTRAINT [FK_HoaDonDetail_HoaDon] FOREIGN KEY([IdHoaDon])
REFERENCES [dbo].[HoaDon] ([Id])
GO
ALTER TABLE [dbo].[HoaDonDetail] CHECK CONSTRAINT [FK_HoaDonDetail_HoaDon]
GO
/****** Object:  ForeignKey [FK_HoaDonDetail_SanPham]    Script Date: 05/14/2012 23:47:35 ******/
ALTER TABLE [dbo].[HoaDonDetail]  WITH CHECK ADD  CONSTRAINT [FK_HoaDonDetail_SanPham] FOREIGN KEY([IdSanPham])
REFERENCES [dbo].[SanPham] ([Id])
GO
ALTER TABLE [dbo].[HoaDonDetail] CHECK CONSTRAINT [FK_HoaDonDetail_SanPham]
GO
/****** Object:  ForeignKey [FK_KhachHang_KhachHangGroup]    Script Date: 05/14/2012 23:47:35 ******/
ALTER TABLE [dbo].[KhachHang]  WITH CHECK ADD  CONSTRAINT [FK_KhachHang_KhachHangGroup] FOREIGN KEY([IdGroup])
REFERENCES [dbo].[KhachHangGroup] ([Id])
GO
ALTER TABLE [dbo].[KhachHang] CHECK CONSTRAINT [FK_KhachHang_KhachHangGroup]
GO
/****** Object:  ForeignKey [FK_SanPham_SanPhamGroup]    Script Date: 05/14/2012 23:47:35 ******/
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_SanPhamGroup] FOREIGN KEY([IdGroup])
REFERENCES [dbo].[SanPhamGroup] ([Id])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_SanPhamGroup]
GO
/****** Object:  ForeignKey [FK_SanPham_XuatXu]    Script Date: 05/14/2012 23:47:35 ******/
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_XuatXu] FOREIGN KEY([IdXuatXu])
REFERENCES [dbo].[XuatXu] ([Id])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_XuatXu]
GO
/****** Object:  ForeignKey [FK_User_UserGroup]    Script Date: 05/14/2012 23:47:35 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserGroup] FOREIGN KEY([IdGroup])
REFERENCES [dbo].[UserGroup] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserGroup]
GO
