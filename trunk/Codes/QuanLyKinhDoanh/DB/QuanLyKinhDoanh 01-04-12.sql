USE [QuanLyKinhDoanh]
GO
/****** Object:  Table [dbo].[HoaDonType]    Script Date: 04/01/2012 04:40:02 ******/
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
/****** Object:  Table [dbo].[KhachHangGroup]    Script Date: 04/01/2012 04:40:02 ******/
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
SET IDENTITY_INSERT [dbo].[KhachHangGroup] ON
INSERT [dbo].[KhachHangGroup] ([Id], [Ten], [Diem], [Mota]) VALUES (2, N'Thường', 0, NULL)
INSERT [dbo].[KhachHangGroup] ([Id], [Ten], [Diem], [Mota]) VALUES (3, N'VIP', 100, NULL)
SET IDENTITY_INSERT [dbo].[KhachHangGroup] OFF
/****** Object:  Table [dbo].[SanPhamGroup]    Script Date: 04/01/2012 04:40:02 ******/
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
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (9, N'ABC', N'test 123', N'aaa')
INSERT [dbo].[SanPhamGroup] ([Id], [Ma], [Ten], [Mota]) VALUES (10, N'TEST', N'test 2', N'fdfd')
SET IDENTITY_INSERT [dbo].[SanPhamGroup] OFF
/****** Object:  Table [dbo].[UserGroup]    Script Date: 04/01/2012 04:40:02 ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 04/01/2012 04:40:02 ******/
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
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (25, 1, N'Admin', N'admin', N'21232F297A57A5A743894AE4A801FC345454433454539', N'123', N'Nữ', N'321', N'aaa', N'bbb', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (26, 2, N'Nguyễn Gia Túc', N'Túc', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'12345', N'Nam', N'54321', N'tuc@yahoo.com', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (27, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (28, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (29, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (30, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (31, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (32, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (33, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (34, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (35, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (36, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (37, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (38, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 1)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (39, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (40, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (41, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (42, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (43, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (44, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (45, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (46, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (47, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (48, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (49, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (50, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (51, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (52, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (53, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (54, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (55, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (56, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (57, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (58, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (59, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (60, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (61, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (62, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (63, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (64, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (65, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 1)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (66, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 1)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (67, 1, N'Test 1', N'test1', N'B16D4ED836A4AF603DB1E5E4A0432A7B45454433454539', N'111', N'Nam', N'222', N'test@', N'test', N'', CAST(0x7A350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 1)
INSERT [dbo].[User] ([Id], [IdGroup], [Ten], [UserName], [Password], [CMND], [GioiTinh], [DienThoai], [Email], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (70, 1, N'test', N'test', N'98F6BCD4621D373CADE4E832627B4F645454433454539', N'', N'Nam', N'', N'', N'', N'', CAST(0x7B350B00 AS Date), N'', CAST(0x7B350B00 AS Date), 0)
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Table [dbo].[SanPham]    Script Date: 04/01/2012 04:40:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdSanPham] [nvarchar](10) NOT NULL,
	[IdGroup] [int] NOT NULL,
	[Ten] [nvarchar](50) NOT NULL,
	[MoTa] [nvarchar](200) NULL,
	[GiaMua] [bigint] NOT NULL,
	[GiaBan] [bigint] NOT NULL,
	[LaiSuat] [float] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[DonViTinh] [nvarchar](10) NOT NULL,
	[XuatXu] [nvarchar](50) NULL,
	[Hieu] [nvarchar](50) NULL,
	[Size] [nvarchar](50) NULL,
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
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [XuatXu], [Hieu], [Size], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (3, N'ABC0001', 9, N'test', N'', 100000, 999000, 999, 3, N'TT', N'a', N'fdf', N'dfsa', 33, N'Ngày', N'', CAST(0x7E350B00 AS Date), N'', CAST(0x7E350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [XuatXu], [Hieu], [Size], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (4, N'ABC0005', 9, N'tstt65565', N'fdsfs43', 0, 0, 0, 0, N'SAFA5454', N'fefdfdt6565', N'vvefdfd65', N'vevefdf4343', 43, N'Năm', N'', CAST(0x7E350B00 AS Date), N'', CAST(0x7F350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [XuatXu], [Hieu], [Size], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (5, N'TEST0005', 10, N'Giày da', N'', 0, 0, 0, 0, N'CÁI', N'tete', N'fdfdas', N'dfas', 0, N'Ngày', N'', CAST(0x80350B00 AS Date), N'', CAST(0x80350B00 AS Date), 0)
INSERT [dbo].[SanPham] ([Id], [IdSanPham], [IdGroup], [Ten], [MoTa], [GiaMua], [GiaBan], [LaiSuat], [SoLuong], [DonViTinh], [XuatXu], [Hieu], [Size], [ThoiGianBaoHanh], [DonViBaoHanh], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (6, N'TEST0007', 10, N'Dép râu', N'', 0, 0, 0, 0, N'MÉT', N'dsaffds', N'fasf', N'fdsa', 0, N'Ngày', N'', CAST(0x80350B00 AS Date), N'', CAST(0x80350B00 AS Date), 0)
SET IDENTITY_INSERT [dbo].[SanPham] OFF
/****** Object:  Table [dbo].[KhachHang]    Script Date: 04/01/2012 04:40:02 ******/
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
/****** Object:  Table [dbo].[HoaDon]    Script Date: 04/01/2012 04:40:02 ******/
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
	[Status] [nvarchar](20) NOT NULL,
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
INSERT [dbo].[HoaDon] ([Id], [IdHoaDon], [IdType], [IdUser], [IdKhachHang], [Status], [ThanhTien], [GhiChu], [CreateBy], [CreateDate], [UpdateBy], [UpdateDate], [DeleteFlag]) VALUES (1, N'M00001', 1, NULL, NULL, N'Đã thanh toán', 300000, N'', N'', CAST(0x0000A026004997C6 AS DateTime), N'', CAST(0x81350B00 AS Date), 0)
SET IDENTITY_INSERT [dbo].[HoaDon] OFF
/****** Object:  Table [dbo].[HoaDonDetail]    Script Date: 04/01/2012 04:40:02 ******/
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
INSERT [dbo].[HoaDonDetail] ([Id], [IdHoaDon], [IdSanPham], [SoLuong], [ThanhTien]) VALUES (1, 1, 3, 3, 300000)
SET IDENTITY_INSERT [dbo].[HoaDonDetail] OFF
/****** Object:  Default [DF_DonHang_ThanhTien]    Script Date: 04/01/2012 04:40:02 ******/
ALTER TABLE [dbo].[HoaDon] ADD  CONSTRAINT [DF_DonHang_ThanhTien]  DEFAULT ((0)) FOR [ThanhTien]
GO
/****** Object:  Default [DF_DonHangDetail_ThanhTien]    Script Date: 04/01/2012 04:40:02 ******/
ALTER TABLE [dbo].[HoaDonDetail] ADD  CONSTRAINT [DF_DonHangDetail_ThanhTien]  DEFAULT ((0)) FOR [ThanhTien]
GO
/****** Object:  Default [DF_KhachHang_GioiTinh]    Script Date: 04/01/2012 04:40:02 ******/
ALTER TABLE [dbo].[KhachHang] ADD  CONSTRAINT [DF_KhachHang_GioiTinh]  DEFAULT (N'Nam') FOR [GioiTinh]
GO
/****** Object:  Default [DF_KhachHang_Diem]    Script Date: 04/01/2012 04:40:02 ******/
ALTER TABLE [dbo].[KhachHang] ADD  CONSTRAINT [DF_KhachHang_Diem]  DEFAULT ((0)) FOR [Diem]
GO
/****** Object:  Default [DF_SanPham_SoLuong]    Script Date: 04/01/2012 04:40:02 ******/
ALTER TABLE [dbo].[SanPham] ADD  CONSTRAINT [DF_SanPham_SoLuong]  DEFAULT ((0)) FOR [SoLuong]
GO
/****** Object:  ForeignKey [FK_HoaDon_HoaDonGroup]    Script Date: 04/01/2012 04:40:02 ******/
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_HoaDonGroup] FOREIGN KEY([IdType])
REFERENCES [dbo].[HoaDonType] ([Id])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_HoaDonGroup]
GO
/****** Object:  ForeignKey [FK_HoaDon_KhachHang]    Script Date: 04/01/2012 04:40:02 ******/
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_KhachHang] FOREIGN KEY([IdKhachHang])
REFERENCES [dbo].[KhachHang] ([Id])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_KhachHang]
GO
/****** Object:  ForeignKey [FK_HoaDon_User]    Script Date: 04/01/2012 04:40:02 ******/
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_User]
GO
/****** Object:  ForeignKey [FK_HoaDonDetail_HoaDon]    Script Date: 04/01/2012 04:40:02 ******/
ALTER TABLE [dbo].[HoaDonDetail]  WITH CHECK ADD  CONSTRAINT [FK_HoaDonDetail_HoaDon] FOREIGN KEY([IdHoaDon])
REFERENCES [dbo].[HoaDon] ([Id])
GO
ALTER TABLE [dbo].[HoaDonDetail] CHECK CONSTRAINT [FK_HoaDonDetail_HoaDon]
GO
/****** Object:  ForeignKey [FK_HoaDonDetail_SanPham]    Script Date: 04/01/2012 04:40:02 ******/
ALTER TABLE [dbo].[HoaDonDetail]  WITH CHECK ADD  CONSTRAINT [FK_HoaDonDetail_SanPham] FOREIGN KEY([IdSanPham])
REFERENCES [dbo].[SanPham] ([Id])
GO
ALTER TABLE [dbo].[HoaDonDetail] CHECK CONSTRAINT [FK_HoaDonDetail_SanPham]
GO
/****** Object:  ForeignKey [FK_KhachHang_NhomKhachHang]    Script Date: 04/01/2012 04:40:02 ******/
ALTER TABLE [dbo].[KhachHang]  WITH CHECK ADD  CONSTRAINT [FK_KhachHang_NhomKhachHang] FOREIGN KEY([IdGroup])
REFERENCES [dbo].[KhachHangGroup] ([Id])
GO
ALTER TABLE [dbo].[KhachHang] CHECK CONSTRAINT [FK_KhachHang_NhomKhachHang]
GO
/****** Object:  ForeignKey [FK_SanPham_SanPhamGroup]    Script Date: 04/01/2012 04:40:02 ******/
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_SanPhamGroup] FOREIGN KEY([IdGroup])
REFERENCES [dbo].[SanPhamGroup] ([Id])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_SanPham_SanPhamGroup]
GO
/****** Object:  ForeignKey [FK_User_UserGroup]    Script Date: 04/01/2012 04:40:02 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserGroup] FOREIGN KEY([IdGroup])
REFERENCES [dbo].[UserGroup] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserGroup]
GO
