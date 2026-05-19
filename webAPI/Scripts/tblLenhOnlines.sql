if object_id(N'dbo.tblLenhOnlines', N'U') is null
begin
    create table dbo.tblLenhOnlines
    (
        ID bigint not null,
        SoThuTuLenh bigint not null,
        HoVaTen nvarchar(255) not null,
        SoDienThoai nvarchar(50) null,
        SoCMND nvarchar(50) null,
        SoXe nvarchar(50) null,
        MaSoThue nvarchar(50) null,
        TenCongTy nvarchar(255) null,
        DiaChi nvarchar(500) null,
        Email nvarchar(255) null,
        HouseBill nvarchar(100) not null,
        NgayLamLenh datetime not null constraint DF_tblLenhOnlines_NgayLamLenh default(getdate()),
        SoCont nvarchar(50) null,
        NgayLayHang datetime null,
        SoToKhai nvarchar(100) null,
        TrangThai int not null constraint DF_tblLenhOnlines_TrangThai default(0),
        TrangThaiHaiQuan TINYINT NOT NULL CONSTRAINT DF_tblLenhOnlines_TrangThaiHaiQuan DEFAULT 0,
        HoanThanh bit not null constraint DF_tblLenhOnlines_HoanThanh default(0),
        IDDanhMucKhachHangDoiLenh bigint not null,
        IDctLenhNhapKhoHangNhapKhauChiTiet bigint null,
        CreateDate datetime not null constraint DF_tblLenhOnlines_CreateDate default(getdate()),
        EditDate datetime null,
        constraint PK_tblLenhOnlines primary key (ID),
        constraint UQ_tblLenhOnlines_SoThuTuLenh unique (SoThuTuLenh),
        constraint UQ_tblLenhOnlines_HouseBill unique (HouseBill),
        constraint FK_tblLenhOnlines_DanhMucKhachHangDoiLenh foreign key (IDDanhMucKhachHangDoiLenh) references dbo.DanhMucKhachHangDoiLenh(ID)
    );
end
go

if not exists (
    select 1
    from sys.indexes
    where object_id = object_id(N'dbo.tblLenhOnlines')
      and name = N'idx_tblLenhOnlines_IDDanhMucKhachHangDoiLenh'
)
begin
    create index idx_tblLenhOnlines_IDDanhMucKhachHangDoiLenh on dbo.tblLenhOnlines(IDDanhMucKhachHangDoiLenh);
end
go

if not exists (
    select 1
    from sys.indexes
    where object_id = object_id(N'dbo.tblLenhOnlines')
      and name = N'idx_tblLenhOnlines_SoThuTuLenh'
)
begin
    create index idx_tblLenhOnlines_SoThuTuLenh on dbo.tblLenhOnlines(SoThuTuLenh);
end
go

if not exists (
    select 1
    from sys.indexes
    where object_id = object_id(N'dbo.tblLenhOnlines')
      and name = N'idx_tblLenhOnlines_SoCont'
)
begin
    create index idx_tblLenhOnlines_SoCont on dbo.tblLenhOnlines(SoCont);
end
go


ALTER TABLE tblLenhOnlines
ADD TrangThaiHaiQuan TINYINT NOT NULL 
    CONSTRAINT DF_tblLenhOnlines_TrangThaiHaiQuan DEFAULT 0;

ALTER TABLE tblLenhOnlines
ADD HoanThanh BIT NOT NULL
    CONSTRAINT DF_tblLenhOnlines_HoanThanh DEFAULT(0);
