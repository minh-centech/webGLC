---------------ALTER DANH MUC KHACH HANG DOI LENH AN TOAN
if object_id(N'dbo.DanhMucKhachHangDoiLenh', N'U') is null
begin
    raiserror(N'Bảng dbo.DanhMucKhachHangDoiLenh chưa tồn tại. Hãy chạy file tblDanhMucKhachHangDoiLenh.sql trước.', 16, 1);
    return;
end
go

if col_length('dbo.DanhMucKhachHangDoiLenh', 'LoaiTaiKhoan') is null
begin
    alter table dbo.DanhMucKhachHangDoiLenh
    add LoaiTaiKhoan tinyint not null
        constraint DF_DanhMucKhachHangDoiLenh_LoaiTaiKhoan default(1);
end
go

if not exists (
    select 1
    from sys.check_constraints
    where parent_object_id = object_id(N'dbo.DanhMucKhachHangDoiLenh')
      and name = N'CK_DanhMucKhachHangDoiLenh_LoaiTaiKhoan'
)
begin
    alter table dbo.DanhMucKhachHangDoiLenh
    add constraint CK_DanhMucKhachHangDoiLenh_LoaiTaiKhoan
    check (LoaiTaiKhoan in (0, 1, 2));
end
go

if col_length('dbo.DanhMucKhachHangDoiLenh', 'IsActive') is null
begin
    alter table dbo.DanhMucKhachHangDoiLenh
    add IsActive bit null;
end
go

if exists (
    select 1
    from sys.columns
    where object_id = object_id(N'dbo.DanhMucKhachHangDoiLenh')
      and name = 'IsActive'
      and is_nullable = 1
)
begin
    update dbo.DanhMucKhachHangDoiLenh
    set IsActive = 0
    where IsActive is null;

    alter table dbo.DanhMucKhachHangDoiLenh
    alter column IsActive bit not null;
end
go

if not exists (
    select 1
    from sys.default_constraints
    where parent_object_id = object_id(N'dbo.DanhMucKhachHangDoiLenh')
      and name = N'DF_DanhMucKhachHangDoiLenh_IsActive'
)
begin
    alter table dbo.DanhMucKhachHangDoiLenh
    add constraint DF_DanhMucKhachHangDoiLenh_IsActive default(0) for IsActive;
end
go

if col_length('dbo.DanhMucKhachHangDoiLenh', 'BanScanSoCMNDCanCuocPath') is null
begin
    alter table dbo.DanhMucKhachHangDoiLenh
    add BanScanSoCMNDCanCuocPath nvarchar(500) null;
end
go

if col_length('dbo.DanhMucKhachHangDoiLenh', 'BanDangKyCaNhanCoChuKyPath') is null
begin
    alter table dbo.DanhMucKhachHangDoiLenh
    add BanDangKyCaNhanCoChuKyPath nvarchar(500) null;
end
go

if not exists (
    select 1
    from sys.indexes
    where object_id = object_id(N'dbo.DanhMucKhachHangDoiLenh')
      and name = N'idx_DanhMucKhachHangDoiLenh_Email'
)
begin
    create index idx_DanhMucKhachHangDoiLenh_Email
    on dbo.DanhMucKhachHangDoiLenh(Email);
end
go

if not exists (
    select 1
    from sys.indexes
    where object_id = object_id(N'dbo.DanhMucKhachHangDoiLenh')
      and name = N'idx_DanhMucKhachHangDoiLenh_SoDienThoai'
)
begin
    create index idx_DanhMucKhachHangDoiLenh_SoDienThoai
    on dbo.DanhMucKhachHangDoiLenh(SoDienThoai);
end
go

if not exists (
    select 1
    from sys.indexes
    where object_id = object_id(N'dbo.DanhMucKhachHangDoiLenh')
      and name = N'idx_DanhMucKhachHangDoiLenh_PartnerGUID'
)
begin
    create index idx_DanhMucKhachHangDoiLenh_PartnerGUID
    on dbo.DanhMucKhachHangDoiLenh(PartnerGUID);
end
go

if not exists (
    select 1
    from sys.indexes
    where object_id = object_id(N'dbo.DanhMucKhachHangDoiLenh')
      and name = N'idx_DanhMucKhachHangDoiLenh_LoaiTaiKhoan_IsActive'
)
begin
    create index idx_DanhMucKhachHangDoiLenh_LoaiTaiKhoan_IsActive
    on dbo.DanhMucKhachHangDoiLenh(LoaiTaiKhoan, IsActive);
end
go
