---------------REMOVE UNIQUE MOT KHACH HANG MOT DOANH NGHIEP
if object_id(N'dbo.NguoiDungDoanhNghiep', N'U') is null
begin
    raiserror(N'Bảng dbo.NguoiDungDoanhNghiep chưa tồn tại.', 16, 1);
    return;
end
go

if exists (
    select 1
    from sys.key_constraints
    where parent_object_id = object_id(N'dbo.NguoiDungDoanhNghiep')
      and name = N'UQ_NguoiDungDoanhNghiep_IDDanhMucKhachHangDoiLenh'
)
begin
    alter table dbo.NguoiDungDoanhNghiep
    drop constraint UQ_NguoiDungDoanhNghiep_IDDanhMucKhachHangDoiLenh;
end
go

if not exists (
    select 1
    from sys.indexes
    where object_id = object_id(N'dbo.NguoiDungDoanhNghiep')
      and name = N'idx_NguoiDungDoanhNghiep_IDDanhMucKhachHangDoiLenh'
)
begin
    create index idx_NguoiDungDoanhNghiep_IDDanhMucKhachHangDoiLenh
    on dbo.NguoiDungDoanhNghiep(IDDanhMucKhachHangDoiLenh);
end
go
