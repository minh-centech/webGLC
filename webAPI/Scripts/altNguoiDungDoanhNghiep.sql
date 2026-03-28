---------------ALTER NGUOI DUNG DOANH NGHIEP AN TOAN
if object_id(N'dbo.NguoiDungDoanhNghiep', N'U') is null
begin
    raiserror(N'Bảng dbo.NguoiDungDoanhNghiep chưa tồn tại. Hãy chạy file tblNguoiDungDoanhNghiep.sql trước.', 16, 1);
    return;
end
go

if col_length('dbo.NguoiDungDoanhNghiep', 'TenDangNhapDangKyDichVu') is null
begin
    alter table dbo.NguoiDungDoanhNghiep
    add TenDangNhapDangKyDichVu nvarchar(128) null;
end
go

if col_length('dbo.NguoiDungDoanhNghiep', 'EmailXuatHoaDon') is null
begin
    alter table dbo.NguoiDungDoanhNghiep
    add EmailXuatHoaDon nvarchar(128) null;
end
go

if col_length('dbo.NguoiDungDoanhNghiep', 'SoCMNDCanCuoc') is null
begin
    alter table dbo.NguoiDungDoanhNghiep
    add SoCMNDCanCuoc nvarchar(50) null;
end
go

if col_length('dbo.NguoiDungDoanhNghiep', 'BanScanSoCMNDCanCuocPath') is null
begin
    alter table dbo.NguoiDungDoanhNghiep
    add BanScanSoCMNDCanCuocPath nvarchar(500) null;
end
go

if col_length('dbo.NguoiDungDoanhNghiep', 'BanDangKyEPortChuKySoPath') is null
begin
    alter table dbo.NguoiDungDoanhNghiep
    add BanDangKyEPortChuKySoPath nvarchar(500) null;
end
go

if col_length('dbo.NguoiDungDoanhNghiep', 'BanScanGiayPhepKinhDoanhPath') is null
begin
    alter table dbo.NguoiDungDoanhNghiep
    add BanScanGiayPhepKinhDoanhPath nvarchar(500) null;
end
go

if col_length('dbo.NguoiDungDoanhNghiep', 'GiayPhepKinhDoanh') is null
begin
    alter table dbo.NguoiDungDoanhNghiep
    add GiayPhepKinhDoanh nvarchar(255) null;
end
go

if col_length('dbo.NguoiDungDoanhNghiep', 'NgayCap') is null
begin
    alter table dbo.NguoiDungDoanhNghiep
    add NgayCap date null;
end
go

if col_length('dbo.NguoiDungDoanhNghiep', 'NoiCap') is null
begin
    alter table dbo.NguoiDungDoanhNghiep
    add NoiCap nvarchar(255) null;
end
go

if col_length('dbo.NguoiDungDoanhNghiep', 'DaiDienCoThamQuyen') is null
begin
    alter table dbo.NguoiDungDoanhNghiep
    add DaiDienCoThamQuyen nvarchar(255) null;
end
go

if col_length('dbo.NguoiDungDoanhNghiep', 'ChucVu') is null
begin
    alter table dbo.NguoiDungDoanhNghiep
    add ChucVu nvarchar(255) null;
end
go

if col_length('dbo.NguoiDungDoanhNghiep', 'DoanhNghiepCongTyDuocUyQuyen') is null
begin
    alter table dbo.NguoiDungDoanhNghiep
    add DoanhNghiepCongTyDuocUyQuyen nvarchar(255) null;
end
go

if col_length('dbo.NguoiDungDoanhNghiep', 'GhiChu') is null
begin
    alter table dbo.NguoiDungDoanhNghiep
    add GhiChu nvarchar(1000) null;
end
go

if col_length('dbo.NguoiDungDoanhNghiep', 'IsActive') is null
begin
    alter table dbo.NguoiDungDoanhNghiep
    add IsActive bit null;
end
go

if exists (
    select 1
    from sys.columns
    where object_id = object_id(N'dbo.NguoiDungDoanhNghiep')
      and name = 'IsActive'
      and is_nullable = 1
)
begin
    update dbo.NguoiDungDoanhNghiep
    set IsActive = 1
    where IsActive is null;

    alter table dbo.NguoiDungDoanhNghiep
    alter column IsActive bit not null;
end
go

if not exists (
    select 1
    from sys.default_constraints
    where parent_object_id = object_id(N'dbo.NguoiDungDoanhNghiep')
      and name = N'DF_NguoiDungDoanhNghiep_IsActive'
)
begin
    alter table dbo.NguoiDungDoanhNghiep
    add constraint DF_NguoiDungDoanhNghiep_IsActive default(1) for IsActive;
end
go

if not exists (
    select 1
    from sys.indexes
    where object_id = object_id(N'dbo.NguoiDungDoanhNghiep')
      and name = N'idx_NguoiDungDoanhNghiep_EmailDoanhNghiep'
)
begin
    create index idx_NguoiDungDoanhNghiep_EmailDoanhNghiep
    on dbo.NguoiDungDoanhNghiep(EmailDoanhNghiep);
end
go

if not exists (
    select 1
    from sys.indexes
    where object_id = object_id(N'dbo.NguoiDungDoanhNghiep')
      and name = N'idx_NguoiDungDoanhNghiep_TenDoanhNghiep'
)
begin
    create index idx_NguoiDungDoanhNghiep_TenDoanhNghiep
    on dbo.NguoiDungDoanhNghiep(TenDoanhNghiep);
end
go
