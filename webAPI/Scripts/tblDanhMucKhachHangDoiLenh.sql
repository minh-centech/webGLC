---------------DANH MỤC ĐỐI TƯỢNG THANH TOÁN
create table DanhMucKhachHangDoiLenh
(
	ID							bigint			not null,
	IDDanhMucDonVi				bigint			not null,
	IDDanhMucLoaiDoiTuong		bigint			not null,
	Email						nvarchar(128)	not null,
	Ten							nvarchar(255)	not null,
	SoDienThoai					nvarchar(128)	not null,
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
