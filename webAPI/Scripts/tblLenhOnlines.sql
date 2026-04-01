create table LenhOnlines
(
	ID				bigint			not null,
	SoThuTuLenh		bigint			not null,
	HoVaTen			nvarchar(255)	not null,
	SoDienThoai		nvarchar(50)	null,
	SoCMND			nvarchar(50)	null,
	SoXe			nvarchar(50)	null,
	MaSoThue		nvarchar(50)	null,
	TenCongTy		nvarchar(255)	null,
	DiaChi			nvarchar(500)	null,
	Email			nvarchar(255)	null,
	HouseBill		nvarchar(100)	not null,
	NgayLamLenh		datetime		not null constraint DF_LenhOnlines_NgayLamLenh default(getdate()),
	SoCont			nvarchar(50)	null,
	NgayLayHang		datetime		null,
	SoToKhai		nvarchar(100)	null,
	TrangThai		int				not null constraint DF_LenhOnlines_TrangThai default(0),
	IDDanhMucKhachHangDoiLenh	bigint			not null,
	CreateDate		datetime		not null constraint DF_LenhOnlines_CreateDate default(getdate()),
	EditDate		datetime		null,
	constraint PK_LenhOnlines primary key (ID),
	constraint UQ_LenhOnlines_SoThuTuLenh unique (SoThuTuLenh),
	constraint UQ_LenhOnlines_HouseBill unique (HouseBill),
	constraint FK_LenhOnlines_DanhMucKhachHangDoiLenh foreign key (IDDanhMucKhachHangDoiLenh) references DanhMucKhachHangDoiLenh(ID)
)
go

create index idx_LenhOnlines_IDDanhMucKhachHangDoiLenh on LenhOnlines(IDDanhMucKhachHangDoiLenh);
create index idx_LenhOnlines_SoThuTuLenh on LenhOnlines(SoThuTuLenh);
create index idx_LenhOnlines_SoCont on LenhOnlines(SoCont);
go
