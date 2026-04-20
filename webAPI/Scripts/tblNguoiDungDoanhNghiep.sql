---------------NGUOI DUNG DOANH NGHIEP
create table NguoiDungDoanhNghiep
(
	ID										bigint			not null,
	IDDanhMucKhachHangDoiLenh				bigint			not null,
	TenDoanhNghiep							nvarchar(255)	not null,
	MaSoThue								nvarchar(50)	not null,
	DiaChi									nvarchar(500)	not null,
	SoDienThoaiDoanhNghiep					nvarchar(50)	not null,
	EmailDoanhNghiep						nvarchar(128)	not null,
	SoFax									nvarchar(50),
	GiayPhepKinhDoanh						nvarchar(255),
	BanScanGiayPhepKinhDoanhPath			nvarchar(500),
	NgayCap									date,
	NoiCap									nvarchar(255),
	DaiDienCoThamQuyen						nvarchar(255),
	ChucVu									nvarchar(255),
	DoanhNghiepCongTyDuocUyQuyen			nvarchar(255),
	TenDangNhapDangKyDichVu					nvarchar(128),
	EmailXuatHoaDon							nvarchar(128),
	SoCMNDCanCuoc							nvarchar(50),
	BanScanSoCMNDCanCuocPath				nvarchar(500),
	BanDangKyEPortChuKySoPath				nvarchar(500),
	GhiChu									nvarchar(1000),
	IsActive								bit				not null constraint DF_NguoiDungDoanhNghiep_IsActive default(1),
	IDDanhMucNguoiSuDungCreate				bigint			not null,
	CreateDate								datetime		not null,
	IDDanhMucNguoiSuDungEdit				bigint,
	EditDate								datetime,
	constraint PK_NguoiDungDoanhNghiep primary key (ID),
	constraint FK_NguoiDungDoanhNghiep_DanhMucKhachHangDoiLenh foreign key (IDDanhMucKhachHangDoiLenh) references DanhMucKhachHangDoiLenh(ID),
	constraint FK_NguoiDungDoanhNghiep_DanhMucNguoiSuDungCreate foreign key (IDDanhMucNguoiSuDungCreate) references DanhMucNguoiSuDung(ID),
	constraint FK_NguoiDungDoanhNghiep_DanhMucNguoiSuDungEdit foreign key (IDDanhMucNguoiSuDungEdit) references DanhMucNguoiSuDung(ID),
	constraint UQ_NguoiDungDoanhNghiep_MaSoThue unique (MaSoThue)
)
go

create index idx_NguoiDungDoanhNghiep_EmailDoanhNghiep on NguoiDungDoanhNghiep(EmailDoanhNghiep);
create index idx_NguoiDungDoanhNghiep_TenDoanhNghiep on NguoiDungDoanhNghiep(TenDoanhNghiep);
