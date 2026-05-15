---------------DANH MỤC ĐỐI TƯỢNG THANH TOÁN
create table DanhMucKhachHangDoiLenh
(
	ID							bigint			not null,
	IDDanhMucDonVi				bigint			not null,
	IDDanhMucLoaiDoiTuong		bigint			not null,
	LoaiTaiKhoan				tinyint			not null constraint DF_DanhMucKhachHangDoiLenh_LoaiTaiKhoan default(1),
	IsActive					bit				not null constraint DF_DanhMucKhachHangDoiLenh_IsActive default(1),
	IsLockAccount				bit				not null constraint DF_DanhMucKhachHangDoiLenh_IsLockAccount default(0),
	Email						nvarchar(128)	not null,
	Ten							nvarchar(255)	not null,
	SoDienThoai					nvarchar(128)	not null,
	EmailXuatHoaDon				nvarchar(128),
	BanScanSoCMNDCanCuocPath	nvarchar(500),
	BanDangKyCaNhanCoChuKyPath	nvarchar(500),
	Password					nvarchar(256)	not null,
	PartnerGUID					nvarchar(36)	not null,
	MaKichHoat					nvarchar(6),
	MaXacNhan					nvarchar(6),

	ThoiGianTaoMaKichHoat		datetime,
	KichHoat					bit				not null,
	IDDanhMucNguoiSuDungCreate	bigint			not null,
	CreateDate					datetime		not null,
	IDDanhMucNguoiSuDungEdit	bigint,
	EditDate					datetime,
	constraint	PK_DanhMucKhachHangDoiLenh primary key (ID),
	constraint	DanhMucKhachHangDoiLenh_DanhMucDoiTuong foreign key (ID) references DanhMucDoiTuong(ID),
	constraint	DanhMucDonVi_DanhMucKhachHangDoiLenh foreign key (IDDanhMucDonVi) references DanhMucDonVi(ID),
	constraint	DanhMucLoaiDoiTuong_DanhMucKhachHangDoiLenh foreign key (IDDanhMucLoaiDoiTuong) references DanhMucLoaiDoiTuong(ID),
	constraint	DanhMucLoaiTaiKhoanKhachHangDoiLenh_DanhMucKhachHangDoiLenh foreign key (LoaiTaiKhoan) references DanhMucLoaiTaiKhoanKhachHangDoiLenh(ID),
	constraint	CK_DanhMucKhachHangDoiLenh_LoaiTaiKhoan check (LoaiTaiKhoan in (0, 1, 2)),
	constraint	Email_DanhMucKhachHangDoiLenh unique(Email),
	constraint	SoDienThoai_DanhMucKhachHangDoiLenh unique(SoDienThoai),
	constraint	PartnerGUID_DanhMucKhachHangDoiLenh unique(PartnerGUID),
	constraint	DanhMucNguoiSuDungCreate_DanhMucKhachHangDoiLenh foreign key (IDDanhMucNguoiSuDungCreate) references DanhMucNguoiSuDung(ID),
	constraint	DanhMucNguoiSuDungEdit_DanhMucKhachHangDoiLenh foreign key (IDDanhMucNguoiSuDungEdit) references DanhMucNguoiSuDung(ID)
)
go
create index idx_DanhMucKhachHangDoiLenh_Email on DanhMucKhachHangDoiLenh(Email);
create index idx_DanhMucKhachHangDoiLenh_SoDienThoai on DanhMucKhachHangDoiLenh(SoDienThoai);
create index idx_DanhMucKhachHangDoiLenh_PartnerGUID on DanhMucKhachHangDoiLenh(PartnerGUID);
create index idx_DanhMucKhachHangDoiLenh_LoaiTaiKhoan_IsActive on DanhMucKhachHangDoiLenh(LoaiTaiKhoan, IsActive);



--Lệnh cập nhật
USE [everWareHouse-CFS-GLC]
GO

-- 1. Thêm cột LoaiTaiKhoan (nếu chưa có)
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('[dbo].[DanhMucKhachHangDoiLenh]') AND name = 'LoaiTaiKhoan')
BEGIN
    ALTER TABLE [dbo].[DanhMucKhachHangDoiLenh]
    ADD [LoaiTaiKhoan] [tinyint] NOT NULL 
        CONSTRAINT [DF_DanhMucKhachHangDoiLenh_LoaiTaiKhoan] DEFAULT (1);
        
    ALTER TABLE [dbo].[DanhMucKhachHangDoiLenh] 
    ADD CONSTRAINT [CK_DanhMucKhachHangDoiLenh_LoaiTaiKhoan] CHECK ([LoaiTaiKhoan] IN (0, 1, 2));
END
GO

-- 2. Thêm cột IsActive (nếu chưa có)
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('[dbo].[DanhMucKhachHangDoiLenh]') AND name = 'IsActive')
BEGIN
    ALTER TABLE [dbo].[DanhMucKhachHangDoiLenh]
    ADD [IsActive] [bit] NOT NULL 
        CONSTRAINT [DF_DanhMucKhachHangDoiLenh_IsActive] DEFAULT (1);
END
GO

-- 3. Thêm cột lưu file căn cước cho tài khoản cá nhân/doanh nghiệp nếu chưa có
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('[dbo].[DanhMucKhachHangDoiLenh]') AND name = 'BanScanSoCMNDCanCuocPath')
BEGIN
    ALTER TABLE [dbo].[DanhMucKhachHangDoiLenh]
    ADD [BanScanSoCMNDCanCuocPath] [nvarchar](500) NULL;
END
GO

-- 4. Thêm cột lưu bản đăng ký scan có chữ ký cho tài khoản cá nhân nếu chưa có
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('[dbo].[DanhMucKhachHangDoiLenh]') AND name = 'BanDangKyCaNhanCoChuKyPath')
BEGIN
    ALTER TABLE [dbo].[DanhMucKhachHangDoiLenh]
    ADD [BanDangKyCaNhanCoChuKyPath] [nvarchar](500) NULL;
END
GO

-- 5. Xử lý các Index (Chỉ tạo nếu chưa tồn tại)
-- Kiểm tra Index cho Email
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_DanhMucKhachHangDoiLenh_Email' AND object_id = OBJECT_ID('[dbo].[DanhMucKhachHangDoiLenh]'))
BEGIN
    CREATE INDEX idx_DanhMucKhachHangDoiLenh_Email ON DanhMucKhachHangDoiLenh(Email);
END
GO

-- Kiểm tra Index cho SoDienThoai
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_DanhMucKhachHangDoiLenh_SoDienThoai' AND object_id = OBJECT_ID('[dbo].[DanhMucKhachHangDoiLenh]'))
BEGIN
    CREATE INDEX idx_DanhMucKhachHangDoiLenh_SoDienThoai ON DanhMucKhachHangDoiLenh(SoDienThoai);
END
GO

-- Kiểm tra Index cho PartnerGUID
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_DanhMucKhachHangDoiLenh_PartnerGUID' AND object_id = OBJECT_ID('[dbo].[DanhMucKhachHangDoiLenh]'))
BEGIN
    CREATE INDEX idx_DanhMucKhachHangDoiLenh_PartnerGUID ON DanhMucKhachHangDoiLenh(PartnerGUID);
END
GO

-- Kiểm tra Index tổng hợp cho LoaiTaiKhoan và IsActive
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_DanhMucKhachHangDoiLenh_LoaiTaiKhoan_IsActive' AND object_id = OBJECT_ID('[dbo].[DanhMucKhachHangDoiLenh]'))
BEGIN
    CREATE INDEX idx_DanhMucKhachHangDoiLenh_LoaiTaiKhoan_IsActive ON DanhMucKhachHangDoiLenh(LoaiTaiKhoan, IsActive);
END
GO
