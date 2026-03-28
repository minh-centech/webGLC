---------------DANH MUC LOAI TAI KHOAN KHACH HANG DOI LENH
create table DanhMucLoaiTaiKhoanKhachHangDoiLenh
(
	ID				tinyint			not null,
	Ma				nvarchar(50)	not null,
	Ten				nvarchar(255)	not null,
	MoTa			nvarchar(500),
	IsActive		bit				not null constraint DF_DanhMucLoaiTaiKhoanKhachHangDoiLenh_IsActive default(1),
	CreateDate		datetime		not null constraint DF_DanhMucLoaiTaiKhoanKhachHangDoiLenh_CreateDate default(getdate()),
	constraint PK_DanhMucLoaiTaiKhoanKhachHangDoiLenh primary key (ID),
	constraint UQ_DanhMucLoaiTaiKhoanKhachHangDoiLenh_Ma unique (Ma)
)
go

insert into DanhMucLoaiTaiKhoanKhachHangDoiLenh (ID, Ma, Ten, MoTa, IsActive)
values
	(0, N'ADMIN', N'Admin', N'Tai khoan quan tri he thong', 1),
	(1, N'CA_NHAN', N'Ca nhan', N'Tai khoan khach hang ca nhan', 1),
	(2, N'DOANH_NGHIEP', N'Doanh nghiep', N'Tai khoan khach hang doanh nghiep', 1);
go
